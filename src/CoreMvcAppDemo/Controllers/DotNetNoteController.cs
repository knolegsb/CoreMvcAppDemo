using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using CoreMvcAppDemo.Models;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Net.Http.Headers;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace CoreMvcAppDemo.Controllers
{
    public class DotNetNoteController : Controller
    {
        // [DNN] 의존성 주입
        private IHostingEnvironment _environment; // 환경변수
        private INoteRepository _repository; // 게시판 리파지토리
        private INoteCommentRepository _commentRepository; // 댓글 리파지토리

        public DotNetNoteController(IHostingEnvironment environment, INoteRepository repository, INoteCommentRepository commentRepository)
        {
            _environment = environment;
            _repository = repository;
            _commentRepository = commentRepository;
        }

        // 공통 속성: 검색 모드: 검색 모드이면 true, 그렇지 않으면 false.
        public bool SearchMode { get; set; }
        public string SearchField { get; set; }
        public string SearchQuery { get; set; }

        /// <summary>
        /// 현재 보여줄 페이지 번호
        /// </summary>
        public int PageIndex { get; set; } = 0;

        /// <summary>
        /// 총 레코드 갯수(글번호 순서 정렬용)
        /// </summary>
        public int TotalRecordCount { get; set; } = 0;

        /// <summary>
        /// 게시판 리스트 페이지
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            // 검색 모드 결정: ?SearchField=Name&SearchQuery=닷넷코리아
            SearchMode = (
                !string.IsNullOrEmpty(Request.Query["SearchField"]) && !string.IsNullOrEmpty(Request.Query["SearchQuery"])
                );

            // 검색 환경이면 속성에 저장
            if (SearchMode)
            {
                SearchField = Request.Query["SearchField"].ToString();
                SearchQuery = Request.Query["SearchQuery"].ToString();
            }

            // [1] 쿼리스트링에 따른 페이지 보여주기
            if (!string.IsNullOrEmpty(Request.Query["Page"].ToString()))
            {
                // Page는 보여지는 쪽은 1, 2, 3, ... 코드단에서는 0, 1, 2, ...
                PageIndex = Convert.ToInt32(Request.Query["Page"]) - 1;
            }
            
            // [2] 쿠키를 사용한 리스트 페이지 번호 유지 적용(optional);
            // 100번째 페이지 보고 있다가 다시 리스트 왔을 때 100번째 페이지 표시
            if (!string.IsNullOrEmpty(Request.Cookies["DotNetNotePageNum"]))
            {
                if (!String.IsNullOrEmpty(Request.Cookies["DotNetNotePageNum"]))
                {
                    PageIndex = Convert.ToInt32(Request.Cookies["DotNetNotePageNum"]);
                }
                else
                {
                    PageIndex = 0;
                }
            }

            // 게시판 리스트 정보 가져오기
            IEnumerable<Note> notes;
            if (!SearchMode)
            {
                TotalRecordCount = _repository.GetCountAll();
                notes = _repository.GetAll(PageIndex);
            }
            else
            {
                TotalRecordCount = _repository.GetCountBySearch(SearchField, SearchQuery);
                notes = _repository.GetSearchAll(PageIndex, SearchField, SearchQuery);
            }

            // 주요 정보를 뷰 페이지로 전송
            ViewBag.TotalRecord = TotalRecordCount;
            ViewBag.SearchMode = SearchMode;
            ViewBag.SearchField = SearchField;
            ViewBag.SearchQuery = SearchQuery;

            return View(notes);
        }

        /// <summary>
        /// 게시판 글쓰기 폼
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Create()
        {
            // 글쓰기 폼은 입력, 수정, 답변에서 _BoardEditorForm.cshtml 공유함
            ViewBag.FormType = BoardWriteFormType.Write;
            ViewBag.TitleDescription = "글 쓰기 - 다음 필드들을 채워주세요.";
            ViewBag.SaveButtonText = "저장";

            return View();
        }

        /// <summary>
        /// 게시판 글쓰기 처리 + 파일 업로드 처리
        /// </summary>
        /// <param name="model"></param>
        /// <param name="files"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create(Note model, ICollection<IFormFile> files)
        {
            // 파일 업로드 처리 시작
            string fileName = String.Empty;
            int fileSize = 0;

            var uploadFolder = Path.Combine(_environment.WebRootPath, "files");

            foreach(var file in files)
            {
                if (file.Length > 0)
                {
                    fileSize = Convert.ToInt32(file.Length);
                    // 파일명 중복 처리
                    fileName = Dul.FileUtility.GetFileNameWithNumbering(uploadFolder, Path.GetFileName(
                        ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"')));

                    // 파일 업로드
                    using (var fileStream = new FileStream(Path.Combine(uploadFolder, fileName), FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }
                }
            }

            Note note = new Note();

            note.Name = model.Name;
            note.Email = Dul.HtmlUtility.Encode(model.Email);
            note.Homepage = model.Homepage;
            note.Title = Dul.HtmlUtility.Encode(model.Title);
            note.Content = model.Content;
            note.FileName = fileName;
            note.FileSize = fileSize;
            note.Password = model.Password;
            note.PostIp = HttpContext.Connection.RemoteIpAddress.ToString(); // IP 주소
            note.Encoding = model.Encoding;

            _repository.Add(note);  // 데이터 저장

            // 데이터 저장 후 리스트 페이지 이동시 toastr로 메시지 출력
            TempData["Message"] = "데이터가 저장되었습니다.";

            return RedirectToAction("Index"); // 저장 후 리스트 페이지로 이동
        }

        /// <summary>
        /// 게시판 파일 강제 다운로드 기능(/BoardDown/:Id)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public FileResult BoardDown(int id)
        {
            string fileName = "";

            // 넘겨져 온 번호에 해당하는 파일명 가져오기(보안때문에... 파일명 숨김)
            fileName = _repository.GetFileNameById(id);

            if (fileName == null)
            {
                return null;
            }
            else
            {
                // 다운로드 카운트 증가 메서드 호출
                _repository.UpdateDownCountById(id);

                byte[] fileBytes = System.IO.File.ReadAllBytes(Path.Combine(_environment.WebRootPath, "files") + "\\" + fileName);

                return File(fileBytes, "application/octet-stream", fileName);
            }
        }

        /// <summary>
        /// 게시판의 상세 보기 페이지(Details, BoardView)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IActionResult Details(int id)
        {
            // 넘겨온 Id 값에 해당하는 레코드 하나 읽어서 note 클래스에 바인딩
            var note = _repository.GetNoteById(id);

            //[!] 인코딩 방식에 따른 데이터 출격:
            // 직접 문자열 비교해도 되지만, 학습목적으로 열거형으로 비교
            ContentEncodingType encoding = (ContentEncodingType)Enum.Parse(typeof(ContentEncodingType), note.Encoding);
            string encodedContent = "";
            switch (encoding)
            {
                // Text: 소스 그대로 표현
                case ContentEncodingType.Text:
                    encodedContent = Dul.HtmlUtility.EncodeWithTabAndSpace(note.Content);
                    break;
                // Html: HTML 형식으로 출력
                case ContentEncodingType.Html:
                    encodedContent = note.Content; // 변환없음
                    break;
                // Mixed: 엔터처리만
                case ContentEncodingType.Mixed:
                    encodedContent = note.Content.Replace("\r\n", "<br />");
                    break;
                // Html: 기본
                default:
                    encodedContent = note.Content; // 변환없음
                    break;
            }
            ViewBag.Content = encodedContent; //[!]

            // 첨부된 파일 확인
            if (note.FileName.Length > 1)
            {
                // [a] 파일 다운로드 링크: String.Format()으로 표현해 봄
                ViewBag.FileName = String.Format("<a href='/DotNetNote/BoardDown?Id={0}'>" + "{1}{2} / 전송수: {3}</a>", note.Id, "<img src=\"/images/ext/ext_zip.gif\" border=\"0\">", note.FileName, note.DownCount);

                // [b] 이미지 미리보기: C# 6.0 String 보간법으로 표현해 봄
                if (Dul.BoardLibrary.IsPhoto(note.FileName))
                {
                    ViewBag.ImageDown = $"<img src=\'/DotNetNote/ImageDown/{note.Id}\'><br />";
                }
            }
            else
            {
                ViewBag.FileName = "(업로드된 파일이 없습니다.)";
            }

            // 현재 글에 해당하는 댓글 리스트와 현재 글 번호를 담아서 전달
            NoteCommentViewModel vm = new NoteCommentViewModel();
            vm.NoteCommentList = _commentRepository.GetNoteComments(note.Id);
            vm.BoardId = note.Id;
            ViewBag.CommentListAndId = vm;

            return View(note);
        }

        /// <summary>
        /// 게시판 삭제 폼
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Delete(int id)
        {
            ViewBag.Id = id;
            return View();
        }

        /// <summary>
        /// 게시판 삭제 처리
        /// </summary>
        /// <param name="id"></param>
        /// <param name="Password"></param>
        /// <returns></returns>
        public IActionResult Delete(int id, string Password)
        {
            if(_repository.DeleteNote(id, Password) > 0)
            {
                TempData["Message"] = "데이터가 삭제되었습니다.";

                // 학습 목적으로 삭제 후의 이동 페이지를 2군데 중 하나로 분기
                if (DateTime.Now.Second % 2 == 0)
                {
                    // [a] 삭제 후 특정 뷰 페이지로 이동
                    return RedirectToAction("DeleteCompleted");
                }
                else
                {
                    // [b] 삭제 후 Index 페이지로 이동
                    return RedirectToAction("Index");
                }
            }
            else
            {
                ViewBag.Message = "삭제되지 않습니다. 비밀번호를 확인하세요.";
            }

            ViewBag.Id = id;
            return View();
        }

        /// <summary>
        /// 게시판 삭제 완료 후 추가적인 처리할 때 페이지
        /// </summary>
        /// <returns></returns>
        public IActionResult DeleteCompleted()
        {
            return View();
        }

        /// <summary>
        /// 게시판 수정 폼
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.FormType = BoardWriteFormType.Modify;
            ViewBag.TitleDescription = "글 수정 - 아래 항목을 수정하세요.";
            ViewBag.SaveButtonText = "수정";

            // 기존 데이터를 바인딩
            var note = _repository.GetNoteById(id);

            // 첨부된 파일명 및 파일크기 기록
            if (note.FileName.Length > 1)
            {
                ViewBag.FileName = note.FileName;
                ViewBag.FileSize = note.FileSize;
                ViewBag.FileNamePrevious = $"기존에 업로드된 파일명: {note.FileName}";
            }
            else
            {
                ViewBag.FileName = "";
                ViewBag.FileSize = 0;
            }

            return View(note);
        }

        /// <summary>
        /// 게시판 수정 처리 + 파일 업로드
        /// </summary>
        /// <param name="model"></param>
        /// <param name="files"></param>
        /// <param name="id"></param>
        /// <param name="previousFileName"></param>
        /// <param name="previousFileSize"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Edit(Note model, ICollection<IFormFile> files, int id, string previousFileName = "", int previousFileSize = 0)
        {
            ViewBag.FormType = BoardWriteFormType.Modify;
            ViewBag.TitleDescription = "글 수정 - 아래 항목을 수정하세요.";
            ViewBag.SaveButtonText = "수정";

            string fileName = "";
            int fileSize = 0;

            if (previousFileName != null)
            {
                fileName = previousFileName;
                fileSize = previousFileSize;
            }

            // 파일 업로드 처리 시작
            var uploadFolder = Path.Combine(_environment.WebRootPath, "files");

            foreach (var file in files)
            {
                if (file.Length > 0)
                {
                    fileSize = Convert.ToInt32(file.Length);
                    // 파일명 중복 처리
                    fileName = Dul.FileUtility.GetFileNameWithNumbering(uploadFolder, Path.GetFileName(ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"')));
                    // 파일업로드
                    using (var fileStream = new FileStream(
                        Path.Combine(uploadFolder, fileName), FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }
                }
            }

            Note note = new Note();

            note.Id = id;
            note.Name = model.Name;
            note.Email = Dul.HtmlUtility.Encode(model.Email);
            note.Homepage = model.Homepage;
            note.Title = Dul.HtmlUtility.Encode(model.Title);
            note.Content = model.Content;
            note.FileName = fileName;
            note.FileSize = fileSize;
            note.Password = model.Password;
            note.ModifyIp = HttpContext.Connection.RemoteIpAddress.ToString(); // IP 주소
            note.Encoding = model.Encoding;

            int r = _repository.UpdateNote(note); // 데이터베이스에 수정 적용
            if (r > 0)
            {
                TempData["Message"] = "수정되었습니다.";
                return RedirectToAction("Details", new { Id = id });
            }
            else
            {
                ViewBag.ErrorMessage = "업데이트가 되지 않습니다. 암호를 확인하세요.";
                return View(note);
            }
        }
    }
}

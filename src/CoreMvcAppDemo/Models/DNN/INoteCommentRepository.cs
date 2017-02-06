using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreMvcAppDemo.Models.DNN
{
    public interface INoteCommentRepository
    {
        void AddNoteComment(INoteCommentRepository model);
        int DeleteNoteComment(int boardId, int id, string password);
        int GetCountBy(int boardId, int id, string password);
        List<NoteComment> GetNoteComments(int boardId);
        List<NoteComment> GetRecentComments();
    }
}

--[3] 해당 글을 세부적으로 읽어오는 저장 프로시저 : ViewNote
Create Procedure dbo.ViewNote
	@Id int
As
	-- 조회수 카운트 1 증가
	Update Notes Set ReadCount = ReadCount + 1 where Id = @Id

	-- 모든 항목 조회
	select * from Notes where Id = @Id
Go
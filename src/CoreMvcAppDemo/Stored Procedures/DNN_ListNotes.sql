--[2] 게시판에서 데이터 출력 : ListNotes
Create Procedure dbo.ListNotes
	@Page int
As
	with DotNetNoteOrderedLists
	As
	(
		select [Id], [Name], [Email], [Title], [PostDate], [ReadCount], [Ref], [Step], [RefOrder], [AnswerNum], [ParentNum], [CommentCount], [FileName], [FileSize], [DownCount], ROW_NUMBER() Over (Order By Ref Desc, RefOrder Asc) As 'RowNumber'
		from Notes
	)
	select * from DotNetNoteOrderedLists
	where RowNumber Between @Page * 10 + 1 And (@Page + 1) * 10
Go


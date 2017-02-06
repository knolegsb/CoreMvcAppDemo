--[9] 게시판에서 데이터 검색 리스트 : SearchNotes

Create Procedure dbo.SearchNotes
	@Page int,
	@SearchField nvarchar(25),
	@SearchQuery nvarchar(25)
As
	with DotNetNoteOrderLists
	As
	(
		select [Id], [Name], [Email], [Title], [PostDate], [ReadCount], [Ref], [Step], [RefOrder], [AnswerNum], [ParentNum], [CommentCount], [FileName], [FileSize], [DownCount], ROW_NUMBER() Over (Order By Ref Desc, RefOrder Asc) As 'RowNumber'
		from Notes
		where (
			case @SearchField
				when 'Name' then [Name]
				when 'Title' then Title
				when 'Content' then Content
				else
				@SearchQuery
			End
		) Like '%' + @SearchQuery + '%'
	)
	select [Id], [Name], [Email], [Title], [PostDate], [ReadCount], [Ref], [Step], [RefOrder], [AnswerNum], [ParentNum], [CommentCount], [FileName], [FileSize], [DownCount], [RowNumber]
	from DotNetNoteOrderLists
	where RowNumber Between @Page * 10 + 1 And (@Page + 1) * 10
	Order By Id Desc
Go
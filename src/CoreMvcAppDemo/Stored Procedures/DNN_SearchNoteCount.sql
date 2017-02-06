--[6] 검색 결과의 레코드 수 반환
Create Proc dbo.SearchNoteCount
	@SearchField nvarchar(25),
	@SearchQuery nvarchar(25)
As
	Set @SearchQuery = '%' + @SearchQuery + '%'

	select Count(*) from Notes where
		(
			case @SearchField
				when 'Name' then [Name]
				when 'Title' then Title
				when 'Content' then Content
				else @SearchQuery
			End
		)
		Like
		@SearchQuery
Go
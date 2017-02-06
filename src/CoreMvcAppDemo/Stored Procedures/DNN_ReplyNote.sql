--[4] 게시판에 글을 답변 : ReplyNote
Create Proc dbo.ReplyNote
	@Name nvarchar(25),
	@Email nvarchar(100),
	@Title nvarchar(150),
	@PostIp nvarchar(15),
	@Content ntext,
	@Password nvarchar(20),
	@Encoding nvarchar(10),
	@Homepage nvarchar(100),
	@ParentNum int,
	@FileName nvarchar(255),
	@FileSize int
As
	--[0] 변수 선언
	declare @MaxRefOrder int
	declare @MaxRefAnswerNum int
	declare @ParentRef int
	declare @ParentStep int
	declare @ParentRefOrder int

	--[1] 부모글의 답변수(AnswerNum)를 1 증가
	Update Notes Set AnswerNum = AnswerNum + 1 where Id = @ParentNum

	--[2] 같은 글에 대해서 답변을 두 번 이상하면 먼저 답변한 게 위에 나타나게 함.
	select @MaxRefOrder = RefOrder, @MaxRefAnswerNum = AnswerNum from Notes
	where
		ParentNum = @ParentNum And
		RefOrder = (select Max(RefOrder) from Notes where ParentNum = @ParentNum)

	If @MaxRefOrder is null
	Begin
		select @MaxRefOrder = RefOrder from Notes where Id = @ParentNum
		set @MaxRefAnswerNum = 0
	End

	--[3] 중간에 답변달 때(비집고 들어갈 자리 마련)
	select
		@ParentRef = Ref, @ParentStep = Step
	from Notes where Id = @ParentNum

	Update Notes
	set 
		RefOrder = RefOrder + 1
	where
		Ref = @ParentRef And RefOrder > (@MaxRefOrder + @MaxRefAnswerNum)

	--[4] 최종저장
	insert Notes
	(
		Name, Email, Title, PostIp, Content, Password, Encoding, Homepage, Ref, Step, RefOrder, ParentNum, FileName, FileSize
	)
	values
	(
		@Name, @Email, @Title, @PostIp, @Content, @Password, @Encoding, @Homepage, @ParentRef, @ParentStep + 1, @MaxRefOrder + @MaxRefAnswerNum + 1, @ParentNum, @FileName, @FileSize
	)
Go
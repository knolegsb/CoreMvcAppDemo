--[7] 해당 글을 지우는 저장 프로시저: 답변글이 있으면 업데이트하고 없으면 지운다.

Create Proc dbo.DeleteNote
	@Id int,
	@Password nvarchar(30)
As
	declare @cnt int
	select @cnt = Count(*) from Notes
	where Id = @Id And Password = @Password

	If @cnt = 0
	Begin
		Return 0
	End

	declare @AnswerNum int
	declare @RefOrder int
	declare @Ref int
	declare @ParentNum int

	select 
		@AnswerNum = AnswerNum, @RefOrder = RefOrder, @Ref = Ref, @ParentNum = ParentNum
	from Notes
	where Id = @Id

	If @AnswerNum = 0
	Begin
		If @RefOrder > 0
		Begin
			Update Notes set RefOrder = RefOrder - 1
			where Ref = @Ref And RefOrder > @RefOrder
			Update Notes set AnswerNum = AnswerNum - 1 where Id = @ParentNum
		End
		Delete Notes where Id = @Id
		Delete Notes
		where Id = @ParentNum And ModifyIp = N'((DELETED))' And AnswerNum = 0
	End
	Else
	Begin
		Update Notes
		set
			Name = N'(Unknown)', Email = '', Password = '',
			Title = N'(삭제된 글입니다.)',
			Content = N'(삭제된 글입니다.)' + N'현재 답변이 포함되어 있기 때문에 내용만 삭제되었습니다.)',
			ModifyIp = N'((DELETED))', FileName = '',
			FileSize = 0, CommentCount = 0
		where Id = @Id
	End
Go

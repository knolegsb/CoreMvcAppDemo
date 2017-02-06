-- [1] 게시판에 글을 작성 : WriteNote
Create Proc dbo.WriteNote
	@Name nvarchar(25),
	@Email nvarchar(100),
	@Title nvarchar(150),
	@PostIp nvarchar(15),
	@Content ntext,
	@Password nvarchar(20),
	@Encoding nvarchar(10),
	@Homepage nvarchar(100),
	@FileName nvarchar(255),
	@FileSize int
As
	Declare @MaxRef Int
	Select @MaxRef = Max(Ref) from Notes

	If @MaxRef is null
		Set @MaxRef = 1
	else
		Set @MaxRef = @MaxRef + 1

	insert Notes
	(
		Name, Email, Title, PostIp, Content, Password, Encoding, Homepage, Ref, FileName, FileSize
	)
	Values
	(
		@Name, @Email, @Title, @PostIp, @Content, @Password, @Encoding, @Homepage, @MaxRef, @FileName, @FileSize
	)
Go
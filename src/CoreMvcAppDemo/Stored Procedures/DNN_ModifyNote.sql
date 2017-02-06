--[8] 해당 글을 수정하는 저장 프로시저 : ModifyNote
Create Proc dbo.ModifyNote
	@Name nvarchar(25),
	@Email nvarchar(100),
	@Title nvarchar(150),
	@ModifyIp nvarchar(15),
	@Content ntext,
	@Password nvarchar(30),
	@Encoding nvarchar(10),
	@Homepage nvarchar(100),
	@FileName nvarchar(255),
	@FileSize int,

	@Id int
As
	Declare @cnt int

	select @cnt = Count(*) from notes where Id = @Id And Password = @Password

	If @cnt > 0
	Begin
		Update Notes
		set
			Name = @Name, Email = @Email, Title = @Title, ModifyIp = @ModifyIp, ModifyDate = GetDate(), Content = @Content, Encoding = @Encoding, Homepage = @Homepage, FileName = @FileName, FileSize = @FileSize
		where Id = @Id

		Select '1'
	End
	else
		select '0'
Go
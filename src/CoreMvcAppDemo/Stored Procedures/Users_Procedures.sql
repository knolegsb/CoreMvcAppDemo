Create Proc dbo.WriteUsers
	@UserID NVarChar(25),
	@Password NVarChar(20)
As
	Insert Into Users Values(@UserID, @Password)
Go

Create Proc dbo.ListUsers
As
	Select [UID], [UserID], [Password] From Users Order By UID Desc
Go

Create Proc dbo.ViewUsers
	@UID int
As
	Select [UID], [UserID], [Password] From Users Where UID = @UID
Go

Create Proc dbo.ModifyUsers
	@UserID nvarchar(25),
	@Password nvarchar(20),
	@UID int
As
	Begin Tran
		Update Users
		Set
			UserID = @UserID,
			[Password] = @Password
		Where UID = @UID
	Commit Tran
Go

Create Proc dbo.DeleteUsers
	@UID int
As
	Delete Users Where UID = @UID
Go

Create Proc dbo.SearchUsers
	@SearchField nvarchar(25),
	@SearchQuery nvarchar(25)
As
	Declare @strSql nvarchar(25)
	Set @strSql = '
		Select * From Users
		Where ' + @SearchField + ' Like ''%' + @SearchQuery + '%'''
	Exec(@strSql)
Go

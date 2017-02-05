CREATE TABLE [dbo].[CommunityCampJoinMembers]
(
	[Id] int not null primary key identity(1,1),
	[CommunityName] nvarchar(25) not null,
	[Name] nvarchar(25) not null,
	[Mobile] nvarchar(30) not null,
	[Email] nvarchar(100) not null,
	[Size] nvarchar(10) not null default('L'),
	[CreationDate] DateTime default(GetDate())
)
Go

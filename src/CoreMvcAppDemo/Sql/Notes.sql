﻿Create Table dbo.Notes
(
	Id int identity(1,1) not null primary key,
	Name nvarchar(25) not null,
	Email nvarchar(100) null,
	Title nvarchar(150) not null,
	PostDate DateTime default GetDate() not null,
	PostIp nvarchar(15) null,
	Content ntext not null,
	Password nvarchar(20) null,
	ReadCount int default 0,
	Encoding nvarchar(10) not null,
	Homepage nvarchar(100) null,
	ModifyDate DateTime null,
	ModifyIp nvarchar(15) null,
	FileName nvarchar(255) null,
	FileSize int default 0,
	DownCount int default 0,
	Ref int not null,
	Step int default 0,
	RefOrder int default 0,
	AnswerNum int default 0,
	ParentNum int default 0,
	CommentCount int default 0,
	Category nvarchar(10) default('Free') null
)
Go
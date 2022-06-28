CREATE TABLE [dbo].[Orders]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	[Status] nvarchar(20) NOT NULL,
	[CreatedDate] datetime2 NOT NULL,
	[UpdatedDate] datetime2,
	[ProductId] INT NOT NULL FOREIGN KEY REFERENCES Products(Id)
)

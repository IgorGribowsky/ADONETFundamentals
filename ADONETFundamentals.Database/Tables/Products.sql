CREATE TABLE [dbo].[Products]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	[Name] nvarchar(50) NOT NULL,
	[Description] nvarchar(100) NOT NULL,
	[Weight] float NOT NULL,
	[Height] float NOT NULL,
	[Width] float NOT NULL,
	[Length] float NOT NULL
)

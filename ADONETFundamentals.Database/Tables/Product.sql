CREATE TABLE [dbo].[Product]
(
	[Id] INT NOT NULL PRIMARY KEY,
	[Name] nvarchar(50) NOT NULL,
	[Description] nvarchar(100) NOT NULL,
	[Weight] float NOT NULL,
	[Height] float NOT NULL,
	[Width] float NOT NULL,
	[Length] float NOT NULL
)

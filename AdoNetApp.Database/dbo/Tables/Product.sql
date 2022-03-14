CREATE TABLE [dbo].[Product]
(
	[Id] INT NOT NULL  IDENTITY, 
    [Name] NVARCHAR(50) NOT NULL, 
    [Description] NVARCHAR(MAX) NOT NULL, 
    [Weight] INT NOT NULL, 
    [Height] INT NOT NULL, 
    [Width] INT NOT NULL, 
    [Length] INT NOT NULL, 
    CONSTRAINT [PK_Product] PRIMARY KEY ([Id])
)

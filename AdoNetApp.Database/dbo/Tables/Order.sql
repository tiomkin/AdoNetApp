CREATE TABLE [dbo].[Order]
(
	[Id] INT NOT NULL  IDENTITY, 
    [Status] NVARCHAR(50) NOT NULL, 
    [CreatedDate] DATETIME NOT NULL, 
    [UpdatedDate] DATETIME NULL, 
    [ProductId] INT NOT NULL, 
    CONSTRAINT [FK_Order_Product] FOREIGN KEY ([ProductId]) REFERENCES [Product]([Id]), 
    CONSTRAINT [PK_Order] PRIMARY KEY ([Id]) 
)

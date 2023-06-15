CREATE TABLE [dbo].[AddressPerson]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [AddressId] INT NOT NULL, 
    [PersonId] INT NOT NULL
)

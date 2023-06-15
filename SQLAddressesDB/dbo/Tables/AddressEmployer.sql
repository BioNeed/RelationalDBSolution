CREATE TABLE [dbo].[AddressEmployer]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [AddressId] INT NOT NULL, 
    [EmployerId] INT NOT NULL
)

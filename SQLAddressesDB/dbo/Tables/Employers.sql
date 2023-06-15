CREATE TABLE [dbo].[Employers]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [CompanyName] NVARCHAR(50) NOT NULL, 
    [IsHiring] BIT NOT NULL
)

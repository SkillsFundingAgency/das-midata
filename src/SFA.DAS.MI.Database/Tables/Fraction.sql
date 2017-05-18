CREATE TABLE [dbo].[Fraction]
(
	[Id] BIGINT IDENTITY(-1,-1) NOT NULL PRIMARY KEY,
	[EmpRef] varchar(20) not null,
	[Amount] decimal(18,4) not null,
	[DateCalculated] DATETIME not null
)

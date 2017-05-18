CREATE TABLE [dbo].[Declaration]
(
	[Id] BIGINT IDENTITY(-1,-1) NOT NULL PRIMARY KEY,
	[EmpRef] varchar(20) not null,
	[PayrollYear] varchar(10) null,
	[PayrollMonth] int null,
	[SubmissionDate] DATETIME NOT NULL,
	[LevyDueYtd] decimal(18,4) not null default 0,
	[LevyAllowanceForYear] decimal(18,4) not null default 0,
	[CessationDate] DATETIME null
)

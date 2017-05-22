CREATE PROCEDURE [dbo].[SaveDeclaration]

	@EmpRef varchar(20),
	@PayrollMonth int,
	@PayrollYear varchar (10),
	@SubmissionDate DATETIME,
	@LevyDueYtd decimal (18,4),
	@LevyAllowanceForYear decimal (18, 4),
	@CessationDate DATETIME
AS
	INSERT INTO [dbo].[Declaration] (EmpRef, PayrollMonth, PayrollYear, SubmissionDate, LevyDueYtd, LevyAllowanceForYear, CessationDate )
	VALUES (@EmpRef, @PayrollMonth, @PayrollYear, @SubmissionDate,@LevyDueYtd, @LevyAllowanceForYear, @CessationDate )
RETURN 0



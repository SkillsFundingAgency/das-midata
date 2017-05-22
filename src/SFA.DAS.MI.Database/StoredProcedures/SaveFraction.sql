CREATE PROCEDURE [dbo].[SaveFraction]

	@EmpRef varchar(20),
	@Amount decimal (18, 4),
	@DateCalculated DATETIME
AS
	INSERT INTO [dbo].[Fraction] (EmpRef, Amount, DateCalculated )
	VALUES (@EmpRef, @Amount, @DateCalculated )
RETURN 0



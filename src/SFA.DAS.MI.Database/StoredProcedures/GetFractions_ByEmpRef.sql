CREATE PROCEDURE [dbo].[GetFractions_ByEmpRef]
	@empRef varchar(20)
AS
	select * from Fraction where EmpRef = @empRef
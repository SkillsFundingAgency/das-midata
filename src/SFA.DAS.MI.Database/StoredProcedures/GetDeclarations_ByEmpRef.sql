CREATE PROCEDURE [dbo].[GetDeclarations_ByEmpRef]
	@empRef varchar(20)
AS
	select * from Declaration where EmpRef = @empRef


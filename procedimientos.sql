﻿

create procedure spInsertarMatricula 
@IIDPERIODO int, @IIDGRADO int, @IIDSECCION int , @IIDALUMNO int
as
BEGIN TRANSACTION

BEGIN TRY
    insert into dbo.Matricula
	(IIDPERIODO,IIDGRADO,IIDSECCION,IIDALUMNO,FECHA,BHABILITADO)
	values(@IIDPERIODO,@IIDGRADO,@IIDSECCION,@IIDALUMNO,getdate(),1);
	SELECT SCOPE_IDENTITY();

    COMMIT TRANSACTION
END TRY
BEGIN CATCH
    SELECT ERROR_NUMBER() AS errNumber
       , ERROR_SEVERITY() AS errSeverity 
       , ERROR_STATE() AS errState
       , ERROR_PROCEDURE() AS errProcedure
       , ERROR_LINE() AS errLine
       , ERROR_MESSAGE() AS errMessage
    IF @@TRANCOUNT > 0
        ROLLBACK TRANSACTION
END CATCH

GO

create procedure spActualizarMatricula 
@IIDMATRICULA int, @IIDPERIODO int, @IIDGRADO int, @IIDSECCION int , @IIDALUMNO int
as
BEGIN TRANSACTION
BEGIN TRY
	UPDATE dbo.Matricula   
	SET IIDPERIODO=@IIDPERIODO,IIDGRADO=@IIDGRADO,IIDSECCION=@IIDSECCION,IIDALUMNO=@IIDALUMNO
	WHERE IIDMATRICULA = @IIDMATRICULA
    COMMIT TRANSACTION
END TRY
BEGIN CATCH
    SELECT ERROR_NUMBER() AS errNumber
       , ERROR_SEVERITY() AS errSeverity 
       , ERROR_STATE() AS errState
       , ERROR_PROCEDURE() AS errProcedure
       , ERROR_LINE() AS errLine
       , ERROR_MESSAGE() AS errMessage
    IF @@TRANCOUNT > 0
        ROLLBACK TRANSACTION
END CATCH

GO

create procedure spInsertarDetalleMatricula
@IIDMATRICULA int,@IIDCURSO int,@NOTA1 int,@NOTA2 int,@NOTA3 int,@NOTA4 int, @PROMEDIO int
as
BEGIN TRANSACTION

BEGIN TRY
    insert into dbo.DetalleMatricula
	(IIDMATRICULA,IIDCURSO,NOTA1,NOTA2,NOTA3,NOTA4,PROMEDIO,bhabilitado)
	values
	(@IIDMATRICULA,@IIDCURSO,@NOTA1,@NOTA2,@NOTA3,@NOTA4,@PROMEDIO,1);
    COMMIT TRANSACTION
END TRY
BEGIN CATCH
    SELECT ERROR_NUMBER() AS errNumber
       , ERROR_SEVERITY() AS errSeverity 
       , ERROR_STATE() AS errState
       , ERROR_PROCEDURE() AS errProcedure
       , ERROR_LINE() AS errLine
       , ERROR_MESSAGE() AS errMessage
    IF @@TRANCOUNT > 0
        ROLLBACK TRANSACTION
END CATCH

GO
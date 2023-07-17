

create procedure spInsertarMatricula 
@IIDPERIODO int, @IIDGRADO int, @IIDSECCION int , @IIDALUMNO int, @FECHA int, @BHABILITADO int
as
insert into dbo.Matricula
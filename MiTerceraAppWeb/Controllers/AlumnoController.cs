using DConexionBase3;
using MiTerceraAppWeb.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MiTerceraAppWeb.Controllers
{
    public class AlumnoController : Controller
    {
        // GET: Alumno
        public ActionResult Index()
        {
            return View();
        }

		public JsonResult buscarAlumnossexo(int sexo)
		{
			try
			{
				List<Alumno> alumnos = new List<Alumno>();
				DBAcceso db = new DBAcceso();
				DataTable dt = db.obtenerAlumnos(sexo);
				foreach (DataRow row in dt.Rows)
				{
					int IIDALUMNO = int.Parse(row["IIDALUMNO"].ToString());
					string NOMBRE = row["NOMBRE"].ToString();
					string APPATERNO = row["APPATERNO"].ToString();
					string APMATERNO = row["APMATERNO"].ToString();
					DateTime FECHANACIMIENTO = DateTime.Parse(row["FECHANACIMIENTO"].ToString());
					int IIDSEXO = int.Parse(row["IIDSEXO"].ToString());
					string TELEFONOPADRE = row["TELEFONOPADRE"].ToString();
					string TELEFONOMADRE = row["TELEFONOMADRE"].ToString();
					int NUMEROHERMANOS = int.Parse(row["NUMEROHERMANOS"].ToString());
					int BHABILITADO = int.Parse(row["BHABILITADO"].ToString());
					string IIDTIPOUSUARIO = row["IIDTIPOUSUARIO"].ToString();
					int bTieneUsuario = int.Parse(row["bTieneUsuario"].ToString());
					Alumno periodo;
					periodo = new Alumno
					{
						IIDALUMNO = IIDALUMNO,
						NOMBRE = NOMBRE,
						APPATERNO = APPATERNO,
						APMATERNO = APMATERNO,
						FECHANACIMIENTO = FECHANACIMIENTO,
						IIDSEXO = IIDSEXO,
						TELEFONOPADRE = TELEFONOPADRE,
						TELEFONOMADRE = TELEFONOMADRE,
						NUMEROHERMANOS = NUMEROHERMANOS,
						BHABILITADO = BHABILITADO,
						IIDTIPOUSUARIO = IIDTIPOUSUARIO,
						bTieneUsuario = bTieneUsuario
					};
					alumnos.Add(periodo);
				}
				return new JsonResult { Data = alumnos, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
			}
			catch (Exception ex)
			{
				return new JsonResult { Data = ex.Message, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
			}
		}
		public JsonResult buscarAlumnos(string nombre)
		{
			try
			{
				List<Alumno> alumnos = new List<Alumno>();
				DBAcceso db = new DBAcceso();
				DataTable dt = db.obtenerAlumnos(nombre);
				//[IIDALUMNO][int] IDENTITY(1, 1) NOT NULL,
				//[NOMBRE] [varchar] (100) NULL,
				//[APPATERNO][varchar] (150) NULL,
				//[APMATERNO][varchar] (150) NULL,
				//[FECHANACIMIENTO][datetime] NULL,
				//[IIDSEXO][int] NULL,
				//[TELEFONOPADRE][varchar] (10) NULL,
				//[TELEFONOMADRE][varchar] (10) NULL,
				//[NUMEROHERMANOS][int] NULL,
				//[BHABILITADO][int] NULL,
				//[IIDTIPOUSUARIO][char](1) NULL,
				//[bTieneUsuario][int] NULL,
				foreach (DataRow row in dt.Rows)
				{
					int IIDALUMNO = int.Parse(row["IIDALUMNO"].ToString());
					string NOMBRE = row["NOMBRE"].ToString();
					string APPATERNO = row["APPATERNO"].ToString();
					string APMATERNO = row["APMATERNO"].ToString();
					DateTime FECHANACIMIENTO = DateTime.Parse(row["FECHANACIMIENTO"].ToString());
					int IIDSEXO = int.Parse(row["IIDSEXO"].ToString());
					string TELEFONOPADRE = row["TELEFONOPADRE"].ToString();
					string TELEFONOMADRE = row["TELEFONOMADRE"].ToString();
					int NUMEROHERMANOS = int.Parse(row["NUMEROHERMANOS"].ToString());
					int BHABILITADO = int.Parse(row["BHABILITADO"].ToString());
					string IIDTIPOUSUARIO = row["IIDTIPOUSUARIO"].ToString();
					int bTieneUsuario = int.Parse(row["bTieneUsuario"].ToString());
					Alumno periodo;
					periodo = new Alumno
					{
						IIDALUMNO = IIDALUMNO,
						NOMBRE = NOMBRE,
						APPATERNO = APPATERNO,
						APMATERNO = APMATERNO,
						FECHANACIMIENTO = FECHANACIMIENTO,
						IIDSEXO = IIDSEXO,
						TELEFONOPADRE = TELEFONOPADRE,
						TELEFONOMADRE = TELEFONOMADRE,
						NUMEROHERMANOS = NUMEROHERMANOS,
						BHABILITADO = BHABILITADO,
						IIDTIPOUSUARIO = IIDTIPOUSUARIO,
						bTieneUsuario = bTieneUsuario
					};
					alumnos.Add(periodo);
				}
				return new JsonResult { Data = alumnos, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
			}
			catch (Exception ex)
			{
				return new JsonResult { Data = ex.Message, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
			}
		}
		public JsonResult listarAlumnos()
		{
			try
			{
				List<Alumno> alumnos = new List<Alumno>();
				DBAcceso db = new DBAcceso();
				DataTable dt = db.obtenerAlumnos();
				foreach (DataRow row in dt.Rows)
				{
					int IID = int.Parse(row["IID"].ToString());
					int IIDALUMNO = int.Parse(row["IIDALUMNO"].ToString());
					string NOMBRE = row["NOMBRE"].ToString();
					string APPATERNO = row["APPATERNO"].ToString();
					string APMATERNO = row["APMATERNO"].ToString();
					DateTime FECHANACIMIENTO = DateTime.Parse(row["FECHANACIMIENTO"].ToString());
					int IIDSEXO = int.Parse(row["IIDSEXO"].ToString());
					string TELEFONOPADRE = row["TELEFONOPADRE"].ToString();
					string TELEFONOMADRE = row["TELEFONOMADRE"].ToString();
					int NUMEROHERMANOS = int.Parse(row["NUMEROHERMANOS"].ToString());
					int BHABILITADO = int.Parse(row["BHABILITADO"].ToString());
					string IIDTIPOUSUARIO = row["IIDTIPOUSUARIO"].ToString();
					int bTieneUsuario = int.Parse(row["bTieneUsuario"].ToString());
					Alumno periodo;
					periodo = new Alumno
					{
						IID = IID,
						IIDALUMNO = IIDALUMNO,
						NOMBRE = NOMBRE,
						APPATERNO = APPATERNO,
						APMATERNO = APMATERNO,
						FECHANACIMIENTO = FECHANACIMIENTO,
						IIDSEXO = IIDSEXO,
						TELEFONOPADRE = TELEFONOPADRE,
						TELEFONOMADRE = TELEFONOMADRE,
						NUMEROHERMANOS = NUMEROHERMANOS,
						BHABILITADO = BHABILITADO,
						IIDTIPOUSUARIO = IIDTIPOUSUARIO,
						bTieneUsuario = bTieneUsuario
					};
					alumnos.Add(periodo);
				}
				return new JsonResult { Data = alumnos, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
			}
			catch (Exception ex)
			{
				return new JsonResult { Data = ex.Message, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
			}
		}

		public JsonResult listarSexo()
		{
			try
			{
				List<Sexo> sexos = new List<Sexo>();
				DBAcceso db = new DBAcceso();
				DataTable dt = db.obtenerSexo();
				foreach (DataRow row in dt.Rows)
				{
					int IID = int.Parse(row["IID"].ToString());
					int IIDSEXO = int.Parse(row["IIDSEXO"].ToString());
					string NOMBRE = row["NOMBRE"].ToString();
					Sexo sexo;
					sexo = new Sexo
					{
						IID = IID,
						IIDSEXO = IIDSEXO,
						NOMBRE = NOMBRE
					};
					sexos.Add(sexo);
				}
				return new JsonResult { Data = sexos, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
			}
			catch (Exception ex)
			{
				return new JsonResult { Data = ex.Message, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
			}
		}
	}
}
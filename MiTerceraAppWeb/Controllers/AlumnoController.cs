using DConexionBase3;
using MiTerceraAppWeb.Models;
using Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
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
				List<AlumnoModels> alumnos = new List<AlumnoModels>();
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
					AlumnoModels periodo;
					periodo = new AlumnoModels
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
				List<AlumnoModels> alumnos = new List<AlumnoModels>();
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
                    AlumnoModels periodo;
					periodo = new AlumnoModels
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
				List<AlumnoModels> alumnos = new List<AlumnoModels>();
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
                    AlumnoModels periodo;
					periodo = new AlumnoModels
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

		public JsonResult recuperarAlumno(int id)
		{
			try
			{
				List<AlumnoModels> alumnos = new List<AlumnoModels>();
				DBAcceso db = new DBAcceso();
				DataTable dt = db.obtenerAlumnoId(id);
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
                    AlumnoModels periodo;
					periodo = new AlumnoModels
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

        [HttpPost]
        public JsonResult guardarDatos(Alumno alumno)
        {
            try
            {
                Miconexion3DataContext bd = new Miconexion3DataContext();
                int nveces = 0;
                nveces = bd.Alumno.Where(p => p.NOMBRE.Equals(alumno.NOMBRE) && p.APPATERNO.Equals(alumno.APPATERNO) && p.APMATERNO.Equals(alumno.APMATERNO)).Count();
                //DBAcceso db = new DBAcceso();
                //alumno.FECHANACIMIENTO = DateTime.Parse(alumno.FECHANACIMIENTOSTRING);
        
                int resultado = 0;
                if (alumno.IIDALUMNO == 0)
                {
                    alumno.IIDTIPOUSUARIO = 'A';
                    if (nveces == 0)
                    {
                        bd.Alumno.InsertOnSubmit(alumno);
                        bd.SubmitChanges();
                        resultado = 1;
                    }
                    else
                    {
                        resultado = -1;
                    }
                }
                else
                {
                    Alumno update = bd.Alumno.Where(p => p.IIDALUMNO.Equals(alumno.IIDALUMNO)).First();
                    update.NOMBRE = alumno.NOMBRE;
                    update.APPATERNO = alumno.APPATERNO;
                    update.APMATERNO = alumno.APMATERNO;
                    update.FECHANACIMIENTO = alumno.FECHANACIMIENTO;
                    update.IIDSEXO = alumno.IIDSEXO;
                    update.TELEFONOPADRE = alumno.TELEFONOPADRE;
                    update.TELEFONOMADRE = alumno.TELEFONOMADRE;
                    update.NUMEROHERMANOS = alumno.NUMEROHERMANOS;
                    update.BHABILITADO = alumno.BHABILITADO;
                    update.IIDTIPOUSUARIO = alumno.IIDTIPOUSUARIO;
                    bd.SubmitChanges();
                    resultado = 1;
                }

                return new JsonResult { Data = resultado, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            catch (Exception ex)
            {
                return new JsonResult { Data = ex.Message, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }

        [HttpPost]
		public JsonResult guardarDatos2(AlumnoModels alumno)
		{
			try
			{
                Miconexion3DataContext bd = new Miconexion3DataContext();
                int nveces = 0;
                nveces = bd.Alumno.Where(p => p.NOMBRE.Equals(alumno.NOMBRE) && p.APPATERNO.Equals(alumno.APPATERNO) && p.APMATERNO.Equals(alumno.APMATERNO)).Count();
                DBAcceso db = new DBAcceso();
				alumno.FECHANACIMIENTO = DateTime.Parse(alumno.FECHANACIMIENTOSTRING);
				int resultado = 0;
                if (alumno.IIDALUMNO == 0)
				{
					if (nveces == 0)
					{
                        resultado = db.insertarAlumno(alumno);
                    }else
					{
						resultado = -1;
					}
                }
                else
				{
					resultado = db.actualizarAlumno(alumno); 
                }
                    
				return new JsonResult { Data = resultado, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
			}
			catch (Exception ex)
			{
				return new JsonResult { Data = ex.Message, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
			}
		}

		public JsonResult eliminarAlumno(int id)
		{
			try
			{
				DBAcceso db = new DBAcceso();
				int resultado = db.eliminarAlumno(id);
				return new JsonResult { Data = resultado, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
			}
			catch (Exception ex)
			{
				return new JsonResult { Data = ex.Message, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
			}
		}
	}
}
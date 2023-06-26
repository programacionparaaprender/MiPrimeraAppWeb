using DConexionBase3;
using MiTerceraAppWeb.Models;
using Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MiTerceraAppWeb.Controllers
{
    public class RepasoHTMLController : Controller
    {
        // GET: RepasoHTML
        public ActionResult Index()
        {
            return View();
        }

		public ActionResult Tabla()
		{
			return View();
		}

		public ActionResult ComboBox()
		{
			return View();
		}

		public ActionResult TablaJS()
		{
			return View();
		}

		public JsonResult listarPersonas()
		{
			List<Persona> listarPersonas = new List<Persona>
			{
				new Persona { idPersona = 1, nombre = "Pedro", apellidoPaterno = "Perez"},
				new Persona { idPersona = 2, nombre = "Jose", apellidoPaterno = "Fonseca"},
				new Persona { idPersona = 3, nombre = "Lucho", apellidoPaterno = "Carmona"}
			};
			return Json(listarPersonas, JsonRequestBehavior.AllowGet);
		}

		public JsonResult buscarCursos(string nombre)
		{
			try
			{
				List<Curso> cursos = new List<Curso>();
				DBAcceso db = new DBAcceso();
				DataTable dt = db.obtenerCursos(nombre);

				foreach (DataRow row in dt.Rows)
				{
					int IIDCURSO = int.Parse(row["IIDCURSO"].ToString());
					string NOMBRE = row["NOMBRE"].ToString();
					string DESCRIPCION = row["DESCRIPCION"].ToString();
					int BHABILITADO = int.Parse(row["BHABILITADO"].ToString());
					Curso curso;
					curso = new Curso { IIDCURSO = IIDCURSO, NOMBRE = NOMBRE, DESCRIPCION = DESCRIPCION, BHABILITADO = BHABILITADO };
					cursos.Add(curso);
				}
				return new JsonResult { Data = cursos, JsonRequestBehavior = JsonRequestBehavior.AllowGet };


			}
			catch (Exception ex)
			{
				return new JsonResult { Data = ex.Message, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
			}
		}

		public JsonResult listarCursos()
		{
			try
			{
				List<Curso> cursos = new List<Curso>();
				DBAcceso db = new DBAcceso();
				DataTable dt = db.obtenerCursos();

				foreach (DataRow row in dt.Rows)
				{
					int IIDCURSO = int.Parse(row["IIDCURSO"].ToString());
					string NOMBRE = row["NOMBRE"].ToString();
					string DESCRIPCION = row["DESCRIPCION"].ToString();
					int BHABILITADO = int.Parse(row["BHABILITADO"].ToString());
					Curso curso;
					curso = new Curso { IIDCURSO = IIDCURSO, NOMBRE = NOMBRE, DESCRIPCION = DESCRIPCION, BHABILITADO = BHABILITADO };
					cursos.Add(curso);
				}
				//Agregar a los campos virtuales [JsonIgnore] libreria es using Newtonsoft.Json;
				//SistemaMatriculaEntities bd = new SistemaMatriculaEntities();
				//var cursos = bd.Curso.ToList();
				//List<Curso> cursos = new List<Curso>
				//{
				//    new Curso { IIDCURSO = 1, NOMBRE = "Curso 1", DESCRIPCION="Descripcion 1", BHABILITADO = 1 },
				//    new Curso { IIDCURSO = 1, NOMBRE = "Curso 2", DESCRIPCION="Descripción 2", BHABILITADO = 1 },
				//    new Curso { IIDCURSO = 1, NOMBRE = "Curso 3", DESCRIPCION="Descripción 3", BHABILITADO = 1 }
				//};
				return new JsonResult { Data = cursos, JsonRequestBehavior = JsonRequestBehavior.AllowGet };


			}
			catch (Exception ex)
			{
				return new JsonResult { Data = ex.Message, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
			}
		}
	}
}
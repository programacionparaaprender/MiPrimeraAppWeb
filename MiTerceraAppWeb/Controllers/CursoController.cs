using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MiTerceraAppWeb.Models;
using System.Data;
using DConexionBase3;
//using DConexionBase3;

namespace MiTerceraAppWeb.Controllers
{
    public class CursoController : Controller
    {
        // GET: Curso
        public ActionResult Index()
        {
            return View();
        }

        public string mensaje()
        {
            return "Bienvenido al curso ASP.NET MVC";
        }

        public string saludo(string nombre)
        {
            return "Hola como estas " + nombre;
        }

        public string saludoCompleto(string nombre, string apellido)
        {
            return "Hola como estas " + nombre + " " + apellido;
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
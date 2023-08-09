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
    public class PeriodoController : Controller
    {
        // GET: Periodo
        public ActionResult Index()
        {
            return View();
        }

		public JsonResult buscarPeriodo(string nombre)
		{
			try
			{
				List<Periodo> periodos = new List<Periodo>();
				DBAcceso db = new DBAcceso();
				DataTable dt = db.obtenerPeriodo(nombre);

				foreach (DataRow row in dt.Rows)
				{
					int IIDPERIODO = int.Parse(row["IIDPERIODO"].ToString());
					string NOMBRE = row["NOMBRE"].ToString();
					DateTime FECHAINICIO = DateTime.Parse(row["FECHAINICIO"].ToString());
					DateTime FECHAFIN = DateTime.Parse(row["FECHAFIN"].ToString());
					int BHABILITADO = int.Parse(row["BHABILITADO"].ToString());
					Periodo periodo;
					periodo = new Periodo { IIDPERIODO = IIDPERIODO, NOMBRE = NOMBRE, FECHAINICIO = FECHAINICIO, FECHAFIN = FECHAFIN, BHABILITADO = BHABILITADO };
					periodos.Add(periodo);
				}
				return new JsonResult { Data = periodos, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
			}
			catch (Exception ex)
			{
				return new JsonResult { Data = ex.Message, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
			}
		}
		public JsonResult listarPeriodo()
		{
			try
			{
				List<Periodo> periodos = new List<Periodo>();
				DBAcceso db = new DBAcceso();
				DataTable dt = db.obtenerPeriodo();

				foreach (DataRow row in dt.Rows)
				{
					int IIDPERIODO = int.Parse(row["IIDPERIODO"].ToString());
					string NOMBRE = row["NOMBRE"].ToString();
					DateTime FECHAINICIO = DateTime.Parse(row["FECHAINICIO"].ToString());
					DateTime FECHAFIN = DateTime.Parse(row["FECHAFIN"].ToString());
					int BHABILITADO = int.Parse(row["BHABILITADO"].ToString());
					Periodo periodo;
					periodo = new Periodo { IIDPERIODO = IIDPERIODO, NOMBRE = NOMBRE, FECHAINICIO = FECHAINICIO, FECHAFIN = FECHAFIN, BHABILITADO = BHABILITADO };
					periodos.Add(periodo);
				}
				return new JsonResult { Data = periodos, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
			}
			catch (Exception ex)
			{
				return new JsonResult { Data = ex.Message, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
			}
		}

		public JsonResult recuperarPeriodo(int id)
		{
			try
			{
				List<Periodo> periodos = new List<Periodo>();
				DBAcceso db = new DBAcceso();
				DataTable dt = db.obtenerPeriodo(id);

				foreach (DataRow row in dt.Rows)
				{
					int IIDPERIODO = int.Parse(row["IIDPERIODO"].ToString());
					string NOMBRE = row["NOMBRE"].ToString();
					DateTime FECHAINICIO = DateTime.Parse(row["FECHAINICIO"].ToString());
					DateTime FECHAFIN = DateTime.Parse(row["FECHAFIN"].ToString());
					int BHABILITADO = int.Parse(row["BHABILITADO"].ToString());
					Periodo periodo;
					periodo = new Periodo { IIDPERIODO = IIDPERIODO, NOMBRE = NOMBRE, FECHAINICIO = FECHAINICIO, FECHAFIN = FECHAFIN, BHABILITADO = BHABILITADO };
					periodos.Add(periodo);
				}
				return new JsonResult { Data = periodos, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
			}
			catch (Exception ex)
			{
				return new JsonResult { Data = ex.Message, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
			}
		}

		public JsonResult guardarDatos(PeriodoModels periodo)
		{
			try
			{
                Miconexion3DataContext bd = new Miconexion3DataContext();
                DBAcceso db = new DBAcceso();
				int resultado = 0;
                if (periodo.IIDPERIODO == 0)
				{
                    int nveces = 0;
                    nveces = bd.Periodo.Where(p => p.NOMBRE.Equals(periodo.NOMBRE) && periodo.BHABILITADO == 1).Count();
					if(nveces == 0)
					{
                        resultado = db.insertarPeriodo(periodo);
                    }
					else
					{
                        resultado = -1;
                    }
                }
                else
				{
                    resultado = db.actualizarPeriodo(periodo);
                }
				return new JsonResult { Data = resultado, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
			}
			catch (Exception ex)
			{
				return new JsonResult { Data = ex.Message, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
			}
		}

		public JsonResult eliminarPeriodo(int id)
		{
			try
			{
				DBAcceso db = new DBAcceso();
				int resultado = db.eliminarPeriodo(id);
				return new JsonResult { Data = resultado, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
			}
			catch (Exception ex)
			{
				return new JsonResult { Data = ex.Message, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
			}
		}
	}
}
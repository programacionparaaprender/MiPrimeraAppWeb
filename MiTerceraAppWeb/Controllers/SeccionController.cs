﻿using DConexionBase3;
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
    public class SeccionController : Controller
    {
        // GET: Seccion
        public ActionResult Index()
        {
            return View();
        }

		public JsonResult listarSecciones()
		{
			try
			{
				List<SeccionModels> secciones = new List<SeccionModels>();
				DBAcceso db = new DBAcceso();
				DataTable dt = db.obtenerSeccion();

				foreach (DataRow row in dt.Rows)
				{
					int IID = int.Parse(row["IID"].ToString());
					string NOMBRE = row["NOMBRE"].ToString();
					int BHABILITADO = int.Parse(row["BHABILITADO"].ToString());
                    SeccionModels periodo;
					periodo = new SeccionModels { IID = IID, NOMBRE = NOMBRE, BHABILITADO = BHABILITADO };
					secciones.Add(periodo);
				}
				return new JsonResult { Data = secciones, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
			}
			catch (Exception ex)
			{
				return new JsonResult { Data = ex.Message, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
			}
		}
	}
}
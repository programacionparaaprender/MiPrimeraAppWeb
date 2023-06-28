using DConexionBase3;
using MiTerceraAppWeb.Models;
using Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace MiTerceraAppWeb.Controllers
{
    public class DocenteController : Controller
    {
        // GET: Docente
        public ActionResult Index()
        {
            return View();
        }

		public JsonResult listarModalidadContrato()
		{
			try
			{
				List<ModalidadContrato> modalidadContratos = new List<ModalidadContrato>();
				DBAcceso db = new DBAcceso();
				DataTable dt = db.obtenerModalidadContrato();
				foreach (DataRow row in dt.Rows)
				{
					int IID = int.Parse(row["IID"].ToString());
					string NOMBRE = row["NOMBRE"].ToString();
					ModalidadContrato modalidadContrato;
					modalidadContrato = new ModalidadContrato
					{
						IID = IID,
						NOMBRE = NOMBRE
					};
					modalidadContratos.Add(modalidadContrato);
				}
				return new JsonResult { Data = modalidadContratos, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
			}
			catch (Exception ex)
			{
				return new JsonResult { Data = ex.Message, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
			}
		}

		public JsonResult buscarDocenteModalidadContrato(int ModalidadContrato)
		{
			try
			{
				List<Docente> docentes = new List<Docente>();
				DBAcceso db = new DBAcceso();
				DataTable dt = db.obtenerDocentes(ModalidadContrato);
				foreach (DataRow row in dt.Rows)
				{
					int IID = int.Parse(row["IID"].ToString());
					int IIDDOCENTE = int.Parse(row["IIDDOCENTE"].ToString());
					string NOMBRE = row["NOMBRE"].ToString();
					string APPATERNO = row["APPATERNO"].ToString();
					string APMATERNO = row["APMATERNO"].ToString();
					string DIRECCION = row["DIRECCION"].ToString();
					string TELEFONOCELULAR = row["TELEFONOCELULAR"].ToString();
					string TELEFONOFIJO = row["TELEFONOFIJO"].ToString();
					string EMAIL = row["EMAIL"].ToString();
					int IIDSEXO = int.Parse(row["IIDSEXO"].ToString());
					DateTime FECHACONTRATO = DateTime.Parse(row["FECHACONTRATO"].ToString());
					byte[] FOTO = Encoding.ASCII.GetBytes(row["FOTO"].ToString());
					int IIDMODALIDADCONTRATO = int.Parse(row["IIDMODALIDADCONTRATO"].ToString());
					int BHABILITADO = int.Parse(row["BHABILITADO"].ToString());
					string IIDTIPOUSUARIO = row["IIDTIPOUSUARIO"].ToString();
					int bTieneUsuario = int.Parse(row["bTieneUsuario"].ToString());
					Docente periodo;
					periodo = new Docente
					{
						IID = IID,
						NOMBRE = NOMBRE,
						APPATERNO = APPATERNO,
						APMATERNO = APMATERNO,
						DIRECCION = DIRECCION,
						TELEFONOCELULAR = TELEFONOCELULAR,
						TELEFONOFIJO = TELEFONOFIJO,
						EMAIL = EMAIL,
						IIDSEXO = IIDSEXO,
						FECHACONTRATO = FECHACONTRATO,
						FOTO = FOTO,
						IIDMODALIDADCONTRATO = IIDMODALIDADCONTRATO,
						BHABILITADO = BHABILITADO,
						IIDTIPOUSUARIO = IIDTIPOUSUARIO,
						bTieneUsuario = bTieneUsuario
					};
					docentes.Add(periodo);
				}
				return new JsonResult { Data = docentes, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
			}
			catch (Exception ex)
			{
				return new JsonResult { Data = ex.Message, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
			}
		}
		public JsonResult listarDocentes()
		{
			try
			{
				List<Docente> docentes = new List<Docente>();
				DBAcceso db = new DBAcceso();
				DataTable dt = db.obtenerDocentes();
				foreach (DataRow row in dt.Rows)
				{
					int IID = int.Parse(row["IID"].ToString());
					int IIDDOCENTE = int.Parse(row["IIDDOCENTE"].ToString());
					string NOMBRE = row["NOMBRE"].ToString();
					string APPATERNO = row["APPATERNO"].ToString();
					string APMATERNO = row["APMATERNO"].ToString();
					string DIRECCION = row["DIRECCION"].ToString();
					string TELEFONOCELULAR = row["TELEFONOCELULAR"].ToString();
					string TELEFONOFIJO = row["TELEFONOFIJO"].ToString();
					string EMAIL = row["EMAIL"].ToString();
					int IIDSEXO = int.Parse(row["IIDSEXO"].ToString());
					DateTime FECHACONTRATO = DateTime.Parse(row["FECHACONTRATO"].ToString());
					//byte[] FOTO = Convert.FromBase64String(row["FOTO"].ToString());
                    byte[] FOTO = Encoding.ASCII.GetBytes(row["FOTO"].ToString());

                    int IIDMODALIDADCONTRATO = int.Parse(row["IIDMODALIDADCONTRATO"].ToString());
					int BHABILITADO = int.Parse(row["BHABILITADO"].ToString());
					string IIDTIPOUSUARIO = row["IIDTIPOUSUARIO"].ToString();
					int bTieneUsuario = int.Parse(row["bTieneUsuario"].ToString());
					Docente periodo;
					periodo = new Docente
					{
						IID = IID,
						NOMBRE = NOMBRE,
						APPATERNO = APPATERNO,
						APMATERNO = APMATERNO,
						DIRECCION = DIRECCION,
						TELEFONOCELULAR = TELEFONOCELULAR,
						TELEFONOFIJO = TELEFONOFIJO,
						EMAIL = EMAIL,
						IIDSEXO = IIDSEXO,
						FECHACONTRATO = FECHACONTRATO,
						FOTO = FOTO,
						IIDMODALIDADCONTRATO = IIDMODALIDADCONTRATO,
						BHABILITADO = BHABILITADO,
						IIDTIPOUSUARIO = IIDTIPOUSUARIO,
					 bTieneUsuario = bTieneUsuario
					};
					docentes.Add(periodo);
				}
				return new JsonResult { Data = docentes, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
			}
			catch (Exception ex)
			{
				return new JsonResult { Data = ex.Message, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
			}
		}


        public JsonResult guardarDatos(Docente docente)
        {
            try
            {
                DBAcceso db = new DBAcceso();
				//docente.FOTO = Convert.FromBase64String(docente.FOTOSTRING);
                docente.FECHACONTRATO = DateTime.Parse(docente.FECHACONTRATOSTRING);
                int resultado = (docente.IIDDOCENTE == 0) ? db.insertarDocente(docente) : db.actualizarDocente(docente);
                return new JsonResult { Data = resultado, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            catch (Exception ex)
            {
                return new JsonResult { Data = ex.Message, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }

        public JsonResult recuperarDocente(int id)
        {
            try
            {
                List<Docente> docentes = new List<Docente>();
                DBAcceso db = new DBAcceso();
                DataTable dt = db.obtenerDocentesId(id);
                foreach (DataRow row in dt.Rows)
                {
                    int IID = int.Parse(row["IID"].ToString());
                    int IIDDOCENTE = int.Parse(row["IIDDOCENTE"].ToString());
                    string NOMBRE = row["NOMBRE"].ToString();
                    string APPATERNO = row["APPATERNO"].ToString();
                    string APMATERNO = row["APMATERNO"].ToString();
                    string DIRECCION = row["DIRECCION"].ToString();
                    string TELEFONOCELULAR = row["TELEFONOCELULAR"].ToString();
                    string TELEFONOFIJO = row["TELEFONOFIJO"].ToString();
                    string EMAIL = row["EMAIL"].ToString();
                    int IIDSEXO = int.Parse(row["IIDSEXO"].ToString());
                    DateTime FECHACONTRATO = DateTime.Parse(row["FECHACONTRATO"].ToString());
                    byte[] FOTO = Encoding.ASCII.GetBytes(row["FOTO"].ToString());
                    int IIDMODALIDADCONTRATO = int.Parse(row["IIDMODALIDADCONTRATO"].ToString());
                    int BHABILITADO = int.Parse(row["BHABILITADO"].ToString());
                    string IIDTIPOUSUARIO = row["IIDTIPOUSUARIO"].ToString();
                    int bTieneUsuario = int.Parse(row["bTieneUsuario"].ToString());
                    Docente periodo;
                    periodo = new Docente
                    {
                        IID = IID,
                        IIDDOCENTE = IIDDOCENTE,
                        NOMBRE = NOMBRE,
                        APPATERNO = APPATERNO,
                        APMATERNO = APMATERNO,
                        DIRECCION = DIRECCION,
                        TELEFONOCELULAR = TELEFONOCELULAR,
                        TELEFONOFIJO = TELEFONOFIJO,
                        EMAIL = EMAIL,
                        IIDSEXO = IIDSEXO,
                        FECHACONTRATO = FECHACONTRATO,
                        FOTO = FOTO,
                        IIDMODALIDADCONTRATO = IIDMODALIDADCONTRATO,
                        BHABILITADO = BHABILITADO,
                        IIDTIPOUSUARIO = IIDTIPOUSUARIO,
                        bTieneUsuario = bTieneUsuario
                    };
                    docentes.Add(periodo);
                }
                return new JsonResult { Data = docentes, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            catch (Exception ex)
            {
                return new JsonResult { Data = ex.Message, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }


        public JsonResult eliminarDocente(int id)
        {
            try
            {
                List<Docente> docentes = new List<Docente>();
                DBAcceso db = new DBAcceso();
                int resultado = db.eliminarDocente(id);
                return new JsonResult { Data = resultado, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            catch (Exception ex)
            {
                return new JsonResult { Data = ex.Message, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }
    }
}
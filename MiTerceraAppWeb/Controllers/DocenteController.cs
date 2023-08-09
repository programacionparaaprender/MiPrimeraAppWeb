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
				List<DocenteModels> docentes = new List<DocenteModels>();
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
					DocenteModels periodo;
					periodo = new DocenteModels
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
				List<DocenteModels> docentes = new List<DocenteModels>();
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
					DocenteModels periodo;
					periodo = new DocenteModels
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
                Miconexion3DataContext bd = new Miconexion3DataContext();
                int nveces = 0;
                nveces = bd.Docente.Where(p => p.NOMBRE.Equals(docente.NOMBRE) 
				&& p.APPATERNO.Equals(docente.APPATERNO) 
				&& p.APMATERNO.Equals(docente.APMATERNO)
				&& p.BHABILITADO == 1).Count();

                DBAcceso db = new DBAcceso();
				//docente.FOTO = Convert.FromBase64String(docente.FOTOSTRING);
                //docente.FECHACONTRATO = DateTime.Parse(docente.FECHACONTRATOSTRING);
				int resultado = 0;
				if (docente.IIDDOCENTE == 0)
				{
					if(nveces == 0)
					{
                        //resultado = db.insertarDocente(docente);
                        docente.IIDTIPOUSUARIO = 'D';
                        bd.Docente.InsertOnSubmit(docente);
                        bd.SubmitChanges();
                        resultado = 1;
                    } else
					{
						resultado = -1;
					}
					
                }
                else
				{
                    //resultado = db.actualizarDocente(docente);
                    Docente update = bd.Docente.Where(p => p.IIDDOCENTE.Equals(docente.IIDDOCENTE)).First();
                    update.NOMBRE = docente.NOMBRE;
                    update.APPATERNO = docente.APPATERNO;
                    update.APMATERNO = docente.APMATERNO;
                    update.FECHACONTRATO = docente.FECHACONTRATO;
                    update.IIDSEXO = docente.IIDSEXO;
                    update.TELEFONOCELULAR = docente.TELEFONOCELULAR;
                    update.TELEFONOFIJO = docente.TELEFONOFIJO;
                    update.EMAIL = docente.EMAIL;
                    update.DIRECCION = docente.DIRECCION;
                    update.BHABILITADO = docente.BHABILITADO;
                    update.IIDTIPOUSUARIO = docente.IIDTIPOUSUARIO;
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

        public JsonResult recuperarDocente(int id)
        {
            try
            {
                List<DocenteModels> docentes = new List<DocenteModels>();
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
                    DocenteModels periodo;
                    periodo = new DocenteModels
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
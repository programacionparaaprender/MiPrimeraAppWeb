using DConexionBase3;
using MiTerceraAppWeb.Models;
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
		public FileContentResult getImage(byte[] biteIMG)
		{
			//var owin = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
			//var result = owin.FindByEmail<ApplicationUser, string>(User.Identity.Name);
			MemoryStream m = new MemoryStream(biteIMG);
			Image image = Image.FromStream(m);
			m = new MemoryStream();
			image.Save(m, ImageFormat.Png);
			m.Position = 0;
			return new FileContentResult(biteIMG, "image/png");
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
	}
}
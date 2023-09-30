using MiTerceraAppWeb.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MiTerceraAppWeb.Controllers
{
    [Seguridad]
    public class PaginaPrincipalController : Controller
    {
        // GET: PaginaPrincipal
        public ActionResult Index()
        {
            int idusuario = (int)Session["usuarioid"];
            using (Miconexion3DataContext bd = new Miconexion3DataContext())
            {
                string nombreCompleto = "";
                Usuario usu = bd.Usuario.Where(p => p.IIDUSUARIO == idusuario).First();
                if (usu.TIPOUSUARIO.Equals('D'))
                {
                    Docente odocente = bd.Docente.Where(p => p.IIDDOCENTE.Equals(usu.IID)).First();
                    nombreCompleto = odocente.NOMBRE + " " + odocente.APPATERNO + " " + odocente.APMATERNO;
                    ViewBag.nombreCompleto = nombreCompleto;
                }
                else
                {
                    Alumno oalumno = bd.Alumno.Where(p => p.IIDALUMNO.Equals(usu.IID)).First();
                    nombreCompleto = oalumno.NOMBRE + " " + oalumno.APPATERNO + " " + oalumno.APMATERNO;
                    ViewBag.nombreCompleto = nombreCompleto;
                }
            }
            return View();
        }
    }
}
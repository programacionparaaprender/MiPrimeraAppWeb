using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MiTerceraAppWeb.Controllers
{
    public class UsuarioController : Controller
    {
        // GET: Usuario
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult listarRol()
        {
            Miconexion3DataContext bd = new Miconexion3DataContext();
            var lista = bd.Rol.Where(p => p.BHABILITADO.Equals(1)).
                Select(p => new
                {
                    IID = p.IIDROL,
                    p.NOMBRE,
                    p.DESCRIPCION
                }).ToList();
            return Json(lista, JsonRequestBehavior.AllowGet);
        }
    }
}
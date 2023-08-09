using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MiTerceraAppWeb.Controllers
{
    public class RolPaginaController : Controller
    {
        // GET: RolPagina
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
                    p.IIDROL,
                    p.NOMBRE,
                    p.DESCRIPCION
                }).ToList();
            return Json(lista, JsonRequestBehavior.AllowGet);
        }

        public JsonResult listarPaginas()
        {
            Miconexion3DataContext bd = new Miconexion3DataContext();
            var lista = bd.Pagina.Where(p => p.BHABILITADO.Equals(1)).
                Select(p => new
                {
                    p.IIDPAGINA,
                    p.MENSAJE,
                    p.BHABILITADO
                }).ToList();
            return Json(lista, JsonRequestBehavior.AllowGet);
        }

        public JsonResult obtenerRol(int idRol)
        {
            Miconexion3DataContext bd = new Miconexion3DataContext();
            var lista = bd.Rol.Where(p => p.IIDROL == idRol && p.BHABILITADO.Equals(1)).
                Select(p => new
                {
                    p.IIDROL,
                    p.NOMBRE,
                    p.DESCRIPCION
                }).ToList();
            return Json(lista, JsonRequestBehavior.AllowGet);
        }

        public JsonResult guardarDatos(Rol rol)
        {
            try
            {
                Miconexion3DataContext bd = new Miconexion3DataContext();
                int IID = rol.IIDROL;
                int nveces = 0;
                nveces = bd.Rol.Where(p => p.NOMBRE.Equals(rol.NOMBRE)).Count();
                int resultado = 0;
                if (IID == 0)
                {
                    if(nveces == 0)
                    {
                        bd.Rol.InsertOnSubmit(rol);
                        bd.SubmitChanges();
                        resultado = 1;
                    }else
                    {
                        resultado = -1;
                    }
                    
                }
                else
                {
                    Rol update = bd.Rol.Where(p => p.IIDROL.Equals(IID)).First();
                    update.NOMBRE = rol.NOMBRE;
                    update.DESCRIPCION = rol.DESCRIPCION;
                    bd.SubmitChanges();
                    resultado = 1;
                }
                
                
                return Json(resultado, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult recuperarInformacion(int id)
        {
            Miconexion3DataContext bd = new Miconexion3DataContext();
            var lista = bd.Rol.Where(p => p.IIDROL.Equals(id) && p.BHABILITADO.Equals(1)).
                Select(p => new
                {
                    p.IIDROL,
                    p.NOMBRE,
                    p.DESCRIPCION
                }).ToList();
            return Json(lista, JsonRequestBehavior.AllowGet);
    
        }

        public JsonResult eliminarInformacion(int id)
        {
            try
            {
                int resultado = 0;
                Miconexion3DataContext bd = new Miconexion3DataContext();
                Rol update = bd.Rol.Where(p => p.IIDROL.Equals(id) && p.BHABILITADO.Equals(1)).First();
                update.BHABILITADO = 0;
                bd.SubmitChanges();
                resultado = 1;
                return new JsonResult { Data = resultado, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            catch (Exception ex)
            {
                return new JsonResult { Data = ex.Message, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }
    }
}
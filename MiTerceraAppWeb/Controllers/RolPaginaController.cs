using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
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
                    IID= p.IIDROL,
                    p.NOMBRE,
                    p.DESCRIPCION
                }).ToList();
            return Json(lista, JsonRequestBehavior.AllowGet);
        }

        public JsonResult listarRolPaginas(int id)
        {
            Miconexion3DataContext bd = new Miconexion3DataContext();
            var lista = (from paginas in bd.Pagina
                         select new
                         {
                             IID = paginas.IIDPAGINA,
                             paginas.IIDPAGINA,
                             paginas.MENSAJE,
                             BHABILITADO = bd.RolPagina.Where(p=> p.IIDPAGINA == paginas.IIDPAGINA && p.IIDROL == id && p.BHABILITADO == 1).Count() > 0
                         }).ToList();
            return Json(lista, JsonRequestBehavior.AllowGet);
        }

        public JsonResult listarPaginas()
        {
            Miconexion3DataContext bd = new Miconexion3DataContext();
            var lista = bd.Pagina.Where(p => p.BHABILITADO.Equals(1)).
                Select(p => new
                {
                    IID= p.IIDPAGINA,
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

        public JsonResult guardarDatos(Rol rol, string valorAEnviar)
        {
            try
            {
                Miconexion3DataContext bd = new Miconexion3DataContext();
                int IID = rol.IIDROL;
                int nveces = 0;
                nveces = bd.Rol.Where(p => p.NOMBRE.Equals(rol.NOMBRE)).Count();
                int resultado = 0;
                using (var transaccion = new TransactionScope())
                {
                    if (IID == 0)
                    {
                        if(nveces == 0)
                        {
                            bd.Rol.InsertOnSubmit(rol);
                            string[] valores = valorAEnviar.Split('$');
                            if (valorAEnviar.Length > 0)
                            {
                                foreach (string valor in valores)
                                {
                                    RolPagina oRolPagina = new RolPagina();
                                    oRolPagina.IIDROL = rol.IIDROL;
                                    oRolPagina.IIDPAGINA = int.Parse(valor);
                                    oRolPagina.BHABILITADO = 1;
                                    bd.RolPagina.InsertOnSubmit(oRolPagina);
                                }
                            }
                            bd.SubmitChanges();
                            resultado = 1;
                            transaccion.Complete();
                        }
                        else
                        {
                            resultado = -1;
                        }
                    
                    }
                    else
                    {
                    
                            Rol update = bd.Rol.Where(p => p.IIDROL.Equals(IID)).First();
                            update.NOMBRE = rol.NOMBRE;
                            update.DESCRIPCION = rol.DESCRIPCION;
                            var lista = bd.RolPagina.Where(p => p.IIDROL == rol.IIDROL).ToList();
                            foreach (RolPagina rolpagina in lista)
                            {
                                rolpagina.BHABILITADO = 0;
                            }
                            string[] valores = valorAEnviar.Split('$');
                            foreach (string valor in valores)
                            {
                            int cantidad = bd.RolPagina.Where(p => p.IIDROL == rol.IIDROL && p.IIDPAGINA == int.Parse(valor)).Count();
                            if (cantidad == 0)
                            {
                                RolPagina oRolPagina = new RolPagina();
                                oRolPagina.IIDROL = rol.IIDROL;
                                oRolPagina.IIDPAGINA = int.Parse(valor);
                                oRolPagina.BHABILITADO = 1;
                                bd.RolPagina.InsertOnSubmit(oRolPagina);
                            }
                            else
                            {
                                RolPagina rolpagina = bd.RolPagina.Where(p => p.IIDROL == rol.IIDROL && p.IIDPAGINA == int.Parse(valor)).First();
                                rolpagina.BHABILITADO = 1;
                            }
                            }

                            bd.SubmitChanges();
                            resultado = 1;
                            transaccion.Complete();
                    
                    }

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
                    IID = p.IIDROL,
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
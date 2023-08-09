using MiTerceraAppWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MiTerceraAppWeb.Controllers
{
    public class GradoSeccionController : Controller
    {
        // GET: GradoSeccion
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult obtenerGradoSeccion()
        {
            try
            {
                Miconexion3DataContext bd = new Miconexion3DataContext();
                var lista = (from gradosec in bd.GradoSeccion
                             join sec in bd.Seccion
                             on gradosec.IIDSECCION equals sec.IIDSECCION
                             join grad in bd.Grado
                             on gradosec.IIDGRADO equals grad.IIDGRADO
                             where gradosec.BHABILITADO.Equals(1)
                             select new
                             {
                                 gradosec.IID,
                                 NOMBREGRADO = grad.NOMBRE,
                                 NOMBRESECCION = sec.NOMBRE
                             }).ToList();
                return Json(lista, JsonRequestBehavior.AllowGet);
            }
            catch(Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }


        public JsonResult recuperarInformacion(int id)
        {
            try
            {
                Miconexion3DataContext bd = new Miconexion3DataContext();
                var consulta = (from gradosec in bd.GradoSeccion
                                where gradosec.BHABILITADO.Equals(1)
                                && gradosec.IID.Equals(id)
                                select new
                                {
                                    gradosec.IID,
                                    gradosec.IIDSECCION,
                                    gradosec.IIDGRADO
                                }).ToList(); 
                return Json(consulta, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult listarSeccion()
        {
            try
            {
                Miconexion3DataContext bd = new Miconexion3DataContext();
                var lista = bd.Seccion.Where(p => p.BHABILITADO.Equals(1)).Select(p=>new {
                    IID=p.IIDSECCION,
                    p.NOMBRE
                });
                return Json(lista, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult listarGrado()
        {
            try
            {
                Miconexion3DataContext bd = new Miconexion3DataContext();
                var lista = bd.Grado.Where(p => p.BHABILITADO.Equals(1)).Select(p => new
                {
                    IID = p.IIDGRADO,
                    p.NOMBRE
                });
                return Json(lista, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult guardarDatos(GradoSeccion gradoSeccion)
        {
            try
            {
                Miconexion3DataContext bd = new Miconexion3DataContext();
                int IID = gradoSeccion.IID;
                int nveces = 0;
                nveces = bd.GradoSeccion.Where(p => p.IIDGRADO.Equals(gradoSeccion.IIDGRADO) 
                && p.IIDSECCION.Equals(gradoSeccion.IIDSECCION)
                && p.BHABILITADO == 1).Count();
                int resultado = 0;
                if (IID == 0)
                {
                    if (nveces == 0)
                    {
                        bd.GradoSeccion.InsertOnSubmit(gradoSeccion);
                        bd.SubmitChanges();
                        resultado = 1;
                    }else
                    {
                        resultado = -1;
                    }
                    
                }
                else
                {
                    GradoSeccion update = bd.GradoSeccion.Where(p => p.IID.Equals(IID)).First();
                    update.IIDGRADO = gradoSeccion.IIDGRADO;
                    update.IIDSECCION = gradoSeccion.IIDSECCION;
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
        public JsonResult eliminarGradoSeccion(int id)
        {
            try
            {
                Miconexion3DataContext bd = new Miconexion3DataContext();
                GradoSeccion update = bd.GradoSeccion.Where(p => p.IID.Equals(id)).First();
                update.BHABILITADO = 0;
                bd.SubmitChanges();
                int resultado = 1;
                return Json(resultado, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }
    }
}
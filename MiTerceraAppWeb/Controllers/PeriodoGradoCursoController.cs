using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MiTerceraAppWeb.Controllers
{
    public class PeriodoGradoCursoController : Controller
    {
        // GET: PeriodoGradoCurso
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult listarPeriodoGradoCurso()
        {
            try
            {
                Miconexion3DataContext bd = new Miconexion3DataContext();
                var lista = from pgc in bd.PeriodoGradoCurso
                             join per in bd.Periodo
                             on pgc.IIDPERIODO equals per.IIDPERIODO
                             join grad in bd.Grado
                             on pgc.IIDGRADO equals grad.IIDGRADO
                             join cur in bd.Curso
                             on pgc.IIDCURSO equals cur.IIDCURSO
                             where pgc.BHABILITADO.Equals(1)
                             select new
                             {
                                 pgc.IID,
                                 NOMBREPERIODO = per.NOMBRE,
                                 NOMBREGRADO = grad.NOMBRE,
                                 NOMBRECURSO = cur.NOMBRE
                             };
                return Json(lista, JsonRequestBehavior.AllowGet);
            }
            catch(Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult recuperarPeriodoGradoCurso(int id)
        {
            try
            {
                Miconexion3DataContext bd = new Miconexion3DataContext();
                var lista = bd.PeriodoGradoCurso.Where(p=> p.IID.Equals(id)).Select(p=>new {
                    p.IID,
                    p.IIDPERIODO,
                    p.IIDGRADO,
                    p.IIDCURSO
                }).ToList();
                return Json(lista, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult listarPeriodo()
        {
            Miconexion3DataContext bd = new Miconexion3DataContext();
            var lista = bd.Periodo.Where(p => p.BHABILITADO.Equals(1)).Select(p => new
            {
                IID=p.IIDPERIODO,
                p.IIDPERIODO,
                p.NOMBRE
            });
            return Json(lista, JsonRequestBehavior.AllowGet);
        }

        public JsonResult listarGrado()
        {
            Miconexion3DataContext bd = new Miconexion3DataContext();
            var lista = bd.Grado.Where(p => p.BHABILITADO.Equals(1)).Select(p => new
            {
                IID=p.IIDGRADO,
                p.IIDGRADO,
                p.NOMBRE
            });
            return Json(lista, JsonRequestBehavior.AllowGet);
        }

        public JsonResult listarCurso()
        {
            Miconexion3DataContext bd = new Miconexion3DataContext();
            var lista = bd.Curso.Where(p => p.BHABILITADO.Equals(1)).Select(p => new
            {
                IID = p.IIDCURSO,
                p.IIDCURSO,
                p.NOMBRE
            });
            return Json(lista, JsonRequestBehavior.AllowGet);
        }

        public JsonResult eliminarPeriodoGradoCurso(int id)
        {
            try
            {
                Miconexion3DataContext bd = new Miconexion3DataContext();
                PeriodoGradoCurso update = bd.PeriodoGradoCurso.Where(p => p.IID.Equals(id)).First();
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

        public JsonResult guardarDatos(PeriodoGradoCurso periodoGradoCurso)
        {
            try
            {
                Miconexion3DataContext bd = new Miconexion3DataContext();
                int nveces = 0;
                nveces = bd.PeriodoGradoCurso.Where(p =>
                p.IIDPERIODO.Equals(periodoGradoCurso.IIDPERIODO)
                && p.IIDGRADO.Equals(periodoGradoCurso.IIDGRADO)
                && p.IIDCURSO.Equals(periodoGradoCurso.IIDCURSO)
                && p.BHABILITADO == 1).Count();
                int IID = periodoGradoCurso.IID;
                int resultado = 0;
                if (IID == 0)
                {
                    if (nveces == 0)
                    {
                        bd.PeriodoGradoCurso.InsertOnSubmit(periodoGradoCurso);
                        bd.SubmitChanges();
                        resultado = 1;
                    }
                    else
                    {
                        resultado = -1;
                    }
                }
                else
                {
                    PeriodoGradoCurso update = bd.PeriodoGradoCurso.Where(p => p.IID.Equals(IID)).First();
                    update.IIDPERIODO = periodoGradoCurso.IIDPERIODO;
                    update.IIDGRADO = periodoGradoCurso.IIDGRADO;
                    update.IIDCURSO = periodoGradoCurso.IIDCURSO;
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
    }
}
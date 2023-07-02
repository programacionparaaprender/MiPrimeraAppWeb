using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MiTerceraAppWeb.Controllers
{
    public class MatriculaController : Controller
    {
        // GET: Matricula
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult listar()
        {
            Miconexion3DataContext bd = new Miconexion3DataContext();
            var lista = from ma in bd.Matricula
                           join per in bd.Periodo
                           on ma.IIDPERIODO equals per.IIDPERIODO
                           join grad in bd.Grado
                           on ma.IIDGRADO equals grad.IIDGRADO
                           join sec in bd.Seccion 
                           on ma.IIDSECCION equals sec.IIDSECCION
                           join alumno in bd.Alumno
                           on ma.IIDALUMNO equals alumno.IIDALUMNO 
                            where ma.IIDMATRICULA.Equals(1)
                            select new
                           {
                               IID=ma.IIDMATRICULA,
                               NOMBREPERIODO=per.NOMBRE,
                               NOMBREGRADO=grad.NOMBRE,
                               NOMBRESECCION=sec.NOMBRE,
                               NOMBREALUMNO=alumno.NOMBRE + " " + alumno.APPATERNO + " " + alumno.APMATERNO
                           };
            return Json(lista, JsonRequestBehavior.AllowGet);
        }

        public JsonResult recuperarInformacion(int id)
        {
            Miconexion3DataContext bd = new Miconexion3DataContext();
            var lista = from gs in bd.Matricula.Where(p => p.IIDMATRICULA.Equals(1)).Select(p => new {
                IID = p.IIDMATRICULA,
                p.IIDPERIODO,
                p.IIDGRADO,
                p.IIDSECCION,
                p.IIDALUMNO
            }).ToList();
            return Json(lista, JsonRequestBehavior.AllowGet);
        }

        public JsonResult listarGradoSeccion()
        {
            Miconexion3DataContext bd = new Miconexion3DataContext();
            var lista = from gs in bd.GradoSeccion
                        join grad in bd.Grado
                        on gs.IIDGRADO equals grad.IIDGRADO
                        join seccion in bd.Seccion
                        on gs.IIDSECCION equals seccion.IIDSECCION
                        select new
                        {
                            gs.IID,
                            NOMBRE = grad.NOMBRE + " - " + seccion.NOMBRE
                        };
            return Json(lista, JsonRequestBehavior.AllowGet);
        }

        public JsonResult listarAlumnos()
        {
            Miconexion3DataContext bd = new Miconexion3DataContext();
            var lista = bd.Alumno.Where(p => p.BHABILITADO.Equals(1)).
                Select(p => new
                {
                    IID = p.IIDALUMNO,
                    NOMBRE = p.NOMBRE + " " + p.APPATERNO + " " + p.APMATERNO
                });
            return Json(lista, JsonRequestBehavior.AllowGet);
        }

        public JsonResult listarPeriodo()
        {
            Miconexion3DataContext bd = new Miconexion3DataContext();
            var lista = bd.Periodo.Where(p => p.BHABILITADO.Equals(1)).Select(p => new
            {
                IID = p.IIDPERIODO,
                p.IIDPERIODO,
                p.NOMBRE
            });
            return Json(lista, JsonRequestBehavior.AllowGet);
        }
    }
}
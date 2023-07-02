using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MiTerceraAppWeb.Controllers
{
    public class GradoSeccionAulaController : Controller
    {
        // GET: GradoSeccionAula
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult listar()
        {
            Miconexion3DataContext bd = new Miconexion3DataContext();
            var lista = from tabla in bd.GradoSeccionAula
                        join periodo in bd.Periodo
                        on tabla.IIDPERIODO equals periodo.IIDPERIODO
                        join gradoSeccion in bd.GradoSeccion
                        on tabla.IIDGRADOSECCION equals gradoSeccion.IID
                        join docente in bd.Docente
                        on tabla.IIDDOCENTE equals docente.IIDDOCENTE
                        join curso in bd.Curso
                        on tabla.IIDCURSO equals curso.IIDCURSO
                        join grado in bd.Grado
                        on gradoSeccion.IIDGRADO equals grado.IIDGRADO
                        where tabla.BHABILITADO.Equals(1)
                        select new
                        {
                            tabla.IID,
                            NOMBREPERIODO=periodo.NOMBRE,
                            NOMBRECURSO=curso.NOMBRE,
                            NOMBREDOCENTE=docente.NOMBRE,
                            NOMBREGRADO=grado.NOMBRE
                        };
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

        public JsonResult listarGrado()
        {
            Miconexion3DataContext bd = new Miconexion3DataContext();
            var lista = bd.Grado.Where(p => p.BHABILITADO.Equals(1)).Select(p => new
            {
                IID = p.IIDGRADO,
                p.IIDGRADO,
                p.NOMBRE
            });
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

        public JsonResult listarAulas()
        {
            Miconexion3DataContext bd = new Miconexion3DataContext();
            var lista = bd.Aula.Where(p => p.BHABILITADO.Equals(1)).
                Select(p => new
                {
                    IID = p.IIDAULA,
                    p.NOMBRE
                });
            return Json(lista, JsonRequestBehavior.AllowGet);
        }

        public JsonResult listarDocentes()
        {
            Miconexion3DataContext bd = new Miconexion3DataContext();
            var lista = bd.Docente.Where(p => p.BHABILITADO.Equals(1)).
                Select(p => new
                {
                    IID = p.IIDDOCENTE,
                    p.NOMBRE
                });
            return Json(lista, JsonRequestBehavior.AllowGet);
        }
        public JsonResult listarCursos(int IIDPERIODO, int IIDGRADOSECCION)
        {
            Miconexion3DataContext bd = new Miconexion3DataContext();
            int iidgrado = (int)bd.GradoSeccion.Where(p => p.IID.Equals(IIDGRADOSECCION)).First().IIDGRADO;
            var lista = from pgc in bd.PeriodoGradoCurso
                        join curso in bd.Curso
                        on pgc.IIDCURSO equals curso.IIDCURSO
                        join periodo in bd.Periodo
                        on pgc.IIDPERIODO equals periodo.IIDPERIODO
                        where pgc.BHABILITADO.Equals(1)
                        && pgc.IIDPERIODO.Equals(IIDPERIODO)
                        && pgc.IIDGRADO.Equals(iidgrado)
                        select new
                        {
                            IID=pgc.IIDCURSO,
                            curso.NOMBRE
                        };
            return Json(lista, JsonRequestBehavior.AllowGet);
        }

        public JsonResult eliminarGradoSeccionAula(int id)
        {
            try
            {
                Miconexion3DataContext bd = new Miconexion3DataContext();
                GradoSeccionAula update = bd.GradoSeccionAula.Where(p => p.IID.Equals(id)).First();
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

        public JsonResult recuperarInformacion(int id)
        {
            Miconexion3DataContext bd = new Miconexion3DataContext();
            var lista = bd.GradoSeccionAula.Where(p => p.IID.Equals(id)).Select(p=> new{
                p.IID,
                p.IIDPERIODO,
                p.IIDGRADOSECCION,
                p.IIDCURSO,
                p.IIDAULA,
                p.IIDDOCENTE
            });
            return Json(lista, JsonRequestBehavior.AllowGet);
        }

        public JsonResult guardarDatos(GradoSeccionAula gradoSeccionAula)
        {
            try
            {
                Miconexion3DataContext bd = new Miconexion3DataContext();
                int IID = gradoSeccionAula.IID;
                if (IID == 0)
                {
                    bd.GradoSeccionAula.InsertOnSubmit(gradoSeccionAula);
                    bd.SubmitChanges();
                }
                else
                {
                    GradoSeccionAula update = bd.GradoSeccionAula.Where(p => p.IID.Equals(IID)).First();

                    update.IIDPERIODO = gradoSeccionAula.IIDPERIODO;
                    update.IIDGRADOSECCION = gradoSeccionAula.IIDGRADOSECCION;
                    update.IIDCURSO = gradoSeccionAula.IIDCURSO;
                    update.IIDAULA = gradoSeccionAula.IIDAULA;
                    update.IIDDOCENTE = gradoSeccionAula.IIDDOCENTE;

                    bd.SubmitChanges();
                }
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
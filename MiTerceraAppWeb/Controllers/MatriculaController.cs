using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
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
                            where ma.BHABILITADO.Equals(1)
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
            var lista = from gra in bd.GradoSeccion
                        join ma in bd.Matricula
                        on gra.IIDGRADO equals ma.IIDGRADO
                        where gra.IIDSECCION.Equals(ma.IIDSECCION) && ma.IIDMATRICULA.Equals(id)
                        select new
                        {
                            IID = ma.IIDMATRICULA,
                            ma.IIDPERIODO,
                            IIDGRADOSECCION = gra.IID,
                            ma.IIDALUMNO
                        };
            
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

        public JsonResult guardarDatos(Matricula matricula, int IIDGRADOSECCION)
        {
            try
            {
                Miconexion3DataContext bd = new Miconexion3DataContext();
                GradoSeccion grad = bd.GradoSeccion.Where(p => p.IID.Equals(IIDGRADOSECCION)).First();
                int IID = matricula.IIDMATRICULA;
                matricula.IIDGRADO = grad.IIDGRADO;
                matricula.IIDSECCION = grad.IIDSECCION;
                
                using (var transaccion=new TransactionScope())
                {
                    if (IID == 0)
                    {
                        bd.Matricula.InsertOnSubmit(matricula);
                        bd.SubmitChanges();
                        int IIDMATRICULA = matricula.IIDMATRICULA;

                        var lista = bd.PeriodoGradoCurso.Where(p => p.IIDPERIODO.Equals(matricula.IIDPERIODO) && p.IIDGRADO.Equals(matricula.IIDGRADO)).Select(p=>p.IIDCURSO);

                        foreach (var item in lista)
                        {
                            DetalleMatricula dm = new DetalleMatricula();
                            //dm.Matricula = matricula;
                            dm.IIDMATRICULA = IIDMATRICULA;
                            dm.IIDCURSO = (int)item;
                            dm.NOTA1 = 0;
                            dm.NOTA2 = 0;
                            dm.NOTA3 = 0;
                            dm.NOTA4 = 0;
                            dm.PROMEDIO = 0;
                            dm.bhabilitado = 1;
                            bd.DetalleMatricula.InsertOnSubmit(dm);
                        }
                        bd.SubmitChanges();
                    }
                    else
                    {
                        Matricula update = bd.Matricula.Where(p => p.IIDMATRICULA.Equals(IID)).First();
                        update.IIDPERIODO = matricula.IIDPERIODO;
                        update.IIDALUMNO = matricula.IIDALUMNO;
                        bd.SubmitChanges();
                    }
                    transaccion.Complete();
                }
                int resultado = 1;
                return Json(resultado, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult eliminarMatricula(int id)
        {
            try
            {
                Miconexion3DataContext bd = new Miconexion3DataContext();
                Matricula update = bd.Matricula.Where(p => p.IIDMATRICULA.Equals(id)).First();
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
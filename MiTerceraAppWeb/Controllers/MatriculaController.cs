using DConexionBase3;
using MiTerceraAppWeb.Filters;
using Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Mvc;

namespace MiTerceraAppWeb.Controllers
{
    [Seguridad]
    public class MatriculaController : Controller
    {
        public JsonResult listar()
        {
            Miconexion3DataContext bd = new Miconexion3DataContext();
            var lista = from matricula in bd.Matricula
                        join periodo in bd.Periodo
                        on matricula.IIDPERIODO equals periodo.IIDPERIODO
                        join grado in bd.Grado
                        on matricula.IIDGRADO equals grado.IIDGRADO
                        join seccion in bd.Seccion
                        on matricula.IIDSECCION equals seccion.IIDSECCION
                        join alumno in bd.Alumno
                        on matricula.IIDALUMNO equals alumno.IIDALUMNO
                        where matricula.BHABILITADO == 1
                        select new
                        {
                            IID = matricula.IIDMATRICULA,
                            NOMBREPERIODO = periodo.NOMBRE,
                            NOMBREGRADO = grado.NOMBRE,
                            NOMBRESECCION = seccion.NOMBRE,
                            NOMBREALUMNO = alumno.NOMBRE + " " + alumno.APPATERNO + " " + alumno.APMATERNO
                        };
            return Json(lista, JsonRequestBehavior.AllowGet);

        }
        public JsonResult listarCursosPorPeriodoYGrado(int iidPeriodo, int iidGradoSeccion)
        {
            Miconexion3DataContext bd = new Miconexion3DataContext();
            int iidGrado = (int)bd.GradoSeccion.Where(p => p.IID == iidGradoSeccion).First().IIDGRADO;
            var lista = (from periodoGradoCurso in bd.PeriodoGradoCurso
                         join curso in bd.Curso
                         on periodoGradoCurso.IIDCURSO equals curso.IIDCURSO
                         where periodoGradoCurso.BHABILITADO == 1
                         && periodoGradoCurso.IIDPERIODO == iidPeriodo
                         && periodoGradoCurso.IIDGRADO == iidGrado
                         select new CursoCLS
                         {
                             IIDCURSO = curso.IIDCURSO,
                             NOMBRE = curso.NOMBRE
                         }).ToList();
            return Json(lista, JsonRequestBehavior.AllowGet);





        }


        // GET: Matricula
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult listarPeriodos()
        {
            Miconexion3DataContext bd = new Miconexion3DataContext();

            var lista = bd.Periodo.Where(p => p.BHABILITADO.Equals(1))
                .Select(p => new
                {
                    IID = p.IIDPERIODO,
                    p.NOMBRE
                });
            return Json(lista, JsonRequestBehavior.AllowGet);
        }

        public JsonResult listarGradoSeccion()
        {
            Miconexion3DataContext bd = new Miconexion3DataContext();
            //1 grado - A
            var lista = from gs in bd.GradoSeccion
                        join grado in bd.Grado
                        on gs.IIDGRADO equals grado.IIDGRADO
                        join seccion in bd.Seccion
                        on gs.IIDSECCION equals seccion.IIDSECCION
                        select new
                        {
                            //No es el mismo (iid con grado seccion)
                            IID = gs.IID,
                            NOMBRE = grado.NOMBRE + " - " + seccion.NOMBRE
                        };

            return Json(lista, JsonRequestBehavior.AllowGet);
        }

        public JsonResult listarAlumnos()
        {
            Miconexion3DataContext bd = new Miconexion3DataContext();

            var lista = bd.Alumno.Where(p => p.BHABILITADO.Equals(1))
                .Select(p => new
                {
                    IID = p.IIDALUMNO,
                    NOMBRE = p.NOMBRE + " " + p.APPATERNO + " " + p.APMATERNO
                });

            return Json(lista, JsonRequestBehavior.AllowGet);
        }


        public int guardarDatos(Matricula oMatricula, int IIDGRADOSECCION, string valorAEnviar, string valorADeshabilitar)
        {
            int nregistradosAfectados = 0;
            // nregistrosAfectados=-1;
            Miconexion3DataContext bd = new Miconexion3DataContext();
            int iidmatricula = oMatricula.IIDMATRICULA;
            GradoSeccion oGradoSeccion = bd.GradoSeccion.Where(p => p.IID.Equals(IIDGRADOSECCION)).First();
            int iidgrado = (int)oGradoSeccion.IIDGRADO;
            int iidseccion = (int)oGradoSeccion.IIDSECCION;
            oMatricula.IIDGRADO = iidgrado;
            oMatricula.IIDSECCION = iidseccion;
            oMatricula.FECHA = DateTime.Now;
            try
            {
                using (var transaccion = new TransactionScope())
                {
                    if (oMatricula.IIDMATRICULA.Equals(0))
                    {
                        int cantidad = bd.Matricula.Where(p => p.IIDALUMNO == oMatricula.IIDALUMNO
                         && p.IIDPERIODO == oMatricula.IIDPERIODO).Count();
                        if (cantidad >= 1)
                        {
                            return -1;
                        }

                        bd.Matricula.InsertOnSubmit(oMatricula);
                        bd.SubmitChanges();
                        int idMatriculaGenerada = oMatricula.IIDMATRICULA;
                        //var lista = bd.PeriodoGradoCurso.Where(p => p.IIDPERIODO.Equals(oMatricula.IIDPERIODO)
                        //  && p.IIDGRADO.Equals(iidgrado) && p.BHABILITADO.Equals(1)).Select(p => p.IIDCURSO);
                        if (valorAEnviar != "" && valorAEnviar != null)
                        {
                            string[] cursos = valorAEnviar.Split('$');
                            foreach (string curso in cursos)
                            {
                                DetalleMatricula dm = new DetalleMatricula();
                                dm.IIDMATRICULA = idMatriculaGenerada;
                                dm.IIDCURSO = int.Parse(curso);
                                dm.NOTA1 = 0;
                                dm.NOTA2 = 0;
                                dm.NOTA3 = 0;
                                dm.NOTA4 = 0;
                                dm.PROMEDIO = 0;
                                dm.bhabilitado = 1;
                                bd.DetalleMatricula.InsertOnSubmit(dm);
                            }
                        }
                        if (valorADeshabilitar != "" && valorADeshabilitar != null)
                        {

                            string[] cursos = valorADeshabilitar.Split('$');
                            foreach (string curso in cursos)
                            {
                                DetalleMatricula dm = new DetalleMatricula();
                                dm.IIDMATRICULA = idMatriculaGenerada;
                                dm.IIDCURSO = int.Parse(curso);
                                dm.NOTA1 = 0;
                                dm.NOTA2 = 0;
                                dm.NOTA3 = 0;
                                dm.NOTA4 = 0;
                                dm.PROMEDIO = 0;
                                dm.bhabilitado = 0;
                                bd.DetalleMatricula.InsertOnSubmit(dm);
                            }

                        }
                        bd.SubmitChanges();
                        transaccion.Complete();
                        nregistradosAfectados = 1;


                    }
                    else
                    {
                        int cantidad = bd.Matricula.Where(p => p.IIDALUMNO == oMatricula.IIDALUMNO
                       && p.IIDPERIODO == oMatricula.IIDPERIODO && p.IIDMATRICULA != oMatricula.IIDMATRICULA).Count();
                        if (cantidad >= 1)
                        {
                            return -1;
                        }

                        //Editar
                        Matricula oMatriculaObjeto = bd.Matricula.Where(p => p.IIDMATRICULA == oMatricula.IIDMATRICULA).First();
                        oMatriculaObjeto.IIDPERIODO = oMatricula.IIDPERIODO;
                        oMatriculaObjeto.IIDGRADO = iidgrado;
                        oMatriculaObjeto.IIDSECCION = iidseccion;
                        oMatriculaObjeto.IIDALUMNO = oMatricula.IIDALUMNO;
                        //detalle
                        var lista = bd.DetalleMatricula.Where(p => p.IIDMATRICULA == oMatricula.IIDMATRICULA);
                        foreach (DetalleMatricula odetalle in lista)
                        {
                            odetalle.bhabilitado = 0;
                        }
                        // 4$3$5  [4,3,5]
                        string[] valores = valorAEnviar.Split('$');
                        if (valorAEnviar != "")
                        {
                            int nVeces = 0;
                            for (int i = 0; i < valores.Length; i++)
                            {
                                nVeces = bd.DetalleMatricula.Where(p =>
                                p.IIDMATRICULA == oMatricula.IIDMATRICULA
                                && p.IIDCURSO == int.Parse(valores[i])).Count();
                                //Si es que existe
                                if (nVeces == 1)
                                {
                                    DetalleMatricula odet = bd.DetalleMatricula.Where(p =>
                                   p.IIDMATRICULA == oMatricula.IIDMATRICULA
                                   && p.IIDCURSO == int.Parse(valores[i])).First();
                                    odet.bhabilitado = 1;
                                }
                                //Si es que no existe
                                else
                                {
                                    DetalleMatricula dm = new DetalleMatricula();
                                    dm.IIDMATRICULA = oMatricula.IIDMATRICULA;
                                    dm.IIDCURSO = int.Parse(valores[i]);
                                    dm.NOTA1 = 0;
                                    dm.NOTA2 = 0;
                                    dm.NOTA3 = 0;
                                    dm.NOTA4 = 0;
                                    dm.PROMEDIO = 0;
                                    dm.bhabilitado = 1;
                                    bd.DetalleMatricula.InsertOnSubmit(dm);
                                }


                            }
                        }
                        bd.SubmitChanges();
                        transaccion.Complete();
                        nregistradosAfectados = 1;



                    }



                }

            }
            catch (Exception ex)
            {
                nregistradosAfectados = 0;

            }

            return nregistradosAfectados;

        }


        public int eliminar(int idMatricula)
        {
            int rpta = 0;
            Miconexion3DataContext bd = new Miconexion3DataContext();
            try
            {
                using (var transaccion = new TransactionScope())
                {
                    Matricula oMatricula = bd.Matricula.Where(p => p.IIDMATRICULA == idMatricula).First();
                    oMatricula.BHABILITADO = 0;

                    var listaDetalleMatricula = bd.DetalleMatricula.Where(p => p.IIDMATRICULA == idMatricula);

                    foreach (DetalleMatricula oDetalleMatricula in listaDetalleMatricula)
                    {
                        oDetalleMatricula.bhabilitado = 0;
                    }
                    bd.SubmitChanges();
                    transaccion.Complete();
                    rpta = 1;


                }

            }
            catch (Exception ex)
            {
                rpta = 0;
            }
            return rpta;

        }



        public JsonResult obtenerMatricula(int iidmatricula)
        {
            using (Miconexion3DataContext bd = new Miconexion3DataContext())
            {

                Matricula matricula = bd.Matricula.Where(p => p.IIDMATRICULA == iidmatricula).First();
                int idgrado = (int)matricula.IIDGRADO;
                int idseccion = (int)matricula.IIDSECCION;
                int iid = bd.GradoSeccion.Where(p => p.IIDGRADO == idgrado && p.IIDSECCION == idseccion).First().IID;

                var oMatricula = bd.Matricula.Where(p => p.IIDMATRICULA == iidmatricula).
                     Select(p => new
                     {
                         IIDMATRICULA = (int)p.IIDMATRICULA,
                         IIDPERIODO = (int)p.IIDPERIODO,
                         IIDSECCION = iid,
                         IIDALUMNO = (int)p.IIDALUMNO

                     }).First();

                return Json(oMatricula, JsonRequestBehavior.AllowGet);

            }

        }

        public JsonResult Cursos(int iidmatricula)
        {

            using (Miconexion3DataContext bd = new Miconexion3DataContext())
            {
                int iidgrado = (int)bd.Matricula.Where(p => p.IIDMATRICULA == iidmatricula).First().IIDGRADO;
                List<int?> lista = bd.PeriodoGradoCurso.Where(p => p.IIDGRADO == iidgrado).Select(p => p.IIDCURSO).ToList();

                var listaCurso = (from detalle in bd.DetalleMatricula
                                  join curso in bd.Curso
                                  on detalle.IIDCURSO equals curso.IIDCURSO
                                  where detalle.IIDMATRICULA == iidmatricula
                                  && lista.Contains(detalle.IIDCURSO)
                                  select new
                                  {
                                      detalle.IIDMATRICULA,
                                      curso.IIDCURSO,
                                      curso.NOMBRE,
                                      detalle.bhabilitado
                                  }).ToList();


                return Json(listaCurso, JsonRequestBehavior.AllowGet);
            }



        }


    }
}
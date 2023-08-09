﻿using DConexionBase3;
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

        public JsonResult obtenerMatricula(int iidmatricula)
        {
            Miconexion3DataContext bd = new Miconexion3DataContext();
            var lista = bd.Matricula.Where(p=>p.IIDMATRICULA == iidmatricula).
            Select(p=> new{
                IIDMATRICULA = (int)p.IIDMATRICULA,
                IIDPERIODO = (int)p.IIDPERIODO,
                IIDSECCION = (int)p.IIDSECCION,
                IIDALUMNO = (int)p.IIDALUMNO
            });
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
        public JsonResult Cursos(int iidmatricula)
        {
            Miconexion3DataContext bd = new Miconexion3DataContext();
            var lista = (from detalle in bd.DetalleMatricula
                         join curso in bd.Curso
                         on detalle.IIDCURSO equals curso.IIDCURSO
                         where detalle.IIDMATRICULA.Equals(iidmatricula)
                         select new {
                             detalle.IIDMATRICULA,
                             curso.IIDCURSO,
                             curso.NOMBRE,
                             detalle.bhabilitado
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

        public JsonResult guardarDatos(Matricula matricula, int IIDGRADOSECCION, string valorAEnviar)
        {
            try
            {
                Miconexion3DataContext bd = new Miconexion3DataContext();
                GradoSeccion grad = bd.GradoSeccion.Where(p => p.IID.Equals(IIDGRADOSECCION)).First();
                matricula.IIDGRADO = grad.IIDGRADO;
                matricula.IIDSECCION = grad.IIDSECCION;

                DBAcceso acceso = new DBAcceso();
                MatriculaModels matriculamodels = new MatriculaModels();
                matriculamodels.IIDGRADO = matricula.IIDGRADO;
                matriculamodels.IIDSECCION = matricula.IIDSECCION;
                matriculamodels.IIDPERIODO = matricula.IIDPERIODO;
                matriculamodels.IIDALUMNO = matricula.IIDALUMNO;
                int IID = matricula.IIDMATRICULA;
                int nveces = 0;
                nveces = bd.Matricula.Where(p => p.IIDALUMNO.Equals(matriculamodels.IIDALUMNO)
                && p.IIDPERIODO.Equals(matriculamodels.IIDPERIODO)
                && p.IIDGRADO.Equals(matriculamodels.IIDGRADO)
                && p.BHABILITADO == 1).Count();
                int resultado = 0;
                if (IID == 0)
                {
                    if (nveces == 0)
                    {
                        IID = acceso.insertarMatricula(matriculamodels);
                        var lista = acceso.obtenerPeriodoGradoCurso(matriculamodels);
                        foreach (DataRow item in lista.Rows)
                        {
                            DetalleMatriculaModels dm = new DetalleMatriculaModels();
                            dm.IIDMATRICULA = IID;
                            dm.IIDCURSO = int.Parse(item["IIDCURSO"].ToString());
                            dm.NOTA1 = 0;
                            dm.NOTA2 = 0;
                            dm.NOTA3 = 0;
                            dm.NOTA4 = 0;
                            dm.PROMEDIO = 0;
                            dm.bhabilitado = 1;
                            acceso.insertarDetalleMatricula(dm);
                        }
                        resultado = 1;
                    }
                    else
                    {
                        resultado = -1;
                    }
                    
                }
                else
                {
                    using (var transaccion = new TransactionScope())
                    {
                        //Editar
                        Matricula oMatricula = bd.Matricula.Where(p => p.IIDMATRICULA == matricula.IIDMATRICULA).First();
                        oMatricula.IIDPERIODO = matricula.IIDPERIODO;
                        oMatricula.IIDGRADO = matricula.IIDGRADO;
                        oMatricula.IIDSECCION = matricula.IIDSECCION;
                        oMatricula.IIDALUMNO = matricula.IIDALUMNO;
                        //detalle
                        var lista = bd.DetalleMatricula.Where(p => p.IIDMATRICULA == matricula.IIDMATRICULA);
                        foreach (DetalleMatricula odetalle in lista)
                        {
                            odetalle.bhabilitado = 0;
                        }
                        string[] valores = valorAEnviar.Split('$');
                        foreach (string valor in valores)
                        {
                            DetalleMatricula odet = bd.DetalleMatricula.Where(p => p.IIDMATRICULA == matricula.IIDMATRICULA
                            && p.IIDCURSO == int.Parse(valor)).First();
                            odet.bhabilitado = 1;
                        }
                        bd.SubmitChanges();
                        transaccion.Complete();
                        resultado = 1;
                    }
                    
                }
                return Json(resultado, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult guardarDatos2(Matricula matricula, int IIDGRADOSECCION)
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
                        //bd.SubmitChanges();
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
                int resultado = 0;
                Miconexion3DataContext bd = new Miconexion3DataContext();
                using (var transaccion = new TransactionScope())
                {
                    Matricula update = bd.Matricula.Where(p => p.IIDMATRICULA.Equals(id)).First();
                    update.BHABILITADO = 0;
                    var listaDetalleMatricula = bd.DetalleMatricula.Where(p => p.IIDMATRICULA.Equals(id));
                    foreach (DetalleMatricula oDetalleMatricula in listaDetalleMatricula)
                    {
                        oDetalleMatricula.bhabilitado = 0;
                    }
                    bd.SubmitChanges();
                    resultado = 1;
                    transaccion.Complete();
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
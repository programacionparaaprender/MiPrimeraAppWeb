using DConexionBase3;
using MiTerceraAppWeb.Models;
using Models;
using System;
using System.Activities.Statements;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Transactions;
using System.Security.Cryptography;
using System.Text;

namespace MiTerceraAppWeb.Controllers
{
    public class UsuarioController : Controller
    {
        // GET: Usuario
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult listarUsuarios()
        {
            List<UsuarioCLS> listaUsuario = new List<UsuarioCLS>();
            using(Miconexion3DataContext bd = new Miconexion3DataContext())
            {
                List<UsuarioCLS> listaAlumno = (from usuario in bd.Usuario
                                               join alumno in bd.Alumno
                                               on usuario.IID equals alumno.IIDALUMNO
                                               join rol in bd.Rol
                                               on usuario.IIDROL equals rol.IIDROL
                                               where usuario.BHABILITADO==1 && usuario.TIPOUSUARIO=='A'
                                               select new UsuarioCLS 
                                               {
                                                   idUsuario = usuario.IIDUSUARIO,
                                                   nombrePersona = alumno.NOMBRE + " " + alumno.APPATERNO + " " + alumno.APMATERNO,
                                                   nombreUsuario = usuario.NOMBREUSUARIO,
                                                   nombreRol = rol.NOMBRE,
                                                   nombreTipoEmpleado = "ALUMNO"
                                               }).ToList();
                listaUsuario.AddRange(listaAlumno);
                List<UsuarioCLS> listaDocente = (from usuario in bd.Usuario
                                                join docente in bd.Docente
                                                on usuario.IID equals docente.IIDDOCENTE
                                                join rol in bd.Rol
                                                on usuario.IIDROL equals rol.IIDROL
                                                where usuario.BHABILITADO == 1 && usuario.TIPOUSUARIO == 'D'
                                                select new UsuarioCLS
                                                {
                                                    idUsuario = usuario.IIDUSUARIO,
                                                    nombrePersona = docente.NOMBRE + " " + docente.APPATERNO + " " + docente.APMATERNO,
                                                    nombreUsuario = usuario.NOMBREUSUARIO,
                                                    nombreRol = rol.NOMBRE,
                                                    nombreTipoEmpleado = "DOCENTE"
                                                }).ToList();
                listaUsuario.AddRange(listaDocente);
                listaUsuario = listaUsuario.OrderBy(p => p.idUsuario).ToList();
            }
            return Json(listaUsuario, JsonRequestBehavior.AllowGet);
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

        public JsonResult listarPersonas()
        {
            Miconexion3DataContext bd = new Miconexion3DataContext();
            List<PersonaCLS> listaPersona = new List<PersonaCLS>();
            List<PersonaCLS> listaAlumno = (from item in bd.Alumno
                        where item.bTieneUsuario == 0
                select new PersonaCLS
                {
                    IID = item.IIDALUMNO,
                    NOMBRE = item.NOMBRE + " " + item.APPATERNO + " " + item.APMATERNO + " (A)",
                }).ToList();
            listaPersona.AddRange(listaAlumno);
            List<PersonaCLS> listaDocente = (from item in bd.Docente
                        where item.bTieneUsuario == 0
                        select new PersonaCLS
                        {
                            IID = item.IIDDOCENTE,
                            NOMBRE = item.NOMBRE + " " + item.APPATERNO + " " + item.APMATERNO + " (D)",
                        }).ToList();
            listaPersona.AddRange(listaDocente);
            listaPersona = listaPersona.OrderBy(p => p.NOMBRE).ToList();
            return Json(listaPersona, JsonRequestBehavior.AllowGet);
        }

        public JsonResult recuperarInformacion(int id)
        {
            try
            {
                List<AlumnoModels> alumnos = new List<AlumnoModels>();
                DBAcceso db = new DBAcceso();
                DataTable dt = db.obtenerAlumnoId(id);
                foreach (DataRow row in dt.Rows)
                {
                    int IID = int.Parse(row["IID"].ToString());
                    int IIDALUMNO = int.Parse(row["IIDALUMNO"].ToString());
                    string NOMBRE = row["NOMBRE"].ToString();
                    string APPATERNO = row["APPATERNO"].ToString();
                    string APMATERNO = row["APMATERNO"].ToString();
                    DateTime FECHANACIMIENTO = DateTime.Parse(row["FECHANACIMIENTO"].ToString());
                    int IIDSEXO = int.Parse(row["IIDSEXO"].ToString());
                    string TELEFONOPADRE = row["TELEFONOPADRE"].ToString();
                    string TELEFONOMADRE = row["TELEFONOMADRE"].ToString();
                    int NUMEROHERMANOS = int.Parse(row["NUMEROHERMANOS"].ToString());
                    int BHABILITADO = int.Parse(row["BHABILITADO"].ToString());
                    string IIDTIPOUSUARIO = row["IIDTIPOUSUARIO"].ToString();
                    int bTieneUsuario = int.Parse(row["bTieneUsuario"].ToString());
                    AlumnoModels periodo;
                    periodo = new AlumnoModels
                    {
                        IID = IID,
                        IIDALUMNO = IIDALUMNO,
                        NOMBRE = NOMBRE,
                        APPATERNO = APPATERNO,
                        APMATERNO = APMATERNO,
                        FECHANACIMIENTO = FECHANACIMIENTO,
                        IIDSEXO = IIDSEXO,
                        TELEFONOPADRE = TELEFONOPADRE,
                        TELEFONOMADRE = TELEFONOMADRE,
                        NUMEROHERMANOS = NUMEROHERMANOS,
                        BHABILITADO = BHABILITADO,
                        IIDTIPOUSUARIO = IIDTIPOUSUARIO,
                        bTieneUsuario = bTieneUsuario
                    };
                    alumnos.Add(periodo);
                }
                return new JsonResult { Data = alumnos, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            catch (Exception ex)
            {
                return new JsonResult { Data = ex.Message, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }

        [HttpPost]
        public JsonResult guardarDatos(Usuario usuario, string nombrePersona)
        {
            try
            {
                Miconexion3DataContext bd = new Miconexion3DataContext();
                int nveces = 0;
                nveces = bd.Usuario.Where(p => p.NOMBREUSUARIO.Equals(usuario.NOMBREUSUARIO)).Count();

                int resultado = 0;
                using (var transaction = new System.Transactions.TransactionScope())
                {
                    if (usuario.IIDUSUARIO == 0)
                    {
                        if (nveces == 0)
                        {
                            string clave = usuario.CONTRA;
                            SHA256Managed sha = new SHA256Managed();
                            byte[] dataNoCifrada = Encoding.Default.GetBytes(clave);
                            byte[] dataCifrada = sha.ComputeHash(dataNoCifrada);
                            usuario.CONTRA = BitConverter.ToString(dataCifrada).Replace("-", "");
                            char tipo = char.Parse(nombrePersona.Substring(nombrePersona.Length - 2, 1));
                            usuario.TIPOUSUARIO = tipo;
                            bd.Usuario.InsertOnSubmit(usuario);
                            if (tipo.Equals("A")) {
                                Alumno oAlumno = bd.Alumno.Where(p => p.IIDALUMNO == usuario.IID).FirstOrDefault();
                                oAlumno.bTieneUsuario = 1;
                            } else {
                                Docente oDocente = bd.Docente.Where(p => p.IIDDOCENTE == usuario.IID).FirstOrDefault();
                                oDocente.bTieneUsuario = 1;
                            }
                            bd.SubmitChanges();
                            resultado = 1;
                            transaction.Complete();
                        }
                        else
                        {
                            resultado = -1;
                        }
                    }
                    else
                    {
                        Usuario update = bd.Usuario.Where(p => p.IIDUSUARIO.Equals(usuario.IIDUSUARIO)).First();
                        update.NOMBREUSUARIO = usuario.NOMBREUSUARIO;
                        //update.CONTRA = usuario.CONTRA;
                        //update.IID = usuario.IID;
                        update.IIDROL = usuario.IIDROL;
                        bd.SubmitChanges();
                        resultado = 1;
                        transaction.Complete();
                    }
                }

                    return new JsonResult { Data = resultado, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            catch (Exception ex)
            {
                return new JsonResult { Data = ex.Message, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }
    }
}
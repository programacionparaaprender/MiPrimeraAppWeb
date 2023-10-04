using Models;
using System;
using System.Activities;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace MiTerceraAppWeb.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Cerrar()
        {
            Variables.acciones = new List<string>();
            Variables.controladores = new List<string>();
            Variables.mensaje = new List<string>();

            return RedirectToAction("Index");
        }

        public int validarUsuario(string usuario, string contra)
        {
            int resultado = 0;
            try
            {
                using (Miconexion3DataContext bd = new Miconexion3DataContext())
                {
                    SHA256Managed sha = new SHA256Managed();
                    byte[] dataNoCifrada = Encoding.Default.GetBytes(contra);
                    byte[] dataCifrada = sha.ComputeHash(dataNoCifrada);
                    //Contraseña
                    string contraCifrada = BitConverter.ToString(dataCifrada).Replace("-", "");
                    resultado = bd.Usuario.Where(p=>p.NOMBREUSUARIO.Equals(usuario) && p.CONTRA.Equals(contraCifrada)).Count();
                    if (resultado > 0)
                    {
                        int usuarioid = bd.Usuario.Where(p => p.NOMBREUSUARIO.Equals(usuario) && p.CONTRA.Equals(contraCifrada)).First().IIDUSUARIO;
                        Session["usuarioid"] = usuarioid;
                        var roles = from usu in bd.Usuario
                                    join rol in bd.Rol
                                    on usu.IIDROL equals rol.IIDROL
                                    join rolpagina in bd.RolPagina
                                    on rol.IIDROL equals rolpagina.IIDROL
                                    join pagina in bd.Pagina
                                    on rolpagina.IIDPAGINA equals pagina.IIDPAGINA
                                    where usu.BHABILITADO.Equals(1) && rolpagina.BHABILITADO.Equals(1)
                                    && usu.IIDUSUARIO.Equals(usuarioid)
                                    select new
                                    {
                                        accions = pagina.ACCION,
                                        controladors = pagina.CONTROLADOR,
                                        mensaje = pagina.MENSAJE
                                    };
                        Variables.acciones = new List<string>();
                        Variables.controladores = new List<string>();
                        Variables.mensaje = new List<string>();
                        foreach (var item in roles)
                        {
                            Variables.acciones.Add(item.accions);
                            Variables.controladores.Add(item.controladors);
                            Variables.mensaje.Add(item.mensaje);
                        }
                    }    
                    
                }
            }
            catch(Exception ex)
            {
                resultado = 0;
            }
            return resultado;
        }
    }
}
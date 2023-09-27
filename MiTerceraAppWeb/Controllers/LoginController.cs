using System;
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
                        Session["usuarioid"] = bd.Usuario.Where(p => p.NOMBREUSUARIO.Equals(usuario) && p.CONTRA.Equals(contraCifrada)).First().IIDUSUARIO;
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
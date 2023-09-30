using Models;
using System;
using System.Activities;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MiTerceraAppWeb.Filters
{
    public class Seguridad : ActionFilterAttribute
    {

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var usuario = HttpContext.Current.Session["usuarioid"];
            List<string> controladores = new List<string>();
            string nombreControlador = "";
            if (Variables.controladores != null /*|| !controladores.Contains(nombreControlador.ToUpper())*/)
            {
                controladores = Variables.controladores.Select(p => p.ToUpper()).ToList();
                nombreControlador = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            }

            if (usuario == null || !controladores.Contains(nombreControlador.ToUpper()))
            {
                filterContext.Result = new RedirectResult("~/Login/Index");
            }

            base.OnActionExecuting(filterContext);
        }

    }
}
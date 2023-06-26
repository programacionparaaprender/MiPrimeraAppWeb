using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MiTerceraAppWeb.Models
{
	public class Alumno
	{
		public int IID { get; set; }
		public int IIDALUMNO { get; set; }
		public string NOMBRE { get; set; }
		public string APPATERNO { get; set; }
		public string APMATERNO { get; set; }
		public DateTime FECHANACIMIENTO { get; set; }
		public int IIDSEXO { get; set; }
		public string TELEFONOPADRE { get; set; }
		public string TELEFONOMADRE { get; set; }
		public int NUMEROHERMANOS { get; set; }
		public int BHABILITADO { get; set; }
		public string IIDTIPOUSUARIO { get; set; }
		public int bTieneUsuario { get; set; }
	}
}
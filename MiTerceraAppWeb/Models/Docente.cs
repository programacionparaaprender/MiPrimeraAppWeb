using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;

namespace MiTerceraAppWeb.Models
{
	public class Docente
	{
		public int IID { get; set; }
		public string NOMBRE { get; set; }
		public string APPATERNO { get; set; }
		public string APMATERNO { get; set; }
		public string DIRECCION { get; set; }
		public string TELEFONOCELULAR { get; set; }
		public string TELEFONOFIJO { get; set; }
		public string EMAIL { get; set; }
		public int IIDSEXO { get; set; }
		public DateTime FECHACONTRATO { get; set; }
		public byte[] FOTO { get; set; }
		public int IIDMODALIDADCONTRATO { get; set; }
		public int BHABILITADO { get; set; }
		public string IIDTIPOUSUARIO { get; set; }
		public int bTieneUsuario { get; set; }
	}
}
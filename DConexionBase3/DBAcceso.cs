using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace DConexionBase3
{
	public class DBAcceso
	{
		//string cadena1 = @"Data Source=LUISALBERTO-PC\SQLEXPRESS;Initial Catalog=ClaseTaller;Integrated Security=True";
		//string cadena2 = @"Data Source=LUISALBERTO-PC\SQLEXPRESS;Initial Catalog=ClaseTaller;User ID=LuisCorrea; Password=yose1342";
		//string cadena = @"Data Source=BONE\SQLEXPRESS;Initial Catalog=TEST;Integrated Security=True";

		public DataTable obtenerCursos(string nombre)
		{
			string strCadSQL = @"SELECT * FROM Curso WHERE NOMBRE LIKE '%" + nombre + "%'";
			return obtenerTablaGenerico(strCadSQL);
		}

		public DataTable obtenerCursos()
		{
			string strCadSQL = @"SELECT * FROM Curso";
			return obtenerTablaGenerico(strCadSQL);
		}

		public DataTable obtenerAlumnos(int sexo)
		{
			string strCadSQL = @"SELECT * FROM Alumno WHERE BHABILITADO=1 AND IIDSEXO=" + sexo;
			return obtenerTablaGenerico(strCadSQL);
		}

		public DataTable obtenerAlumnos(int sexo, string nombre)
		{
			string strCadSQL = @"SELECT * FROM Alumno WHERE BHABILITADO=1 AND IIDSEXO=" + sexo + " AND NOMBRE LIKE '%" + nombre + "%'";
			return obtenerTablaGenerico(strCadSQL);
		}

		public DataTable obtenerAlumnos(string nombre)
		{
			string strCadSQL = @"SELECT * FROM Alumno WHERE BHABILITADO=1 AND NOMBRE LIKE '%" + nombre + "%'";
			return obtenerTablaGenerico(strCadSQL);
		}

		public DataTable obtenerSexo()
		{
			string strCadSQL = @"SELECT IIDSEXO as IID, IIDSEXO, NOMBRE FROM Sexo WHERE BHABILITADO=1";
			return obtenerTablaGenerico(strCadSQL);
		}

		public DataTable obtenerModalidadContrato()
		{
			string strCadSQL = @"SELECT IIDMODALIDADCONTRATO as IID, NOMBRE FROM ModalidadContrato WHERE BHABILITADO=1";
			return obtenerTablaGenerico(strCadSQL);
		}

		public DataTable obtenerDocentes(int ModalidadContrato)
		{
			string strCadSQL = @"SELECT IIDDOCENTE as IID, * FROM Docente WHERE BHABILITADO=1 AND IIDMODALIDADCONTRATO=" + ModalidadContrato.ToString();
			return obtenerTablaGenerico(strCadSQL);
		}

		public DataTable obtenerDocentes()
		{
			string strCadSQL = @"SELECT IIDDOCENTE as IID, * FROM Docente WHERE BHABILITADO=1";
			return obtenerTablaGenerico(strCadSQL);
		}

		public DataTable obtenerAlumnos()
		{
			string strCadSQL = @"SELECT IIDALUMNO as IID, * FROM Alumno WHERE BHABILITADO=1";
			return obtenerTablaGenerico(strCadSQL);
		}
		public DataTable obtenerPeriodo(string nombre)
		{
			string strCadSQL = @"SELECT IIDPERIODO as IID,* FROM Periodo WHERE BHABILITADO=1 AND NOMBRE LIKE '%" + nombre + "%'";
			return obtenerTablaGenerico(strCadSQL);
		}

		public DataTable obtenerSeccion()
		{
			string strCadSQL = @"SELECT IIDSECCION as IID,* FROM Seccion WHERE BHABILITADO=1";
			return obtenerTablaGenerico(strCadSQL);
		}

		public DataTable obtenerPeriodo()
		{
			string strCadSQL = @"SELECT IIDPERIODO as IID,* FROM Periodo WHERE BHABILITADO=1";
			return obtenerTablaGenerico(strCadSQL);
		}

		public DataTable obtenerTablaGenerico(string strCadSQL)
		{
			String cadenaConexion = ConfigurationManager.ConnectionStrings["CadenaConexion"].ConnectionString;
			DataTable dt = new DataTable();
			try
			{
				using (SqlConnection conexion = new SqlConnection(cadenaConexion))
				{
					conexion.Open();
					SqlDataReader myReader = null;
					SqlCommand myCommand = new SqlCommand(strCadSQL, conexion);
					myReader = myCommand.ExecuteReader();
					dt.Load(myReader);
				}
			}
			catch (Exception e)
			{
				Console.WriteLine(e.ToString());
			}
			return dt;
		}
	}
}

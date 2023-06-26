using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Models;

namespace DConexionBase3
{
	public class DBAcceso
	{
		//string cadena1 = @"Data Source=LUISALBERTO-PC\SQLEXPRESS;Initial Catalog=ClaseTaller;Integrated Security=True";
		//string cadena2 = @"Data Source=LUISALBERTO-PC\SQLEXPRESS;Initial Catalog=ClaseTaller;User ID=LuisCorrea; Password=yose1342";
		//string cadena = @"Data Source=BONE\SQLEXPRESS;Initial Catalog=TEST;Integrated Security=True";

		public DataTable obtenerCursos(int IIDCURSO)
		{
			string strCadSQL = @"SELECT * FROM Curso WHERE BHABILITADO=1 AND IIDCURSO =" + IIDCURSO;
			return obtenerTablaGenerico(strCadSQL);
		}

		public DataTable obtenerCursos(string nombre)
		{
			string strCadSQL = @"SELECT * FROM Curso WHERE BHABILITADO=1 AND NOMBRE LIKE '%" + nombre + "%'";
			return obtenerTablaGenerico(strCadSQL);
		}

		public int actualizarCurso(Curso curso)
		{
			int resultado = 0;
			try
			{
				string NOMBRE = curso.NOMBRE;
				string DESCRIPCION = curso.DESCRIPCION;
				//int BHABILITADO = curso.BHABILITADO;
				int IIDCURSO = curso.IIDCURSO;
				string cadenaConexion = ConfigurationManager.ConnectionStrings["CadenaConexion"].ConnectionString;
				using (SqlConnection conexion = new SqlConnection(cadenaConexion))
				{
					conexion.Open();
					string strCadSQL = @"UPDATE Curso Set NOMBRE=@NOMBRE,DESCRIPCION=@DESCRIPCION WHERE IIDCURSO=@IIDCURSO";
					SqlCommand comando = new SqlCommand(strCadSQL, conexion);
					comando.Parameters.AddWithValue("@IIDCURSO", IIDCURSO);
					comando.Parameters.AddWithValue("@NOMBRE", NOMBRE);
					comando.Parameters.AddWithValue("@DESCRIPCION", DESCRIPCION);
					//comando.Parameters.AddWithValue("@BHABILITADO", BHABILITADO);
					resultado = comando.ExecuteNonQuery();
					conexion.Close();
				}
				return resultado;
			}
			catch (Exception)
			{
				Console.WriteLine("actualizarCurso");
				return 0;
			}
		}

		public int insertarCurso(Curso curso)
		{
			int resultado = 0;
			try
			{
				string NOMBRE = curso.NOMBRE;
				string DESCRIPCION = curso.DESCRIPCION;
				int BHABILITADO = curso.BHABILITADO;
				string cadenaConexion = ConfigurationManager.ConnectionStrings["CadenaConexion"].ConnectionString;
				using(SqlConnection conexion = new SqlConnection(cadenaConexion))
				{
					conexion.Open();
					string strCadSQL = @"INSERT INTO Curso (NOMBRE,DESCRIPCION,BHABILITADO) Values(@NOMBRE,@DESCRIPCION,@BHABILITADO)";
					SqlCommand comando = new SqlCommand(strCadSQL, conexion);
					comando.Parameters.AddWithValue("@NOMBRE", NOMBRE);
					comando.Parameters.AddWithValue("@DESCRIPCION", DESCRIPCION);
					comando.Parameters.AddWithValue("@BHABILITADO", BHABILITADO);
					resultado = comando.ExecuteNonQuery();
					conexion.Close();
				}
				return resultado;
			}
			catch (Exception)
			{
				Console.WriteLine("insertarCurso");
				return 0;
			}
		}

		public int insertarPeriodo(Periodo periodo)
		{
			int resultado = 0;
			try
			{
				string NOMBRE = periodo.NOMBRE;
				DateTime FECHAINICIO = periodo.FECHAINICIO;
				DateTime FECHAFIN = periodo.FECHAFIN;
				int BHABILITADO = periodo.BHABILITADO;
				string cadenaConexion = ConfigurationManager.ConnectionStrings["CadenaConexion"].ConnectionString;
				using (SqlConnection conexion = new SqlConnection(cadenaConexion))
				{
					conexion.Open();
					string strCadSQL = @"INSERT INTO Periodo (NOMBRE,FECHAINICIO,FECHAFIN,BHABILITADO) Values(@NOMBRE,@FECHAINICIO,@FECHAFIN,@BHABILITADO)";
					SqlCommand comando = new SqlCommand(strCadSQL, conexion);
					comando.Parameters.AddWithValue("@NOMBRE", NOMBRE);
					comando.Parameters.AddWithValue("@FECHAINICIO", FECHAINICIO);
					comando.Parameters.AddWithValue("@FECHAFIN", FECHAFIN);
					comando.Parameters.AddWithValue("@BHABILITADO", BHABILITADO);
					resultado = comando.ExecuteNonQuery();
					conexion.Close();
				}
				return resultado;
			}
			catch (Exception)
			{
				Console.WriteLine("insertarPeriodo");
				return 0;
			}
		}

		public int actualizarPeriodo(Periodo periodo)
		{
			int resultado = 0;
			try
			{
				string NOMBRE = periodo.NOMBRE;
				DateTime FECHAINICIO = periodo.FECHAINICIO;
				DateTime FECHAFIN = periodo.FECHAFIN;
				int BHABILITADO = periodo.BHABILITADO;
				int IIDPERIODO = periodo.IIDPERIODO;
				string cadenaConexion = ConfigurationManager.ConnectionStrings["CadenaConexion"].ConnectionString;
				using (SqlConnection conexion = new SqlConnection(cadenaConexion))
				{
					conexion.Open();
					string strCadSQL = @"UPDATE Curso Set NOMBRE=@NOMBRE,FECHAINICIO=@FECHAINICIO,FECHAFIN=@FECHAFIN WHERE IIDPERIODO=@IIDPERIODO";
					SqlCommand comando = new SqlCommand(strCadSQL, conexion);
					comando.Parameters.AddWithValue("@IIDPERIODO", IIDPERIODO);
					comando.Parameters.AddWithValue("@NOMBRE", NOMBRE);
					comando.Parameters.AddWithValue("@FECHAINICIO", FECHAINICIO);
					comando.Parameters.AddWithValue("@FECHAFIN", FECHAFIN);
					comando.Parameters.AddWithValue("@BHABILITADO", BHABILITADO);
					resultado = comando.ExecuteNonQuery();
					conexion.Close();
				}
				return resultado;
			}
			catch (Exception)
			{
				Console.WriteLine("actualizarCurso");
				return 0;
			}
		}
		public int eliminarPeriodo(int id)
		{
			int resultado = 0;
			try
			{
				string cadenaConexion = ConfigurationManager.ConnectionStrings["CadenaConexion"].ConnectionString;
				using (SqlConnection conexion = new SqlConnection(cadenaConexion))
				{
					conexion.Open();
					string strCadSQL = @"UPDATE Periodo Set BHABILITADO=0 WHERE IIDPERIODO=@IIDPERIODO";
					SqlCommand comando = new SqlCommand(strCadSQL, conexion);
					comando.Parameters.AddWithValue("@IIDPERIODO", id);
					resultado = comando.ExecuteNonQuery();
					conexion.Close();
				}
				return resultado;
			}
			catch (Exception)
			{
				Console.WriteLine("eliminarCurso2");
				return 0;
			}
		}


		public int eliminarCurso2(int IIDCURSO)
		{
			int resultado = 0;
			try
			{
				string cadenaConexion = ConfigurationManager.ConnectionStrings["CadenaConexion"].ConnectionString;
				using (SqlConnection conexion = new SqlConnection(cadenaConexion))
				{
					conexion.Open();
					string strCadSQL = @"UPDATE Curso Set BHABILITADO=0 WHERE IIDCURSO=@IIDCURSO";
					SqlCommand comando = new SqlCommand(strCadSQL, conexion);
					comando.Parameters.AddWithValue("@IIDCURSO", IIDCURSO);
					resultado = comando.ExecuteNonQuery();
					conexion.Close();
				}
				return resultado;
			}
			catch (Exception)
			{
				Console.WriteLine("eliminarCurso2");
				return 0;
			}
		}

		public int eliminarCurso(int IIDCURSO)
		{
			int resultado = 0;
			try
			{
				string cadenaConexion = ConfigurationManager.ConnectionStrings["CadenaConexion"].ConnectionString;
				using (SqlConnection conexion = new SqlConnection(cadenaConexion))
				{
					conexion.Open();
					string strCadSQL = @"DELETE FROM Curso WHERE IIDCURSO=@IIDCURSO";
					SqlCommand comando = new SqlCommand(strCadSQL, conexion);
					comando.Parameters.AddWithValue("@IIDCURSO", IIDCURSO);
					resultado = comando.ExecuteNonQuery();
					conexion.Close();
				}
				return resultado;
			}
			catch (Exception)
			{
				Console.WriteLine("Error");
				return 0;
			}
		}

		public DataTable obtenerCursos()
		{
			string strCadSQL = @"SELECT * FROM Curso WHERE BHABILITADO=1";
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

		public DataTable obtenerPeriodo(int id)
		{
			string strCadSQL = @"SELECT IIDPERIODO as IID,* FROM Periodo WHERE BHABILITADO=1 AND IIDPERIODO=" + id;
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

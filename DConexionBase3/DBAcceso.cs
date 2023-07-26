using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Models;
using MiTerceraAppWeb.Models;

namespace DConexionBase3
{
	public class DBAcceso
	{
        //string cadena1 = @"Data Source=LUISALBERTO-PC\SQLEXPRESS;Initial Catalog=ClaseTaller;Integrated Security=True";
        //string cadena2 = @"Data Source=LUISALBERTO-PC\SQLEXPRESS;Initial Catalog=ClaseTaller;User ID=LuisCorrea; Password=yose1342";
        //string cadena = @"Data Source=BONE\SQLEXPRESS;Initial Catalog=TEST;Integrated Security=True";

        public int insertarDetalleMatricula(DetalleMatriculaModels m)
        {
            try
            {
                string cadenaConexion = ConfigurationManager.ConnectionStrings["CadenaConexion"].ConnectionString;
                int resultado = 0;
                using (SqlConnection conexion = new SqlConnection(cadenaConexion))
                {
                    conexion.Open();
                    string strCadSQL = "spInsertarDetalleMatricula";
                    SqlCommand comando = new SqlCommand();
                    comando.Connection = conexion;
                    comando.CommandText = strCadSQL;
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@IIDMATRICULA", m.IIDMATRICULA);
                    comando.Parameters.AddWithValue("@IIDCURSO", m.IIDCURSO);
                    comando.Parameters.AddWithValue("@NOTA1", m.NOTA1);
                    comando.Parameters.AddWithValue("@NOTA2", m.NOTA2);
                    comando.Parameters.AddWithValue("@NOTA3", m.NOTA3);
                    comando.Parameters.AddWithValue("@NOTA4", m.NOTA4);
                    comando.Parameters.AddWithValue("@PROMEDIO", m.PROMEDIO);
                    resultado = comando.ExecuteNonQuery();
                    conexion.Close();
                    return resultado;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("insertarDetalleMatricula");
                return 0;
            }
        }

        public int actualizarMatricula(MatriculaModels m)
        {
            int resultado = 0;
            string metodo = "actualizarMatricula";
            string storeProcedure = "spActualizarMatricula";
            try
            {
                string cadenaConexion = ConfigurationManager.ConnectionStrings["CadenaConexion"].ConnectionString;
                Dictionary<string, object> variables = new Dictionary<string, object>();
                variables.Add("@IIDMATRICULA", m.IIDMATRICULA);
                variables.Add("@IIDPERIODO", m.IIDPERIODO);
                variables.Add("@IIDGRADO", m.IIDGRADO);
                variables.Add("@IIDSECCION", m.IIDSECCION);
                variables.Add("@IIDALUMNO", m.IIDALUMNO);
                resultado = procedimientoAlmacenado(metodo, storeProcedure, variables);
            }
            catch (Exception ex)
            {
                Console.WriteLine("actualizarMatricula");
            }
            return resultado;
        }

        public int insertarMatricula(MatriculaModels m)
        {
            /*
             SqlCommand cmd = new SqlCommand("spMostrarProfesor", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds, "Profesor");
                this.dgvMostrar.DataSource = ds.Tables["Profesor"];
             */
            try
            {
                string cadenaConexion = ConfigurationManager.ConnectionStrings["CadenaConexion"].ConnectionString;
                int resultado = 0;
                using (SqlConnection conexion = new SqlConnection(cadenaConexion))
                {
                    conexion.Open();
                    string strCadSQL = @"SistemaMatricula.dbo.spInsertarMatricula @IIDPERIODO,@IIDGRADO,@IIDSECCION,@IIDALUMNO";
                    //SqlCommand comando = new SqlCommand(strCadSQL, conexion);
                    SqlCommand comando = new SqlCommand();
                    comando.Connection = conexion;
                    comando.CommandText = "spInsertarMatricula";

                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@IIDPERIODO", m.IIDPERIODO);
                    comando.Parameters.AddWithValue("@IIDGRADO", m.IIDGRADO);
                    comando.Parameters.AddWithValue("@IIDSECCION", m.IIDSECCION);
                    comando.Parameters.AddWithValue("@IIDALUMNO", m.IIDALUMNO);
                    //comando.Parameters.AddWithValue("@FECHA", m.FECHA);
                    //comando.Parameters.AddWithValue("@BHABILITADO", m.BHABILITADO);
                    //resultado = comando.ExecuteNonQuery();
                    SqlDataReader myReader;
                    myReader = comando.ExecuteReader();
                    DataTable dt = new DataTable();
                    dt.Load(myReader);
                    if (dt.Rows.Count == 1)
                    {
                        DataRow row = dt.Rows[0];
                        resultado = int.Parse(row[0].ToString());
                    }
                    conexion.Close();
                    return resultado;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("insertarMatricula");
                return 0;
            }
        }

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

		public int actualizarCurso(CursoModels curso)
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

		public int insertarCurso(CursoModels curso)
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

        public int insertarDocente(DocenteModels docente)
        {
            int resultado = 0;
            try
            {
				//int IIDDOCENTE = docente.IIDDOCENTE;
                string NOMBRE = docente.NOMBRE;
                string APPATERNO = docente.APPATERNO;
                string APMATERNO = docente.APMATERNO;
                string DIRECCION = docente.DIRECCION;
                string TELEFONOCELULAR = docente.TELEFONOCELULAR;
                string TELEFONOFIJO = docente.TELEFONOFIJO;
                string EMAIL = docente.EMAIL;
                int IIDSEXO = docente.IIDSEXO;
                DateTime FECHACONTRATO = docente.FECHACONTRATO;
                byte[] FOTO = docente.FOTO;
                int IIDMODALIDADCONTRATO = docente.IIDMODALIDADCONTRATO;
				int BHABILITADO = docente.BHABILITADO;


                string cadenaConexion = ConfigurationManager.ConnectionStrings["CadenaConexion"].ConnectionString;
                using (SqlConnection conexion = new SqlConnection(cadenaConexion))
                {
                    conexion.Open();
                    string strCadSQL = @"INSERT INTO Docente (NOMBRE,APPATERNO,APMATERNO,DIRECCION,TELEFONOCELULAR,TELEFONOFIJO,EMAIL,IIDSEXO,FECHACONTRATO,FOTO,IIDMODALIDADCONTRATO,BHABILITADO) Values(@NOMBRE,@APPATERNO,@APMATERNO,@DIRECCION,@TELEFONOCELULAR,@TELEFONOFIJO,@EMAIL,@IIDSEXO,@FECHACONTRATO,@FOTO,@IIDMODALIDADCONTRATO,@BHABILITADO)";
                   
                    SqlCommand comando = new SqlCommand(strCadSQL, conexion);
                    //comando.Parameters.AddWithValue("@IIDALUMNO", IIDALUMNO);
                    comando.Parameters.AddWithValue("@NOMBRE", NOMBRE);
                    comando.Parameters.AddWithValue("@APPATERNO", APPATERNO);
                    comando.Parameters.AddWithValue("@APMATERNO", APMATERNO);
                    comando.Parameters.AddWithValue("@DIRECCION", DIRECCION);
                    comando.Parameters.AddWithValue("@TELEFONOCELULAR", TELEFONOCELULAR);
                    comando.Parameters.AddWithValue("@TELEFONOFIJO", TELEFONOFIJO);
                    comando.Parameters.AddWithValue("@EMAIL", EMAIL);
                    comando.Parameters.AddWithValue("@IIDSEXO", IIDSEXO);
                    comando.Parameters.AddWithValue("@FECHACONTRATO", FECHACONTRATO);
                    comando.Parameters.AddWithValue("@FOTO", FOTO);
                    comando.Parameters.AddWithValue("@IIDMODALIDADCONTRATO", IIDMODALIDADCONTRATO);
                    comando.Parameters.AddWithValue("@BHABILITADO", BHABILITADO);
                    resultado = comando.ExecuteNonQuery();
                    conexion.Close();
                }
                return resultado;
            }
            catch (Exception)
            {
                Console.WriteLine("insertarAlumno");
                return 0;
            }
        }

        public int actualizarDocente(DocenteModels docente)
        {
            int resultado = 0;
            try
            {
                int IIDDOCENTE = docente.IIDDOCENTE;
                string NOMBRE = docente.NOMBRE;
                string APPATERNO = docente.APPATERNO;
                string APMATERNO = docente.APMATERNO;
                string DIRECCION = docente.DIRECCION;
                string TELEFONOCELULAR = docente.TELEFONOCELULAR;
                string TELEFONOFIJO = docente.TELEFONOFIJO;
                string EMAIL = docente.EMAIL;
                int IIDSEXO = docente.IIDSEXO;
                DateTime FECHACONTRATO = docente.FECHACONTRATO;
                byte[] FOTO = docente.FOTO;
                int IIDMODALIDADCONTRATO = docente.IIDMODALIDADCONTRATO;
                //int BHABILITADO = docente.BHABILITADO;


                string cadenaConexion = ConfigurationManager.ConnectionStrings["CadenaConexion"].ConnectionString;
                using (SqlConnection conexion = new SqlConnection(cadenaConexion))
                {
                    conexion.Open();
                    string strCadSQL = @"update Docente  set NOMBRE=@NOMBRE,APPATERNO=@APPATERNO,APMATERNO=@APMATERNO,DIRECCION=@DIRECCION,TELEFONOCELULAR=@TELEFONOCELULAR,TELEFONOFIJO=@TELEFONOFIJO,EMAIL=@EMAIL,IIDSEXO=@IIDSEXO,FECHACONTRATO=@FECHACONTRATO,FOTO=@FOTO,IIDMODALIDADCONTRATO=@IIDMODALIDADCONTRATO WHERE IIDDOCENTE=@IIDDOCENTE";
                    SqlCommand comando = new SqlCommand(strCadSQL, conexion);
                    comando.Parameters.AddWithValue("@IIDDOCENTE", IIDDOCENTE);
                    comando.Parameters.AddWithValue("@NOMBRE", NOMBRE);
                    comando.Parameters.AddWithValue("@APPATERNO", APPATERNO);
                    comando.Parameters.AddWithValue("@APMATERNO", APMATERNO);
                    comando.Parameters.AddWithValue("@DIRECCION", DIRECCION);
                    comando.Parameters.AddWithValue("@TELEFONOCELULAR", TELEFONOCELULAR);
                    comando.Parameters.AddWithValue("@TELEFONOFIJO", TELEFONOFIJO);
                    comando.Parameters.AddWithValue("@EMAIL", EMAIL);
                    comando.Parameters.AddWithValue("@IIDSEXO", IIDSEXO);
                    comando.Parameters.AddWithValue("@FECHACONTRATO", FECHACONTRATO);
                    comando.Parameters.AddWithValue("@FOTO", FOTO);
                    comando.Parameters.AddWithValue("@IIDMODALIDADCONTRATO", IIDMODALIDADCONTRATO);
                    //comando.Parameters.AddWithValue("@BHABILITADO", BHABILITADO);
                    resultado = comando.ExecuteNonQuery();
                    conexion.Close();
                }
                return resultado;
            }
            catch (Exception)
            {
                Console.WriteLine("actualizarDocente");
                return 0;
            }
        }

        public int insertarAlumno(AlumnoModels alumno)
		{
			int resultado = 0;
			try
			{
				//int IIDALUMNO = alumno.IIDALUMNO;
				string NOMBRE = alumno.NOMBRE;
				string APPATERNO = alumno.APPATERNO;
				string APMATERNO = alumno.APMATERNO;
				DateTime FECHANACIMIENTO = alumno.FECHANACIMIENTO;
				int IIDSEXO = alumno.IIDSEXO;
				string TELEFONOPADRE = alumno.TELEFONOPADRE;
				string TELEFONOMADRE = alumno.TELEFONOMADRE;
				int NUMEROHERMANOS = alumno.NUMEROHERMANOS;
				int BHABILITADO = alumno.BHABILITADO;
				string cadenaConexion = ConfigurationManager.ConnectionStrings["CadenaConexion"].ConnectionString;
				using (SqlConnection conexion = new SqlConnection(cadenaConexion))
				{
					conexion.Open();
					string strCadSQL = @"INSERT INTO Alumno (NOMBRE,APPATERNO,APMATERNO,FECHANACIMIENTO,IIDSEXO,TELEFONOPADRE,TELEFONOMADRE,NUMEROHERMANOS,BHABILITADO) Values(@NOMBRE,@APPATERNO,@APMATERNO,@FECHANACIMIENTO,@IIDSEXO,@TELEFONOPADRE,@TELEFONOMADRE,@NUMEROHERMANOS,@BHABILITADO)";
					SqlCommand comando = new SqlCommand(strCadSQL, conexion);
					//comando.Parameters.AddWithValue("@IIDALUMNO", IIDALUMNO);
					comando.Parameters.AddWithValue("@NOMBRE", NOMBRE);
					comando.Parameters.AddWithValue("@APPATERNO", APPATERNO);
					comando.Parameters.AddWithValue("@APMATERNO", APMATERNO);
					comando.Parameters.AddWithValue("@FECHANACIMIENTO", FECHANACIMIENTO);
					comando.Parameters.AddWithValue("@IIDSEXO", IIDSEXO);
					comando.Parameters.AddWithValue("@TELEFONOPADRE", TELEFONOPADRE);
					comando.Parameters.AddWithValue("@TELEFONOMADRE", TELEFONOMADRE);
					comando.Parameters.AddWithValue("@NUMEROHERMANOS", NUMEROHERMANOS);
					comando.Parameters.AddWithValue("@BHABILITADO", BHABILITADO);
					resultado = comando.ExecuteNonQuery();
					conexion.Close();
				}
				return resultado;
			}
			catch (Exception)
			{
				Console.WriteLine("insertarAlumno");
				return 0;
			}
		}

		public int eliminarAlumno(int IIDALUMNO)
		{
			int resultado = 0;
			try
			{
				string cadenaConexion = ConfigurationManager.ConnectionStrings["CadenaConexion"].ConnectionString;
				using (SqlConnection conexion = new SqlConnection(cadenaConexion))
				{
					conexion.Open();
					string strCadSQL = @"UPDATE Alumno Set BHABILITADO=0 WHERE IIDALUMNO=@IIDALUMNO";
					SqlCommand comando = new SqlCommand(strCadSQL, conexion);
					comando.Parameters.AddWithValue("@IIDALUMNO", IIDALUMNO);
					resultado = comando.ExecuteNonQuery();
					conexion.Close();
				}
				return resultado;
			}
			catch (Exception)
			{
				Console.WriteLine("eliminarAlumno");
				return 0;
			}
		}

		public int actualizarAlumno(AlumnoModels alumno)
		{
			int resultado = 0;
			try
			{
				int IIDALUMNO = alumno.IIDALUMNO;
				string NOMBRE = alumno.NOMBRE;
				string APPATERNO = alumno.APPATERNO;
				string APMATERNO = alumno.APMATERNO;
				DateTime FECHANACIMIENTO = alumno.FECHANACIMIENTO;
				int IIDSEXO = alumno.IIDSEXO;
				string TELEFONOPADRE = alumno.TELEFONOPADRE;
				string TELEFONOMADRE = alumno.TELEFONOMADRE;
				int NUMEROHERMANOS = alumno.NUMEROHERMANOS;
				//int BHABILITADO = alumno.BHABILITADO;
				string cadenaConexion = ConfigurationManager.ConnectionStrings["CadenaConexion"].ConnectionString;
				using (SqlConnection conexion = new SqlConnection(cadenaConexion))
				{
					conexion.Open();
					string strCadSQL = @"UPDATE Alumno Set NOMBRE=@NOMBRE,APPATERNO=@APPATERNO,APMATERNO=@APMATERNO,FECHANACIMIENTO=@FECHANACIMIENTO,IIDSEXO=@IIDSEXO,TELEFONOPADRE=@TELEFONOPADRE,TELEFONOMADRE=@TELEFONOMADRE,NUMEROHERMANOS=@NUMEROHERMANOS WHERE IIDALUMNO=@IIDALUMNO";
					SqlCommand comando = new SqlCommand(strCadSQL, conexion);
					comando.Parameters.AddWithValue("@IIDALUMNO", IIDALUMNO);
					comando.Parameters.AddWithValue("@NOMBRE", NOMBRE);
					comando.Parameters.AddWithValue("@APPATERNO", APPATERNO);
					comando.Parameters.AddWithValue("@APMATERNO", APMATERNO);
					comando.Parameters.AddWithValue("@FECHANACIMIENTO", FECHANACIMIENTO);
					comando.Parameters.AddWithValue("@IIDSEXO", IIDSEXO);
					comando.Parameters.AddWithValue("@TELEFONOPADRE", TELEFONOPADRE);
					comando.Parameters.AddWithValue("@TELEFONOMADRE", TELEFONOMADRE);
					comando.Parameters.AddWithValue("@NUMEROHERMANOS", NUMEROHERMANOS);
					//comando.Parameters.AddWithValue("@BHABILITADO", BHABILITADO);
					resultado = comando.ExecuteNonQuery();
					conexion.Close();
				}
				return resultado;
			}
			catch (Exception ex)
			{
				Console.WriteLine("actualizarAlumno " +ex.Message);
				return 0;
			}
		}

		public int insertarPeriodo(PeriodoModels periodo)
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

		public int actualizarPeriodo(PeriodoModels periodo)
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
				Console.WriteLine("actualizarPeriodo");
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

		public DataTable obtenerAlumnoId(int id)
		{
			string strCadSQL = @"SELECT IIDALUMNO as IID, * FROM Alumno WHERE BHABILITADO=1 AND IIDALUMNO=" + id;
			return obtenerTablaGenerico(strCadSQL);
		}

        public DataTable obtenerPeriodoGradoCurso(MatriculaModels matricula)
        {
            //var lista = bd.PeriodoGradoCurso.Where(p => p.IIDPERIODO.Equals(matricula.IIDPERIODO) && p.IIDGRADO.Equals(matricula.IIDGRADO)).Select(p => p.IIDCURSO);
            string strCadSQL = @"SELECT * FROM PeriodoGradoCurso WHERE IIDPERIODO="+ matricula.IIDPERIODO + " AND IIDGRADO=" + matricula.IIDGRADO;
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

        public DataTable obtenerDocentesId(int id)
        {
            string strCadSQL = @"SELECT IIDDOCENTE as IID, * FROM Docente WHERE BHABILITADO=1 AND IIDDOCENTE=" + id;
            return obtenerTablaGenerico(strCadSQL);
        }

        public int eliminarDocente(int IIDDOCENTE)
        {
            int resultado = 0;
            try
            {
                string cadenaConexion = ConfigurationManager.ConnectionStrings["CadenaConexion"].ConnectionString;
                using (SqlConnection conexion = new SqlConnection(cadenaConexion))
                {
                    conexion.Open();
                    string strCadSQL = @"UPDATE Docente set BHABILITADO=0 WHERE IIDDOCENTE=@IIDDOCENTE";
                    SqlCommand comando = new SqlCommand(strCadSQL, conexion);
                    comando.Parameters.AddWithValue("@IIDDOCENTE", IIDDOCENTE);
                    resultado = comando.ExecuteNonQuery();
                    conexion.Close();
                }
                return resultado;
            }
            catch (Exception)
            {
                Console.WriteLine("eliminarDocente");
                return 0;
            }
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

        public int procedimientoAlmacenado(string metodo, string storeProcedure, Dictionary<string, object> variables)
        {
            try
            {
                string cadenaConexion = ConfigurationManager.ConnectionStrings["CadenaConexion"].ConnectionString;
                int resultado = 0;
                using (SqlConnection conexion = new SqlConnection(cadenaConexion))
                {
                    conexion.Open();
                    SqlCommand comando = new SqlCommand();
                    comando.Connection = conexion;
                    comando.CommandText = storeProcedure;
                    comando.CommandType = CommandType.StoredProcedure;
                    foreach (KeyValuePair<string, object> entry in variables)
                    {   
                        comando.Parameters.AddWithValue(entry.Key, entry.Value);
                    }
                    resultado = comando.ExecuteNonQuery();
                    conexion.Close();
                    return resultado;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(metodo);
                return 0;
            }
        }
    }
}

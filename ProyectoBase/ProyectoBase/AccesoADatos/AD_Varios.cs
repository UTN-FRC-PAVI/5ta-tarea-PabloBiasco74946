using ProyectoBase.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoBase.AccesoADatos
{
    class AD_Varios
    {
        public static DataTable ObtenerTabla(string nombreTabla)
        {
            string cadenaConexion = System.Configuration.ConfigurationManager.AppSettings["CadenaBD"];
            SqlConnection cn = new SqlConnection(cadenaConexion);
            try
            {
                bool resultado = false;
                SqlCommand cmd = new SqlCommand();
                string consulta = "SELECT * FROM "+nombreTabla;

                cmd.Parameters.Clear();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = consulta;

                cn.Open();
                cmd.Connection = cn;

                DataTable tabla = new DataTable();

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(tabla);
                return tabla;
            }
            catch (Exception ex)
            {

                throw;
            }
            finally
            {
                cn.Close();
            }

        }

        public static DataTable ObtenerTiposDocumentos()
        {
            string cadenaConexion = System.Configuration.ConfigurationManager.AppSettings["CadenaBD"];
            SqlConnection cn = new SqlConnection(cadenaConexion);
            try
            {
                bool resultado = false;
                SqlCommand cmd = new SqlCommand();
                string consulta = "GetTipoDocumentos";

                cmd.Parameters.Clear();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = consulta;

                cn.Open();
                cmd.Connection = cn;

                DataTable tabla = new DataTable();

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(tabla);
                return tabla;
            }
            catch (Exception ex)
            {

                throw;
            }
            finally
            {
                cn.Close();
            }
        }





        public static DataTable ObtenerCarreras()
        {
            string cadenaConexion = System.Configuration.ConfigurationManager.AppSettings["CadenaBD"];
            SqlConnection cn = new SqlConnection(cadenaConexion);
            try
            {
                bool resultado = false;
                SqlCommand cmd = new SqlCommand();
                string consulta = "GetCarreras";

                cmd.Parameters.Clear();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = consulta;

                cn.Open();
                cmd.Connection = cn;

                DataTable tabla = new DataTable();

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(tabla);
                return tabla;
            }

            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                cn.Close();
            }
        }



        public static bool AgregarPersonaADB(Persona per)
        {


            string cadenaConexion = System.Configuration.ConfigurationManager.AppSettings["CadenaBD"];
            SqlConnection cn = new SqlConnection(cadenaConexion);
            bool resultado = false;

            try
            {
                SqlCommand cmd = new SqlCommand();
                string consulta = "INSERT INTO personas (Nombre, Apellido, FechaNacimiento, IdSexo, IdTipoDocumento, NumeroDocumento, Calle, NroCasa, Soltero, Casado, Hijos, CantidadHijos, IdCarrera) VALUES(@Nombre, @Apellido, @FechaNacimiento, @IdSexo, @IdTipoDocumento, @NumeroDocumento, @Calle, @NroCasa, @Soltero, @Casado, @Hijos, @CantidadHijos, @IdCarrera)";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@Nombre", per.NombrePersona);
                cmd.Parameters.AddWithValue("@Apellido", per.ApellidoPersona);
                cmd.Parameters.AddWithValue("@FechaNacimiento", per.FechaNacimientoPersona);
                cmd.Parameters.AddWithValue("@IdSexo", per.SexoPersona);
                cmd.Parameters.AddWithValue("@IdTipoDocumento", per.TipoDocumentoPersona);
                cmd.Parameters.AddWithValue("@NumeroDocumento", per.DocumentoPersona);
                cmd.Parameters.AddWithValue("@Calle", per.CallePersona);
                cmd.Parameters.AddWithValue("@NroCasa", per.NroCasaPersona);
                cmd.Parameters.AddWithValue("@Soltero", per.SolteroPersona);
                cmd.Parameters.AddWithValue("@Casado", per.CasadoPersona);
                cmd.Parameters.AddWithValue("@Hijos", per.HijosPersona);
                cmd.Parameters.AddWithValue("@CantidadHijos", per.CantidadHijosPersona);
                cmd.Parameters.AddWithValue("@IdCarrera", per.CarreraPersona);

                cmd.CommandType = CommandType.Text;
                cmd.CommandText = consulta;

                cn.Open();
                cmd.Connection = cn;
                cmd.ExecuteNonQuery();
                resultado = true;

            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                cn.Close();
            }





            return resultado;
        }
    }
}

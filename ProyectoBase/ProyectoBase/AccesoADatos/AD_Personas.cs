using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoBase.AccesoADatos
{
    class AD_Personas
    {
        public static DataTable ObtenerListadoPersonasReducido()
        {


            string cadenaConexion = System.Configuration.ConfigurationManager.AppSettings["CadenaBD"];
            SqlConnection cn = new SqlConnection(cadenaConexion);

            try
            {
                bool resultado = false;
                SqlCommand cmd = new SqlCommand();
                string consulta = "SELECT Nombre, Apellido, IdTipoDocumento, NumeroDocumento FROM personas";

                cmd.Parameters.Clear();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = consulta;

                cn.Open();
                cmd.Connection = cn;

                DataTable tabla = new DataTable();

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(tabla);
                return tabla;
                if (tabla.Rows.Count == 1)
                {
                    resultado = true;
                }

                else
                {
                    resultado = false;
                }
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
    }
}

using ProyectoBase.Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProyectoBase
{
    public partial class AltaPersona : Form
    {
        public AltaPersona()
        {
            InitializeComponent();
        }

        private void AltaPersona_Load(object sender, EventArgs e)
        {
            txtCantidadHijos.Enabled = false;
            btnActualizar.Enabled = false;
            CargarComboTiposDocumentos();
            CargarComboCarreras();
            CargarGrilla();
            // cmbCarrera.Items.Add("Ing. en sistemas de información");
            //cmbCarrera.Items.Add("Ing. Mecánica");
            //cmbCarrera.Items.Add("Ing. Industrial");
            //cmbTipoDocumento.Items.Add("DNI");
            //cmbTipoDocumento.Items.Add("Pasaporte");
            //cmbTipoDocumento.Items.Add("Libreta universitaria");

        }


        private void CargarGrilla()
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
                GdrPersonas.DataSource = tabla;
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
        private void CargarComboTiposDocumentos()
        {
            string cadenaConexion = System.Configuration.ConfigurationManager.AppSettings["CadenaBD"];
            SqlConnection cn = new SqlConnection(cadenaConexion);
            try
            {
                bool resultado = false;
                SqlCommand cmd = new SqlCommand();
                string consulta = "SELECT * FROM tipo_documentos";

                cmd.Parameters.Clear();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = consulta;

                cn.Open();
                cmd.Connection = cn;

                DataTable tabla = new DataTable();

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(tabla);
                cmbTipoDocumento.DataSource = tabla;
                cmbTipoDocumento.DisplayMember = "Nombre";
                cmbTipoDocumento.ValueMember = "Id";
                cmbTipoDocumento.SelectedIndex = -1;
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

        private void CargarComboCarreras()
        {
            string cadenaConexion = System.Configuration.ConfigurationManager.AppSettings["CadenaBD"];
            SqlConnection cn = new SqlConnection(cadenaConexion);
            try
            {
                bool resultado = false;
                SqlCommand cmd = new SqlCommand();
                string consulta = "SELECT * FROM carreras";

                cmd.Parameters.Clear();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = consulta;

                cn.Open();
                cmd.Connection = cn;

                DataTable tabla = new DataTable();

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(tabla);
                cmbCarrera.DataSource = tabla;
                cmbCarrera.DisplayMember = "Nombre";
                cmbCarrera.ValueMember = "Id";
                cmbCarrera.SelectedIndex = -1;
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

        private Persona ObtenerDatosPersona()
        {
            Persona p = new Persona();
            p.NombrePersona = txtNombre.Text.Trim();
            p.ApellidoPersona = txtApellido.Text.Trim();
            p.FechaNacimientoPersona = DateTime.Parse(txtFechaNacimiento.Text);


            if (rdMasculino.Checked)
            {
                p.SexoPersona = 1;
            }
            else if (rdFeminino.Checked)
            {
                p.SexoPersona = 2;
            }
            else
            {

                p.SexoPersona = 3;
            }

            p.TipoDocumentoPersona = (int)cmbTipoDocumento.SelectedValue;
            p.DocumentoPersona = txtNumeroDocumento.Text.Trim();
            p.CallePersona = txtCalle.Text.Trim();
            p.NroCasaPersona = txtNumeroCasa.Text.ToString();

            if (chkSoltero.Checked)
            {
                p.SolteroPersona = true;
            }
            else
            {
                p.SolteroPersona = false;
            }


            if (chkCasado.Checked)
            {
                p.CasadoPersona = true;
            }
            else
            {
                p.CasadoPersona = false;
            }


            if (chkHijos.Checked)
            {
                p.HijosPersona = true;
            }
            else
            {
                p.HijosPersona = false;
            }


            if (txtCantidadHijos.Text.Equals(""))
            {
                p.CantidadHijosPersona = 0;
            }

            else
            {
                p.CantidadHijosPersona = int.Parse(txtCantidadHijos.Text);
            }

            p.CarreraPersona = (int)cmbCarrera.SelectedValue;

            return p;

        }



        private void btnGuardarPersona_Click(object sender, EventArgs e)
        {
            Persona p = ObtenerDatosPersona();
            //MessageBox.Show(nombre + " " + apellido + " " + sexo + " " + TipoDocumento + " " + NroDocumento + " " + NroCasa);



            //MessageBox.Show("Datos de la persona: " + per.NombrePersona + per.ApellidoPersona + " " + per.DocumentoPersona);
            bool resultado = AgregarPersonaADB(p);
            if (resultado)
            {
                MessageBox.Show("Persona agregada con éxito");

                CargarComboCarreras();
                CargarComboTiposDocumentos();
                CargarGrilla();
            }
            else
            {
                MessageBox.Show("Error al agregar persona.");
            }

        }

        private bool AgregarPersonaADB(Persona per)
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



        private void AgregarPersona(Persona per)
        {
            DataGridViewRow fila = new DataGridViewRow();
            DataGridViewTextBoxCell celdaDocumento = new DataGridViewTextBoxCell();
            celdaDocumento.Value = per.DocumentoPersona;
            fila.Cells.Add(celdaDocumento);

            DataGridViewTextBoxCell celdaApellido = new DataGridViewTextBoxCell();
            celdaDocumento.Value = per.ApellidoPersona;
            fila.Cells.Add(celdaApellido);

            DataGridViewTextBoxCell celdaNombre = new DataGridViewTextBoxCell();
            celdaDocumento.Value = per.NombrePersona;
            fila.Cells.Add(celdaNombre);

            GdrPersonas.Rows.Add(fila);
            MessageBox.Show("Persona agregada con exito");
            txtNombre.Focus();

        }
        private void chkHijos_CheckedChanged(object sender, EventArgs e)
        {
            if (chkHijos.Checked)
            {
                txtCantidadHijos.Enabled = true;

            }
            else
            {
                txtCantidadHijos.Enabled = false;
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {

        }



        private bool ExisteGrilla(string criterioABuscasr)
        {
            bool resultado = false;
            for (int i = 0; i < GdrPersonas.Rows.Count; i++)
            {
                if (GdrPersonas.Rows[i].Cells["Documento"].Value.Equals(criterioABuscasr))
                {
                    resultado = true;
                    break;
                }

            }

            return resultado;
        }

        private void GdrPersonas_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void GdrPersonas_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int indice = e.RowIndex;
            btnActualizar.Enabled = true;
            DataGridViewRow filaseleccionada = GdrPersonas.Rows[indice];
            string documento = filaseleccionada.Cells["Documento"].Value.ToString();
            Persona p = ObtenerPersona(documento);
            CargarCampos(p);
        }


        private void CargarCampos(Persona p)
        {
            txtNombre.Text = p.NombrePersona;
            txtApellido.Text = p.ApellidoPersona;
            DateTime fecha = p.FechaNacimientoPersona;
            string dia = "";
            string mes = "";
            string año = "";
            dia = fecha.Date.Day.ToString();
            if (dia.Length == 1)
            {
                dia = "0" + dia;
            }

            mes = fecha.Date.Month.ToString();
            if (mes.Length == 1)
            {
                mes = "0" + mes;
            }

            año = fecha.Date.Date.Year.ToString();
            txtFechaNacimiento.Text = dia + mes + año;
            if (p.SexoPersona == 1)
            {
                rdMasculino.Checked = true;
            }

            else if(p.SexoPersona == 2)
            {
                rdFeminino.Checked = true;
            }

            else
            {
                rdOtro.Checked = true;
            }

            cmbTipoDocumento.SelectedValue = p.TipoDocumentoPersona;
            txtNumeroDocumento.Text = p.DocumentoPersona;
            txtCalle.Text = p.CallePersona;
            if(p.CasadoPersona)
            {
                chkCasado.Checked = true;
            }

            else
            {
                chkCasado.Checked = false;

            }


            if (p.SolteroPersona)
            {
                chkSoltero.Checked = true;
            }

            else
            {
                chkSoltero.Checked = false;

            }

            if (p.HijosPersona)
            {
                chkHijos.Checked = true;
            }

            else
            {
                chkHijos.Checked = false;

            }

            if (p.CantidadHijosPersona > 0)
            {
                txtCantidadHijos.Text = p.CantidadHijosPersona.ToString();
            }

            else
            {
                txtCantidadHijos.Text = "";

            }

            cmbCarrera.SelectedValue = p.CarreraPersona;
        }



        private Persona ObtenerPersona(string documento)
        {
            string cadenaConexion = System.Configuration.ConfigurationManager.AppSettings["CadenaBD"];
            SqlConnection cn = new SqlConnection(cadenaConexion);
            Persona p = new Persona();
            CargarCampos(p);

            try
            {
                SqlCommand cmd = new SqlCommand();
                string consulta = "SELECT * FROM personas where NumeroDocumento like @documento";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@Documento", documento);
                

                cmd.CommandType = CommandType.Text;
                cmd.CommandText = consulta;

                cn.Open();
                cmd.Connection = cn;
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr != null && dr.Read())
                {
                    p.NombrePersona = dr["Nombre"].ToString();
                    p.ApellidoPersona = dr["Apellido"].ToString();
                    p.FechaNacimientoPersona = DateTime.Parse(dr["FechaNacimiento"].ToString());
                    p.SexoPersona = int.Parse(dr["IdSexo"].ToString());
                    p.TipoDocumentoPersona = int.Parse(dr["IdTipoDocumento"].ToString());
                    p.DocumentoPersona = dr["NumeroDocumento"].ToString();
                    p.CallePersona = dr["Calle"].ToString();
                    p.NroCasaPersona = dr["NroCasa"].ToString();
                    p.SolteroPersona = bool.Parse(dr["Soltero"].ToString());
                    p.CasadoPersona = bool.Parse(dr["Casado"].ToString());
                    p.HijosPersona = bool.Parse(dr["Hijos"].ToString());
                    p.CantidadHijosPersona = int.Parse(dr["CantidadHijos"].ToString());
                    p.CarreraPersona = int.Parse(dr["IdCarrera"].ToString());




                }

            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                cn.Close();
            }





            return p;
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            Persona p = ObtenerDatosPersona();
            bool resultado = ActualizarPersona(p);
            if(resultado)
            {
                MessageBox.Show("Persona actualizada con éxito");
                //LimpiarCampos
                CargarGrilla();
                CargarComboCarreras();
                CargarComboTiposDocumentos();
            }

        }


        private bool ActualizarPersona(Persona per)
         {


            string cadenaConexion = System.Configuration.ConfigurationManager.AppSettings["CadenaBD"];
        SqlConnection cn = new SqlConnection(cadenaConexion);
        bool resultado = false;

            try
            {



                SqlCommand cmd = new SqlCommand();
        string consulta = "UPDATE personas SET Nombre = @nombre, Apellido = @apellido, FechaNacimiento = @FechaNacimiento, IdSexo = @IdSexo, IdTipoDocumento = @IdTipoDocumento, NumeroDocumento = @NumeroDocumento, Calle = @Calle, NroCasa = @NroCasa, Soltero = @Soltero, Casado = @Casado, Hijos = @Hijos, CantidadHijos = @CantidadHijos, IdCarrera = @IdCarrera WHERE NumeroDocumento like @NumeroDocumento";
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







       


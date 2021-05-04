using ProyectoBase.AccesoADatos;
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
    public partial class Usuarios : Form
    {
        public Usuarios()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void Usuarios_Load(object sender, EventArgs e)
        {
            CargarGrilla();
        }

        private void CargarGrilla()
        {
            try
            {
                grillaUsuarios.DataSource = AD_Usuarios.ObtenerListadoUsuarios();

            }
            catch (Exception)
            {
                MessageBox.Show("Error al obtener listado de usuarios.");
            }
        }

       

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }
        private void LimpiarCampos()
        {
            txtNombreDeUsuario.Text = "";
            txtPassword.Text = "";
            txtRepetirPassword.Text = "";
        }

        private void btnAltaUsuario_Click(object sender, EventArgs e)
        {
            if(txtNombreDeUsuario.Text.Equals(""))
            {
                MessageBox.Show("Ingrese nombre de usuario!");
            }
            else
            {
                if(txtPassword.Text.Equals(txtRepetirPassword.Text) == true)
                {
                    try
                    {
                        bool resultado = AD_Usuarios.InsertarUsuario(txtNombreDeUsuario.Text, txtPassword.Text);
                        if (resultado)
                        {
                            MessageBox.Show("Usuario dado de alta exitosamente!");
                            LimpiarCampos();
                            CargarGrilla();
                            txtNombreDeUsuario.Focus();
                        }
                        else
                        {
                            MessageBox.Show("Error al dar de alta al usuario");
                        }
                    }
                    catch (Exception)
                    {

                        MessageBox.Show("Error al insertar nuevo usuario...");
                        txtNombreDeUsuario.Focus();
                    }
                }
                else
                {
                    MessageBox.Show("Los passwords no coinciden");
                }
            }
        }   
    
    }
   
}

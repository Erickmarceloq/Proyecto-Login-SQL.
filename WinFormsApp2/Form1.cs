using System.Data;
using Microsoft.Data.SqlClient;
using System.Windows.Forms;
using System;

namespace WinFormsApp2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {

            string usuarioIngresado = txtUsuario.Text.Trim();
            string passwordIngresado = txtPassword.Text.Trim();

            string cadenaConexion = @"Server=(localdb)\MSSQLLocalDB;Database=pubs;Trusted_Connection=True;TrustServerCertificate=True;";

            string consulta = "SELECT COUNT(*) FROM employee WHERE TRIM(emp_id) = @usuario AND TRIM(lname) = @password";

            try
            {
                using (SqlConnection conexion = new SqlConnection(cadenaConexion))
                {
                    conexion.Open();
                    using (SqlCommand comando = new SqlCommand(consulta, conexion))
                    {

                        comando.Parameters.AddWithValue("@usuario", usuarioIngresado);
                        comando.Parameters.AddWithValue("@password", passwordIngresado);

                        int resultado = Convert.ToInt32(comando.ExecuteScalar());


                        if (resultado > 0)
                        {
                            MessageBox.Show("¡Bienvenido al sistema, acceso concedido!", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Usuario o contraseña incorrectos.", "Error de Autenticación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hubo un error al conectar con la base de datos: " + ex.Message, "Error Crítico", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

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
using MySql.Data.MySqlClient;

namespace AccesoBaseDatos1
{
    public partial class mysql : Form
    {
        private string Servidor = "localhost";
        private string Basedatos = "ESCOLAR";
        private string UsuarioId = "root";
        private string Password = "";
        private void EjecutaComando(string ConsultaSQL)
        {
            try
            {
                string strConn = $"Server={Servidor};" +
                    $"Database={Basedatos};" +
                    $"User Id={UsuarioId};" +
                    $"Password={Password}";

                MySqlConnection conn = new MySqlConnection(strConn);
                conn.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = ConsultaSQL;
                cmd.ExecuteNonQuery();


                llenarGrid();

            }
            catch (MySqlException Ex)
            {
                MessageBox.Show(Ex.Message);
            }
            catch (Exception Ex)
            {
                MessageBox.Show("Error en el sistema");
            }
        }
        private void llenarGrid()
        {
            try
            {
                string strConn = $"Server={Servidor};" +
                    $"Database={Basedatos};" +
                    $"User Id={UsuarioId};" +
                    $"Password={Password}";

                MySqlConnection conn = new MySqlConnection(strConn);
                conn.Open();

                string sqlQuery = "select * from Alumnos";
                MySqlDataAdapter adp = new MySqlDataAdapter(sqlQuery, conn);

                DataSet ds = new DataSet();
                adp.Fill(ds, "Alumnos");
                dgvAlumnos.DataSource = ds.Tables[0];
            }
            catch (SqlException dbEx)
            {
                MessageBox.Show($"Error de base de datos: {dbEx.Message}",
                               "Error",
                               MessageBoxButtons.OK,
                               MessageBoxIcon.Error);
            }
            catch (Exception generalEx)
            {
                MessageBox.Show("Ha ocurrido un problema inesperado",
                               "Error del sistema",
                               MessageBoxButtons.OK,
                               MessageBoxIcon.Warning);
            }
        }


        public mysql()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 sqlserver = new Form1();

            
            sqlserver.Show();

            
            this.Hide();
        }

        private void mysql_Load(object sender, EventArgs e)
        {

        }

        private void btnInsertar_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtNoControl.Text.Trim().Length > 0 &&
                    txtNombre.Text.Trim().Length > 0 &&
                    txtCarrera.Text.Trim().Length > 0)
                {
                    string strConn = $"Server={Servidor};" +
                        $"Database={Basedatos};" +
                        $"User Id={UsuarioId};" +
                        $"Password={Password}";


                    MySqlConnection conn = new MySqlConnection(strConn);
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = "INSERT INTO " +
                        "Alumnos (NoControl, nombre, carrera) " +
                        "VALUES ('" + txtNoControl.Text +
                        "', '" + txtNombre.Text +
                        "', " + txtCarrera.Text + ")";
                    cmd.ExecuteNonQuery();

                    llenarGrid();
                }
                else { }
            }
            catch (Exception Ex) { }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            try
            {
                // Verifica si hay una fila seleccionada en el DataGridView
                if (dgvAlumnos.SelectedRows.Count > 0)
                {
                    // Obtiene la fila seleccionada y el NoControl correspondiente
                    DataGridViewRow filaSeleccionada = dgvAlumnos.SelectedRows[0];
                    string noControl = filaSeleccionada.Cells["NoControl"].Value.ToString();

                    // Verifica que los campos de texto no estén vacíos
                    if (!string.IsNullOrWhiteSpace(txtNoControl.Text) &&
                        !string.IsNullOrWhiteSpace(txtNombre.Text) &&
                        !string.IsNullOrWhiteSpace(txtCarrera.Text))
                    {
                        string cadenaConexion = $"Server={Servidor};Database={Basedatos};User Id={UsuarioId};Password={Password};";

                        using (MySqlConnection conexion = new MySqlConnection(cadenaConexion))
                        {
                            conexion.Open();
                            MySqlCommand comando = new MySqlCommand
                            {
                                Connection = conexion,
                                CommandText = "UPDATE Alumnos SET Nombre = @Nombre, Carrera = @Carrera WHERE NoControl = @NoControl"
                            };

                            // Se usan parámetros para evitar SQL injection
                            comando.Parameters.AddWithValue("@NoControl", txtNoControl.Text);
                            comando.Parameters.AddWithValue("@Nombre", txtNombre.Text);
                            comando.Parameters.AddWithValue("@Carrera", txtCarrera.Text);

                            int filasAfectadas = comando.ExecuteNonQuery();

                            if (filasAfectadas > 0)
                            {
                                MessageBox.Show("Datos actualizados exitosamente.");
                                llenarGrid();
                            }
                            else
                            {
                                MessageBox.Show("No se encontró el registro a modificar.");
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Todos los campos son obligatorios.");
                    }
                }
                else
                {
                    MessageBox.Show("Seleccione un registro antes de continuar.");
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error de base de datos: " + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error: " + ex.Message);
            }

        }

        private void btnBorrar_Click(object sender, EventArgs e)
        {
            // Verifica si hay una fila seleccionada antes de proceder
            if (dgvAlumnos.SelectedRows.Count < 1)
            {
                MessageBox.Show("Debes seleccionar un alumno para eliminar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Extrae el NoControl de la fila seleccionada
            string idAlumno = dgvAlumnos.SelectedRows[0].Cells["NoControl"].Value.ToString();

            // Solicita confirmación antes de eliminar el registro
            DialogResult confirmacion = MessageBox.Show(
                $"¿Deseas eliminar al alumno con No. Control {idAlumno}?",
                "Confirmación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (confirmacion != DialogResult.Yes) return;

            try
            {
                string conexionDB = $"Server={Servidor};Database={Basedatos};User Id={UsuarioId};Password={Password};";

                using (MySqlConnection conexion = new MySqlConnection(conexionDB))
                {
                    conexion.Open();

                    // Comando SQL con parámetros para evitar inyección de SQL
                    string consulta = "DELETE FROM Alumnos WHERE NoControl = @NoControl";

                    using (MySqlCommand comando = new MySqlCommand(consulta, conexion))
                    {
                        comando.Parameters.AddWithValue("@NoControl", idAlumno);
                        int registrosEliminados = comando.ExecuteNonQuery();

                        if (registrosEliminados > 0)
                        {
                            MessageBox.Show("El alumno ha sido eliminado correctamente.", "Operación exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            llenarGrid(); // Refresca la lista de alumnos
                        }
                        else
                        {
                            MessageBox.Show("No se encontró el registro del alumno.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (MySqlException errorSql)
            {
                MessageBox.Show($"Se ha producido un error en la base de datos: {errorSql.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception errorGeneral)
            {
                MessageBox.Show($"Ocurrió un problema inesperado: {errorGeneral.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string buscar = txtNoControl.Text.Trim();

            if (string.IsNullOrWhiteSpace(buscar))
            {
                MessageBox.Show("Por favor, introduce un valor para buscar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string conexionDB = $"Server={Servidor};Database={Basedatos};User Id={UsuarioId};Password={Password};";

                using (MySqlConnection conexion = new MySqlConnection(conexionDB))
                {
                    conexion.Open();

                    // Consulta SQL para buscar registros coincidentes
                    string consulta = @"
            SELECT * FROM Alumnos 
            WHERE NoControl LIKE @Buscar 
               OR Nombre LIKE @Buscar";

                    using (MySqlDataAdapter adaptador = new MySqlDataAdapter(consulta, conexion))
                    {
                        adaptador.SelectCommand.Parameters.AddWithValue("@Buscar", $"%{buscar}%");

                        DataTable datos = new DataTable();
                        adaptador.Fill(datos);

                        // Mostrar los resultados en el DataGridView
                        dgvAlumnos.DataSource = datos;

                        if (datos.Rows.Count == 0)
                        {
                            MessageBox.Show("No se encontraron registros que coincidan con la búsqueda.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
            catch (MySqlException errorSql)
            {
                MessageBox.Show($"Error en la base de datos: {errorSql.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception errorGeneral)
            {
                MessageBox.Show($"Se ha producido un error inesperado: {errorGeneral.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btnCrearBD_Click(object sender, EventArgs e)
        {
            try
            {
                string strConn = $"Server={Servidor};User Id={UsuarioId};Password={Password};";


                MySqlConnection conn = new MySqlConnection(strConn);
                conn.Open();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "CREATE DATABASE IF NOT EXISTS ESCOLAR";
                cmd.ExecuteNonQuery();
                MessageBox.Show("Base de datos creada con exito");
            }
            catch (MySqlException Ex)
            {
                MessageBox.Show(Ex.Message);
            }
            catch (Exception Ex)
            {
                MessageBox.Show("Error en el sistema");
            }
        }

        private void btnCreaTabla_Click(object sender, EventArgs e)
        {
            {
                EjecutaComando("CREATE TABLE " +
                        "Alumnos (NoControl varchar(8), Nombre varchar(50), Carrera int)");
            }
        }
    }
}

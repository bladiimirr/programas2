using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace AccesoBaseDatos1
{
    public partial class Form1 : Form
    {
        private string Servidor = "MSI\\SQLEXPRESS2022";
        private string Basedatos = "ESCOLAR";
        private string UsuarioId = "sa";
        private string Password = "basedata";

        private void EjecutaComando(string ConsultaSQL)
        {
            try
            {
                string strConn = $"Server={Servidor};" +
                    $"Database={Basedatos};" +
                    $"User Id={UsuarioId};" +
                    $"Password={Password}";

                SqlConnection conn = new SqlConnection(strConn);
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = ConsultaSQL;
                cmd.ExecuteNonQuery();


                llenarGrid();

            }
            catch (SqlException Ex)
            {
                MessageBox.Show(Ex.Message);
            }
            catch (Exception Ex)
            {
                MessageBox.Show("Error en el sistema");
            }
        }
        //try
        //{
        //    string strConn = $"Server={Servidor};" +
        //        $"Database={Basedatos};" +
        //        $"User Id={UsuarioId};" +
        //        $"Password={Password}";

        //    if(chkSQLServer.Checked)
        //    {
        //        SqlConnection conn = new SqlConnection(strConn);
        //        conn.Open();
        //        SqlCommand cmd = new SqlCommand();
        //        cmd.Connection = conn;
        //        cmd.CommandText = ConsultaSQL;
        //        cmd.ExecuteNonQuery();
        //    }

        //    llenarGrid();

        //}
        //catch (SqlException Ex)
        //{
        //    MessageBox.Show(Ex.Message);
        //}
        //catch (Exception Ex)
        //{
        //    MessageBox.Show("Error en el sistema");
        //}

        private void llenarGrid()
        {
            try
            {
                var connectionParams = new
                {
                    Server = Servidor,
                    Database = Basedatos,
                    User = UsuarioId,
                    Pass = Password
                };

                using (var dbConnection = new SqlConnection(
                    $"Data Source={connectionParams.Server};" +
                    $"Initial Catalog={connectionParams.Database};" +
                    $"User ID={connectionParams.User};" +
                    $"Password={connectionParams.Pass};"))
                {
                    dbConnection.Open();

                    const string query = "SELECT * FROM Alumnos";
                    var dataFetcher = new SqlDataAdapter(query, dbConnection);

                    var dataContainer = new DataSet();
                    dataFetcher.Fill(dataContainer, "Estudiantes");

                    dgvAlumnos.DataSource = dataContainer.Tables[0];
                }
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
            


            //try
            //{
            //    string strConn = $"Server={Servidor};" +
            //        $"Database={Basedatos};" +
            //        $"User Id={UsuarioId};" +
            //        $"Password={Password}";

            //    if (chkSQLServer.Checked)
            //    {
            //        SqlConnection conn = new SqlConnection(strConn);
            //        conn.Open();

            //        string sqlQuery = "select * from Alumnos";
            //        SqlDataAdapter adp = new SqlDataAdapter(sqlQuery, conn);

            //        DataSet ds = new DataSet();
            //        adp.Fill(ds, "Alumnos");
            //        dgvAlumnos.DataSource = ds.Tables[0];
            //    }

            //    dgvAlumnos.Refresh();
            //}
            //catch (SqlException Ex)
            //{
            //    MessageBox.Show(Ex.Message);
            //}
            //catch (Exception Ex)
            //{
            //    MessageBox.Show("Error en el sistema");
            //}
        }

        public Form1()
        {
            InitializeComponent();
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


                    SqlConnection conn = new SqlConnection(strConn);
                    conn.Open();
                    SqlCommand cmd = new SqlCommand();
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
            //try
            //{
            //    if (
            //            

            //        if (chkSQLServer.Checked)
            //        {
            //            SqlConnection conn = new SqlConnection(strConn);
            //            conn.Open();
            //            SqlCommand cmd = new SqlCommand();
            //            cmd.Connection = conn;
            //            cmd.CommandText = "INSERT INTO " +
            //                "Alumnos (NoControl, nombre, carrera) " +
            //                "VALUES ('" + txtNoControl.Text +
            //                "', '" + txtNombre.Text +
            //                "', " + txtCarrera.Text + ")";
            //            cmd.ExecuteNonQuery();
            //        }

            //        llenarGrid();
            //    }

            //}
            //catch (SqlException Ex)
           
            //}
        }
        
        private void Form1_Load(object sender, EventArgs e)
        {
            //chkSQLServer.Checked = true;
            llenarGrid();
        }

        private void btnRefrescar_Click(object sender, EventArgs e)
        {
            llenarGrid();
        }

        private void chkSQLServer_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void dgvAlumnos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnCrearBD_Click_1(object sender, EventArgs e)
        {
            try
            {
                var dbConfig = new
                {
                    Host = Servidor,
                    User = UsuarioId,
                    Secret = Password
                };

                using (var dbLink = new SqlConnection(
                    $"Server={dbConfig.Host};" +
                    $"Database=master;" +
                    $"UID={dbConfig.User};" +
                    $"PWD={dbConfig.Secret};"))
                {
                    dbLink.Open();

                    var dbCreator = new SqlCommand
                    {
                        Connection = dbLink,
                        CommandText = "CREATE DATABASE ESCOLAR"
                    };

                    dbCreator.ExecuteNonQuery();
                    MessageBox.Show("La base de datos ESCOLAR se creó correctamente",
                                  "Creacion exitosa",
                                  MessageBoxButtons.OK,
                                  MessageBoxIcon.Information);
                }
            }
            catch (SqlException sqlError)
            {
                MessageBox.Show($"Error SQL: {sqlError.Message}",
                               "Error de base de datos",
                               MessageBoxButtons.OK,
                               MessageBoxIcon.Error);
            }
            catch (Exception unexpectedError)
            {
                MessageBox.Show("Ocurrió un error inesperado en el sistema",
                               "Error general",
                               MessageBoxButtons.OK,
                               MessageBoxIcon.Warning);
            }
           
            //try
            //{
            //    string strConn = $"Server={Servidor};" +
            //        $"Database=master;" +
            //        $"User Id={UsuarioId};" +
            //        $"Password={Password}";

            //    if (chkSQLServer.Checked)
            //    {
            //        SqlConnection conn = new SqlConnection(strConn);
            //        conn.Open();

            //        SqlCommand cmd = new SqlCommand();
            //        cmd.Connection = conn;
            //        cmd.CommandText = "CREATE DATABASE ESCOLAR";
            //        cmd.ExecuteNonQuery();

            //    }


            
        }

        private void btnCreaTabla_Click_1(object sender, EventArgs e)
        {
            EjecutaComando("CREATE TABLE " +
                    "Alumnos (NoControl varchar(10), nombre varchar(50), carrera int)");  }
 private void btnActualizar_Click(object sender, EventArgs e) {
            try{
                 if (dgvAlumnos.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Selecciona un registro para actualizar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                DataGridViewRow filaSeleccionada = dgvAlumnos.SelectedRows[0];
                string noControl = filaSeleccionada.Cells["NoControl"].Value.ToString();
     
                if (string.IsNullOrWhiteSpace(txtNoControl.Text) ||
                    string.IsNullOrWhiteSpace(txtNombre.Text) ||
                    string.IsNullOrWhiteSpace(txtCarrera.Text))
                {
                    MessageBox.Show("Todos los campos son obligatorios.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                string cadenaConexion = $"Server={Servidor};Database={Basedatos};User Id={UsuarioId};Password={Password};";

                using (SqlConnection conexion = new SqlConnection(cadenaConexion))
                {
                    conexion.Open();

                    string consultaSQL = "UPDATE Alumnos SET Nombre = @Nombre, Carrera = @Carrera WHERE NoControl = @NoControl";

                    using (SqlCommand comando = new SqlCommand(consultaSQL, conexion))
                    {
                        // Asignar valores a los parámetros
                        comando.Parameters.AddWithValue("@NoControl", txtNoControl.Text);
                        comando.Parameters.AddWithValue("@Nombre", txtNombre.Text);
                        comando.Parameters.AddWithValue("@Carrera", txtCarrera.Text);

                        int registrosActualizados = comando.ExecuteNonQuery();

                        if (registrosActualizados > 0)
                        {
                            MessageBox.Show("Registro actualizado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            llenarGrid(); // Refrescar los datos
                        }
                        else
                        {
                            MessageBox.Show("No se encontró el alumno a actualizar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }}}}
            catch (SqlException ex)
            {
                MessageBox.Show($"Error en la base de datos: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error inesperado: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btnBorrar_Click(object sender, EventArgs e)
        {
            

            // Verifica si hay una fila seleccionada antes de intentar eliminar
            if (dgvAlumnos.SelectedRows.Count == 0)
            {
                MessageBox.Show("Por favor, selecciona un alumno para eliminar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Obtiene el número de control del alumno seleccionado
            string noControl = dgvAlumnos.SelectedRows[0].Cells["NoControl"].Value.ToString();

            // Solicita confirmación antes de eliminar el registro
            DialogResult confirmacion = MessageBox.Show(
                $"¿Estás seguro de que deseas eliminar al alumno con No. Control {noControl}?",
                "Confirmación de eliminación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (confirmacion != DialogResult.Yes)
            {
                return;
            }

            try
            {
                // Construye la cadena de conexión con los datos del servidor
                string cadenaConexion = $"Server={Servidor};Database={Basedatos};User Id={UsuarioId};Password={Password};";

                using (SqlConnection conexion = new SqlConnection(cadenaConexion))
                {
                    conexion.Open();

                    // Prepara la consulta para eliminar el alumno de la base de datos
                    string consultaSQL = "DELETE FROM Alumnos WHERE NoControl = @NoControl";

                    using (SqlCommand comando = new SqlCommand(consultaSQL, conexion))
                    {
                        comando.Parameters.AddWithValue("@NoControl", noControl);
                        int filasAfectadas = comando.ExecuteNonQuery();

                        if (filasAfectadas > 0)
                        {
                            MessageBox.Show("El alumno ha sido eliminado correctamente.", "Operación exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            llenarGrid(); // Actualiza la vista con los datos actualizados
                        }
                        else
                        {
                            MessageBox.Show("No se encontró el alumno en la base de datos.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"Error al conectarse con la base de datos: {ex.Message}", "Error de SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error inesperado: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }



        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string Buscar = txtNoControl.Text.Trim();

            if (string.IsNullOrEmpty(Buscar))
            {
                MessageBox.Show("Ingresa un dato de búsqueda.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string strConn = $"Server={Servidor};Database={Basedatos};User Id={UsuarioId};Password={Password};";

                using (SqlConnection conn = new SqlConnection(strConn))
                {
                    conn.Open();

                    // Consulta SQL con parámetros (búsqueda flexible)
                    string query = @"
                SELECT * FROM Alumnos 
                WHERE NoControl LIKE @Criterio OR 
                      Nombre LIKE @Criterio
            ";

                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    adapter.SelectCommand.Parameters.AddWithValue("@Criterio", $"%{Buscar}%");

                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    // Mostrar resultados en el DataGridView
                    dgvAlumnos.DataSource = dt;

                    if (dt.Rows.Count == 0)
                    {
                        MessageBox.Show("No se encontraron resultados.", "Notificacion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"Error de SQL: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            mysql form2 = new mysql();

            // Mostrar Form2
            form2.Show();

            // Cerrar Form1
           this.Hide();
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection.Emit;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace interfaz_grafica
{
    public partial class Gestión_de_Contactos : Form
    {
        public Gestión_de_Contactos()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void mENUToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void Gestión_de_Contactos_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
         
            }
        
        

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click_1(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            string nombre = Nombre.Text.Trim();
            string telefono = Telefono.Text.Trim();
            string correo = Correo.Text.Trim();

            // Verificar que los campos no estn vacíos
            if (string.IsNullOrWhiteSpace(Nombre.Text))
            {
                MessageBox.Show("El nombre es obligatorio.", "Falta información", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (string.IsNullOrWhiteSpace(Telefono.Text))
            {
                MessageBox.Show("Necesitamos un telefono de contacto", "Falta información", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (string.IsNullOrWhiteSpace(Correo.Text))
            {
                MessageBox.Show("No olvides incluir un correo electrónico.", "Falta información", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                // Si todos los campos estAn llenos, anadimos el contacto
                MessageBox.Show("El contacto se agrego correctamente.", "Contacto agregado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                listBox1.Items.Add($"{Nombre.Text} - {Telefono.Text} - {Correo.Text}");

            
                Nombre.Text = "";
                Telefono.Text = "";
                Correo.Text = "";
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // Verificar si se ha seleccionado un contacto
            if (listBox1.SelectedIndex != -1)
            {
                // Confirmar la eliminación del contacto
                var confirmResult = MessageBox.Show("¿Estás seguro de que quieres eliminar este contacto?", "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (confirmResult == DialogResult.Yes)
                {
                    listBox1.Items.RemoveAt(listBox1.SelectedIndex);
                    MessageBox.Show("El contacto ha sido eliminado.", "Eliminado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                // Si no se selecciona un contacto
                MessageBox.Show("¡Por favor, selecciona un contacto primero!", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void aCERCADEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Desarrollado por: \n\n Bladimir Nava vergara\n 23760330\n 4SA", "Información sobre el autor", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Nombre.Text = "";
            Telefono.Text = "";
            Correo.Text = "";
        }

        // USO DE KEYPRESS PARA BLOQUEAR LETRAS Y LIMITAR LA LONGITUD DE NUMEROS EN LA TEXBOX DE TELEFONO
        private void Telefono_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)8)
            {
                e.Handled = true;
            }
            else if (Telefono.Text.Length >= 10 && e.KeyChar != (char)8)
            {
                // Si el texto ya tiene 10 caracteres, evitar que se ingrese más
                e.Handled = true;
            }
        }

        private void Nombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Permitir solo letras en el texbox del nombre
            if (!char.IsLetter(e.KeyChar) && e.KeyChar != (char)8)
            {
                e.Handled = true;  // Cancela la tecla presionada si no es una letra ni retroceso
            }
        }

        private void sALIRToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
   


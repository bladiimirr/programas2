using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace prueba_de_botones
{
    public partial class soloNumeros : Form
    {
        public soloNumeros()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            soloLetras form2 = new soloLetras();

            // Mostrar el segundo formulario
            form2.Show();

            // Cerrar el formulario actual (Form1)
            this.Hide();
        }
    }
}

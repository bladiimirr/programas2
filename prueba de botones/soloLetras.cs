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
    public partial class soloLetras : Form
    {
        public soloLetras()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            soloNumeros form2 = new soloNumeros();
            form2.Show();

            this.Hide();
        }

        private void validarLetras1_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            ConfirmacionBoton form2 = new ConfirmacionBoton();
            form2.Show();

            this.Hide();  
        }
    }
}

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

    /**/
    public partial class ConfirmacionBoton : Form
    {
        public ConfirmacionBoton()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            soloLetras form2 = new soloLetras();
            form2.Show();

            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Compiladores_Interpretes_B
{
    public partial class EntradaSalida : Form
    {

        List<string> cadenasPuestas;

        private double val;
        public double getVal { get { return val; } }
        public EntradaSalida(List<string> cad)
        {
            InitializeComponent();
            val = 0;
            cadenasPuestas = cad;
        }

        private void EntradaSalida_Load(object sender, EventArgs e)
        {
            Salida.Lines = cadenasPuestas.ToArray();
            Entrada.Focus();
            this.Location = new Point(100, 100);
            TopMost = true;
        }



        public void update(List<string> cad)
        {
            cadenasPuestas = cad;
            Salida.Lines = cadenasPuestas.ToArray();
            this.Focus();
            Entrada.Focus();
            

        }
        private void boton_OK_Click(object sender, EventArgs e)
        {
            if (this.Modal)
            {
                try
                {
                    val = double.Parse(Entrada.Text);
                    this.Close();

                }
                catch
                {
                    MessageBox.Show("Valor no valido");
                }

            }
        }
    }
}

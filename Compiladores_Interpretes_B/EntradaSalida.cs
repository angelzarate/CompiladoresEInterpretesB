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
        public EntradaSalida(List<string> cad)
        {
            InitializeComponent();

            cadenasPuestas = cad;
        }

        private void EntradaSalida_Load(object sender, EventArgs e)
        {
            Salida.Lines = cadenasPuestas.ToArray();
            Entrada.Focus();
            TopMost = true;
        }



        public void update(List<string> cad)
        {
            cadenasPuestas = cad;
            Salida.Lines = cadenasPuestas.ToArray();
            Entrada.Focus();

        }
        private void boton_OK_Click(object sender, EventArgs e)
        {

        }
    }
}

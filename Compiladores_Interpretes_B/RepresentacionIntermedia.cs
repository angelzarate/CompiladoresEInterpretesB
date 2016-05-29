using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Compiladores_Interpretes_B
{
    class RepresentacionIntermedia
    {
        public struct Cuadruplo
        {
            public object operador;
            public object argumento1;
            public object argumento2;
            public object resultado;

            public Cuadruplo(object op, object ar1, object ar2, object res)
            {
                operador = op;
                argumento1 = ar1;
                argumento2 = ar2;
                resultado = res;
            }
        }


        public struct Simbolo
        {
            string nombre;
            object valor;
            object tipo;
        }


        private List<Cuadruplo> tablaDeCuadruplos;
        public List<Cuadruplo> gsTablaCuadruplo { get { return tablaDeCuadruplos; } set { tablaDeCuadruplos = value; } }


        public int M { get { return tablaDeCuadruplos.Count; } }

        private List<Simbolo> tablaDeSimbolos;
        public List<Simbolo> gsTablaDeSimbolos { get { return tablaDeSimbolos; } set { tablaDeSimbolos = value; } }


        public RepresentacionIntermedia()
        {
            tablaDeCuadruplos = new List<Cuadruplo>();
            tablaDeSimbolos = new List<Simbolo>();

        }

        public void insertaCuadruplo(string op, string a1, string a2, string res)
        {
            tablaDeCuadruplos.Add(new Cuadruplo(op, a1, a2, res));
        }

        /// <summary>
        /// Rellena un salto antes generado con la direccion correspondiente
        /// </summary>
        /// <param name="numCuadruplo"></param>
        /// <param name="dir"></param>
        public void backpatch(List<int> listCuadruplo, int dir)
        {
            Cuadruplo aux;
            try
            {
                foreach (int i in listCuadruplo)
                {
                    aux = tablaDeCuadruplos[i];
                    aux.resultado = dir;
                    tablaDeCuadruplos.RemoveAt(i);
                    tablaDeCuadruplos.Insert(i, aux);
                }
            }
            catch
            {

            }
        }

        public void imprimeTablaCuadruplos(DataGridView tabla)
        {
            try
            {
                int i = 0;
                tabla.Rows.Clear();
                foreach (Cuadruplo c in tablaDeCuadruplos)
                {
                    tabla.Rows.Add();
                    tabla.Rows[i].Cells[0].Value = i;
                    tabla.Rows[i].Cells[1].Value = c.operador;
                    tabla.Rows[i].Cells[2].Value = c.argumento1;
                    tabla.Rows[i].Cells[3].Value = c.argumento2;
                    tabla.Rows[i].Cells[4].Value = c.resultado;
                    i++;
                }
            }
            catch(Exception exc)
            {
                MessageBox.Show("" + exc);
            }
        }




    }
}

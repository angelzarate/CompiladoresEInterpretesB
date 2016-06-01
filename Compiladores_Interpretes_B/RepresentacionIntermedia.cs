using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Compiladores_Interpretes_B
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
        public string nombre;
        public object valor;
        public object tipo;
        public List<int> arreglo; // Estructura que representa la costruccion de un arreglo
        public Simbolo(string nom, object val, object t, List<int> arr)
        {
            nombre = nom;
            valor = val;
            tipo = t;
            arreglo = arr;
        }

    }


    class RepresentacionIntermedia
    {
      
        private List<Cuadruplo> tablaDeCuadruplos;
        public List<Cuadruplo> gsTablaCuadruplo { get { return tablaDeCuadruplos; } set { tablaDeCuadruplos = value; } }


        private List<Simbolo> tablaDeSimbolos;
        public List<Simbolo> gsTablaDeSimbolos { get { return tablaDeSimbolos; } set { tablaDeSimbolos = value; } }



        public int M { get { return tablaDeCuadruplos.Count; } }


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
                if (listCuadruplo != null)
                {
                    foreach (int i in listCuadruplo)
                    {
                        aux = tablaDeCuadruplos[i];
                        aux.resultado = dir;
                        tablaDeCuadruplos.RemoveAt(i);
                        tablaDeCuadruplos.Insert(i, aux);
                    }
                }
            }
            catch
            {

            }
        }

        public void eliminaCuadruplo(int i)
        {
            try
            {
                tablaDeCuadruplos.RemoveAt(i);
            }
            catch
            {

            }
        }


        public void imprimeTablaDeSimbolos(DataGridView tabla)
        {
            try
            {
                List<int> ltipo;
                string str;
                int i = 0;
                tabla.Rows.Clear();
                foreach(Simbolo s in tablaDeSimbolos)
                {
                    str = "";
                    tabla.Rows.Add();
                    tabla.Rows[i].Cells[0].Value = i;
                    tabla.Rows[i].Cells[1].Value = s.nombre;
                    tabla.Rows[i].Cells[2].Value = s.valor;
                    
                    foreach(int n in s.arreglo)
                    {
                        str += n.ToString() + " "; 
                    }
                    tabla.Rows[i].Cells[3].Value = str;
                    ltipo = s.arreglo;
                    i++;
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



        public void insertaSimbolo(string nom, object val, object tipo, List<int> arr)
        {
            tablaDeSimbolos.Add(new Simbolo(nom, val, tipo, arr));
        }


        public List<int> getTipoSimbolo(string nom)
        {
            Simbolo simb =  tablaDeSimbolos.Find(delegate(Simbolo s)
            {
                return s.nombre.Equals(nom);
            });

            
            return simb.arreglo;
        }



    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiladores_Interpretes_B
{
    class Interprete
    {
        private List<Cuadruplo> tablaDeCuadruplos;
        public List<Cuadruplo> getSetTablaDeCuadruplos { get { return tablaDeCuadruplos; } set { tablaDeCuadruplos = value; } }


        private List<Simbolo> tablaDeSimbolos;
        public List<Simbolo> getSettablaDeSimbolos { get { return tablaDeSimbolos; } set { tablaDeSimbolos = value; } }


        private int contadorProg;
        public int getContadorProg { get { return contadorProg; } }


        public Interprete(List<Cuadruplo> tc, List<Simbolo> ts)
        {
            this.tablaDeSimbolos = ts;
            this.tablaDeCuadruplos = tc;
        }



        public void EjecutarPrograma()
        {
            contadorProg = this.buscaInicio();
            if(contadorProg != -1)
            {

            }
        }



        private int buscaInicio()
        {
            int inicio = -1;
            try
            {
                inicio = tablaDeSimbolos.FindIndex(delegate(Simbolo s)
                {
                    return s.nombre.Equals("principal");
                });

                if (inicio >= 0 && inicio < tablaDeSimbolos.Count) // encontro el simbolo que contiene el simbolo que contiene la funcion principal
                {

                    inicio = (int)tablaDeSimbolos[inicio].valor;
                }
                else
                {
                    inicio = -1;
                }
            }
            catch
            {

            }
            return inicio;
        }





        private void rutinaAritmetica( Cuadruplo cuad)
        {
            switch(cuad.operador.ToString())
            {
                case "+":

                break;

                case "-":

                break;

                case "*":

                break;

                case "/":

                break;

            }
        }









      

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiladores_Interpretes_B
{
    class Interprete
    {

        #region Variables miembro de la clase

        private List<Cuadruplo> tablaDeCuadruplos;
        public List<Cuadruplo> getSetTablaDeCuadruplos { get { return tablaDeCuadruplos; } set { tablaDeCuadruplos = value; } }


        private List<Simbolo> tablaDeSimbolos;
        public List<Simbolo> getSettablaDeSimbolos { get { return tablaDeSimbolos; } set { tablaDeSimbolos = value; } }


        private int contadorProg;
        public int getContadorProg { get { return contadorProg; } }
        EntradaSalida io;

        bool error;
        string errorSemantico;



        private List<Simbolo> listaParametroLocales;
        private List<int> pilaDeLLamadas;
        private List<Simbolo> parametrosGenerales;
        private object valueReturn;


        List<string> ListaDeCadenas;


        #endregion


        #region Constructor
        public Interprete(List<Cuadruplo> tc, List<Simbolo> ts)
        {
            this.tablaDeSimbolos = ts;
            this.tablaDeCuadruplos = tc;
            listaParametroLocales = new List<Simbolo>();
            pilaDeLLamadas = new List<int>();
            parametrosGenerales = new List<Simbolo>();
            ListaDeCadenas = new List<string>();
            error = false;
            io = new EntradaSalida(ListaDeCadenas);
            io.Show();
           
        }

        #endregion

        #region Ejecucion de programa
        public void EjecutarPrograma()
        {
            contadorProg = this.buscaInicio("principal");
            pilaDeLLamadas.Insert(0, contadorProg);
            if(contadorProg != -1)
            {
                while(tablaDeCuadruplos[contadorProg].operador.Equals("END") == false && error == false)
                {

                    procesaCuadruplos(tablaDeCuadruplos[contadorProg]);
                }
                ListaDeCadenas.Add("Programa Terminado");
                io.update(ListaDeCadenas);
            }

        }



        #endregion


        #region Busca inicio de programa
        private int buscaInicio(string simbolo)
        {
            int inicio = -1;
            try
            {
                inicio = tablaDeSimbolos.FindIndex(delegate(Simbolo s)
                {
                    return s.nombre.Equals(simbolo);
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

        #endregion


        #region indice respecto a la tabla de simbolos
        public int buscaSimbolo(string simbolo)
        {
            int inicio = -1;
            try
            {
                inicio = tablaDeSimbolos.FindIndex(delegate(Simbolo s)
                {
                    return s.nombre.Equals(simbolo);
                });
            }
            catch
            {

            }
            return inicio;




        }

        #endregion


        #region procesador de cuadruplos
        public void procesaCuadruplos(Cuadruplo cuad)
        {
            string operador = cuad.operador.ToString();
            if (operador.Equals("*") || operador.Equals("/") || operador.Equals("+") || operador.Equals("-"))
            {
                this.rutinaAritmetica(cuad);
                contadorProg++;
            }
            else
            {
                if (operador.Equals("="))
                {
                    this.rutinaAsignacion(cuad);
                    contadorProg++;
                }
                else
                {
                    if (operador.Equals("goto") || operador.Equals("if <") || operador.Equals("if <=") || operador.Equals("if ==") || operador.Equals("if !=") || operador.Equals("if >") || operador.Equals("if >="))
                    {
                        this.rutinaGoto(cuad);
                    }
                    else
                    {
                        if (operador.Equals("param"))
                        {
                            int i = buscaSimbolo(cuad.resultado.ToString());
                            if (i != -1)
                            {
                                Simbolo s = tablaDeSimbolos[i];
                                parametrosGenerales.Insert(0,new Simbolo(s.nombre, s.valor, s.tipo, s.arreglo)); 
                                contadorProg++;
                            }
                            else
                            {
                                error = true;
                                errorSemantico = "No existe el simbolo " + cuad.resultado.ToString();
                            }

                        }
                        else
                        {
                            if (operador.Equals("call f") || operador.Equals("call p"))
                            {

                                int i = buscaSimbolo(cuad.argumento1.ToString());

                                if (i != -1 && ( tablaDeSimbolos[i].tipo.ToString().Equals("func") || tablaDeSimbolos[i].tipo.ToString().Equals("proc") ))
                                {
                                    pilaDeLLamadas.Insert(0,contadorProg);
                                    Simbolo s = tablaDeSimbolos[i];
                                    contadorProg = (int)s.valor;
                                   // listaParametroLocales.Clear();
                                    for (i = 0; i < int.Parse(cuad.argumento2.ToString()); i++)
                                    {
                                        listaParametroLocales.Insert(0,parametrosGenerales[0]);
                                        parametrosGenerales.RemoveAt(0);
                                    }
                                    //listaParametroLocales.Reverse(); 
                                }
                                else
                                {
                                    error = true;
                                    errorSemantico = "No existe el simbolo " + cuad.resultado.ToString();
                                }


                            }
                            else
                            {

                                if (operador.Equals("return"))
                                {
                                    this.rutinaReturn(tablaDeCuadruplos[contadorProg]);
                                }
                                else
                                {
                                    if (operador.Equals("print"))
                                    {
                                        imprimir(tablaDeCuadruplos[contadorProg]);
                                        contadorProg++;
                                    }
                                    else
                                    {
                                        if(operador.Equals("read"))
                                        {
                                            Read(tablaDeCuadruplos[contadorProg]);
                                            contadorProg++;
                                        }

                                        else
                                        {
                                            contadorProg++;
                                        }
                                    }
                                }

                            }
                        }
                    }
                }
            }
        }
        #endregion




        #region

        private void Read(Cuadruplo cuad)
        {
            io.Close();
            io = new EntradaSalida(ListaDeCadenas);
            io.update(ListaDeCadenas);
            io.ShowDialog();

            setValue(cuad.resultado.ToString(), io.getVal);
            ListaDeCadenas.Add(io.getVal.ToString());
            io = new EntradaSalida(ListaDeCadenas);
            io.update(ListaDeCadenas);
            io.Show();
            



        }

        #endregion









        #region Print

        private void imprimir(Cuadruplo cuad)
        {
            int t = 0;
            object obj = getValue(cuad.argumento1.ToString(), ref t);
            if (obj != null)
            {
                this.ListaDeCadenas.Add(obj.ToString());

                io.update(ListaDeCadenas);
            }
            else
            {
                error = true;
                errorSemantico = "No existe el argumento " + cuad.argumento1.ToString();
            }

        }

        #endregion



        #region Rutina de Return
        private void rutinaReturn(Cuadruplo cuad)
        {
            int tipo = 0;
            if(cuad.resultado != null)
            {
                valueReturn = getValue(cuad.resultado.ToString(), ref tipo);
                contadorProg = pilaDeLLamadas[0];
                pilaDeLLamadas.RemoveAt(0);
                if (pilaDeLLamadas.Count!=0)
                {
                    if (tablaDeCuadruplos[contadorProg].resultado.ToString().Equals("") == false)
                    {

                        setValue(tablaDeCuadruplos[contadorProg].resultado.ToString(), valueReturn);
                        tipo = int.Parse(tablaDeCuadruplos[contadorProg].argumento2.ToString());
                        for(int i = 0; i< tipo; i++)
                        {
                            listaParametroLocales.RemoveAt(0);
                        }
                    }
                    contadorProg++;
                }
                else
                {
                    contadorProg = tablaDeCuadruplos.Count - 1;

                }
            }
            else
            {


            }
           





        }

        #endregion




        #region Rutina de operaciones Aritmaticas

        private void rutinaAritmetica(Cuadruplo cuad)
        {
            int tipo = 0, tipo2 = 0;
            object val2 = getValue(cuad.argumento2.ToString(), ref tipo2), val1 = getValue(cuad.argumento1.ToString(), ref tipo);
            double resultado = 0;

            string ar1 = cuad.argumento1.ToString(), ar2 = cuad.argumento2.ToString();
            if(ar1[0] == '$') // si corresponde a un parametro
            {
                ar1 = ar1.Replace("$", "");
                
                tipo = int.Parse(ar1); // Regresa el apuntador a la lista de parametros
                
                val1 = listaParametroLocales[tipo-1].valor;
              

            }
            if(ar2[0] == '$') //si corresponde a un parametro
            {
                ar2 = ar2.Replace("$", "");
                tipo2 = int.Parse(ar2);
                val2 = listaParametroLocales[tipo2-1].valor;
            }
            /* typear las variables */

            if (val1 != null && val2 != null)
            {
               
                switch (cuad.operador.ToString())
                {
                    case "+":
                        resultado = double.Parse(val1.ToString()) + double.Parse(val2.ToString());    
                    break;

                    case "-":
                        resultado = double.Parse(val1.ToString()) - double.Parse(val2.ToString());
                    break;

                    case "*":
                        resultado = double.Parse(val1.ToString()) * double.Parse(val2.ToString());
                    break;

                    case "/":
                        resultado = double.Parse(val1.ToString()) / double.Parse(val2.ToString());
                    break;

                }
                setValue(cuad.resultado.ToString(),resultado);
            }
            else
            {

                if(val1 != null && val2 == null)
                {
                    if(cuad.operador.ToString().Equals("-"))
                    {
                        resultado = double.Parse(val1.ToString()) * -1;
                        setValue(cuad.resultado.ToString(), resultado);
                    }
                }
            }

        }

        #endregion

        #region rutinaDeAsignacion
        private void rutinaAsignacion(Cuadruplo cuad)
        {
            string res = cuad.resultado.ToString();
            string arg1 = cuad.argumento1.ToString();
            string arg2 = cuad.argumento2.ToString();

            string[] separador = { "[", "]", " "};
            string[] arreglo; // nombre del arreglo y desplazamiento
            int desp;

            string despArray;
            object value = null;
            int tipo = 0;
            double number;

            List<object> arr;

            if(res.Contains('[')==true) // si se le asigna a un arreglo  a[i] = x
            {
                arreglo = res.Replace(" ","").Split(separador, StringSplitOptions.RemoveEmptyEntries);
                res = arreglo[0]; // nombre del arreglo
                despArray = arreglo[1];
                desp = this.getDesplazamientoArray(res, despArray);
                if (desp != -1)
                {
                    try
                    {
                        value = getValue(res, ref tipo);
                        arr = value as List<object>;
                        value = getValue(arg1, ref tipo);
                        arr[desp] = value;
                        value = arr;
                        setValue(res, value);
                    }
                    catch
                    {
                        error = true;
                        errorSemantico = "Error en acceso del arreglo " + res;

                    }
                }

            }
            else
            {
                if(arg1.Contains('[') == true)
                {
                    arreglo = arg1.Replace(" ","").Split(separador, StringSplitOptions.RemoveEmptyEntries);
                    arg1 = arreglo[0]; // nombre
                    despArray = arreglo[1];

                    desp = this.getDesplazamientoArray(arg1, despArray);
                    if (desp != -1)
                    {
                        try
                        {
                            value = getValue(arg1, ref tipo);
                            arr = value as List<object>;
                            value = getValue(res, ref tipo);
                            value = arr[desp] ;
                            setValue(res, value);
                        }
                        catch
                        {
                            error = true;
                            errorSemantico = "Error en acceso del arreglo " + res;

                        }
                    }

                }
                else
                {
                    bool result = double.TryParse(arg1, out number);
                    if (result)
                    {
                        value = number;
                    }
                    else
                    {
                        value = getValue(arg1, ref tipo);
                    }
                    if (value != null)
                    {
                        setValue(res, value);
                    }

                }

            }
        }

        #endregion



        private int getDesplazamientoArray(string array, string desp)
        {
            int i = buscaSimbolo(array);
            int tipo=0;
            
            if(i != -1)
            {
                Simbolo s = tablaDeSimbolos[i];
                if(s.arreglo.Count > 1)
                {
                    i = int.Parse(getValue(desp,ref tipo).ToString());
                    tipo = s.arreglo[s.arreglo.Count - 1];
                    switch(tipo)
                    {
                        case 1:
                            i = i / sizeof(int);
                        break;
                        case 2:
                            i = i / sizeof(float);
                        break;
                        case 3:
                            i = i / sizeof(char);
                        break;

                    }

                }
                else
                {
                    error = true;
                    this.errorSemantico = "la variable " + array + " no es un arreglo";
                }
            
            }
            return i;

        }


        #region Rutina de saltos


        private void rutinaGoto(Cuadruplo cuad)
        {
            int tipo = 0, tipo2 = 0;
            object val2 = getValue(cuad.argumento2.ToString(), ref tipo2), val1 = getValue(cuad.argumento1.ToString(), ref tipo);
            bool band = false;
            double v1= 0, v2= 0;
            if (cuad.operador.Equals("goto") == false)
            {
                v1 = double.Parse(val1.ToString());
                v2 = double.Parse(val2.ToString());
            }
            switch(cuad.operador.ToString())
            {
                case "goto":
                    band = true;
                break;
                
                case "if <":
                    if ( v1 < v2)
                    {
                        band = true;
                    }
                break;

                case "if <=":
                    if (v1 <= v2)
                    {
                        band = true;
                    }
                break;



                case "if >":
                    if (v1 > v2)
                    {
                        band = true;
                    }
                break;

                case "if >=":
                    if (v1 >= v2)
                    {
                        band = true;
                    }
                break;


                case "if ==":
                    if (v1 == v2)
                    {
                        band = true;
                    }
                break;




                case "if !=":
                    if (v1 != v2)
                    {
                        band = true;
                    }
                break;

            }

            if(band == true)
            {
                contadorProg = int.Parse(cuad.resultado.ToString());
            }
            else
            {
                contadorProg++;
            }

        }

        #endregion

        #region Get Value
        public object getValue(string variable,ref int tipo)
        {
            object obj = null;
            double number;
            bool result = double.TryParse(variable, out number); // si es un numero
            if (result)
            {
                obj = number;
            }
            else
            {




                int i = buscaSimbolo(variable);
                if (i != -1)
                {
                    obj = tablaDeSimbolos[i].valor;
                    if (tablaDeSimbolos[i].arreglo.Count == 1) // tipo sencillo char, int , float
                    {
                        tipo = tablaDeSimbolos[i].arreglo[0];
                    }
                    else
                    {
                        if (tablaDeSimbolos[i].arreglo.Count == 0)  // si es un temporal los temporales no tienen tipos
                        {
                            tipo = 0;
                        }
                        else
                        {
                            tipo = 5; // arreglo
                        }
                    }

                }
                else
                {
                    if(variable.Length > 0 && variable[0] == '$')  // si hace referencia a un parametro enviado previamente
                    {
                        tipo = int.Parse(variable.Replace("$","")); // Regresa el apuntador a la lista de parametros
                        obj = listaParametroLocales[tipo - 1].valor; // obtiene el valor por copia del parametro
                    }
                }


            }
            return obj;
        }


        # endregion



        #region SetValue


        public void setValue(string variable, object val)
        {
            Simbolo aux;
            int i = buscaSimbolo(variable);
            if (i != -1)
            {
                aux = this.tablaDeSimbolos[i];
                aux.valor = val;
                this.tablaDeSimbolos.RemoveAt(i);
                this.tablaDeSimbolos.Insert(i, aux);
            }
            else
            {
                if (variable[0] == '$')  // si hace referencia a un parametro enviado previamente
                {
                    i = int.Parse(variable.Replace("$", "")); // Regresa el apuntador a la lista de parametros
                    aux = listaParametroLocales[i - 1]; // obtiene el valor por copia del parametro
                    aux.valor = val;
                    this.tablaDeSimbolos.RemoveAt(i);
                    this.tablaDeSimbolos.Insert(i, aux);

                }
                error = true;

            }

        }

        #endregion

    }
}

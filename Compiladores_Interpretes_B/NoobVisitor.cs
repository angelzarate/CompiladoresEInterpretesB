using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiladores_Interpretes_B
{
    class NoobVisitor: NoobBaseVisitor<object>
    {
       

        #region estructura Arreglo

        /// <summary>
        ///  Estructura para acceder a una posicion del arreglo
        /// </summary>
        public struct structArray
        {
            public string nombre;
            public string address;
            public int apArray;

            public structArray(string nom, string add, int ap)
            {
                nombre = nom;
                address = add;
                apArray = ap;

            }

            /* Constructor por copia */
            public structArray(structArray arr1)
            {
                nombre = arr1.nombre;
                address = arr1.nombre;
                apArray = arr1.apArray;
            }
        }
        #endregion

        #region atributos de la clase
        private int tipo;
        private RepresentacionIntermedia repInt;
        public RepresentacionIntermedia gsRepInt { get { return repInt; } }


        
        private int temp;

        #endregion

        #region constructor
        public NoobVisitor()
        {
            repInt = new RepresentacionIntermedia();
            tipo = -1;

        }
        #endregion

        #region creacion de variables temporales
        private string creaTemp()
        {
            string t = " _T " + temp.ToString();
            temp++;
            return t;
        }

        #endregion


        # region programa
        public override object VisitPrograma(NoobParser.ProgramaContext context)
        {
            object obj = Visit(context.sentencia());
            List<int> s = obj as List<int>;

            obj = Visit(context.fin());
            int m = int.Parse(obj.ToString());
            if(s != null)
            {
                repInt.backpatch(s, m);

            }
            return null;
        }


        public override object VisitFinal(NoobParser.FinalContext context)
        {
            int m = repInt.M;
            repInt.insertaCuadruplo("END", "", "", "");
            return m;
        }




        public override object VisitBloqSent(NoobParser.BloqSentContext context)
        {
            return Visit(context.sentencia()); 
            //return base.VisitBloqSent(context);
        }

        public override object VisitBloqRec(NoobParser.BloqRecContext context)
        {
            List<int> Lnext, l2Next;
            int M;
            object obj;
            obj = Visit(context.bloque());
            l2Next = obj as List<int>;
            M = repInt.M;
            if (l2Next != null)
            {
                repInt.backpatch(l2Next, M);
            }
            obj = Visit(context.sentencia());
            Lnext = obj as List<int>;
            if(Lnext == null)
            {
                Lnext = new List<int>();
            }
            return Lnext;
            //return base.VisitBloqRec(context);
        }



        public override object VisitSentBloque(NoobParser.SentBloqueContext context)
        {
            object obj = Visit(context.bloque());
            List<int> s = obj as List<int>;
            if (s == null)
            {
                s = new List<int>();
            }
            return s;
            //return base.VisitSentBloque(context);
        }


#endregion


        #region Expresiones Aritmeticas
        public override object VisitInt(NoobParser.IntContext context)
        {
            string s = context.INT().GetText();
            return int.Parse(s);
        }

        public override object VisitIde(NoobParser.IdeContext context)
        {
            string obj = context.ID().GetText(); 
            return obj;
        }




        public override object VisitAddSub(NoobParser.AddSubContext context)
        {

            object left = Visit(context.expr(0));
            object right = Visit(context.expr(1));
            string oper = " ", res = creaTemp();
            if (context.op.Type == NoobParser.ADD)         //Suma
            {
                oper = "+";
            }
            else                                                // Resta
            {
                oper = "-";
               
            }
            repInt.insertaCuadruplo(oper, left.ToString(), right.ToString(), res);

            return res;

        }

        public override object VisitMulDiv(NoobParser.MulDivContext context)
        {
            object left = Visit(context.expr(0));
            object right = Visit(context.expr(1));
            string oper = " ", res = creaTemp();

            if (context.op.Type == NoobParser.MUL) // Multiplicacion
            {
                oper = "*";

            }
            else                                      // Divicion
            {
                oper = "/";
            }
            repInt.insertaCuadruplo(oper, left.ToString(), right.ToString(), res);
            return res;
        }
        public override object VisitId(NoobParser.IdContext context)
        {

            return context.var.GetText();
        }

       

        public override object VisitParens(NoobParser.ParensContext context)
        {
            return Visit(context.expr());
        }






        public override object VisitAsignacion(NoobParser.AsignacionContext context)
        {
            object a1 = Visit(context.expr());
            //object a1 = base.VisitAsignacion(context);
            this.repInt.insertaCuadruplo("=", a1.ToString(), "", context.var.GetText());
            return context.var.GetText();
        }

        public override object VisitMenos(NoobParser.MenosContext context)
        {
            object a1 = Visit(context.expr());
            //object a1 =  base.VisitMenos(context);
            string s = creaTemp();
            this.repInt.insertaCuadruplo("-", a1.ToString(), "", s);
            return s;


        }

        #endregion

        #region booleanos
        public override object VisitBsencillo(NoobParser.BsencilloContext context)
        {
            object left = Visit(context.expr(0));
            object right = Visit(context.expr(1));
            List<List<int>> ltrueFalse = new List<List<int>>();
            ltrueFalse.Add(new List<int>()); // lista True
            ltrueFalse.Add(new List<int>()); // lista False
            ltrueFalse[0].Add(repInt.M);
            ltrueFalse[1].Add(repInt.M + 1);

            this.repInt.insertaCuadruplo("if " + context.rel.Text,  left.ToString(),right.ToString(), "____");
            this.repInt.insertaCuadruplo("goto", "", "", "_____");
            return ltrueFalse;
        }

        public override object VisitBor(NoobParser.BorContext context)
        {
            object left = Visit(context.boolean(0));    // B1

            int M = repInt.M;
            object right = Visit(context.boolean(1));  // B2
            List<List<int>> btrueFalse = new List<List<int>>();
            List<List<int>> b1trueFalse = left as List<List<int>>;
            List<List<int>> b2trueFalse = right as List<List<int>>;
            repInt.backpatch(b1trueFalse[1],M );  // backpatch (b1.false, M.instr)
            List<int> trueList = new List<int>(b1trueFalse[0]);  // b.trueList = merge(b1.true, b2.true)
            trueList.AddRange(b2trueFalse[0]);
            btrueFalse.Add(trueList); // lista True
            btrueFalse.Add(b2trueFalse[1]); // lista False



            return btrueFalse;


            
        }

        public override object VisitBand(NoobParser.BandContext context)
        {
            object left = Visit(context.boolean(0));    // B1

            int M = repInt.M;
            object right = Visit(context.boolean(1));  // B2
            List<List<int>> btrueFalse = new List<List<int>>();

            List<List<int>> b1trueFalse = left as List<List<int>>;
            List<List<int>> b2trueFalse = right as List<List<int>>;

            repInt.backpatch(b1trueFalse[0], M);  // backpatch (b1.false, M.instr)
            List<int> falseList = new List<int>(b1trueFalse[1]);  // b.trueList = merge(b1.true, b2.true)
            falseList.AddRange(b2trueFalse[1]);
            btrueFalse.Add(b2trueFalse[0]); // lista False
            btrueFalse.Add(falseList); // lista True
            



            return btrueFalse;

        }

        public override object VisitNb(NoobParser.NbContext context)
        {
            object left = Visit(context.boolean());    // B1
           
            List<List<int>> btrueFalse = new List<List<int>>();

            List<List<int>> b1trueFalse = left as List<List<int>>;

            btrueFalse.Add(b1trueFalse[1]);
            btrueFalse.Add(b1trueFalse[0]);

            return btrueFalse;
            
        }

        public override object VisitBparens(NoobParser.BparensContext context)
        {
            return Visit(context.boolean()); 
               // base.VisitBparens(context);
        }

        #endregion


        #region condicional
        public override object VisitCondicion(NoobParser.CondicionContext context)
        {
            
            List<int> S1;
            object obj = Visit(context.boolean());
            List<List<int>> b = obj as List<List<int>>;
            int M = repInt.M;
            obj = Visit(context.sentencia());
            S1 = obj as List<int>;
            List<int> S2;

            List<int> NList = N(); //N
            int M2 = repInt.M; // M2
            if (S1 == null)
            {
                S1 = new List<int>();
            }
            obj = Visit(context.sentelse()); // Si tiene else
            if(obj != null) // Contiene else
            {
                S2 = obj as List<int>;
                S2.AddRange(NList);
                S2.AddRange(S1);
                S1 = S2;
                repInt.backpatch(b[0], M);
                repInt.backpatch(b[1], M2);

            }
            else
            {
                repInt.eliminaCuadruplo(NList[0]);
                repInt.backpatch(b[0], M);
                S1.AddRange(b[1]);
            }

            
            return S1;


        }

        public override object VisitVacio(NoobParser.VacioContext context)
        {
            return null;
        }

        public override object VisitCelse(NoobParser.CelseContext context)
        {
            // Else S2
            object obj;
            
            obj = Visit(context.sentencia());  //S2

            List<int> s2 = obj as List<int>; // S2List

            if(s2 == null)
            {
                s2 = new List<int>();
            }
            return s2;

            
            
        }

        #endregion


        #region N
        /// <summary>
        ///  genera un salto incondicional 
        /// </summary>
        /// <returns></returns>
        public List<int> N()
        {
            List<int> Nlist = new List<int>();
            Nlist.Add(repInt.M);
            repInt.insertaCuadruplo("goto", "", "", "");
            return Nlist;
        }

        #endregion


        #region Ciclos
        public override object VisitSwhile(NoobParser.SwhileContext context)
        {
            List<int> S1;
            int M1 = repInt.M;
            object obj = Visit(context.boolean());
            List<List<int>> b = obj as List<List<int>>;
            int M2 = repInt.M;
            obj = Visit(context.sentencia());
            S1 = obj as List<int>;
            if(S1 == null)
            {
                S1 = new List<int>();
            }
            repInt.backpatch(b[0], M2);
            repInt.backpatch(S1, M1);
            repInt.insertaCuadruplo("goto", "", "", M1.ToString());
            S1 = b[1];
            return S1;
            
        }



        public override object VisitSdowhile(NoobParser.SdowhileContext context)
        {
            List<int> S1;
            int M1 = repInt.M;
            object obj = Visit(context.sentencia());
            S1 = obj as List<int>;
            if (S1 == null)
            {
                S1 = new List<int>();
            }


           
            int M2 = repInt.M;
            obj = Visit(context.boolean());
            List<List<int>> b = obj as List<List<int>>;
           
            repInt.backpatch(b[0], M1);
            repInt.backpatch(S1, M2);
            S1 = b[1];
            return S1;
            
        }



        public override object VisitSentIterador(NoobParser.SentIteradorContext context)
        {
            int m1, m2, m3;
            Visit(context.assignment(0));
            m1 = repInt.M;
            List<List<int>> btruefalse = Visit(context.boolean()) as List<List<int>>;
            m2 = repInt.M;
            Visit(context.assignment(1));
            List<int> N1 = N();
            m3 = repInt.M;
            List<int> s1 = Visit(context.sentencia()) as List<int>;
            repInt.backpatch(btruefalse[0], m3);
            repInt.backpatch(N1, m1);
            repInt.backpatch(s1, m2);
            repInt.insertaCuadruplo("goto", "", "", m2.ToString());
            



            return btruefalse[1];

         //   return base.VisitSentIterador(context);
        }

        #endregion






        #region declaracion de variables

        public override object VisitInicioDeclaracion(NoobParser.InicioDeclaracionContext context)
        {
            List<int> ltipo = new List<int>();
            object b = Visit(context.tipo());  // tipo
            object nom = Visit(context.variable()); // nombre
            object c = Visit(context.array()); // si es arreglo 
            //Visit(context.otroId());
            ltipo = c as List<int>;
            if(ltipo == null)
            {
                ltipo = new List<int>();
            }
            ltipo.Add(int.Parse(b.ToString()));
            repInt.insertaSimbolo(nom.ToString(), 0, null , ltipo);
            Visit(context.otroId());
            
            return  new List<int>();
            //return base.VisitInicioDeclaracion(context);
        }


        public override object VisitDeclaracionTipo(NoobParser.DeclaracionTipoContext context)
        {

            int t = -1;                // 1 = int , 2 = float , 3 = char 
            if (context.type.Type == NoobParser.Int)
            {
                t = 1;
            }
            if (context.type.Type == NoobParser.Float)
            {
                t = 2;
            }
            if (context.type.Type == NoobParser.Char)
            {
                t = 3;
            }

            tipo = t;
            return t;

            //return base.VisitDeclaracionTipo(context);
        }


        public override object VisitDeclaracionArray(NoobParser.DeclaracionArrayContext context)
        {
            List<int> ltipo = new List<int>();
            int ent = int.Parse(context.n.Text);
            object obj = Visit(context.array());
            ltipo = obj as List<int>;
            ltipo.Insert(0, ent);
            ltipo.Insert(0, 0);  // el cero representa el 

            return ltipo;
            //  return base.VisitDeclaracionArray(context);
        }

        public override object VisitDeclaracionArrayVacio(NoobParser.DeclaracionArrayVacioContext context)
        {
            return new List<int>();
        }


        public override object VisitDeclaracionOtroId(NoobParser.DeclaracionOtroIdContext context)
        {
            object nom = Visit(context.variable());
            object obj = Visit(context.array());
            List<int> ltipo = obj as List<int>;
            ltipo.Add(tipo);
            repInt.insertaSimbolo(nom.ToString(), 0, null, ltipo);
            Visit(context.otroId());
            return null;
           // return base.VisitDeclaracionOtroId(context);
        }

        public override object VisitFinDeclaracion(NoobParser.FinDeclaracionContext context)
        {
            return base.VisitFinDeclaracion(context);
        }

        #endregion



        #region Operaciones de Arreglos

        /* operaciones de Arreglos */
        public override object VisitAsignacionArray(NoobParser.AsignacionArrayContext context)
        {
            object obj = Visit(context.accesoArray());
            object obj2 = Visit(context.expr());
            string arr;
            structArray arreglo = (structArray)obj;
            arr = arreglo.nombre + " [" + arreglo.address + "]";

            repInt.insertaCuadruplo("=", obj2.ToString(), "", arr);

            return null;

            //return base.VisitAsignacionArray(context);
        }


        public override object VisitGetElementoArray(NoobParser.GetElementoArrayContext context)
        {

            


            object obj = Visit(context.accesoArray());
            string arr;
            structArray arreglo = (structArray)obj;
            arr = arreglo.nombre + " [" + arreglo.address + "]";
            string temp = creaTemp();
            repInt.insertaCuadruplo("=", arr, "", temp);


            return temp;
            //return base.VisitGetElementoArray(context);
        }


        public override object VisitIdAccesoArray(NoobParser.IdAccesoArrayContext context)
        {
            string temp = creaTemp();
            object obj = Visit(context.variable());
            structArray sArray = new structArray(obj.ToString(),temp, 2);
            obj = Visit(context.expr());
            List<int> l = repInt.getTipoSimbolo(sArray.nombre);
            int width = getTamArray(l,2);
            repInt.insertaCuadruplo("*",obj.ToString(),width.ToString(),sArray.address);



            return sArray;
            
            //return base.VisitIdAccesoArray(context);
        }


        public override object VisitRecursionAccesoArray(NoobParser.RecursionAccesoArrayContext context)
        {
            structArray sArray = new structArray();
           


            structArray sArray1 = (structArray)Visit(context.accesoArray());

            sArray.nombre = sArray1.nombre;
            sArray.apArray = sArray1.apArray + 2;
           


            object obj = Visit(context.expr());


           
           
            List<int> l = repInt.getTipoSimbolo(sArray.nombre);
            int width = getTamArray(l, sArray.apArray);

            string t = creaTemp();
            sArray.address = creaTemp();
            repInt.insertaCuadruplo("*", obj.ToString(), width.ToString(), t);
            repInt.insertaCuadruplo("+", sArray1.address, t, sArray.address);



            return sArray;
//            return base.VisitRecursionAccesoArray(context);
        }




        int getTamArray(List<int> tipoArray, int indice)
        {
            int tam = 1;
            int i = indice;
            for (; i < tipoArray.Count - 1; i += 2)
            {
                tam = tipoArray[i + 1] * tam;
            }
            switch (tipoArray[i])
            {
                case 1:
                    tam = tam * sizeof(int);
                    break;
                case 2:
                    tam = tam * sizeof(float);
                    break;
                case 3:
                    tam = tam * sizeof(char);
                    break;

            }
            return tam;

        }

        #endregion


        #region definicion de funciones y procedimientos
        public override object VisitDefinicionFuncion(NoobParser.DefinicionFuncionContext context)
        {
            object obj = Visit(context.variable());
            string id = obj.ToString();
            int m1, m2;
            m1 = repInt.M;
            List<int> snext, aux = new List<int>();
            obj = Visit(context.sentencia());
            m2 = repInt.M;
            snext = obj as List<int>;
            repInt.insertaSimbolo(id, m1, "func",aux);
            repInt.backpatch(snext, m2);
            repInt.insertaCuadruplo("endf", "", "", "");

            return aux;


            //return base.VisitDefinicionFuncion(context);
        }

        public override object VisitDefinicionProcedimiento(NoobParser.DefinicionProcedimientoContext context)
        {

            object obj = Visit(context.variable());
            string id = obj.ToString();
            int m1, m2;
            m1 = repInt.M;
            List<int> snext, aux = new List<int>();
            obj = Visit(context.sentencia());
            m2 = repInt.M;
            snext = obj as List<int>;
            repInt.insertaSimbolo(id, m1, "proc", aux);
            repInt.backpatch(snext, m2);
            repInt.insertaCuadruplo("endp", "", "", "");

            return aux;

            //return base.VisitDefinicionProcedimiento(context);
        }


        #endregion



        #region llamado a funciones y procedimientos
        public override object VisitCallFuncion(NoobParser.CallFuncionContext context)
        {


            string address = creaTemp();
            string id = Visit(context.variable()).ToString();
            int pcount = (int)Visit(context.parametro());
            repInt.insertaCuadruplo("=", "call " + id, pcount.ToString(), address);
            return address;

            //return base.VisitCallFuncion(context);
        }

        public override object VisitCallProcedimiento(NoobParser.CallProcedimientoContext context)
        {
            
            string id = Visit(context.variable()).ToString();
            int pcount = (int)Visit(context.parametro());
            repInt.insertaCuadruplo(" ", "call " + id, pcount.ToString(), " ");
            return new List<int>();

            //return base.VisitCallProcedimiento(context);
        }
        #endregion



        #region Return de funciones y procedeimientos
        public override object VisitRetGeneral(NoobParser.RetGeneralContext context)
        {
            Visit(context.ret());
            return new List<int>(); 
            //return base.VisitRetGeneral(context);
        }


        public override object VisitRetFunc(NoobParser.RetFuncContext context)
        {

            string addres = Visit(context.expresion()).ToString();
            repInt.insertaCuadruplo("return", "", "", addres);

            return null;
        }
        

        public override object VisitRetProc(NoobParser.RetProcContext context)
        {
            repInt.insertaCuadruplo("return", "", "", "");
            return null;
            //return base.VisitRetProc(context);
        }
        #endregion


        #region parametros de funciones y procedimientos
        public override object VisitParametroVacio(NoobParser.ParametroVacioContext context)
        {
            int i = 0;
            return i;
            //return base.VisitParametroVacio(context);
        }

        public override object VisitVacioParam(NoobParser.VacioParamContext context)
        {
            int i = 0;
            return i;
            //return base.VisitVacioParam(context);
        }

        public override object VisitPrimerParametro(NoobParser.PrimerParametroContext context)
        {
            int nparam;

            string addres = Visit(context.expresion()).ToString();
            repInt.insertaCuadruplo("param", "", "", addres);
            nparam = 1 + (int)Visit(context.paramRec());

            return nparam;
            //return base.VisitPrimerParametro(context);
        }

        public override object VisitOtrosParametros(NoobParser.OtrosParametrosContext context)
        {
            int nparam;

            string addres = Visit(context.expresion()).ToString();
            repInt.insertaCuadruplo("param", "", "", addres);
            nparam = 1 + (int)Visit(context.paramRec());

            return nparam;


            //return base.VisitOtrosParametros(context);
        }
        #endregion


        #region  entrada y salida

        public override object VisitSentenciaPrint(NoobParser.SentenciaPrintContext context)
        {
            return base.VisitSentenciaPrint(context);
        }

        public override object VisitSentenciaRead(NoobParser.SentenciaReadContext context)
        {
            return base.VisitSentenciaRead(context);
        }



        #endregion









    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiladores_Interpretes_B
{
    class NoobVisitor: NoobBaseVisitor<object>
    {
        private RepresentacionIntermedia repInt;
        public RepresentacionIntermedia gsRepInt { get { return repInt; } }


        private int tipo;

        private int temp;
        public NoobVisitor()
        {
            repInt = new RepresentacionIntermedia();
            tipo = -1;

        }
        private string creaTemp()
        {
            string t = "T" + temp.ToString();
            temp++;
            return t;
        }

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

        
        public List<int> N()
        {
            List<int> Nlist = new List<int>();
            Nlist.Add(repInt.M);
            repInt.insertaCuadruplo("goto", "", "", "");
            return Nlist;
        }


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



        public override object VisitSentBloque(NoobParser.SentBloqueContext context)
        {
            object obj = Visit(context.bloque());
            List<int> s = obj as List<int>;
            if(s == null)
            {
                s = new List<int>();
            }
            return s;
            //return base.VisitSentBloque(context);
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
            this.repInt.insertaCuadruplo("-", a1.ToString(), "", s );
            return s;


        }




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

    
    
    
    
    
    
    
    
    }
}

using System;
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

        private int temp;
        public NoobVisitor()
        {
            repInt = new RepresentacionIntermedia();

        }
        private string creaTemp()
        {
            string t = "T" + temp.ToString();
            temp++;
            return t;
        }


        public override object VisitInt(NoobParser.IntContext context)
        {
            return int.Parse(context.INT().GetText());
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

        public override object VisitAsig(NoobParser.AsigContext context)
        {
            object a1 = base.VisitAsig(context);

            this.repInt.insertaCuadruplo("=", a1.ToString(), "", context.var.GetText());
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
            return base.VisitBparens(context);
        }


        public override object VisitCondicion(NoobParser.CondicionContext context)
        {
            
            List<int> S1;
            object obj = Visit(context.boolean());
            List<List<int>> b = obj as List<List<int>>;
            int M = repInt.M;
            obj = Visit(context.sentencia());
            S1 = obj as List<int>;

            repInt.backpatch(b[0], M);
            if(S1 == null)
            {
                S1 = new List<int>();
            }
            S1.AddRange(b[1]);
            return S1;
        }


    }
}

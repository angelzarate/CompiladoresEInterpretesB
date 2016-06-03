using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Antlr4.Runtime;
using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;


namespace Compiladores_Interpretes_B
{
    public partial class Form1 : Form
    {

        private string ruta;
        public Form1()
        {
            InitializeComponent();
            ruta = "";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dtgTablaSimbolos.Font = new Font("Arial", 11);
            TablaDeCuadruplos.Font = new Font("Arial", 11);




        }

        private void Opciones_Archivo(object sender, EventArgs e)
        {
            ToolStripMenuItem opcion = sender as ToolStripMenuItem;
            switch (opcion.Text)
            {
                case "Nuevo":
                    AreaDeEscritura.Clear();
                    ruta = "";
                
                break;
                case "Abrir":
                    this.AbrirArchivo();
                break;
                case "Guardar":
                    this.GuardarArchivo();
                break;
                case "Salir":
                    this.Close();
                break;

            }
           

        }


        private void CompilarButton_Click(object sender, EventArgs e)
        {
            try
            {
                if(ruta.Equals("")==true)
                {
                    this.GuardarArchivo();

                    if(ruta.Equals("") == false)
                    {
                        this.Compilar();
                    }
                    else
                    {
                        MessageBox.Show("Debes Guardar el proyecto Antes de Compilar");
                    }
                }
                else
                {
                    this.Compilar();

                }

            }
            catch(Exception exc)
            {
                MessageBox.Show(""+exc);
            }

        }


        private void Compilar()
        {
            AreaDeEscritura.SaveFile(ruta, RichTextBoxStreamType.PlainText);
            StreamReader inputStream = new StreamReader(ruta);
            try
            {
                
              
                AntlrInputStream input = new AntlrInputStream(inputStream.ReadToEnd());
                NoobLexer lexer = new NoobLexer(input);
                CommonTokenStream tokens = new CommonTokenStream(lexer);
                NoobParser parser = new NoobParser(tokens);
                IParseTree tree = parser.prog();
                NoobVisitor visitor = new NoobVisitor();
                visitor.Visit(tree);
                
                inputStream.Close();
                string arbol = tree.ToStringTree(parser);
                Interprete inter = new Interprete(visitor.gsRepInt.gsTablaCuadruplo, visitor.gsRepInt.gsTablaDeSimbolos);
                inter.EjecutarPrograma();

                visitor.gsRepInt.imprimeTablaCuadruplos(this.TablaDeCuadruplos);
                visitor.gsRepInt.imprimeTablaDeSimbolos(this.dtgTablaSimbolos);

            }
            catch(Exception e)
            {
                MessageBox.Show(" Error Sintactico "+e);
                inputStream.Close();
            }

        }

        private void GuardarArchivo()
        {
            SaveFileDialog saveDialog = new SaveFileDialog();

            string cad = Application.StartupPath;
            cad = cad.Replace("\\bin\\Debug", "");
            saveDialog.InitialDirectory = cad;

            saveDialog.Filter = "txt files (*.txt)|*.txt";
            saveDialog.FilterIndex = 2;
            saveDialog.RestoreDirectory = true;
            if (saveDialog.ShowDialog() == DialogResult.OK && saveDialog.FileName.Length > 0)
            {
                ruta = saveDialog.FileName;
                AreaDeEscritura.SaveFile(saveDialog.FileName, RichTextBoxStreamType.PlainText);
            }


        }

        private void AbrirArchivo()
        {
            OpenFileDialog openDialog = new OpenFileDialog();
            openDialog.Filter = "txt files (*.txt)|*.txt";
            string cad = Application.StartupPath;
            cad = cad.Replace("\\bin\\Debug", "");
            openDialog.InitialDirectory = cad;

            openDialog.FilterIndex = 2;
            openDialog.RestoreDirectory = true;

            if (openDialog.ShowDialog() == DialogResult.OK && openDialog.FileName.Length > 0)
            {
                ruta = openDialog.FileName;
                AreaDeEscritura.LoadFile(openDialog.FileName, RichTextBoxStreamType.PlainText);
            }

        }

        private void OpcionesTexto_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem opcion = sender as ToolStripMenuItem;
            switch (opcion.Text)
            {
                case "Copiar":
                    AreaDeEscritura.Copy();
                    break;

                case "Pegar":
                    AreaDeEscritura.Paste();
                    break;
                case "Cortar":
                    AreaDeEscritura.Cut();
                    break;
            }
        }

       



    }
}

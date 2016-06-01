namespace Compiladores_Interpretes_B
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador requerida.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén utilizando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido del método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.AreaDeEscritura = new System.Windows.Forms.RichTextBox();
            this.opcionesTexto = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.copiarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.cortarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.pegarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Menu = new System.Windows.Forms.MenuStrip();
            this.archivoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nuevoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.abrirToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.guardarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.salirToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.BarraDeHerramientas = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.CompilarButton = new System.Windows.Forms.ToolStripButton();
            this.TablaDeCuadruplos = new System.Windows.Forms.DataGridView();
            this.Dir = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Operador = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Argumento1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Argumento2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Resultado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dtgTablaSimbolos = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Nombre = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Valor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Tipo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.opcionesTexto.SuspendLayout();
            this.Menu.SuspendLayout();
            this.BarraDeHerramientas.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TablaDeCuadruplos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgTablaSimbolos)).BeginInit();
            this.SuspendLayout();
            // 
            // AreaDeEscritura
            // 
            this.AreaDeEscritura.AcceptsTab = true;
            this.AreaDeEscritura.ContextMenuStrip = this.opcionesTexto;
            this.AreaDeEscritura.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AreaDeEscritura.Location = new System.Drawing.Point(12, 70);
            this.AreaDeEscritura.Name = "AreaDeEscritura";
            this.AreaDeEscritura.Size = new System.Drawing.Size(471, 475);
            this.AreaDeEscritura.TabIndex = 0;
            this.AreaDeEscritura.Text = "";
            // 
            // opcionesTexto
            // 
            this.opcionesTexto.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copiarToolStripMenuItem,
            this.toolStripSeparator1,
            this.cortarToolStripMenuItem,
            this.toolStripSeparator2,
            this.pegarToolStripMenuItem});
            this.opcionesTexto.Name = "opcionesTexto";
            this.opcionesTexto.Size = new System.Drawing.Size(110, 82);
            // 
            // copiarToolStripMenuItem
            // 
            this.copiarToolStripMenuItem.Name = "copiarToolStripMenuItem";
            this.copiarToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.copiarToolStripMenuItem.Text = "Copiar";
            this.copiarToolStripMenuItem.Click += new System.EventHandler(this.OpcionesTexto_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(106, 6);
            // 
            // cortarToolStripMenuItem
            // 
            this.cortarToolStripMenuItem.Name = "cortarToolStripMenuItem";
            this.cortarToolStripMenuItem.Size = new System.Drawing.Size(109, 22);
            this.cortarToolStripMenuItem.Text = "Cortar";
            this.cortarToolStripMenuItem.Click += new System.EventHandler(this.OpcionesTexto_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(106, 6);
            // 
            // pegarToolStripMenuItem
            // 
            this.pegarToolStripMenuItem.Name = "pegarToolStripMenuItem";
            this.pegarToolStripMenuItem.Size = new System.Drawing.Size(109, 22);
            this.pegarToolStripMenuItem.Text = "Pegar";
            this.pegarToolStripMenuItem.Click += new System.EventHandler(this.OpcionesTexto_Click);
            // 
            // Menu
            // 
            this.Menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.archivoToolStripMenuItem});
            this.Menu.Location = new System.Drawing.Point(0, 0);
            this.Menu.Name = "Menu";
            this.Menu.Size = new System.Drawing.Size(1104, 24);
            this.Menu.TabIndex = 1;
            this.Menu.Text = "menuStrip1";
            // 
            // archivoToolStripMenuItem
            // 
            this.archivoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.nuevoToolStripMenuItem,
            this.toolStripSeparator3,
            this.abrirToolStripMenuItem,
            this.toolStripSeparator4,
            this.guardarToolStripMenuItem,
            this.toolStripSeparator5,
            this.salirToolStripMenuItem});
            this.archivoToolStripMenuItem.Name = "archivoToolStripMenuItem";
            this.archivoToolStripMenuItem.Size = new System.Drawing.Size(60, 20);
            this.archivoToolStripMenuItem.Text = "Archivo";
            // 
            // nuevoToolStripMenuItem
            // 
            this.nuevoToolStripMenuItem.Name = "nuevoToolStripMenuItem";
            this.nuevoToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.nuevoToolStripMenuItem.Text = "Nuevo";
            this.nuevoToolStripMenuItem.Click += new System.EventHandler(this.Opciones_Archivo);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(113, 6);
            // 
            // abrirToolStripMenuItem
            // 
            this.abrirToolStripMenuItem.Name = "abrirToolStripMenuItem";
            this.abrirToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.abrirToolStripMenuItem.Text = "Abrir";
            this.abrirToolStripMenuItem.Click += new System.EventHandler(this.Opciones_Archivo);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(113, 6);
            // 
            // guardarToolStripMenuItem
            // 
            this.guardarToolStripMenuItem.Name = "guardarToolStripMenuItem";
            this.guardarToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.guardarToolStripMenuItem.Text = "Guardar";
            this.guardarToolStripMenuItem.Click += new System.EventHandler(this.Opciones_Archivo);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(113, 6);
            // 
            // salirToolStripMenuItem
            // 
            this.salirToolStripMenuItem.Name = "salirToolStripMenuItem";
            this.salirToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.salirToolStripMenuItem.Text = "Salir";
            this.salirToolStripMenuItem.Click += new System.EventHandler(this.Opciones_Archivo);
            // 
            // BarraDeHerramientas
            // 
            this.BarraDeHerramientas.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.CompilarButton});
            this.BarraDeHerramientas.Location = new System.Drawing.Point(0, 24);
            this.BarraDeHerramientas.Name = "BarraDeHerramientas";
            this.BarraDeHerramientas.Size = new System.Drawing.Size(1104, 43);
            this.BarraDeHerramientas.TabIndex = 2;
            this.BarraDeHerramientas.Text = "toolStrip1";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(56, 40);
            this.toolStripLabel1.Text = "Compilar";
            // 
            // CompilarButton
            // 
            this.CompilarButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.CompilarButton.Image = ((System.Drawing.Image)(resources.GetObject("CompilarButton.Image")));
            this.CompilarButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.CompilarButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.CompilarButton.Name = "CompilarButton";
            this.CompilarButton.Size = new System.Drawing.Size(40, 40);
            this.CompilarButton.Text = "Compilar";
            this.CompilarButton.Click += new System.EventHandler(this.CompilarButton_Click);
            // 
            // TablaDeCuadruplos
            // 
            this.TablaDeCuadruplos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.TablaDeCuadruplos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Dir,
            this.Operador,
            this.Argumento1,
            this.Argumento2,
            this.Resultado});
            this.TablaDeCuadruplos.Location = new System.Drawing.Point(506, 70);
            this.TablaDeCuadruplos.Name = "TablaDeCuadruplos";
            this.TablaDeCuadruplos.ReadOnly = true;
            this.TablaDeCuadruplos.Size = new System.Drawing.Size(553, 300);
            this.TablaDeCuadruplos.TabIndex = 3;
            // 
            // Dir
            // 
            this.Dir.HeaderText = "Direccion";
            this.Dir.Name = "Dir";
            this.Dir.ReadOnly = true;
            // 
            // Operador
            // 
            this.Operador.HeaderText = "Operador";
            this.Operador.Name = "Operador";
            this.Operador.ReadOnly = true;
            // 
            // Argumento1
            // 
            this.Argumento1.HeaderText = "Argumento 1";
            this.Argumento1.Name = "Argumento1";
            this.Argumento1.ReadOnly = true;
            // 
            // Argumento2
            // 
            this.Argumento2.HeaderText = "Argumento 2";
            this.Argumento2.Name = "Argumento2";
            this.Argumento2.ReadOnly = true;
            // 
            // Resultado
            // 
            this.Resultado.HeaderText = "Resultado";
            this.Resultado.Name = "Resultado";
            this.Resultado.ReadOnly = true;
            // 
            // dtgTablaSimbolos
            // 
            this.dtgTablaSimbolos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgTablaSimbolos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.Nombre,
            this.Valor,
            this.Tipo});
            this.dtgTablaSimbolos.Location = new System.Drawing.Point(506, 376);
            this.dtgTablaSimbolos.Name = "dtgTablaSimbolos";
            this.dtgTablaSimbolos.ReadOnly = true;
            this.dtgTablaSimbolos.Size = new System.Drawing.Size(443, 300);
            this.dtgTablaSimbolos.TabIndex = 3;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "Direccion";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // Nombre
            // 
            this.Nombre.HeaderText = "Nombre";
            this.Nombre.Name = "Nombre";
            this.Nombre.ReadOnly = true;
            // 
            // Valor
            // 
            this.Valor.HeaderText = "Valor";
            this.Valor.Name = "Valor";
            this.Valor.ReadOnly = true;
            // 
            // Tipo
            // 
            this.Tipo.HeaderText = "Tipo";
            this.Tipo.Name = "Tipo";
            this.Tipo.ReadOnly = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1104, 681);
            this.Controls.Add(this.dtgTablaSimbolos);
            this.Controls.Add(this.TablaDeCuadruplos);
            this.Controls.Add(this.BarraDeHerramientas);
            this.Controls.Add(this.AreaDeEscritura);
            this.Controls.Add(this.Menu);
            this.MainMenuStrip = this.Menu;
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.opcionesTexto.ResumeLayout(false);
            this.Menu.ResumeLayout(false);
            this.Menu.PerformLayout();
            this.BarraDeHerramientas.ResumeLayout(false);
            this.BarraDeHerramientas.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TablaDeCuadruplos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgTablaSimbolos)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox AreaDeEscritura;
        private System.Windows.Forms.MenuStrip Menu;
        private System.Windows.Forms.ToolStrip BarraDeHerramientas;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripButton CompilarButton;
        private System.Windows.Forms.ContextMenuStrip opcionesTexto;
        private System.Windows.Forms.ToolStripMenuItem copiarToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem cortarToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem pegarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem archivoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem nuevoToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem abrirToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem guardarToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem salirToolStripMenuItem;
        private System.Windows.Forms.DataGridView TablaDeCuadruplos;
        private System.Windows.Forms.DataGridViewTextBoxColumn Dir;
        private System.Windows.Forms.DataGridViewTextBoxColumn Operador;
        private System.Windows.Forms.DataGridViewTextBoxColumn Argumento1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Argumento2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Resultado;
        private System.Windows.Forms.DataGridView dtgTablaSimbolos;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Nombre;
        private System.Windows.Forms.DataGridViewTextBoxColumn Valor;
        private System.Windows.Forms.DataGridViewTextBoxColumn Tipo;
    }
}


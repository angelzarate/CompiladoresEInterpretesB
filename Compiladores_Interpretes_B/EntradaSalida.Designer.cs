namespace Compiladores_Interpretes_B
{
    partial class EntradaSalida
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Salida = new System.Windows.Forms.RichTextBox();
            this.Entrada = new System.Windows.Forms.TextBox();
            this.boton_OK = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Salida
            // 
            this.Salida.BackColor = System.Drawing.SystemColors.Control;
            this.Salida.Location = new System.Drawing.Point(12, 12);
            this.Salida.Name = "Salida";
            this.Salida.ReadOnly = true;
            this.Salida.Size = new System.Drawing.Size(685, 303);
            this.Salida.TabIndex = 0;
            this.Salida.Text = "";
            // 
            // Entrada
            // 
            this.Entrada.Location = new System.Drawing.Point(13, 334);
            this.Entrada.Name = "Entrada";
            this.Entrada.Size = new System.Drawing.Size(684, 20);
            this.Entrada.TabIndex = 1;
            // 
            // boton_OK
            // 
            this.boton_OK.Location = new System.Drawing.Point(292, 380);
            this.boton_OK.Name = "boton_OK";
            this.boton_OK.Size = new System.Drawing.Size(75, 23);
            this.boton_OK.TabIndex = 2;
            this.boton_OK.Text = "OK";
            this.boton_OK.UseVisualStyleBackColor = true;
            this.boton_OK.Click += new System.EventHandler(this.boton_OK_Click);
            // 
            // EntradaSalida
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(709, 426);
            this.Controls.Add(this.boton_OK);
            this.Controls.Add(this.Entrada);
            this.Controls.Add(this.Salida);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EntradaSalida";
            this.Text = "EntradaSalida";
            this.Load += new System.EventHandler(this.EntradaSalida_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox Salida;
        private System.Windows.Forms.TextBox Entrada;
        private System.Windows.Forms.Button boton_OK;
    }
}
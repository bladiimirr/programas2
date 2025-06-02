namespace prueba_de_botones
{
    partial class soloLetras
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
            this.validarLetras1 = new MiBiblioteca3.ValidarLetras();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // validarLetras1
            // 
            this.validarLetras1.Location = new System.Drawing.Point(238, 110);
            this.validarLetras1.Name = "validarLetras1";
            this.validarLetras1.Size = new System.Drawing.Size(355, 190);
            this.validarLetras1.TabIndex = 0;
            this.validarLetras1.Load += new System.EventHandler(this.validarLetras1_Load);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(691, 364);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(84, 40);
            this.button1.TabIndex = 1;
            this.button1.Text = "Anterior";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(558, 365);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(104, 39);
            this.button2.TabIndex = 2;
            this.button2.Text = "SIguiente";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 22.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(231, 65);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(363, 42);
            this.label1.TabIndex = 3;
            this.label1.Text = "Solo Permite Letras";
            // 
            // soloLetras
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.validarLetras1);
            this.Name = "soloLetras";
            this.Text = "soloLetras";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MiBiblioteca3.ValidarLetras validarLetras1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label1;
    }
}
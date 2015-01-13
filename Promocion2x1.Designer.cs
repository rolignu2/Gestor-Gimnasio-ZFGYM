namespace GestorGimnasio
{
    partial class Promocion2x1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Promocion2x1));
            this.panel1 = new System.Windows.Forms.Panel();
            this.cmdcerrar = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtapellido = new System.Windows.Forms.TextBox();
            this.txtnombre = new System.Windows.Forms.TextBox();
            this.linkapellido = new System.Windows.Forms.LinkLabel();
            this.linknombre = new System.Windows.Forms.LinkLabel();
            this.cmdaceptar = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.cmdcerrar);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.cmdaceptar);
            this.panel1.Location = new System.Drawing.Point(3, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(384, 107);
            this.panel1.TabIndex = 0;
            // 
            // cmdcerrar
            // 
            this.cmdcerrar.ForeColor = System.Drawing.Color.Black;
            this.cmdcerrar.Image = ((System.Drawing.Image)(resources.GetObject("cmdcerrar.Image")));
            this.cmdcerrar.Location = new System.Drawing.Point(289, 56);
            this.cmdcerrar.Name = "cmdcerrar";
            this.cmdcerrar.Size = new System.Drawing.Size(77, 41);
            this.cmdcerrar.TabIndex = 4;
            this.cmdcerrar.UseVisualStyleBackColor = true;
            this.cmdcerrar.Click += new System.EventHandler(this.cmdcerrar_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtapellido);
            this.groupBox1.Controls.Add(this.txtnombre);
            this.groupBox1.Controls.Add(this.linkapellido);
            this.groupBox1.Controls.Add(this.linknombre);
            this.groupBox1.ForeColor = System.Drawing.Color.Yellow;
            this.groupBox1.Location = new System.Drawing.Point(9, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(274, 94);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "PROMOCION 2X1 :";
            // 
            // txtapellido
            // 
            this.txtapellido.Location = new System.Drawing.Point(79, 57);
            this.txtapellido.Name = "txtapellido";
            this.txtapellido.Size = new System.Drawing.Size(159, 20);
            this.txtapellido.TabIndex = 2;
            // 
            // txtnombre
            // 
            this.txtnombre.Location = new System.Drawing.Point(79, 29);
            this.txtnombre.Name = "txtnombre";
            this.txtnombre.Size = new System.Drawing.Size(159, 20);
            this.txtnombre.TabIndex = 1;
            // 
            // linkapellido
            // 
            this.linkapellido.AutoSize = true;
            this.linkapellido.LinkColor = System.Drawing.Color.Red;
            this.linkapellido.Location = new System.Drawing.Point(16, 60);
            this.linkapellido.Name = "linkapellido";
            this.linkapellido.Size = new System.Drawing.Size(62, 13);
            this.linkapellido.TabIndex = 1;
            this.linkapellido.TabStop = true;
            this.linkapellido.Text = "APELLIDO:";
            // 
            // linknombre
            // 
            this.linknombre.AutoSize = true;
            this.linknombre.LinkColor = System.Drawing.Color.Red;
            this.linknombre.Location = new System.Drawing.Point(16, 32);
            this.linknombre.Name = "linknombre";
            this.linknombre.Size = new System.Drawing.Size(57, 13);
            this.linknombre.TabIndex = 0;
            this.linknombre.TabStop = true;
            this.linknombre.Text = "NOMBRE:";
            // 
            // cmdaceptar
            // 
            this.cmdaceptar.ForeColor = System.Drawing.Color.Black;
            this.cmdaceptar.Image = ((System.Drawing.Image)(resources.GetObject("cmdaceptar.Image")));
            this.cmdaceptar.Location = new System.Drawing.Point(289, 7);
            this.cmdaceptar.Name = "cmdaceptar";
            this.cmdaceptar.Size = new System.Drawing.Size(77, 41);
            this.cmdaceptar.TabIndex = 3;
            this.cmdaceptar.UseVisualStyleBackColor = true;
            this.cmdaceptar.Click += new System.EventHandler(this.cmdaceptar_Click);
            // 
            // Promocion2x1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(399, 129);
            this.Controls.Add(this.panel1);
            this.Name = "Promocion2x1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Promocion2x1";
            this.Load += new System.EventHandler(this.Promocion2x1_Load);
            this.panel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.LinkLabel linkapellido;
        private System.Windows.Forms.LinkLabel linknombre;
        private System.Windows.Forms.TextBox txtapellido;
        private System.Windows.Forms.TextBox txtnombre;
        private System.Windows.Forms.Button cmdcerrar;
        private System.Windows.Forms.Button cmdaceptar;
    }
}
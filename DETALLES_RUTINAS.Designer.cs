namespace ZONE_FITNESS_3._0_FINAL
{
    partial class DETALLES_RUTINAS
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DETALLES_RUTINAS));
            this.txtdetalles = new System.Windows.Forms.RichTextBox();
            this.picdetalles = new System.Windows.Forms.PictureBox();
            this.lbldetalle = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.picdetalles)).BeginInit();
            this.SuspendLayout();
            // 
            // txtdetalles
            // 
            this.txtdetalles.Location = new System.Drawing.Point(263, 56);
            this.txtdetalles.Name = "txtdetalles";
            this.txtdetalles.Size = new System.Drawing.Size(319, 182);
            this.txtdetalles.TabIndex = 0;
            this.txtdetalles.Text = "";
            // 
            // picdetalles
            // 
            this.picdetalles.Location = new System.Drawing.Point(12, 59);
            this.picdetalles.Name = "picdetalles";
            this.picdetalles.Size = new System.Drawing.Size(237, 179);
            this.picdetalles.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picdetalles.TabIndex = 1;
            this.picdetalles.TabStop = false;
            // 
            // lbldetalle
            // 
            this.lbldetalle.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbldetalle.ForeColor = System.Drawing.Color.White;
            this.lbldetalle.Location = new System.Drawing.Point(12, 9);
            this.lbldetalle.Name = "lbldetalle";
            this.lbldetalle.Size = new System.Drawing.Size(570, 28);
            this.lbldetalle.TabIndex = 2;
            this.lbldetalle.Text = "NO EXISTE EN LA BASE DE DATOS ";
            this.lbldetalle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbldetalle.Click += new System.EventHandler(this.lbldetalle_Click);
            // 
            // DETALLES_RUTINAS
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(598, 253);
            this.Controls.Add(this.lbldetalle);
            this.Controls.Add(this.picdetalles);
            this.Controls.Add(this.txtdetalles);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "DETALLES_RUTINAS";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DETALLES_RUTINAS";
            this.Load += new System.EventHandler(this.DETALLES_RUTINAS_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picdetalles)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox txtdetalles;
        private System.Windows.Forms.PictureBox picdetalles;
        private System.Windows.Forms.Label lbldetalle;
    }
}
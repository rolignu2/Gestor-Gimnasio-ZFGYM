namespace ZONE_FITNESS_3._0_FINAL
{
    partial class CONTRASEÑA_LOGIN
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CONTRASEÑA_LOGIN));
            this.link = new System.Windows.Forms.LinkLabel();
            this.label1 = new System.Windows.Forms.Label();
            this.progreso_ = new System.Windows.Forms.ProgressBar();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // link
            // 
            this.link.AutoSize = true;
            this.link.LinkColor = System.Drawing.Color.Yellow;
            this.link.Location = new System.Drawing.Point(12, 52);
            this.link.Name = "link";
            this.link.Size = new System.Drawing.Size(64, 13);
            this.link.TabIndex = 0;
            this.link.TabStop = true;
            this.link.Text = "MUY BAJO ";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(12, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(157, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "NIVEL DE SEGURIDAD:";
            // 
            // progreso_
            // 
            this.progreso_.Location = new System.Drawing.Point(114, 52);
            this.progreso_.Name = "progreso_";
            this.progreso_.Size = new System.Drawing.Size(134, 11);
            this.progreso_.TabIndex = 2;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // CONTRASEÑA_LOGIN
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DimGray;
            this.ClientSize = new System.Drawing.Size(256, 72);
            this.Controls.Add(this.progreso_);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.link);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "CONTRASEÑA_LOGIN";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "CONTRASEÑA_LOGIN";
            this.Load += new System.EventHandler(this.CONTRASEÑA_LOGIN_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.LinkLabel link;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ProgressBar progreso_;
        private System.Windows.Forms.Timer timer1;
    }
}
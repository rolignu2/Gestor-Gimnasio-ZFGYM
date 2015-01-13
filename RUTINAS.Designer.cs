namespace ZONE_FITNESS_3._0_FINAL
{
    partial class RUTINAS
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RUTINAS));
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.panel_arriba = new System.Windows.Forms.Panel();
            this.panel_Centro = new System.Windows.Forms.Panel();
            this.panelizquierdo = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 10;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // timer2
            // 
            this.timer2.Enabled = true;
            this.timer2.Interval = 1000;
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // panel_arriba
            // 
            this.panel_arriba.BackColor = System.Drawing.Color.Black;
            this.panel_arriba.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel_arriba.Location = new System.Drawing.Point(12, 12);
            this.panel_arriba.Name = "panel_arriba";
            this.panel_arriba.Size = new System.Drawing.Size(959, 59);
            this.panel_arriba.TabIndex = 3;
            // 
            // panel_Centro
            // 
            this.panel_Centro.BackColor = System.Drawing.Color.Black;
            this.panel_Centro.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel_Centro.Location = new System.Drawing.Point(331, 77);
            this.panel_Centro.Name = "panel_Centro";
            this.panel_Centro.Size = new System.Drawing.Size(640, 529);
            this.panel_Centro.TabIndex = 1;
            // 
            // panelizquierdo
            // 
            this.panelizquierdo.BackColor = System.Drawing.Color.Black;
            this.panelizquierdo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelizquierdo.Location = new System.Drawing.Point(12, 77);
            this.panelizquierdo.Name = "panelizquierdo";
            this.panelizquierdo.Size = new System.Drawing.Size(313, 529);
            this.panelizquierdo.TabIndex = 0;
            // 
            // RUTINAS
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(982, 609);
            this.Controls.Add(this.panel_arriba);
            this.Controls.Add(this.panel_Centro);
            this.Controls.Add(this.panelizquierdo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "RUTINAS";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "RUTINAS";
            this.Load += new System.EventHandler(this.RUTINAS_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelizquierdo;
        private System.Windows.Forms.Panel panel_Centro;
        private System.Windows.Forms.Panel panel_arriba;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Timer timer2;

    }
}
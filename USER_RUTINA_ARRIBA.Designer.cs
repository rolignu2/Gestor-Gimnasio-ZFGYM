namespace ZONE_FITNESS_3._0_FINAL
{
    partial class USER_RUTINA_ARRIBA
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.lbltitulo = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.combo_dia = new System.Windows.Forms.ComboBox();
            this.shapeContainer1 = new Microsoft.VisualBasic.PowerPacks.ShapeContainer();
            this.lbl_line_chape = new Microsoft.VisualBasic.PowerPacks.LineShape();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // lbltitulo
            // 
            this.lbltitulo.AutoSize = true;
            this.lbltitulo.Font = new System.Drawing.Font("Ravie", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbltitulo.ForeColor = System.Drawing.Color.White;
            this.lbltitulo.Location = new System.Drawing.Point(10, 15);
            this.lbltitulo.Name = "lbltitulo";
            this.lbltitulo.Size = new System.Drawing.Size(366, 30);
            this.lbltitulo.TabIndex = 0;
            this.lbltitulo.Text = "ZONE FITNESS RUTINAS ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Wide Latin", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(626, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(110, 26);
            this.label2.TabIndex = 1;
            this.label2.Text = "DIA :";
            // 
            // combo_dia
            // 
            this.combo_dia.Font = new System.Drawing.Font("Ravie", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.combo_dia.FormattingEnabled = true;
            this.combo_dia.Location = new System.Drawing.Point(742, 24);
            this.combo_dia.Name = "combo_dia";
            this.combo_dia.Size = new System.Drawing.Size(205, 27);
            this.combo_dia.TabIndex = 2;
            this.combo_dia.SelectedIndexChanged += new System.EventHandler(this.combo_dia_SelectedIndexChanged);
            // 
            // shapeContainer1
            // 
            this.shapeContainer1.Location = new System.Drawing.Point(0, 0);
            this.shapeContainer1.Margin = new System.Windows.Forms.Padding(0);
            this.shapeContainer1.Name = "shapeContainer1";
            this.shapeContainer1.Shapes.AddRange(new Microsoft.VisualBasic.PowerPacks.Shape[] {
            this.lbl_line_chape});
            this.shapeContainer1.Size = new System.Drawing.Size(959, 53);
            this.shapeContainer1.TabIndex = 3;
            this.shapeContainer1.TabStop = false;
            // 
            // lbl_line_chape
            // 
            this.lbl_line_chape.BorderColor = System.Drawing.Color.White;
            this.lbl_line_chape.BorderWidth = 3;
            this.lbl_line_chape.Name = "lbl_line_chape";
            this.lbl_line_chape.X1 = 15;
            this.lbl_line_chape.X2 = 504;
            this.lbl_line_chape.Y1 = 44;
            this.lbl_line_chape.Y2 = 44;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // USER_RUTINA_ARRIBA
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.Controls.Add(this.combo_dia);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.shapeContainer1);
            this.Controls.Add(this.lbltitulo);
            this.Name = "USER_RUTINA_ARRIBA";
            this.Size = new System.Drawing.Size(959, 53);
            this.Load += new System.EventHandler(this.USER_RUTINA_ARRIBA_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbltitulo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox combo_dia;
        private Microsoft.VisualBasic.PowerPacks.ShapeContainer shapeContainer1;
        private Microsoft.VisualBasic.PowerPacks.LineShape lbl_line_chape;
        private System.Windows.Forms.Timer timer1;
    }
}

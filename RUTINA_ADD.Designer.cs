namespace ZONE_FITNESS_3._0_FINAL
{
    partial class RUTINA_ADD
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RUTINA_ADD));
            this.label1 = new System.Windows.Forms.Label();
            this.combo_set = new System.Windows.Forms.ComboBox();
            this.combo_rep = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmd_add = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(84, 49);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "N° SETS:";
            // 
            // combo_set
            // 
            this.combo_set.FormattingEnabled = true;
            this.combo_set.Location = new System.Drawing.Point(143, 41);
            this.combo_set.Name = "combo_set";
            this.combo_set.Size = new System.Drawing.Size(105, 21);
            this.combo_set.TabIndex = 1;
            // 
            // combo_rep
            // 
            this.combo_rep.FormattingEnabled = true;
            this.combo_rep.Location = new System.Drawing.Point(143, 78);
            this.combo_rep.Name = "combo_rep";
            this.combo_rep.Size = new System.Drawing.Size(105, 21);
            this.combo_rep.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(36, 86);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(104, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "N° REPETICIONES:";
            // 
            // cmd_add
            // 
            this.cmd_add.Location = new System.Drawing.Point(24, 117);
            this.cmd_add.Name = "cmd_add";
            this.cmd_add.Size = new System.Drawing.Size(281, 26);
            this.cmd_add.TabIndex = 4;
            this.cmd_add.Text = "GUARDAR";
            this.cmd_add.UseVisualStyleBackColor = true;
            this.cmd_add.Click += new System.EventHandler(this.cmd_add_Click);
            // 
            // RUTINA_ADD
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(319, 150);
            this.Controls.Add(this.cmd_add);
            this.Controls.Add(this.combo_rep);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.combo_set);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "RUTINA_ADD";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "RUTINA_ADD";
            this.Load += new System.EventHandler(this.RUTINA_ADD_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox combo_set;
        private System.Windows.Forms.ComboBox combo_rep;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button cmd_add;
    }
}
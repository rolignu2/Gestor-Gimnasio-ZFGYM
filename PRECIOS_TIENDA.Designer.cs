namespace ZONE_FITNESS_3._0_FINAL
{
    partial class PRECIOS_TIENDA
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
            this.grilla_tienda = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.LINKCLOSE = new System.Windows.Forms.LinkLabel();
            this.LINK_DEL = new System.Windows.Forms.LinkLabel();
            this.LINK_ADD = new System.Windows.Forms.LinkLabel();
            this.label1 = new System.Windows.Forms.Label();
            this.TXTNOMBRE = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.grilla_tienda)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // grilla_tienda
            // 
            this.grilla_tienda.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grilla_tienda.Location = new System.Drawing.Point(183, 12);
            this.grilla_tienda.Name = "grilla_tienda";
            this.grilla_tienda.Size = new System.Drawing.Size(279, 96);
            this.grilla_tienda.TabIndex = 0;
            this.grilla_tienda.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grilla_tienda_CellClick);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.LINKCLOSE);
            this.groupBox1.Controls.Add(this.LINK_DEL);
            this.groupBox1.Controls.Add(this.LINK_ADD);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.TXTNOMBRE);
            this.groupBox1.ForeColor = System.Drawing.Color.White;
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(165, 96);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "PRECIOS TIENDA";
            // 
            // LINKCLOSE
            // 
            this.LINKCLOSE.AutoSize = true;
            this.LINKCLOSE.LinkColor = System.Drawing.Color.Red;
            this.LINKCLOSE.Location = new System.Drawing.Point(145, 0);
            this.LINKCLOSE.Name = "LINKCLOSE";
            this.LINKCLOSE.Size = new System.Drawing.Size(14, 13);
            this.LINKCLOSE.TabIndex = 2;
            this.LINKCLOSE.TabStop = true;
            this.LINKCLOSE.Text = "X";
            this.LINKCLOSE.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LINKCLOSE_LinkClicked);
            // 
            // LINK_DEL
            // 
            this.LINK_DEL.AutoSize = true;
            this.LINK_DEL.LinkColor = System.Drawing.Color.Yellow;
            this.LINK_DEL.Location = new System.Drawing.Point(16, 68);
            this.LINK_DEL.Name = "LINK_DEL";
            this.LINK_DEL.Size = new System.Drawing.Size(58, 13);
            this.LINK_DEL.TabIndex = 3;
            this.LINK_DEL.TabStop = true;
            this.LINK_DEL.Text = "ELIMINAR";
            this.LINK_DEL.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LINK_DEL_LinkClicked);
            // 
            // LINK_ADD
            // 
            this.LINK_ADD.AutoSize = true;
            this.LINK_ADD.LinkColor = System.Drawing.Color.Yellow;
            this.LINK_ADD.Location = new System.Drawing.Point(81, 68);
            this.LINK_ADD.Name = "LINK_ADD";
            this.LINK_ADD.Size = new System.Drawing.Size(60, 13);
            this.LINK_ADD.TabIndex = 2;
            this.LINK_ADD.TabStop = true;
            this.LINK_ADD.Text = "AGREGAR";
            this.LINK_ADD.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LINK_ADD_LinkClicked);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(16, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "ADD PRODUCTO";
            // 
            // TXTNOMBRE
            // 
            this.TXTNOMBRE.Location = new System.Drawing.Point(19, 41);
            this.TXTNOMBRE.Name = "TXTNOMBRE";
            this.TXTNOMBRE.Size = new System.Drawing.Size(122, 20);
            this.TXTNOMBRE.TabIndex = 2;
            // 
            // PRECIOS_TIENDA
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(466, 121);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.grilla_tienda);
            this.Name = "PRECIOS_TIENDA";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PRECIOS_TIENDA";
            this.Load += new System.EventHandler(this.PRECIOS_TIENDA_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grilla_tienda)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView grilla_tienda;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox TXTNOMBRE;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.LinkLabel LINKCLOSE;
        private System.Windows.Forms.LinkLabel LINK_DEL;
        private System.Windows.Forms.LinkLabel LINK_ADD;
    }
}
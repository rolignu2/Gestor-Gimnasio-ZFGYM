namespace GestorGimnasio
{
    partial class PROMOCIONES
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
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblcantidad = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.check_seleccionar_todo = new System.Windows.Forms.CheckBox();
            this.button2 = new System.Windows.Forms.Button();
            this.checkcorreo = new System.Windows.Forms.CheckedListBox();
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.cmdeditar = new System.Windows.Forms.Button();
            this.timecuerpo = new System.Windows.Forms.Timer(this.components);
            this.button1 = new System.Windows.Forms.Button();
            this.lblmensaje = new System.Windows.Forms.Label();
            this.timerhilo = new System.Windows.Forms.Timer(this.components);
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Stencil", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(159, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(117, 19);
            this.label1.TabIndex = 0;
            this.label1.Text = "PROMOCIONES";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.groupBox3);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(2, 9);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(338, 405);
            this.panel1.TabIndex = 1;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::GestorGimnasio.Properties.Resources.logozfg;
            this.pictureBox1.Location = new System.Drawing.Point(39, 4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(100, 50);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 5;
            this.pictureBox1.TabStop = false;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.linkLabel1);
            this.groupBox3.Controls.Add(this.groupBox1);
            this.groupBox3.Controls.Add(this.groupBox2);
            this.groupBox3.Controls.Add(this.checkcorreo);
            this.groupBox3.ForeColor = System.Drawing.Color.White;
            this.groupBox3.Location = new System.Drawing.Point(10, 54);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(321, 348);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Opciones";
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.LinkColor = System.Drawing.Color.Yellow;
            this.linkLabel1.Location = new System.Drawing.Point(8, 74);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(146, 13);
            this.linkLabel1.TabIndex = 0;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "SERVIDOR CONECTADO A:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblcantidad);
            this.groupBox1.Controls.Add(this.button3);
            this.groupBox1.Location = new System.Drawing.Point(11, 19);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(295, 49);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            // 
            // lblcantidad
            // 
            this.lblcantidad.AutoSize = true;
            this.lblcantidad.Location = new System.Drawing.Point(6, 21);
            this.lblcantidad.Name = "lblcantidad";
            this.lblcantidad.Size = new System.Drawing.Size(34, 13);
            this.lblcantidad.TabIndex = 1;
            this.lblcantidad.Text = "Total:";
            // 
            // button3
            // 
            this.button3.ForeColor = System.Drawing.Color.Black;
            this.button3.Location = new System.Drawing.Point(142, 16);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(137, 23);
            this.button3.TabIndex = 0;
            this.button3.Text = "Correos No Existentes";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.check_seleccionar_todo);
            this.groupBox2.Controls.Add(this.button2);
            this.groupBox2.Location = new System.Drawing.Point(11, 295);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(295, 47);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            // 
            // check_seleccionar_todo
            // 
            this.check_seleccionar_todo.AutoSize = true;
            this.check_seleccionar_todo.ForeColor = System.Drawing.Color.Yellow;
            this.check_seleccionar_todo.Location = new System.Drawing.Point(18, 21);
            this.check_seleccionar_todo.Name = "check_seleccionar_todo";
            this.check_seleccionar_todo.Size = new System.Drawing.Size(115, 17);
            this.check_seleccionar_todo.TabIndex = 4;
            this.check_seleccionar_todo.Text = "Seleccionar Todos";
            this.check_seleccionar_todo.UseVisualStyleBackColor = true;
            this.check_seleccionar_todo.CheckedChanged += new System.EventHandler(this.checkseleccionar_CheckedChanged);
            // 
            // button2
            // 
            this.button2.ForeColor = System.Drawing.Color.Black;
            this.button2.Location = new System.Drawing.Point(154, 17);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(113, 23);
            this.button2.TabIndex = 1;
            this.button2.Text = "Enviar";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // checkcorreo
            // 
            this.checkcorreo.FormattingEnabled = true;
            this.checkcorreo.Location = new System.Drawing.Point(11, 90);
            this.checkcorreo.Name = "checkcorreo";
            this.checkcorreo.Size = new System.Drawing.Size(295, 199);
            this.checkcorreo.TabIndex = 1;
            // 
            // webBrowser1
            // 
            this.webBrowser1.Location = new System.Drawing.Point(346, 95);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.Size = new System.Drawing.Size(453, 316);
            this.webBrowser1.TabIndex = 2;
            // 
            // cmdeditar
            // 
            this.cmdeditar.Location = new System.Drawing.Point(346, 66);
            this.cmdeditar.Name = "cmdeditar";
            this.cmdeditar.Size = new System.Drawing.Size(195, 23);
            this.cmdeditar.TabIndex = 3;
            this.cmdeditar.Text = "Nuevo Cuerpo del Mensaje";
            this.cmdeditar.UseVisualStyleBackColor = true;
            this.cmdeditar.Click += new System.EventHandler(this.cmdeditar_Click);
            // 
            // timecuerpo
            // 
            this.timecuerpo.Enabled = true;
            this.timecuerpo.Interval = 10;
            this.timecuerpo.Tick += new System.EventHandler(this.timecuerpo_Tick);
            // 
            // button1
            // 
            this.button1.ForeColor = System.Drawing.Color.Black;
            this.button1.Location = new System.Drawing.Point(547, 66);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(185, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "Guardar Cambios";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // lblmensaje
            // 
            this.lblmensaje.AutoSize = true;
            this.lblmensaje.BackColor = System.Drawing.Color.Transparent;
            this.lblmensaje.ForeColor = System.Drawing.Color.White;
            this.lblmensaje.Location = new System.Drawing.Point(346, 50);
            this.lblmensaje.Name = "lblmensaje";
            this.lblmensaje.Size = new System.Drawing.Size(56, 13);
            this.lblmensaje.TabIndex = 4;
            this.lblmensaje.Text = "lblmensaje";
            // 
            // timerhilo
            // 
            this.timerhilo.Tick += new System.EventHandler(this.timerhilo_Tick);
            // 
            // PROMOCIONES
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.BackgroundImage = global::GestorGimnasio.Properties.Resources.FondoEstandarv1_2;
            this.ClientSize = new System.Drawing.Size(811, 427);
            this.Controls.Add(this.lblmensaje);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.cmdeditar);
            this.Controls.Add(this.webBrowser1);
            this.Controls.Add(this.panel1);
            this.Name = "PROMOCIONES";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PROMOCIONES";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PROMOCIONES_FormClosing);
            this.Load += new System.EventHandler(this.PROMOCIONES_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.CheckedListBox checkcorreo;
        private System.Windows.Forms.WebBrowser webBrowser1;
        private System.Windows.Forms.Button cmdeditar;
        private System.Windows.Forms.Timer timecuerpo;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox check_seleccionar_todo;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label lblmensaje;
        private System.Windows.Forms.Timer timerhilo;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lblcantidad;
        private System.Windows.Forms.Button button3;
    }
}
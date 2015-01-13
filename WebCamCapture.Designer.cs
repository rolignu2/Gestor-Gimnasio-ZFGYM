namespace GestorGimnasio
{
    partial class WebCamCapture
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
            this.cmdcapturar = new System.Windows.Forms.Button();
            this.cmdguardar = new System.Windows.Forms.Button();
            this.pctbox_imagen = new System.Windows.Forms.PictureBox();
            this.pctbox_webcam = new System.Windows.Forms.PictureBox();
            this.button1 = new System.Windows.Forms.Button();
            this.combodispositivos = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.pctbox_imagen)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pctbox_webcam)).BeginInit();
            this.SuspendLayout();
            // 
            // cmdcapturar
            // 
            this.cmdcapturar.Image = global::GestorGimnasio.Properties.Resources._11339;
            this.cmdcapturar.Location = new System.Drawing.Point(353, 293);
            this.cmdcapturar.Name = "cmdcapturar";
            this.cmdcapturar.Size = new System.Drawing.Size(71, 43);
            this.cmdcapturar.TabIndex = 7;
            this.cmdcapturar.UseVisualStyleBackColor = true;
            this.cmdcapturar.Click += new System.EventHandler(this.cmdcapturar_Click);
            // 
            // cmdguardar
            // 
            this.cmdguardar.Image = global::GestorGimnasio.Properties.Resources.save1;
            this.cmdguardar.Location = new System.Drawing.Point(199, 293);
            this.cmdguardar.Name = "cmdguardar";
            this.cmdguardar.Size = new System.Drawing.Size(71, 43);
            this.cmdguardar.TabIndex = 6;
            this.cmdguardar.UseVisualStyleBackColor = true;
            this.cmdguardar.Click += new System.EventHandler(this.cmdguardar_Click);
            // 
            // pctbox_imagen
            // 
            this.pctbox_imagen.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pctbox_imagen.Location = new System.Drawing.Point(341, 12);
            this.pctbox_imagen.Name = "pctbox_imagen";
            this.pctbox_imagen.Size = new System.Drawing.Size(300, 275);
            this.pctbox_imagen.TabIndex = 5;
            this.pctbox_imagen.TabStop = false;
            // 
            // pctbox_webcam
            // 
            this.pctbox_webcam.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pctbox_webcam.Location = new System.Drawing.Point(26, 12);
            this.pctbox_webcam.Name = "pctbox_webcam";
            this.pctbox_webcam.Size = new System.Drawing.Size(300, 275);
            this.pctbox_webcam.TabIndex = 4;
            this.pctbox_webcam.TabStop = false;
            // 
            // button1
            // 
            this.button1.Image = global::GestorGimnasio.Properties.Resources.iconoCerrar21;
            this.button1.Location = new System.Drawing.Point(276, 293);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(71, 43);
            this.button1.TabIndex = 8;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // combodispositivos
            // 
            this.combodispositivos.FormattingEnabled = true;
            this.combodispositivos.Location = new System.Drawing.Point(473, 293);
            this.combodispositivos.Name = "combodispositivos";
            this.combodispositivos.Size = new System.Drawing.Size(168, 21);
            this.combodispositivos.TabIndex = 9;
            this.combodispositivos.Text = "Seleccione Controlador";
            this.combodispositivos.SelectedIndexChanged += new System.EventHandler(this.combodispositivos_SelectedIndexChanged);
            // 
            // WebCamCapture
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(688, 348);
            this.Controls.Add(this.combodispositivos);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.cmdcapturar);
            this.Controls.Add(this.cmdguardar);
            this.Controls.Add(this.pctbox_imagen);
            this.Controls.Add(this.pctbox_webcam);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "WebCamCapture";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "WebCamCapture";
            this.Load += new System.EventHandler(this.WebCamCapture_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pctbox_imagen)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pctbox_webcam)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pctbox_imagen;
        private System.Windows.Forms.PictureBox pctbox_webcam;
        private System.Windows.Forms.Button cmdguardar;
        private System.Windows.Forms.Button cmdcapturar;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ComboBox combodispositivos;
    }
}
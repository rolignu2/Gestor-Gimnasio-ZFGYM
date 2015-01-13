using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

using System.Threading;
using ZONE_FITNESS_3;
using ZONE_FITNESS_3._0_FINAL.CLASES;
using System.IO;

namespace ZONE_FITNESS_3._0_FINAL
{
    public partial class Form1 : Form
    {
        conectar_ conectar_ = new conectar_();
        GestorGimnasio.CLASES.CheckUpdate update;


        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            this.Opacity = 0.92;
            this.Text = "RSOFT 2010 ";
            progressBar1.Maximum = 100;
            progressBar1.Minimum = 0;
            progressBar1.Style = ProgressBarStyle.Marquee;
            conectar_.GetAdodbConection();
            conectar_.GetoledbConection();
           // Checkupdate();
           // Actualizar.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            try
            {
                if (conectar_.IsaliveConect() != false)
                {
                    LOGIN login = new LOGIN();
                    timer1.Enabled = false;
                    this.Hide();
                    login.Show();
                }
            }
            catch
            {
                if (conectar_.IsaliveConect() == true)
                {
                    LOGIN login = new LOGIN();
                    timer1.Enabled = false;
                    this.Hide();
                    login.Show();
                }
            }

               

        }

        private void Checkupdate()
        {
            update = new GestorGimnasio.CLASES.CheckUpdate(this);
            update.Setconfig("https://dl.dropbox.com/u/75344773/Zonefitness_update/", "update.xml", false);
            update.Iniciar();
        }

        private void Actualizar_Tick(object sender, EventArgs e)
        {
            try
            {
                if (update.IsNotAliveUpdate() != true)
                {
                    label1.Text = "Actualizando...";
                }
                else this.Actualizar.Enabled = false;
            }
            catch { }
        }

       
    
    }
}

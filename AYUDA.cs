using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using ZONE_FITNESS_3._0_FINAL.CLASES;

namespace ZONE_FITNESS_3._0_FINAL
{
    public partial class AYUDA : Form
    {
        public AYUDA()
        {
            InitializeComponent();
        }

        private void AYUDA_Load(object sender, EventArgs e)
        {
            this.Opacity = 0.92;

            version_gestor version = new version_gestor();

            List<string> datos = version.GetVersion();

            label2.Text = "VERSION: " + datos[1].ToString();
            label3.Text = "NOMBRE: " + version.Nombre;
            label4.Text = "COPYRIGHT: " + version.Copyright;
            label5.Text = "COMPAÑIA: " + version.Compañia;
            label6.Text = "ENSAMBLADOR: " + datos[0].ToString();
            label7.Text = "NOMBRE DEL ARCHIVO: " + datos[2].ToString();
            label8.Text = "DESCRIPCION: " + datos[3].ToString();
            label9.Text = "COMPILADO: " + datos[4].ToString();

            Uri navegador = new Uri(Ayuda.direccion_ayuda_online());
            webBrowser1.Navigate(navegador);
            webBrowser1.ScrollBarsEnabled = true;
        }

        private void cmd_ayuda_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start("iexplore.exe", Ayuda.direccion_ayuda_online() );
            }
            catch { }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                GestorGimnasio.CLASES.CheckUpdate update = new GestorGimnasio.CLASES.CheckUpdate(this);
                update.Setconfig("https://dl.dropbox.com/u/75344773/Gestor_Gym_Actualizacion/", "update.xml", false);
                update.Iniciar();
            }
            catch { }
        }

        private void CMD_REGISTRAR_Click(object sender, EventArgs e)
        {
            REGISTRAR_ REGISTRAR = new REGISTRAR_();
            REGISTRAR.Show();
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using ZONE_FITNESS_3._0_FINAL.CLASES;
using System.Threading;
using GestorGimnasio.CLASES;

namespace GestorGimnasio
{
    public partial class DATOS_SOCIOS : Form
    {
        public DATOS_SOCIOS()
        {
            InitializeComponent();
        }

        private void DatosSocios_Load(object sender, EventArgs e)
        {
            this.Name = "Vista previa";
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            GetDatos();
        }

        private void GetDatos()
        {

            if (EnvioDatos_Socios.Error == true) { this.Close(); return; }

            try
            {
                linknombre.Text = EnvioDatos_Socios.Datos_Socios[0].ToString() + " " + EnvioDatos_Socios.Datos_Socios[1].ToString();
                linkinicio.Text = EnvioDatos_Socios.Datos_Socios[2].ToString();
                linkexpira.Text = EnvioDatos_Socios.Datos_Socios[3].ToString();
                linkcouta.Text = EnvioDatos_Socios.Datos_Socios[4].ToString();
                linkpago.Text = EnvioDatos_Socios.Datos_Socios[5].ToString();
                txtcoment.Text = EnvioDatos_Socios.Datos_Socios[6].ToString();
                linkfactura.Text = EnvioDatos_Socios.Datos_Socios[9].ToString();
                linknsocio2.Text = EnvioDatos_Socios.Datos_Socios[7].ToString();
                linkasocio2.Text  = EnvioDatos_Socios.Datos_Socios[8].ToString();
                linkmail.Text = EnvioDatos_Socios.Datos_Socios[10].ToString();


                try
                {

                    Thread hiloImagen = new Thread(delegate()
                    {
                        try
                        {
                            Seguridad.DownLoadImage(pictureBox1, EnvioDatos_Socios.Datos_Socios[11].ToString());
                          
                        }
                        catch { }
                    });
                    hiloImagen.Start();
                }
                catch
                {
                    return;
                }

            }
            catch { }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }


    }
}

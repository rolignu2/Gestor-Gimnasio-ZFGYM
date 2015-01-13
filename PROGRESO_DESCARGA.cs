using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Net;

namespace ZONE_FITNESS_3._0_FINAL
{
    public partial class PROGRESO_DESCARGA : Form
    {

        WebClient wc = new WebClient(); 


        public PROGRESO_DESCARGA()
        {
            InitializeComponent();
        }


        public static string ADRESS = "http://external.ivirtualdocket.com/update.cab";
        public static string DIRECCION_DESCARGA = System.IO.Directory.GetCurrentDirectory() + @"\DESCARGAS";

        WebClient myWebClient = new WebClient();

        private void PROGRESO_DESCARGA_Load(object sender, EventArgs e)
        {
           
            this.Text = "ACTUALIZAR...";
            this.BackColor = Color.DarkBlue;


            linkLabel1.Text = "0%";
            label1.Text = "0Kb/0Kb";
            label1.ForeColor = Color.Red;
            
        }

        private void cancelar_descarga()
        {
            myWebClient.CancelAsync();
            progressBar1.Value = 0;
            linkLabel1.Text = "0";
            label1.Text = "0Kb/0Kb";
            MessageBox.Show("DESCARGA CANCELADA ", "CANCELADO", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            this.Close();
        }

        private void Iniciar_descarga()
        {

            GET_DIRECCION();//obtenemos la direccion
            progressBar1.Value = 0;
            progressBar1.Maximum = 100;
            myWebClient.DownloadDataCompleted += new DownloadDataCompletedEventHandler(myWebClient_DownloadDataCompleted);
            myWebClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(myWebClient_DownloadProgressChanged);
            Uri uri = new Uri(ADRESS);
            myWebClient.DownloadFileAsync(uri, DIRECCION_DESCARGA);
        }
        void myWebClient_DownloadProgressChanged(object sender, System.Net.DownloadProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
            label1.Text = e.BytesReceived/1000 + "Kb /" + e.TotalBytesToReceive/1000 + "Kb";
            linkLabel1.Text = progressBar1.Value + "%";
            if (progressBar1.Value == 47) { linkLabel1.ForeColor = Color.Blue; }
        }


        void myWebClient_DownloadDataCompleted(object sender, DownloadDataCompletedEventArgs e)
        {
            progressBar1.Value = progressBar1.Maximum;
            MessageBox.Show("DESCARGA COMPLETADA CON EXITO ", "DESCARGA COMPLETA", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Iniciar_descarga();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            cancelar_descarga();
        }

        private string GET_DIRECCION()
        {
            try
            {

                SaveFileDialog abrir_directorio = new SaveFileDialog();
                string[] seleccionar_ruta = new string[5000];

                object rutas;
                abrir_directorio.ValidateNames = true;
                abrir_directorio.Title = "BUSCAR DIRECCION...";
                abrir_directorio.FileName = "Gestor_gym.exe";
                abrir_directorio.ShowDialog();

                if (abrir_directorio.FileName != null)
                {
                    seleccionar_ruta = abrir_directorio.FileNames;
                    foreach (object rutas_loopVariable in seleccionar_ruta)
                    {
                        rutas = rutas_loopVariable;//variable objeto dandole un ciclo booleano al object
                        System.IO.FileInfo fileinfo = new System.IO.FileInfo(Convert.ToString(rutas));//como es un objeto necesitamos hacerlo string o cadena
                        DIRECCION_DESCARGA = Convert.ToString(fileinfo);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return DIRECCION_DESCARGA;
       
        }


    }
}

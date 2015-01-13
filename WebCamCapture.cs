using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ZONE_FITNESS_3._0_FINAL;
using AForge.Video.DirectShow;
using AForge.Video;

namespace GestorGimnasio
{
    public partial class WebCamCapture : Form
    {

        private bool existenDispositivos = false;
        private bool fotografiaHecha = false;
        private FilterInfoCollection dispositivosDeVideo;
        private VideoCaptureDevice fuenteDeVideo = null;

        public WebCamCapture()
        {
            InitializeComponent();
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CerrarFormulario();
            this.Close();
        }

        private void WebCamCapture_Load(object sender, EventArgs e)
        {
            fuenteDeVideo = new VideoCaptureDevice();
            PRINCIPAL.WebcamCapture = string.Empty;
            BuscarDispositivos();
            ConectarDispositivo(0);
           
        }

        private void ConectarDispositivo(int Dispositivo)
        {

            if (existenDispositivos)
            {
                fuenteDeVideo.Source = dispositivosDeVideo[Dispositivo].MonikerString;
                fuenteDeVideo.NewFrame += new NewFrameEventHandler(MostrarImagen);
                if (fuenteDeVideo.IsRunning)
                {
                    fuenteDeVideo.Stop();
                    fuenteDeVideo.Start();
                }
                else
                    fuenteDeVideo.Start();
            }
            else
            {
                MessageBox.Show("No se encuentra ningún dispositivo de vídeo en el sistema", "Información",
                                MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                CerrarFormulario();
            }
        }

        private void BuscarDispositivos()
        {
            dispositivosDeVideo = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            combodispositivos.Items.Clear();
           
            if (dispositivosDeVideo.Count == 0)
                existenDispositivos = false;
            else
            {
                for(int i = 0 ; i < dispositivosDeVideo.Count ; i++)
                {
                    combodispositivos.Items.Add("Dispositivo " + i);
                }
                existenDispositivos = true;
            }
               
        }

        private void CerrarFormulario()
        {
            if (fuenteDeVideo != null)
            {
                if (fuenteDeVideo.IsRunning)
                {
                    fuenteDeVideo.SignalToStop();
                    fuenteDeVideo = null;
                }
            }

            Dispose();
            Close();
        }

        private void MostrarImagen(object sender, NewFrameEventArgs eventArgs)
        {
            Bitmap imagen = (Bitmap)eventArgs.Frame.Clone();
            pctbox_webcam.Image = imagen;
        }

        /*
         *  Deja de capturar imágenes, obteniendo la última capturada
         */
        private void Capturar()
        {
            if (fuenteDeVideo != null)
            {
                if (fuenteDeVideo.IsRunning)
                {
                    pctbox_imagen.Image = pctbox_webcam.Image;
                }
            }
        }

        private void combodispositivos_SelectedIndexChanged(object sender, EventArgs e)
        {
            ConectarDispositivo(combodispositivos.SelectedIndex);
        }

        private void cmdcapturar_Click(object sender, EventArgs e)
        {
            Capturar();
            fotografiaHecha = true;
        }

        private void cmdguardar_Click(object sender, EventArgs e)
        {
            if (fotografiaHecha)
            {
                // Recorto la imagen conforme lo mostrado (Size del pctbox_webcam)
                Rectangle formaRecorte = new Rectangle(0, 0, 300, 300);
                Bitmap imagenOrigen = (Bitmap)pctbox_imagen.Image;
                Bitmap imagen = imagenOrigen.Clone(formaRecorte, imagenOrigen.PixelFormat);

                // Y la guardo
                try
                {
                    Random rnd1 = new Random(DateTime.Now.Millisecond);
                    Random rnd2 = new Random(DateTime.Now.Millisecond);


                    string ruta = null;
                    SaveFileDialog savedialog = new SaveFileDialog();
                    savedialog.ValidateNames = true;
                    savedialog.Title = "Guardar Imagen";
                    savedialog.FileName = "Imagen" + rnd1.Next(0, 100) + rnd2.Next(100, 999) + ".jpg";
                    savedialog.ShowDialog();

                    if (savedialog.FileName != null)
                    {
                        string[] seleccionar_ruta = savedialog.FileNames; 
                       
                        foreach (object rutas_loopVariable in seleccionar_ruta)
                        {
                            var rutas = rutas_loopVariable;
                            System.IO.FileInfo fileinfo = new System.IO.FileInfo(Convert.ToString(rutas));//como es un objeto necesitamos hacerlo string o cadena
                            ruta = Convert.ToString(fileinfo);
                        }
                    }
                    PRINCIPAL.WebcamCapture = ruta;
                    PRINCIPAL.EnableTimeWebCam = true;
                    imagen.Save(ruta);
                    MessageBox.Show("Fotografía almacenada", "Información",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                catch (Exception exc)
                {
                    MessageBox.Show(exc.Message, "Información",
                                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            else
            {
                MessageBox.Show("Para guardar la fotografía, use en primer lugar el botón de Captura", "Información",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

    }
}

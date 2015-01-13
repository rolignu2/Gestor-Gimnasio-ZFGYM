using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MatthiWare;
using MatthiWare.Utilities;
using MatthiWare.UpdateFormat;
using MatthiWare.Helpers;

using System.Windows.Forms;
using System.Net;


namespace GestorGimnasio.CLASES
{
    class CheckUpdate
    {

        protected Form frm;
        protected UpdateDialog update;
        protected System.Threading.Thread hilo;

        public CheckUpdate(Form formulario)
        {
            this.frm = formulario;
        }

        public void Setconfig(string servidor, string file, bool especialmensaje)
        {
            try
            {
                update = new UpdateDialog(this.frm);
                update.DownloadServerPath = servidor;
                update.ConfigFile = file;
                update.latestVersion += new UpdateDialog.onLatestVersion(update_latestVersion);
                update.newerVersion += new UpdateDialog.onNewerVersion(update_newerVersion);
                update.Error += new UpdateDialog.onError(update_Error);
                update.ShowSpecialMessage = especialmensaje;
                update.ShowErrorMessages = false;
            }
            catch { }
        }

        protected bool funConexion()
        {

            HttpWebRequest Req = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(new Uri("http://www.dropbox.com"));
            System.Net.HttpWebResponse res = default(System.Net.HttpWebResponse);

            try
            {
                Req = (System.Net.HttpWebRequest)System.Net.WebRequest.Create("http://www.dropbox.com");


                res = (System.Net.HttpWebResponse)Req.GetResponse();

                Req.Abort();

                if (res.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        public void Iniciar()
        {
            hilo = new System.Threading.Thread(verificarconexion);
            hilo.Start();
        }

        public bool IsNotAliveUpdate()
        {
            if (!(hilo.IsAlive)) return true;
            else return false;
        }

        protected void verificarconexion()
        {
            try
            {
                if (funConexion() == true)
                {
                    update.StartChecking();
                }
                else
                {
                    return;
                }
            }
            catch { }
        }

        private void update_Error(object sender, UpdateArgs ErrUpdate)
        {
            MessageBox.Show(" Estamos resolviendo este problema gracias ");
        }

        private void update_latestVersion(object sender, UpdateArgs mesupdate)
        {
            update.ShowSpecialMessage = true;
            update.SpecialMessage = "SU VERSION YA ESTA ACTUALIZADA";
        }

        private void update_newerVersion(object sender, UpdateArgs mesupdate)
        {
            update.ShowSpecialMessage = true;
            update.SpecialMessage = "GESTOR DE GIMNASIO HA SIDO ACTUALIZADO A SU VERSION  " + update.getNewestVersion.ToString();
            return;
        }

       

    }
}

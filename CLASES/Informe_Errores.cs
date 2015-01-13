using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Windows.Forms;


namespace GestorGimnasio.CLASES
{
    class Informe_Errores
    {

       /* private static string Direccion = "";
        private static string Archivo = "";
        private static string user = "";
        private static string Pass = "";*/

        protected static List<string> Error = new List<string>();

        protected static bool funConexion()
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
        protected static Boolean Envio_mensaje(Boolean enviar)
        {
            switch (enviar)
            {
                case true:
                    MessageBox.Show("!!MUCHAS GRACIAS POR AYUDARNOS A MEJORAR NUESTRO SOFTWARE\n\n\r\rEL ERROR HA SIDO ENVIADO CON EXITO");
                    return true;
                case false:
                    MessageBox.Show("IMPOSIBLE CONECTARSE CON EL SERVIDOR; PUEDE QUE ESTE BLOQUEADO O NO DISPONGA DE UNA CONEXION A INTERNET");
                    return true;
            }

            return true;
        }

        protected static void Conectar_Servidor()
        {

            try
            {
                if (!(File.Exists(System.IO.Directory.GetCurrentDirectory().ToString() + @"/ErrTemporal.txt")))
                {
                    File.Create(System.IO.Directory.GetCurrentDirectory().ToString() + @"/ErrTemporal.txt").Close();
                }

                StreamWriter escribir = new StreamWriter(System.IO.Directory.GetCurrentDirectory().ToString() + @"/ErrTemporal.txt", true);

                foreach (var dato in Error)
                {
                    escribir.WriteLine(dato.ToString());
                }

                escribir.Close();

            }
            catch { }

        }
        protected static void Verificar()
        {
            if (funConexion() == false)
            {
                Envio_mensaje(false);
            }
            else
            {
                Conectar_Servidor();
                Envio_mensaje(true);
            }
        }

        private static void Servidor()
        {

        }

        public static void Enviar_Error(string error)
        {

            DialogResult resultado = MessageBox.Show("Se ha producido un error en \n\n" + error + "\n\n¿Desea reportarlo?", "Error", MessageBoxButtons.YesNo, MessageBoxIcon.Error);

            try
            {
                if (resultado == DialogResult.Yes)
                {
                    Error.Add(error);
                    System.Threading.Thread Hilo = new System.Threading.Thread(Verificar);
                    Hilo.Start();
                }
            }
            catch { }

            return;
        }
    }
}

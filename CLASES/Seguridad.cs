using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Net;
using System.IO;

namespace GestorGimnasio.CLASES
{
    class Seguridad
    {

        private static String ftpcarpeta = @"/zfgym_img//";
        private static String ftpServerIP = "wildsoft.webuda.com";
        private static String ftpUserID = "a6389965";
        private static String ftpPassword = "linux2011";

        public static Boolean IsEmail(String email)
        {
            String expresion;
            expresion = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";
       

            if (Regex.IsMatch(email, expresion))
            {
                if (Regex.Replace(email, expresion, String.Empty).Length == 0)
                    return true;
                else
                    return false;
            }
            else
                return false;
        }

        public static Boolean UploadImagen(string rutaArchivo)
        {

            if (rutaArchivo == "") return false;

            FileInfo fileInf = new FileInfo(rutaArchivo);
            string uri = "ftp://" + ftpServerIP + "/" + fileInf.Name;
            FtpWebRequest reqFTP;

            try
            {
                reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri("ftp://" + ftpServerIP + ftpcarpeta + "/" + fileInf.Name));
                reqFTP.Credentials = new NetworkCredential(ftpUserID, ftpPassword);
                reqFTP.KeepAlive = false;
                reqFTP.Method = WebRequestMethods.Ftp.UploadFile;
                reqFTP.UseBinary = true;
                reqFTP.ContentLength = fileInf.Length;

                int buffLength = 4048;
                byte[] buff = new byte[buffLength];
                int contentLen;

                FileStream fs = fileInf.OpenRead();

                try
                {
                    Stream strm = reqFTP.GetRequestStream();

                    contentLen = fs.Read(buff, 0, buffLength);

                    while (contentLen != 0)
                    {
                        strm.Write(buff, 0, contentLen);
                        contentLen = fs.Read(buff, 0, buffLength);
                    }

                    strm.Close();
                    fs.Close();

                }
                catch { }
            }
            catch { }

            return true;
        }

        public static Boolean ExisteImagen(string archivo)
        {

            try
            {

                FtpWebRequest dirFtp = 
                    ((FtpWebRequest)FtpWebRequest.Create(new Uri("ftp://wildsoft.webuda.com" + ftpcarpeta)));
                
                NetworkCredential cr =
                    new NetworkCredential(ftpUserID, ftpPassword);
               
                dirFtp.Credentials = cr;

                dirFtp.Method = WebRequestMethods.Ftp.ListDirectory;

                StreamReader reader =
                       new StreamReader(dirFtp.GetResponse().GetResponseStream());
              

                while (reader.Peek() >= 0)
                {
                    var FtpImagen = reader.ReadLine();
                    if (string.Equals(archivo, FtpImagen))
                    {
                        reader.Close();
                        return true;
                    }
                }

                reader.Close();
            }
            catch { }

           
            return false;
        }

        public static void DownLoadImage(PictureBox ImagenSocio , string NombreImagen)
        {


            try
            {
                FtpWebRequest Request = ((FtpWebRequest)FtpWebRequest.Create(new Uri("ftp://wildsoft.webuda.com" + ftpcarpeta + NombreImagen)));
                NetworkCredential cr = new NetworkCredential(ftpUserID, ftpPassword);
                Request.Credentials = cr;
                FtpWebResponse response = ((FtpWebResponse)Request.GetResponse());

                ImagenSocio.Image = System.Drawing.Image.FromStream(response.GetResponseStream());
            }
            catch 
            {
            }
        }

        public static string ObtenerImagen(string DireccionImagen)
        {
            string IMAGEN_RUTA = string.Empty;
            string[] VECTOR_IMG = new string[100];

            if (DireccionImagen != System.IO.Directory.GetCurrentDirectory() + @"\IMG_SOCIOS\NO_IMG.jpg"
                 && DireccionImagen != "C://" && DireccionImagen != "")
            {
                try
                {
                    string[] ImagenSocio = DireccionImagen.Split(new string[] { @"\" }, StringSplitOptions.None);
                    int contador = ImagenSocio.Length;
                    IMAGEN_RUTA = ImagenSocio[contador - 1];
                }
                catch { IMAGEN_RUTA = "NO_IMG.jpg";  }
            }
            else
            {
                IMAGEN_RUTA = "NO_IMG.jpg";
            }

            return IMAGEN_RUTA;
        }

      
    }
}

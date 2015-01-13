using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;

namespace GestorGimnasio.CLASES
{
    static class Ftp
    {

        public static String rutaArchivo = "";
        public static String ftpServerIP = "wildsoft.webuda.com";
        public static String ftpUserID = "a6389965";
        public static String ftpPassword = "linux2011";
        public static String Carpeta = @"/zfgym_img//";
        public static List<string> lectura = new List<string>();

        public static string LocalDirectory = "";
        public static string RemoteFile = "";
        public static string carpeta_remota = @"/zfgym_img//";

        private static string user = ftpUserID;
        private static string pass = ftpPassword;
        private static string dir = @ftpServerIP + Carpeta;

        static public void upload()
        {
            if (rutaArchivo == "") return;

            FileInfo fileInf = new FileInfo(rutaArchivo);
            string uri = "ftp://" + ftpServerIP + "/" + fileInf.Name;
            FtpWebRequest reqFTP;

            try
            {
                reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri("ftp://" + ftpServerIP + Carpeta + "/" + fileInf.Name));
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
                catch
                {

                }
            }
            catch { }
        }

        public static void DownloadFTP()
        {
            // Creo el objeto ftp

            FtpWebRequest ftp = (FtpWebRequest)FtpWebRequest.Create(new Uri("ftp://wildsoft.webuda.com" + carpeta_remota + RemoteFile));

            // Fijo las credenciales, usuario y contraseña
            ftp.Credentials = new NetworkCredential(ftpUserID, ftpPassword);

            // Le digo que no mantenga la conexión activa al terminar.
            ftp.KeepAlive = false;

            // Indicamos que la operación es descargar un archivo...
            ftp.Method = WebRequestMethods.Ftp.DownloadFile;

            // … en modo binario … (podria ser como ASCII)
            ftp.UseBinary = true;

            // Desactivo cualquier posible proxy http.
            // Ojo pues de saltar este paso podría usar 
            // un proxy configurado en iexplorer
            ftp.Proxy = null;

            // Activar si se dispone de SSL
            ftp.EnableSsl = false;

            // Configuro el buffer a 2 KBytes
            int buffLength = 2048;
            byte[] buff = new byte[buffLength];
            int contentLen;

            LocalDirectory = Path.Combine(LocalDirectory, Path.GetFileName(RemoteFile));
            using (FileStream fs = new FileStream(LocalDirectory, FileMode.Create,
                                                  FileAccess.Write, FileShare.None))
            using (Stream strm = ftp.GetResponse().GetResponseStream())
            {
                // Leer del buffer 2kb cada vez
                contentLen = strm.Read(buff, 0, buffLength);

                // mientras haya datos en el buffer...
                while (contentLen != 0)
                {
                    // escribir en el stream del fichero
                    //el contenido del stream de conexión
                    fs.Write(buff, 0, contentLen);
                    contentLen = strm.Read(buff, 0, buffLength);
                }
            }


        }

        public static List<string> listarFTP()
        {

            lectura.Clear();
            FtpWebRequest dirFtp = ((FtpWebRequest)FtpWebRequest.Create(new Uri("ftp://wildsoft.webuda.com" + carpeta_remota + "NO_IMG.jpg")));

            // Los datos del usuario (credenciales)
            NetworkCredential cr = new NetworkCredential(user, pass);
            dirFtp.Credentials = cr;

            // El comando a ejecutar
            dirFtp.Method = WebRequestMethods.Ftp.ListDirectory;

            // Obtener el resultado del comando
            StreamReader reader =
                new StreamReader(dirFtp.GetResponse().GetResponseStream());

            try
            {
                while (reader.Peek() >= 0)
                {
                    lectura.Add(reader.ReadLine());
                }
            }
            catch { }

            // Cerrar el stream abierto.
            reader.Close();

            return lectura;

        }

    }

}

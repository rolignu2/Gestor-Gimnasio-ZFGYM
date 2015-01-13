using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Security.Cryptography;

using System.Xml;
using System.Configuration;

namespace ZONE_FITNESS_3._0_FINAL.CLASES
{
    class ClaveProd
    {

       protected const int MAXWORD = 8;

       protected static string encriptar(string cadena, string clave)
        {
            // Aqui va el codigo de la funcion encriptar
            // Convierto la cadena y la clave en arreglos de bytes
            // para poder usarlas en las funciones de encriptacion

            try
            {
                byte[] cadenaBytes = Encoding.UTF8.GetBytes(cadena);
                byte[] claveBytes = Encoding.UTF8.GetBytes(clave);
                // Creo un objeto de la clase Rijndael
                RijndaelManaged rij = new RijndaelManaged();
                // Configuro para que utilice el modo ECB
                rij.Mode = CipherMode.ECB;
                // Configuro para que use encriptacion de 256 bits.
                rij.BlockSize = 256;
                // Declaro que si necesitara mas bytes agregue ceros.
                rij.Padding = PaddingMode.Zeros;
                // Declaro un encriptador que use mi clave secreta y un vector
                // de inicializacion aleatorio
                ICryptoTransform encriptador;
                encriptador = rij.CreateEncryptor(claveBytes, rij.IV);
                // Declaro un stream de memoria para que guarde los datos
                // encriptados a medida que se van calculando
                MemoryStream memStream = new MemoryStream();
                // Declaro un stream de cifrado para que pueda escribir aqui
                // la cadena a encriptar. Esta clase utiliza el encriptador
                // y el stream de memoria para realizar la encriptacion
                // y para almacenarla
                CryptoStream cifradoStream;
                cifradoStream = new CryptoStream(memStream, encriptador,
                CryptoStreamMode.Write);
                // Escribo los bytes a encriptar. A medida que se va escribiendo
                // se va encriptando la cadena
                cifradoStream.Write(cadenaBytes, 0, cadenaBytes.Length);
                // Aviso que la encriptación se terminó
                cifradoStream.FlushFinalBlock();
                // Convert our encrypted data from a memory stream into a byte
                byte[] cipherTextBytes = memStream.ToArray();
                // Cierro los dos streams creados
                memStream.Close();
                cifradoStream.Close();
                // Convierto el resultado en base 64 para que sea legible
                // y devuelvo el resultado
                return Convert.ToBase64String(cipherTextBytes);
            }
            catch
            {
                Console.WriteLine("longitud de la cadena es demaciado extensa par auna encriptacion de 64 bits");
                Console.ReadLine();
            }

            return "ERROR";
        }

       protected static  string desencriptar(string cadena, string clave)
        {
            // Aqui va el codigo de la funcion desencriptar
            // Convierto la cadena y la clave en arreglos de bytes
            // para poder usarlas en las funciones de encriptacion
            // En este caso la cadena la convierta usando base 64
            // que es la codificacion usada en el metodo encriptar
            byte[] cadenaBytes = Convert.FromBase64String(cadena);
            byte[] claveBytes = Encoding.UTF8.GetBytes(clave);
            // Creo un objeto de la clase Rijndael
            RijndaelManaged rij = new RijndaelManaged();
            // Configuro para que utilice el modo ECB
            rij.Mode = CipherMode.ECB;
            // Configuro para que use encriptacion de 256 bits.
            rij.BlockSize = 256;
            // Declaro que si necesitara mas bytes agregue ceros.
            rij.Padding = PaddingMode.Zeros;
            // Declaro un desencriptador que use mi clave secreta y un
            // de inicializacion aleatorio
            ICryptoTransform desencriptador;
            desencriptador = rij.CreateDecryptor(claveBytes, rij.IV);
            // Declaro un stream de memoria para que guarde los datos
            // encriptados
            MemoryStream memStream = new MemoryStream(cadenaBytes);
            // Declaro un stream de cifrado para que pueda leer de aqui
            // la cadena a desencriptar. Esta clase utiliza el
            // y el stream de memoria para realizar la desencriptacion
            CryptoStream cifradoStream;
            cifradoStream = new CryptoStream(memStream, desencriptador,
            CryptoStreamMode.Read);
            // Declaro un lector para que lea desde el stream de cifrado.
            // A medida que vaya leyendo se ira desencriptando.
            StreamReader lectorStream = new StreamReader(cifradoStream);
            // Leo todos los bytes y lo almaceno en una cadena
            string resultado = lectorStream.ReadToEnd();
            // Cierro los dos streams creados
            memStream.Close();
            cifradoStream.Close();
            // Devuelvo la cadena
            return resultado;
        }

       protected static string GET_XML_DATA()
       {

           string KEY_ = "";
           List<string> data = new List<string>();

           try
           {

               XmlDocument xmldoc = new XmlDocument();

               xmldoc.Load(System.IO.Directory.GetCurrentDirectory() + @"\CLV_P.xml");

               XmlNodeList clv = xmldoc.GetElementsByTagName("CLV_PROD_");
               XmlNodeList lista = ((XmlElement)clv[0]).GetElementsByTagName("KEY_");

               for (int i = 0; i < clv.Count; i++)
               {

                   data.Add(clv[i].InnerText.ToString());
               }

               KEY_ = string.Join("", data);

           }
           catch (XmlException ex)
           {
               MessageBox.Show(ex.Message);
           }

           return KEY_;
          
       }

       protected static void SEND_XML_DATA(string ENCRIPTACION)
       {
           try
           {

               string xmlpath = System.IO.Directory.GetCurrentDirectory() + @"\CLV_P.xml";

               List<string> data = new List<string>();

               using (XmlTextWriter writer = new XmlTextWriter(xmlpath, Encoding.UTF8))
               {

                   writer.Formatting = Formatting.Indented;

                   writer.WriteStartDocument();
                   writer.WriteStartElement("CLV_PROD_");

                   writer.WriteStartElement("KEY_");
                   writer.WriteString(ENCRIPTACION);

                   writer.WriteEndElement();
                   writer.WriteEndElement();


                   writer.Close();

               }


           }
           catch (XmlException ex)
           {
               MessageBox.Show(ex.Message);
           }
       }

       public static void SET_ENCRIPT_PRODUCTO(string DATAKEY)
       {

           string CLAVE_ = "RSOF20111890WIN323590ADL3006";
           string TEMPKEY = DATAKEY;
           int i = 0;
           int nodekey = 0;
           int contador = 0;

           for( i = 0 ; i < 18 ; i++)
           {
               switch (TEMPKEY[i])
               {

                   case 'R':
                       contador++;
                       break;
                   case 'N':
                       contador++;
                       break;
                   case 'D':
                       contador++;
                       break;
                   case 'H':
                       contador++;
                       break;
                   case 'T':
                       contador++;
                       break;
                   case 'P':
                       contador++;
                       break;
                   case 'O':
                       contador++;
                       break;
                   case 'L':
                       contador++;
                       break;
                   case '1':
                       contador++;
                       break;
                   case '2':
                       nodekey++;
                       break;
                   case '3':
                       nodekey++;
                       break;
                   case '4':
                       nodekey++;
                       break;
                   case '5':
                       nodekey++;
                       break;
                   case '6':
                       nodekey++;
                       break;
                   case '7':
                       nodekey++;
                       break;
                   case '8':
                       nodekey++;
                       break;
                   case '9':
                       nodekey++;
                       break;
                   case 'A':
                       nodekey++;
                       break;
                   case 'B':
                       nodekey++;
                       break;
                   case 'C':
                       nodekey++;
                       break;
                   case 'Z':
                       nodekey++;
                       break;
                   case 'E':
                       nodekey++;
                       break;
                   case 'F':
                       nodekey++;
                       break;
                  

               }
           }

           if ((contador == MAXWORD && nodekey == 10) || (contador == 9 && nodekey ==9))
           {
               SEND_XML_DATA(encriptar(DATAKEY, CLAVE_));
           }
            
       }

       public static string GET_PRODUCTO()
       {

           string CLAVE_ = "RSOF20111890WIN323590ADL3006";
           string CADENA = "";

           CADENA = GET_XML_DATA();

           if (CADENA == "" || CADENA == null)
           {
               return "NO REGISTRADO";
           }
           else
           {
              return desencriptar(CADENA, CLAVE_);
           }
       }

       public static bool VALUE_PRODUCTO(DataGridView Grilla)
       {

           int cantidad = Grilla.RowCount - 1;

           try
           {
               if (GET_PRODUCTO() == "NO REGISTRADO")
               {
                   if (cantidad >= 16)
                   {
                       MessageBox.Show("MAXIMO DE SOCIOS QUE PERMITE ESTA COPIA SIN REGISTRAR " + cantidad + " SOCIOS", "REGISTRA TU COPIA");
                       DialogResult REGISTRO = MessageBox.Show("¿DESEA REGISTRAR SU COPIA?", "REGISTRAR", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                       if (DialogResult.Yes == REGISTRO)
                       {
                           REGISTRAR_ REG = new REGISTRAR_();
                           REG.Show();
                       }
                       else
                       {
                           return false;
                       }
                   }
                   else return true;
               }

               else
               {
                   return true;
               }

            
           }
           catch { }

           return false;

       }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Windows.Forms;

using System.Web;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.IO;

using ADODB;
using System.Drawing;


namespace ZONE_FITNESS_3._0_FINAL.CLASES
{

    //envia el mensaje a los socios expirados
   class EMAIL
   {

       private static Control control;
       private static PictureBox picture = new PictureBox();
       private static System.Threading.Tasks.Task Tarea;

       private static string CORREO_SERVIDOR, FROM_CORREO, PASSWORD_SERVIDOR, CUERPO_CORREO  , HOSTING;
     
       private static void ObtenerCliente()
      {

          conectar_.RS_CORREO_ = new Recordset();

          conectar_.RS_CORREO_.Open("SELECT * FROM CORREO", conectar_.CN, CursorTypeEnum.adOpenDynamic, LockTypeEnum.adLockOptimistic);

          conectar_.RS_CORREO_.MoveLast();
          CORREO_SERVIDOR = conectar_.RS_CORREO_.Fields[1].Value;
          FROM_CORREO = conectar_.RS_CORREO_.Fields[1].Value;
          PASSWORD_SERVIDOR = conectar_.RS_CORREO_.Fields[2].Value;
          CUERPO_CORREO = conectar_.RS_CORREO_.Fields[3].Value;
          HOSTING = conectar_.RS_CORREO_.Fields[4].Value;

          conectar_.RS_CORREO_.Close();
      }

       public static void EnviarCorreoSmtp ( CheckedListBox listado_ )
       {

           Tarea = System.Threading.Tasks.Task.Factory.StartNew(delegate()
           {
               int i;

               string[] correo = new string[100];
               string[] correo_socio = new string[1000];
               string[] host_port = new string[] { "", "" };
               string[] servidor_name = new string[] { "", "" };
               List<string> Correo = new List<string>();


               try
               {
                   ObtenerCliente();
               }
               catch { }

               if (listado_.CheckedItems.Count == 0)
               {
                   MessageBox.Show("NO SE HA SELECCIONADO NINGUN CORREO ELECTRONICO A ENVIAR ", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                   return;
               }

               DialogResult diag = MessageBox.Show("¿DESEA ENVIAR CORREO(S) EN ESTE MOMENTO?", "ENVIAR", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
               if (diag != DialogResult.Yes) { return; }
               try
               {

                   MailMessage msg = new MailMessage();
                   SmtpClient clienteSmtp = new SmtpClient();

                   if (HOSTING.Contains('/'))
                   {
                       host_port = HOSTING.Split('/');
                       clienteSmtp.Host = host_port[0];
                   }
                   else
                   {
                       clienteSmtp.Host = HOSTING;
                   }


                   switch (HOSTING)
                   {
                       case "smtp.live.com":
                           clienteSmtp.Port = 587;
                           break;
                       case "smtp.gmail.com":
                           clienteSmtp.Port = 587;
                           break;
                       default:
                           clienteSmtp.Port = int.Parse(host_port[1].ToString());
                           break;

                   }

                   if (CORREO_SERVIDOR.Contains('/'))
                   {
                       servidor_name = CORREO_SERVIDOR.Split('/');
                       clienteSmtp.Credentials = new NetworkCredential(servidor_name[0], PASSWORD_SERVIDOR);
                       msg.From = new MailAddress(servidor_name[1]);
                   }
                   else
                   {
                       clienteSmtp.Credentials = new NetworkCredential(CORREO_SERVIDOR, PASSWORD_SERVIDOR);
                       msg.From = new MailAddress(CORREO_SERVIDOR);
                   }


                   clienteSmtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                   clienteSmtp.EnableSsl = true;
                   msg.Priority = MailPriority.Normal;
                   msg.Subject = correo_titulos.subject__;
                   msg.IsBodyHtml = true;



                   try
                   {

                       for (i = 0; i < listado_.CheckedItems.Count; i++)
                       {
                           correo_socio = Convert.ToString(listado_.CheckedItems[i]).Split('-');
                           if (GestorGimnasio.CLASES.Seguridad.IsEmail(correo_socio[1]))
                           {
                               msg.To.Add(correo_socio[1]);
                               if (msg.To.Count >= 5)
                               {

                                   msg.Body = "BUEN DIA:\n " + CUERPO_CORREO;
                                   clienteSmtp.Send(msg);
                                   msg.To.Clear();
                               }
                           }

                       }

                       if (msg.To.Count != 0 && msg.To.Count <= 4)
                       {
                           msg.Body = "BUEN DIA:\n " + CUERPO_CORREO;
                           clienteSmtp.Send(msg);
                           msg.To.Clear();
                       }

                   }
                   catch (SmtpFailedRecipientsException ex)
                   {
                       for (int ix = 0; ix < ex.InnerExceptions.Length; ix++)
                       {
                           SmtpStatusCode status = ex.InnerExceptions[ix].StatusCode;
                           if (status == SmtpStatusCode.MailboxBusy ||
                               status == SmtpStatusCode.MailboxUnavailable)
                           {
                               System.Threading.Thread.Sleep(5000);
                               clienteSmtp.Send(msg);
                           }
                           else if (status == SmtpStatusCode.TransactionFailed)
                           {
                               Console.WriteLine("Failed to deliver message to {0}",
                                   ex.InnerExceptions[ix].FailedRecipient);
                               MessageBox.Show("No se pudo realizar la transaccion completa");

                           }
                           else if (status == SmtpStatusCode.SyntaxError)
                           {

                           }
                           else if (status == SmtpStatusCode.ClientNotPermitted)
                           {

                           }
                           else if (status == SmtpStatusCode.GeneralFailure)
                           {

                           }
                           else if (status == SmtpStatusCode.ExceededStorageAllocation)
                           {


                           }

                       }
                   }
                   catch
                   {
                       MessageBox.Show("No se pudo realizar la transaccion completa");
                   }

               }
               catch (Exception ex) { MessageBox.Show(ex.Message + "ERROR HOTMAIL\n\n PUEDE QUE NO HAYA CONEXION A INTERNET"); }
           });
       }

       public static bool VerificarCorreoActivo(string Correo)
      {
          try
          {
              ObtenerCliente();
          }
          catch { }

          bool Validado = false;
          MailMessage msg = new MailMessage();
          SmtpClient clienteSmtp = new SmtpClient();

          string[] host_port = new string[] { "", "" };
          string[] servidor_name = new string[] { "", "" };

          if (HOSTING.Contains('/'))
          {
              host_port = HOSTING.Split('/');
              clienteSmtp.Host = host_port[0];
          }
          else
          {
              clienteSmtp.Host = HOSTING;
          }


          switch (HOSTING)
          {
              case "smtp.live.com":
                  clienteSmtp.Port = 587;
                  break;
              case "smtp.gmail.com":
                  clienteSmtp.Port = 587;
                  break;
              default:
                  clienteSmtp.Port = int.Parse(host_port[1].ToString());
                  break;

          }

          if (CORREO_SERVIDOR.Contains('/'))
          {
              servidor_name = CORREO_SERVIDOR.Split('/');
              clienteSmtp.Credentials = new NetworkCredential(servidor_name[0], PASSWORD_SERVIDOR);
              msg.From = new MailAddress(servidor_name[1]);
          }
          else
          {
              clienteSmtp.Credentials = new NetworkCredential(CORREO_SERVIDOR, PASSWORD_SERVIDOR);
              msg.From = new MailAddress(CORREO_SERVIDOR);
          }


          clienteSmtp.DeliveryMethod = SmtpDeliveryMethod.Network;
          clienteSmtp.EnableSsl = true;
          msg.Priority = MailPriority.Normal;
          msg.Subject = correo_titulos.subject__;   //gimnasio ... sujeto 
          msg.IsBodyHtml = true;

          msg.To.Add(Correo);
          msg.Body = "Buen Dia " + Correo +
              " <br><br><b>Por este medio estamos verificando que su cuenta de correo electronico sea valida" +
              "<br>.Zone fitness gym esta trabajando para brindarles mejor servicio. <br><br> ATTE. STAFF</b>";

          try
          {
              clienteSmtp.Send(msg);
          }
          catch (SmtpFailedRecipientsException ex)
          {
              for (int i = 0; i < ex.InnerExceptions.Length; i++)
              {
                  SmtpStatusCode status = ex.InnerExceptions[i].StatusCode;
                  if (status == SmtpStatusCode.MailboxBusy ||
                      status == SmtpStatusCode.MailboxUnavailable)
                  {
                      System.Threading.Thread.Sleep(5000);
                      clienteSmtp.Send(msg);
                      Validado = true;
                      return Validado;
                  }
                  else if (status == SmtpStatusCode.TransactionFailed)
                  {
                      Console.WriteLine("Failed to deliver message to {0}",
                          ex.InnerExceptions[i].FailedRecipient);
                      Validado = false;
                      return Validado;
                  }
                  else if (status == SmtpStatusCode.SyntaxError)
                  {
                      Validado = false;
                      return Validado;
                  }
                  else if (status == SmtpStatusCode.ClientNotPermitted)
                  {
                      Validado = true;
                      return Validado;
                  }
                  else if (status == SmtpStatusCode.GeneralFailure)
                  {
                      Validado = true;
                      return Validado;
                  }
                  else if (status == SmtpStatusCode.ExceededStorageAllocation)
                  {
                      Validado = true;
                      return Validado;
                  }

              }
          }
          catch
          {
              Validado = false;
              return Validado;
          }

          msg.To.Clear();
          Validado = true;
          return Validado;
      }

       public static void Loop(Control ctl , Point p)
       {
           try
           {
               control = ctl;
               picture.Image = Image.FromFile(System.IO.Directory.GetCurrentDirectory() + @"/url.gif");
               picture.Show();
               picture.Size = new Size(60, 60);
               picture.BackColor = Color.Transparent;
               picture.SizeMode = PictureBoxSizeMode.StretchImage;
               picture.Location = p;
               picture.BringToFront();
               ctl.Controls.Add(picture);
           }
           catch { }
           return;
       }

       public static void Loop(Control ctl, Point p , Size size)
       {
           try
           {
               control = ctl;
               picture.Image = Image.FromFile(System.IO.Directory.GetCurrentDirectory() + @"/url.gif");
               picture.Show();
               picture.Size = size;
               picture.SizeMode = PictureBoxSizeMode.StretchImage;
               picture.Location = p;
               picture.BringToFront();
               ctl.Controls.Add(picture);
           }
           catch { }
           return;
       }

       public static void EndLoop()
       {
           try
           {
               control.Controls.Remove(picture);
           }
           catch { }
           return;
       }

   }
}

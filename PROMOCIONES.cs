using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using ZONE_FITNESS_3._0_FINAL.CLASES;
using GestorGimnasio.Properties;
using Design;
using System.Threading;
using System.Web;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;

namespace GestorGimnasio
{
    public partial class PROMOCIONES : Form
    {
        public PROMOCIONES()
        {
            InitializeComponent();
        }

        private List<string> ParametrosCorreo = new List<string>();
        private static string CuepoDocumento = null;
        private Thread hilo;
        public static Dictionary<string, string> CorreosNoexistentes = new Dictionary<string, string>();

        private void PROMOCIONES_Load(object sender, EventArgs e)
        {
            this.BackColor = Color.DarkBlue;
            this.Opacity = 0.92;
            button3.Enabled = false;
            lblmensaje.Text = "";
            timecuerpo.Enabled = false;
            ConexionPromociones();
            linkLabel1.Text = "Servidor: " + ParametrosCorreo[1].ToString(); 
           
        }

        private void ConexionPromociones()
        {
            conectar_.RS_CORREO_ = new ADODB.Recordset();

            checkcorreo.Items.Clear();
            ParametrosCorreo.Clear();
            CorreosNoexistentes.Clear();


            try
            {
                if (conectar_.RS_CORREO_.State == 1)
                    conectar_.RS_CORREO_.Close();

                conectar_.RS_CORREO_.Open(
                    "SELECT * FROM envio_promociones",
                    conectar_.CN,
                    ADODB.CursorTypeEnum.adOpenDynamic,
                    ADODB.LockTypeEnum.adLockOptimistic);

                conectar_.RS_CORREO_.MoveLast();

                ParametrosCorreo.Add(conectar_.RS_CORREO_.Fields[1].Value);
            }
            catch { ParametrosCorreo.Add("No existe cuerpo generado"); }


            try
            {

                conectar_.RS_CORREO_.Close();

                conectar_.RS_CORREO_.Open(
                  "SELECT * FROM CORREO",
                  conectar_.CN,
                  ADODB.CursorTypeEnum.adOpenDynamic,
                  ADODB.LockTypeEnum.adLockOptimistic);

                conectar_.RS_CORREO_.MoveLast();

                ParametrosCorreo.Add(conectar_.RS_CORREO_.Fields[1].Value);
                ParametrosCorreo.Add(conectar_.RS_CORREO_.Fields[2].Value);
                ParametrosCorreo.Add(conectar_.RS_CORREO_.Fields[4].Value);

                conectar_.RS_CORREO_.Close();
            }
            catch
            {
            }

            conectar_.RS_CORREO_.Open(
                  "SELECT E_mail, Nombre , Apellido  FROM socios",
                  conectar_.CN,
                  ADODB.CursorTypeEnum.adOpenDynamic,
                  ADODB.LockTypeEnum.adLockOptimistic);

            conectar_.RS_CORREO_.MoveFirst();

            while (!conectar_.RS_CORREO_.EOF)
            {
                if (conectar_.RS_CORREO_.Fields[0].Value != "NO HAY CORREO"
                    && conectar_.RS_CORREO_.Fields[0].Value != "" 
                    && conectar_.RS_CORREO_.Fields[0].Value !=null)
                {
                    if (GestorGimnasio.CLASES.Seguridad.IsEmail(conectar_.RS_CORREO_.Fields[0].Value))
                    {
                        checkcorreo.Items.Add(conectar_.RS_CORREO_.Fields[0].Value);
                    }
                    else
                    {
                        button3.Enabled = true;
                        CorreosNoexistentes.Add(
                            conectar_.RS_CORREO_.Fields[1].Value
                            + "="
                            + conectar_.RS_CORREO_.Fields[2].Value,
                            conectar_.RS_CORREO_.Fields[0].Value);
                    }
                }
                conectar_.RS_CORREO_.MoveNext();
            }
            lblcantidad.Text = "Total correos a enviar: " + checkcorreo.Items.Count.ToString();
            webBrowser1.DocumentText = ParametrosCorreo[0];

            conectar_.RS_CORREO_.Close();

        }

        private void cmdeditar_Click(object sender, EventArgs e)
        {
            try
            {
                EditorForm.CuerpoMensaje = null;
                EditorForm editar = new EditorForm();
                editar.Show();
                timecuerpo.Enabled = true;
            }
            catch { }
        }

        private void timecuerpo_Tick(object sender, EventArgs e)
        {
            if (EditorForm.CuerpoMensaje == null)
            {
                lblmensaje.Text = "";
                return;
            }
            else
            {
                CuepoDocumento = EditorForm.CuerpoMensaje;
                lblmensaje.Text = "Se necesita guardar los cambios";
                return;
            }
        }

        private void checkseleccionar_CheckedChanged(object sender, EventArgs e)
        {
            if (check_seleccionar_todo.Checked == true)
            {
                for (int i = 0; i < checkcorreo.Items.Count; i++)
                {
                    checkcorreo.SetItemChecked(i, true);
                }
            }
            else
            {
                for (int i = 0; i < checkcorreo.Items.Count; i++)
                {
                    checkcorreo.SetItemChecked(i, false);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            try
            {

                if (conectar_.RS_CORREO_.State == 1)
                    conectar_.RS_CORREO_.Close();

                conectar_.RS_CORREO_.Open(
                    "SELECT * FROM envio_promociones",
                    conectar_.CN,
                    ADODB.CursorTypeEnum.adOpenDynamic,
                    ADODB.LockTypeEnum.adLockOptimistic);

                conectar_.RS_CORREO_.AddNew(); 
                {
                    conectar_.RS_CORREO_.Fields[1].Value = CuepoDocumento;
                    conectar_.RS_CORREO_.Update();
                }

                MessageBox.Show("CAMBIOS GUARDADOS CON EXITO", "EXITO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ConexionPromociones();
                lblmensaje.Text = "";
            }
            catch { }

        }

        private void PROMOCIONES_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (conectar_.RS_CORREO_.State == 1)
                conectar_.RS_CORREO_.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            EMAIL.Loop(checkcorreo, new Point(80, 10));
            hilo = new Thread(delegate()
            {
                EnviarCorreo();
            });
            timerhilo.Enabled = true;
            timerhilo.Start();
            hilo.Priority = ThreadPriority.Highest;
            hilo.Start();
        }

        private void EnviarCorreo()
        {
            DialogResult diag = MessageBox.Show("¿DESEA ENVIAR CORREO(S) EN ESTE MOMENTO?", "ENVIAR", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
            if (diag != DialogResult.Yes) { return; }
           try
           {

               MailMessage msg = new MailMessage();
               SmtpClient clienteSmtp = new SmtpClient();

               if (ParametrosCorreo[3] == "smtp.gmail.com")//comienza verificando si el smtp es gmail
                {
                    //gmail
                    clienteSmtp.Host = "smtp.gmail.com";
                    clienteSmtp.Port = 587;

                }
                else
                {
                    //hotmail
                    clienteSmtp.Host = "smtp.live.com";
                    clienteSmtp.Port = 587;
                }


               clienteSmtp.Credentials = new NetworkCredential(ParametrosCorreo[1], ParametrosCorreo[2]);
               msg.Subject = correo_titulos.subject__;
               msg.From = new MailAddress(ParametrosCorreo[1]);
               msg.IsBodyHtml = true;//html si 
               msg.Body = ParametrosCorreo[0];
               clienteSmtp.EnableSsl = true;


               try
               {
 
                   for ( int i = 0; i < checkcorreo.CheckedItems.Count; i++)
                   {
                       try
                       {
                           string correo = checkcorreo.CheckedItems[i].ToString();
                           msg.Body = "BUEN DIA:\n " + ParametrosCorreo[0];
                           msg.To.Add(new MailAddress(correo));
                           checkcorreo.SetItemChecked(i, false);
                       }
                       catch {
                           checkcorreo.SetItemChecked(i, true);
                       }
                   }

                   clienteSmtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                   clienteSmtp.Send(msg);
                   msg.To.Clear();
               }
               catch (Exception ex)
               {
                   MessageBox.Show("ERROR GMAIL\n\n AL ENVIAR CORREO DE FORMA MASIVA SE PUEDE PRODUCIR UN ERROR TIPO SPAM\n\n PARA SOLUSIONAR ESTE PROBLEMA INICIE SESION " + ParametrosCorreo[1] + "    " + ex.Message);
               }

           }
           catch (Exception ex) { MessageBox.Show(ex.Message + "ERROR HOTMAIL\n\n PUEDE QUE NO HAYA CONEXION A INTERNET"); }
        }

        private void timerhilo_Tick(object sender, EventArgs e)
        {
            try
            {
                if (!hilo.IsAlive)
                {
                    EMAIL.EndLoop();
                    timerhilo.Stop();
                    return;
                }
            }
            catch { }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            EMAILINEXISTENTES email = new EMAILINEXISTENTES();
            email.Show();
        }
    }
}

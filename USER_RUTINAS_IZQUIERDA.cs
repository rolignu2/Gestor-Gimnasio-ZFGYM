using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using ZONE_FITNESS_3._0_FINAL.CLASES;
using ADODB;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;

using System.Net.Mail;
using System.Net;
using System.Data.OleDb;
using System.Threading;
using System.Data.Odbc;

namespace ZONE_FITNESS_3._0_FINAL
{
    public partial class USER_RUTINAS_IZQUIERDA : UserControl
    {
        public static string NOMBRE_ = "";
        public static string APELLIDO_ = "";
        public static string SQL_RUTINA_BODY = "SELECT * FROM RUTINA_HTML";

        private static string SQL_RUTINA = "";
        private static string SQL_SOCIOS_BUSCAR ="";

        public MailMessage msg = new MailMessage();
        private Thread hilogrilla;

        public USER_RUTINAS_IZQUIERDA()
        {
            InitializeComponent();
        }

        private void USER_RUTINAS_IZQUIERDA_Load(object sender, EventArgs e)
        {
            TXT_APELLIDO.ReadOnly = true;
            TXT_NOMBRE.ReadOnly = true;
            txtmail.ReadOnly = true;
            conexion_html();
            hilogrilla = new Thread(delegate()
            {
                GRILLA();
            });
            hilogrilla.Start();
        }

        private void eliminardatos()
        {
            grilla_socios.DataSource = null;
        }

        private void GRILLA()
        {
            eliminardatos();
                try
                {
                    conectar_.mysqladapter = new OleDbDataAdapter("SELECT * FROM SOCIOS", conectar_.mysqlconection);
                    conectar_.ds_oledb = new DataTable();//tabla es = nueva tabla
                    conectar_.mysqladapter.Fill(conectar_.ds_oledb);
                    grilla_socios.DataSource = conectar_.ds_oledb;
                    grilla_socios.Columns[0].Visible = false;
                    for (int i = 0; i <= grilla_socios.ColumnCount; i++)
                    {
                        if (!(i == 15 || i == 1 || i == 2))
                        {
                            grilla_socios.Columns[i].Visible = false;
                        }
                    }
                }
                catch { }
        }

        private void grilla_socios_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int i = this.grilla_socios.CurrentRow.Index;

                TXT_NOMBRE.Text = Convert.ToString(grilla_socios[1, i].Value);
                TXT_APELLIDO.Text = Convert.ToString(grilla_socios[2, i].Value);
                txtmail.Text = Convert.ToString(grilla_socios[15, i].Value);

                NOMBRE_ = TXT_NOMBRE.Text;
                APELLIDO_ = TXT_APELLIDO.Text;
            }
            catch { }
        }

        private void lbl_rutina_pdf_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(System.IO.Directory.GetCurrentDirectory() + @"\PDF\" + NOMBRE_ + "_" + APELLIDO_ + ".pdf");
            }
            catch { }            
            }

        private void cmd_vista_prev_Click(object sender, EventArgs e)
        {
            try
            {
                int contador = 0;
                int estado1 = 0;
                int estado2 = 0;
                int estado3 = 0;
                int estado4 = 0;
                int estado5 = 0;
                int estado6 = 0;
                int estado7 = 0;

                string path = System.IO.Directory.GetCurrentDirectory() + @"\PDF\";
                SQL_RUTINA = "SELECT * FROM SOCIO_RUTINA WHERE Nombre='" + NOMBRE_ + "'AND Apellido='" + APELLIDO_ + "'";
                conectar_.SOCIO_RUTINA = new Recordset();
                conectar_.SOCIO_RUTINA.Open(SQL_RUTINA, conectar_.CN, CursorTypeEnum.adOpenDynamic, LockTypeEnum.adLockOptimistic);
                //
                Document document = new Document();
                PdfWriter.GetInstance(document, new FileStream(path + NOMBRE_ + "_" + APELLIDO_ + ".pdf", FileMode.OpenOrCreate));
                document.Open();
                //
                Chunk DATOS_PERSONALES_0 = new Chunk("NOMBRE: " + TXT_NOMBRE.Text + " " + TXT_APELLIDO.Text + "                                                              FECHA: " +  DateTime.Now.Day.ToString() + "/" + DateTime.Now.Month.ToString() + "/" + DateTime.Now.Year.ToString() + "\n\n", FontFactory.GetFont("ARIAL", 12, iTextSharp.text.Font.ITALIC));
                Paragraph DATOS_PERSONALES_1 = new Paragraph(DATOS_PERSONALES_0);
                DATOS_PERSONALES_1.Alignment = Element.ALIGN_LEFT;
                document.Add(DATOS_PERSONALES_1);

                while (!(conectar_.SOCIO_RUTINA.EOF))
                {
                    contador++;

                    if (conectar_.SOCIO_RUTINA.Fields[3].Value == "LUNES")
                    {
                        
                        if (estado1 == 0)
                        {
                            Chunk titulo = new Chunk("DIA LUNES\n\n", FontFactory.GetFont("ARIAL", 14, iTextSharp.text.Font.UNDERLINE));
                            Paragraph tituloX = new Paragraph(titulo);
                            tituloX.Alignment = Element.ALIGN_CENTER;
                            document.Add(tituloX);
                        }
                        estado1 = 1;
                        Paragraph paragraph = new Paragraph("N° " + contador + "\n" + conectar_.SOCIO_RUTINA.Fields[4].Value + "\nSETS:" + conectar_.SOCIO_RUTINA.Fields[5].Value + "\nREPETICIONES:" + conectar_.SOCIO_RUTINA.Fields[6].Value + "\n\n\n\n\n");
                        paragraph.Alignment = Element.IMGRAW;
                        iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance(System.IO.Directory.GetCurrentDirectory() + @"\IMG_RUTINAS\" + conectar_.SOCIO_RUTINA.Fields[7].Value);
                        //jpg.ScalePercent(40f);
                        //jpg.SetAbsolutePosition(document.PageSize.Width - 36f - 72f,document.PageSize.Height - 36f - 216.6f);
                        jpg.ScaleToFit(90f, 105f);
                        jpg.Alignment = iTextSharp.text.Image.TEXTWRAP;
                        jpg.IndentationLeft = 5f;
                        jpg.SpacingAfter = 5f;
                        jpg.BorderWidthTop = 0f;
                        document.Add(jpg);
                        document.Add(paragraph);
                       
                    }
                       conectar_.SOCIO_RUTINA.MoveNext();
                }
                conectar_.SOCIO_RUTINA.MoveFirst();
                while (!(conectar_.SOCIO_RUTINA.EOF))
                {
                     if (conectar_.SOCIO_RUTINA.Fields[3].Value == "MARTES")
                    {
                      
                        if (estado2 == 0)
                        {
                            Chunk titulo = new Chunk("DIA MARTES\n\n", FontFactory.GetFont("ARIAL", 14, iTextSharp.text.Font.UNDERLINE));
                            Paragraph tituloX = new Paragraph(titulo);
                            tituloX.Alignment = Element.ALIGN_CENTER;
                            document.Add(tituloX);
                        }
                        estado2 = 1;
                        Paragraph paragraph = new Paragraph("N° " + contador + "\n" + conectar_.SOCIO_RUTINA.Fields[4].Value + "\nSETS:" + conectar_.SOCIO_RUTINA.Fields[5].Value + "\nREPETICIONES:" + conectar_.SOCIO_RUTINA.Fields[6].Value + "\n\n\n\n\n");
                        paragraph.Alignment = Element.IMGRAW;
                        iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance(System.IO.Directory.GetCurrentDirectory() + @"\IMG_RUTINAS\" + conectar_.SOCIO_RUTINA.Fields[7].Value);
                        //jpg.ScalePercent(45f);
                        jpg.ScaleToFit(90f, 105f);
                        jpg.Alignment = iTextSharp.text.Image.TEXTWRAP;
                        jpg.IndentationLeft = 5f;
                        jpg.SpacingAfter = 5f;
                        jpg.BorderWidthTop = 0f;
                        document.Add(jpg);
                        document.Add(paragraph);

                    }
                       conectar_.SOCIO_RUTINA.MoveNext();
                }
                 conectar_.SOCIO_RUTINA.MoveFirst();
                while (!(conectar_.SOCIO_RUTINA.EOF))
                {
            
                    if (conectar_.SOCIO_RUTINA.Fields[3].Value == "MIERCOLES")
                    {

                        if (estado3 == 0)
                        {
                            Chunk titulo = new Chunk("DIA MIERCOLES\n\n", FontFactory.GetFont("ARIAL", 14, iTextSharp.text.Font.UNDERLINE));
                            Paragraph tituloX = new Paragraph(titulo);
                            tituloX.Alignment = Element.ALIGN_CENTER;
                            document.Add(tituloX);
                        }
                        estado3 = 1;
                        Paragraph paragraph = new Paragraph("N° " + contador + "\n" + conectar_.SOCIO_RUTINA.Fields[4].Value + "\nSETS:" + conectar_.SOCIO_RUTINA.Fields[5].Value + "\nREPETICIONES:" + conectar_.SOCIO_RUTINA.Fields[6].Value + "\n\n\n\n\n");
                        paragraph.Alignment = Element.IMGRAW;
                        iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance(System.IO.Directory.GetCurrentDirectory() + @"\IMG_RUTINAS\" + conectar_.SOCIO_RUTINA.Fields[7].Value);
                        //jpg.ScalePercent(45f);
                        jpg.ScaleToFit(90f, 105f);
                        jpg.Alignment = iTextSharp.text.Image.TEXTWRAP;
                        jpg.IndentationLeft = 5f;
                        jpg.SpacingAfter = 5f;
                        jpg.BorderWidthTop = 0f;
                        document.Add(jpg);
                        document.Add(paragraph);
                    }
                       conectar_.SOCIO_RUTINA.MoveNext();
                }
                 conectar_.SOCIO_RUTINA.MoveFirst();
                while (!(conectar_.SOCIO_RUTINA.EOF))
                {
                 if (conectar_.SOCIO_RUTINA.Fields[3].Value == "JUEVES")
                    {

                        if (estado4 == 0)
                        {
                            Chunk titulo = new Chunk("DIA JUEVES\n\n", FontFactory.GetFont("ARIAL", 14, iTextSharp.text.Font.UNDERLINE));
                            Paragraph tituloX = new Paragraph(titulo);
                            tituloX.Alignment = Element.ALIGN_CENTER;
                            document.Add(tituloX);
                        }
                        estado4 = 1;
                        Paragraph paragraph = new Paragraph("N° " + contador + "\n" + conectar_.SOCIO_RUTINA.Fields[4].Value + "\nSETS:" + conectar_.SOCIO_RUTINA.Fields[5].Value + "\nREPETICIONES:" + conectar_.SOCIO_RUTINA.Fields[6].Value + "\n\n\n\n\n");
                        paragraph.Alignment = Element.IMGRAW;
                        iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance(System.IO.Directory.GetCurrentDirectory() + @"\IMG_RUTINAS\" + conectar_.SOCIO_RUTINA.Fields[7].Value);
                       // jpg.ScalePercent(45f);
                        jpg.ScaleToFit(90f, 105f);
                        jpg.Alignment = iTextSharp.text.Image.TEXTWRAP;
                        jpg.IndentationLeft = 5f;
                        jpg.SpacingAfter = 5f;
                        jpg.BorderWidthTop = 0f;
                        document.Add(jpg);
                        document.Add(paragraph);
                    }
                       conectar_.SOCIO_RUTINA.MoveNext();
                }
                 conectar_.SOCIO_RUTINA.MoveFirst();
                 while (!(conectar_.SOCIO_RUTINA.EOF))
                {
                     if (conectar_.SOCIO_RUTINA.Fields[3].Value == "VIERNES")
                    {

                        if (estado5 == 0)
                        {
                            Chunk titulo = new Chunk("DIA VIERNES\n\n", FontFactory.GetFont("ARIAL", 14, iTextSharp.text.Font.UNDERLINE));
                            Paragraph tituloX = new Paragraph(titulo);
                            tituloX.Alignment = Element.ALIGN_CENTER;
                            document.Add(tituloX);
                        }
                        estado5 = 1;
                        Paragraph paragraph = new Paragraph("N° " + contador + "\n" + conectar_.SOCIO_RUTINA.Fields[4].Value + "\nSETS:" + conectar_.SOCIO_RUTINA.Fields[5].Value + "\nREPETICIONES:" + conectar_.SOCIO_RUTINA.Fields[6].Value + "\n\n\n\n\n");
                        paragraph.Alignment = Element.IMGRAW;
                        iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance(System.IO.Directory.GetCurrentDirectory() + @"\IMG_RUTINAS\" + conectar_.SOCIO_RUTINA.Fields[7].Value);
                       // jpg.ScalePercent(45f);
                        jpg.ScaleToFit(90f, 105f);
                        jpg.Alignment = iTextSharp.text.Image.TEXTWRAP;
                        jpg.IndentationLeft = 5f;
                        jpg.SpacingAfter = 5f;
                        jpg.BorderWidthTop = 0f;
                        document.Add(jpg);
                        document.Add(paragraph);
                    }
                        conectar_.SOCIO_RUTINA.MoveNext();
                 }
                 conectar_.SOCIO_RUTINA.MoveFirst();
                 while (!(conectar_.SOCIO_RUTINA.EOF))
                 {
                     if (conectar_.SOCIO_RUTINA.Fields[3].Value == "SABADO")
                    {

                        if (estado6 == 0)
                        {
                            Chunk titulo = new Chunk("DIA SABADO\n\n", FontFactory.GetFont("ARIAL", 14, iTextSharp.text.Font.UNDERLINE));
                            Paragraph tituloX = new Paragraph(titulo);
                            tituloX.Alignment = Element.ALIGN_CENTER;
                            document.Add(tituloX);
                        }
                        estado6 = 1;
                        Paragraph paragraph = new Paragraph("N° " + contador + "\n" + conectar_.SOCIO_RUTINA.Fields[4].Value + "\nSETS:" + conectar_.SOCIO_RUTINA.Fields[5].Value + "\nREPETICIONES:" + conectar_.SOCIO_RUTINA.Fields[6].Value + "\n\n\n\n\n");
                        paragraph.Alignment = Element.IMGRAW;
                        iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance(System.IO.Directory.GetCurrentDirectory() + @"\IMG_RUTINAS\" + conectar_.SOCIO_RUTINA.Fields[7].Value);
                       // jpg.ScalePercent(45f);
                        jpg.ScaleToFit(90f, 105f);
                        jpg.Alignment = iTextSharp.text.Image.TEXTWRAP;
                        jpg.IndentationLeft = 5f;
                        jpg.SpacingAfter = 5f;
                        jpg.BorderWidthTop = 0f;
                        document.Add(jpg);
                        document.Add(paragraph);
                    }
                        conectar_.SOCIO_RUTINA.MoveNext();
                 }
                 conectar_.SOCIO_RUTINA.MoveFirst();
                 while (!(conectar_.SOCIO_RUTINA.EOF))
                 {
                    if (conectar_.SOCIO_RUTINA.Fields[3].Value == "DOMINGO")
                    {

                        if (estado7 == 0)
                        {
                            Chunk titulo = new Chunk("DIA DOMINGO\n\n", FontFactory.GetFont("ARIAL", 14, iTextSharp.text.Font.UNDERLINE));
                            Paragraph tituloX = new Paragraph(titulo);
                            tituloX.Alignment = Element.ALIGN_CENTER;
                            document.Add(tituloX);
                        }
                        estado7 = 1;
                        Paragraph paragraph = new Paragraph("N° " + contador + "\n" + conectar_.SOCIO_RUTINA.Fields[4].Value + "\nSETS:" + conectar_.SOCIO_RUTINA.Fields[5].Value + "\nREPETICIONES:" + conectar_.SOCIO_RUTINA.Fields[6].Value + "\n\n\n\n\n");
                        paragraph.Alignment = Element.IMGRAW;
                        iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance(System.IO.Directory.GetCurrentDirectory() + @"\IMG_RUTINAS\" + conectar_.SOCIO_RUTINA.Fields[7].Value);
                        jpg.ScaleToFit(90f, 105f);
                        jpg.Alignment = iTextSharp.text.Image.TEXTWRAP;
                        jpg.IndentationLeft = 5f;
                        jpg.SpacingAfter = 5f;
                        jpg.BorderWidthTop = 0f;
                        document.Add(jpg);
                        document.Add(paragraph);
                    }
                        conectar_.SOCIO_RUTINA.MoveNext();
                 }

                      document.Close();
                      lbl_rutina_pdf.Text = "VISTA PREVIA " + NOMBRE_ + "_" + APELLIDO_;
                }
                 
            catch { }
            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (TXT_APELLIDO.Text == "" || TXT_NOMBRE.Text == ""){cmd_vista_prev.Enabled = false;}
            else{cmd_vista_prev.Enabled = true;}
        }

        private void linkcorreo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                lblespere.Visible = true;
                MessageBox.Show("SE LE ENVIARA LA RUTINA AL CORREO " + txtmail.Text, "ENVIAR MAIL", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //System.IO.Directory.GetCurrentDirectory() + @"\PDF\" + NOMBRE_ + "_" + APELLIDO_ + ".pdf"    
        
                SmtpClient clienteSmtp = new SmtpClient();

                if (conectar_.RS_CORREO_.Fields[4].Value == "smtp.gmail.com")
                {

                    clienteSmtp.Host = "smtp.gmail.com";
                    clienteSmtp.Port = 587;

                }
                else
                {
                    clienteSmtp.Host = "smtp.live.com";
                    clienteSmtp.Port = 587;
                }

                clienteSmtp.Credentials = new NetworkCredential(conectar_.RS_CORREO_.Fields[1].Value, conectar_.RS_CORREO_.Fields[2].Value);
                msg.From = new MailAddress(conectar_.RS_CORREO_.Fields[1].Value);
                msg.To.Add(new MailAddress(txtmail.Text));               
                msg.IsBodyHtml = true;
                if (conectar_.RUTINA_HTML.Fields[1].Value == null || conectar_.RUTINA_HTML.Fields[1].Value == "")
                {
                    msg.Subject = "ZONE FITNESS GYM RUTINA DE ENTRENAMIENTO";
                    msg.Body = "SALUDOS:\n\nTU GIMNASIO TE ENVIA TU RUTINA DE EJERCICIO; SOLO DEBES DE DESCARGAR EL ARCHIVO PDF ";
                }
                else
                {
                    msg.Subject = conectar_.RUTINA_HTML.Fields[1].Value;
                    msg.Body = conectar_.RUTINA_HTML.Fields[2].Value;
                }
                msg.Attachments.Add(new Attachment(System.IO.Directory.GetCurrentDirectory() + @"\PDF\" + NOMBRE_ + "_" + APELLIDO_ + ".pdf"));
                clienteSmtp.EnableSsl = true;
                clienteSmtp.Send(msg);
                lblespere.Visible = false;
                       
            }
            catch(Exception ex) {
                MessageBox.Show("ERROR AL ENVIAR EL CORREO, VERIFIQUE SI EXISTE UN DESTINATARIO.\n\n EN DADO CASO EXISTA UN DESTINO VERIFIQUE CON SU TECNICO SI EL CORREO SE DETECTA COMO SPAM (VIRUS).\n\n" + ex.Message , "ERR", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                lblespere.Visible = false;
                return;
            }
        }

        private void conexion_html()
        {
            try
            {
                conectar_.RUTINA_HTML = new Recordset();
                conectar_.RUTINA_HTML.Open(SQL_RUTINA_BODY, conectar_.CN, CursorTypeEnum.adOpenDynamic, LockTypeEnum.adLockBatchOptimistic);
                conectar_.RUTINA_HTML.MoveLast();
            }
            catch { }
        }

        private void BUSCAR()
        {

                grilla_socios.DataSource = null;

                if (conectar_.mysqlconection.State == 0) { conectar_.mysqlconection.Open(); }
                else
                {
                    conectar_.mysqlconection.Close();
                    conectar_.mysqlconection.Open();
                }
                SQL_SOCIOS_BUSCAR = "SELECT * FROM SOCIOS WHERE Nombre ='" + txtbuscar.Text  + "'";
                conectar_.mysqladapter = new OleDbDataAdapter(SQL_SOCIOS_BUSCAR, conectar_.mysqlconection);//conecta el adapter con la sentencia sql
                conectar_.ds_oledb = new DataTable();//tabla es = nueva tabla
                conectar_.mysqladapter.Fill(conectar_.ds_oledb);//el adaptador con toda la base de sql se le llenara en la tabla

                grilla_socios.DataSource = conectar_.ds_oledb;//el DataTable table se pasara a la grilla
                grilla_socios.Columns[0].Visible = false;

                if (grilla_socios.RowCount == 1)
                {
                    grilla_socios.DataSource = null;

                    if (conectar_.mysqlconection.State == 0) { conectar_.mysqlconection.Open(); }
                    else
                    {
                        conectar_.mysqlconection.Close();
                        conectar_.mysqlconection.Open();
                    }
                    SQL_SOCIOS_BUSCAR = "SELECT * FROM SOCIOS WHERE Apellido ='" + txtbuscar.Text + "'";
                    conectar_.mysqladapter = new OleDbDataAdapter(SQL_SOCIOS_BUSCAR, conectar_.mysqlconection);//conecta el adapter con la sentencia sql
                    conectar_.ds_oledb = new DataTable();//tabla es = nueva tabla
                    conectar_.mysqladapter.Fill(conectar_.ds_oledb);//el adaptador con toda la base de sql se le llenara en la tabla

                    grilla_socios.DataSource = conectar_.ds_oledb;//el DataTable table se pasara a la grilla
                    grilla_socios.Columns[0].Visible = false;

                    grilla_socios.DataSource = null;

                                if (conectar_.mysqlconection.State == 0) { conectar_.mysqlconection.Open(); }
                                else
                                {
                                    conectar_.mysqlconection.Close();
                                    conectar_.mysqlconection.Open();
                                }
                                conectar_.mysqladapter = new OleDbDataAdapter("SELECT * FROM SOCIOS", conectar_.mysqlconection);//conecta el adapter con la sentencia sql
                                conectar_.ds_oledb = new DataTable();//tabla es = nueva tabla
                                conectar_.mysqladapter.Fill(conectar_.ds_oledb);//el adaptador con toda la base de sql se le llenara en la tabla

                                grilla_socios.DataSource = conectar_.ds_oledb;//el DataTable table se pasara a la grilla
                                grilla_socios.Columns[0].Visible = false;

                            } 

                        }

        private void cmdbuscar_Click(object sender, EventArgs e)
        {
            BUSCAR();
        }

                    }

        }


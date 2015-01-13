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
using System.Threading;

namespace ZONE_FITNESS_3._0_FINAL
{
    public partial class USER_RUTINA_CENTRO : UserControl
    {

        /*puede que en este user control sea el mas complicado ya que es donde se efectual la mayor parte 
         del codigo de programacion de las rutinas , se explicara un poco detallado */


        conectar_ CONEXION = new conectar_();

        public static string SQL_RUTINA = "";
        //
        public static int contador = 0;
        public static int detalle = 0;
        public static string nombre_detalle = "";
        public static string nombre_add = "";
        public static int agregar = 0;
        public static int CERRAR_ = 0;
        //

        public USER_RUTINA_CENTRO()
        {
            InitializeComponent();
        }

        private void USER_RUTINA_CENTRO_Load(object sender, EventArgs e)
        {
            CERRAR_ = 0;
            combo_buscar_ejercicio();
            lbls_visibles_no();
        }

        private void combo_buscar_ejercicio()
        {
            combo_buscar.Items.Clear();
            combo_buscar.Text = "Seleccionar un grupo muscular";
            combo_buscar.Items.Add("Abdominales Inferiores"); //0
            combo_buscar.Items.Add("Abdominales superiores"); //0
            combo_buscar.Items.Add("Antebrazos"); //0
            combo_buscar.Items.Add("Bíceps"); //0
            combo_buscar.Items.Add("Caderas"); //0
            combo_buscar.Items.Add("Cuádriceps"); //0
            combo_buscar.Items.Add("Dorsales"); //0
            combo_buscar.Items.Add("Espalda (romboides)"); //0
            combo_buscar.Items.Add("Exterior femoral (Abductors)"); //0
            combo_buscar.Items.Add("Gemelos"); //0
            combo_buscar.Items.Add("Glúteos"); //0
            combo_buscar.Items.Add("Hombros (deltoides)"); //0
            combo_buscar.Items.Add("Laterales"); //0
            combo_buscar.Items.Add("Lumbares"); //0
            combo_buscar.Items.Add("Pectorales"); //0
            combo_buscar.Items.Add("Trapecios"); //0
            combo_buscar.Items.Add("Traseros"); //0
            combo_buscar.Items.Add("Tríceps"); //0
            combo_buscar.Items.Add("otros"); //0
        }

        private void lbls_visibles_no()
        {
            lbl1.Visible = false;
            lbl2.Visible = false;
            lbl3.Visible = false;
            lbl4.Visible = false;
            lbl5.Visible = false;
            lbl6.Visible = false;
            lbl7.Visible = false;
            lbl8.Visible = false;

            lbladd1.Visible = false;
            lbladd2.Visible = false;
            lbladd3.Visible = false;
            lbladd4.Visible = false;
            lbladd5.Visible = false;
            lbladd6.Visible = false;
            lbladd7.Visible = false;
            lbladd8.Visible = false;

            lbldet1.Visible = false;
            lbldet2.Visible = false;
            lbldet3.Visible = false;
            lbldet4.Visible = false;
            lbldet5.Visible = false;
            lbldet6.Visible = false;
            lbldet7.Visible = false;
            lbldet8.Visible = false;
        }

        private void lbls_visibles_si()
        {
            lbl1.Visible = true;
            lbl2.Visible = true;
            lbl3.Visible = true;
            lbl4.Visible = true;
            lbl5.Visible = true;
            lbl6.Visible = true;
            lbl7.Visible = true;
            lbl8.Visible = true;

            lbladd1.Visible = true;
            lbladd2.Visible = true;
            lbladd3.Visible = true;
            lbladd4.Visible = true;
            lbladd5.Visible = true;
            lbladd6.Visible = true;
            lbladd7.Visible = true;
            lbladd8.Visible = true;

            lbldet1.Visible = true;
            lbldet2.Visible = true;
            lbldet3.Visible = true;
            lbldet4.Visible = true;
            lbldet5.Visible = true;
            lbldet6.Visible = true;
            lbldet7.Visible = true;
            lbldet8.Visible = true;
        }

        private void borrar_lbl()
        {
            lbl1.Text = "";
            lbl2.Text = "";
            lbl3.Text = "";
            lbl4.Text = "";
            lbl5.Text = "";
            lbl6.Text = "";
            lbl7.Text = "";
            lbl8.Text = "";
        }

        private void combo_buscar_SelectedIndexChanged(object sender, EventArgs e)
        {
            lbls_visibles_si();
            borrar_lbl();
            lbl_generar_bus.Visible = false;
            detalle = 0;
            contador = 0;
            //
            //
            switch (combo_buscar.SelectedIndex)
            {

                case 0:
                    SQL_RUTINA = "SELECT * FROM RUTINAS WHERE Categoria='ABDOMINALES INFERIORES'";
                    break;
                case 1:
                    SQL_RUTINA = "SELECT * FROM RUTINAS WHERE Categoria='ABDOMINALES SUPERIORES'";
                    break;
                case 2:
                    SQL_RUTINA = "SELECT * FROM RUTINAS WHERE Categoria='ANTEBRAZOS'";
                    break;
                case 3:
                    SQL_RUTINA = "SELECT * FROM RUTINAS WHERE Categoria='BICEPS'";
                    break;
                case 4:
                    SQL_RUTINA = "SELECT * FROM RUTINAS WHERE Categoria='CADERAS'";
                    break;
                case 5:
                    SQL_RUTINA = "SELECT * FROM RUTINAS WHERE Categoria='CUADRICEPS'";
                    break;
                case 6:
                    SQL_RUTINA = "SELECT * FROM RUTINAS WHERE Categoria='DORSALES'";
                    break;
                case 7:
                    SQL_RUTINA = "SELECT * FROM RUTINAS WHERE Categoria='ESPALDA'";
                    break;
                case 8:
                    SQL_RUTINA = "SELECT * FROM RUTINAS WHERE Categoria='EXTERIOR FEMORAL'";
                    break;
                case 9:
                    SQL_RUTINA = "SELECT * FROM RUTINAS WHERE Categoria='GEMELOS'";
                    break;
                case 10:
                    SQL_RUTINA = "SELECT * FROM RUTINAS WHERE Categoria='GLUTEOS'";
                    break;
                case 11:
                    SQL_RUTINA = "SELECT * FROM RUTINAS WHERE Categoria='HOMBROS'";
                    break;
                case 12:
                    SQL_RUTINA = "SELECT * FROM RUTINAS WHERE Categoria='LATERALES'";
                    break;
                case 13:
                    SQL_RUTINA = "SELECT * FROM RUTINAS WHERE Categoria='LUMBARES'";
                    break;
                case 14:
                    SQL_RUTINA = "SELECT * FROM RUTINAS WHERE Categoria='PECTORALES'";
                    break;
                case 15:
                    SQL_RUTINA = "SELECT * FROM RUTINAS WHERE Categoria='TRAPECIOS'";
                    break;
                case 16:
                    SQL_RUTINA = "SELECT * FROM RUTINAS WHERE Categoria='TRASEROS'";
                    break;
                case 17:
                    SQL_RUTINA = "SELECT * FROM RUTINAS WHERE Categoria='TRICEPS'";
                    break;
                case 18:
                    SQL_RUTINA = "SELECT * FROM RUTINAS WHERE Categoria='OTROS'";
                    break;

            }

            try
            {

                conectar_.RUTINAS_ = new Recordset();
                conectar_.RUTINAS_.Open(SQL_RUTINA, conectar_.CN, CursorTypeEnum.adOpenDynamic, LockTypeEnum.adLockOptimistic);
                conectar_.RUTINAS_.MoveFirst();

                pic1.Image = null;
                pic2.Image = null;
                pic3.Image = null;
                pic4.Image = null;
                pic5.Image = null;
                pic6.Image = null;
                pic7.Image = null;
                pic8.Image = null;

                while (!(conectar_.RUTINAS_.EOF))
                {

                    try
                    {
                        if (pic1.Image == null)
                        {
                            pic1.Image = System.Drawing.Image.FromFile(System.IO.Directory.GetCurrentDirectory() + @"\IMG_RUTINAS\" + conectar_.RUTINAS_.Fields[3].Value);
                            lbl1.Text = conectar_.RUTINAS_.Fields[1].Value;
                            conectar_.RUTINAS_.MoveNext();
                            detalle++;
                        }
                        else if (pic2.Image == null)
                        {
                            pic2.Image = System.Drawing.Image.FromFile(System.IO.Directory.GetCurrentDirectory() + @"\IMG_RUTINAS\" + conectar_.RUTINAS_.Fields[3].Value);
                            lbl2.Text = conectar_.RUTINAS_.Fields[1].Value;
                            if (conectar_.RUTINAS_.EOF == true)
                            {
                                break;
                            }
                            else
                            {
                                detalle++;
                                conectar_.RUTINAS_.MoveNext();
                            }
                        }
                        else if (pic3.Image == null)
                        {
                            pic3.Image = System.Drawing.Image.FromFile(System.IO.Directory.GetCurrentDirectory() + @"\IMG_RUTINAS\" + conectar_.RUTINAS_.Fields[3].Value);
                            lbl3.Text = conectar_.RUTINAS_.Fields[1].Value;
                            if (conectar_.RUTINAS_.EOF == true)
                            {
                                break;
                            }
                            else
                            {
                                detalle++;
                                conectar_.RUTINAS_.MoveNext();
                            }
                        }
                        else if (pic4.Image == null)
                        {
                            pic4.Image = System.Drawing.Image.FromFile(System.IO.Directory.GetCurrentDirectory() + @"\IMG_RUTINAS\" + conectar_.RUTINAS_.Fields[3].Value);
                            lbl4.Text = conectar_.RUTINAS_.Fields[1].Value;
                            if (conectar_.RUTINAS_.EOF == true)
                            {
                                break;
                            }
                            else
                            {
                                detalle++;
                                conectar_.RUTINAS_.MoveNext();
                            }
                        }
                        else if (pic5.Image == null)
                        {
                            pic5.Image = System.Drawing.Image.FromFile(System.IO.Directory.GetCurrentDirectory() + @"\IMG_RUTINAS\" + conectar_.RUTINAS_.Fields[3].Value);
                            lbl5.Text = conectar_.RUTINAS_.Fields[1].Value;
                            if (conectar_.RUTINAS_.EOF == true)
                            {
                                break;
                            }
                            else
                            {
                                detalle++;
                                conectar_.RUTINAS_.MoveNext();
                            }
                        }
                        else if (pic6.Image == null)
                        {
                            pic6.Image = System.Drawing.Image.FromFile(System.IO.Directory.GetCurrentDirectory() + @"\IMG_RUTINAS\" + conectar_.RUTINAS_.Fields[3].Value);
                            lbl6.Text = conectar_.RUTINAS_.Fields[1].Value;
                            if (conectar_.RUTINAS_.EOF == true)
                            {
                                break;
                            }
                            else
                            {
                                detalle++;
                                conectar_.RUTINAS_.MoveNext();
                            }
                        }
                        else if (pic7.Image == null)
                        {
                            pic7.Image = System.Drawing.Image.FromFile(System.IO.Directory.GetCurrentDirectory() + @"\IMG_RUTINAS\" + conectar_.RUTINAS_.Fields[3].Value);
                            lbl7.Text = conectar_.RUTINAS_.Fields[1].Value;
                            if (conectar_.RUTINAS_.EOF == true)
                            {
                                break;
                            }
                            else
                            {
                                detalle++;
                                conectar_.RUTINAS_.MoveNext();
                            }
                        }
                        else if (pic8.Image == null)
                        {
                            if (conectar_.RUTINAS_.EOF == false)
                            {
                                pic8.Image = System.Drawing.Image.FromFile(System.IO.Directory.GetCurrentDirectory() + @"\IMG_RUTINAS\" + conectar_.RUTINAS_.Fields[3].Value);
                                lbl8.Text = conectar_.RUTINAS_.Fields[1].Value;
                                detalle++;
                                break;
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                    catch { conectar_.RUTINAS_.MoveNext(); }
                }
            }
            catch { }
        }

        private void lbldet1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            nombre_detalle = lbl1.Text;
            DETALLES_RUTINAS detalle_ = new DETALLES_RUTINAS();
            detalle_.Show();
        }

        private void lbldet2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            nombre_detalle = lbl2.Text;
            DETALLES_RUTINAS detalle_ = new DETALLES_RUTINAS();
            detalle_.Show();
        }

        private void lbldet3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            nombre_detalle = lbl3.Text;
            DETALLES_RUTINAS detalle_ = new DETALLES_RUTINAS();
            detalle_.Show();
        }

        private void lbldet4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            nombre_detalle = lbl4.Text;
            DETALLES_RUTINAS detalle_ = new DETALLES_RUTINAS();
            detalle_.Show();
        }

        private void lbldet5_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            nombre_detalle = lbl5.Text;
            DETALLES_RUTINAS detalle_ = new DETALLES_RUTINAS();
            detalle_.Show();
        }

        private void lbldet6_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            nombre_detalle = lbl6.Text;
            DETALLES_RUTINAS detalle_ = new DETALLES_RUTINAS();
            detalle_.Show();
        }

        private void lbldet7_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            nombre_detalle = lbl7.Text;
            DETALLES_RUTINAS detalle_ = new DETALLES_RUTINAS();
            detalle_.Show();
        }

        private void lbldet8_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            nombre_detalle = lbl8.Text;
            DETALLES_RUTINAS detalle_ = new DETALLES_RUTINAS();
            detalle_.Show();
        }

        private void lblsiguiente_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            pic1.Image = null;
            pic2.Image = null;
            pic3.Image = null;
            pic4.Image = null;
            pic5.Image = null;
            pic6.Image = null;
            pic7.Image = null;
            pic8.Image = null;

            borrar_lbl();

            try
            {

                try
                {
                    contador = 0;
                    conectar_.RUTINAS_.MoveFirst();
                   // Thread.Sleep(1000);
                }
                catch
                {
                }
                
                while(!(conectar_.RUTINAS_.EOF)){
                    contador++;
                    if (contador == detalle)
                    {
                        break;
                    }
                    else
                    {
                        conectar_.RUTINAS_.MoveNext();
                    }
                }
           
                while (!(conectar_.RUTINAS_.EOF))
                {
                    if (pic1.Image == null)
                    {
                        pic1.Image = System.Drawing.Image.FromFile(System.IO.Directory.GetCurrentDirectory() + @"\IMG_RUTINAS\" + conectar_.RUTINAS_.Fields[3].Value);
                        lbl1.Text = conectar_.RUTINAS_.Fields[1].Value;
                        conectar_.RUTINAS_.MoveNext();
                        detalle++;
                    }
                    else if (pic2.Image == null)
                    {
                        pic2.Image = System.Drawing.Image.FromFile(System.IO.Directory.GetCurrentDirectory() + @"\IMG_RUTINAS\" + conectar_.RUTINAS_.Fields[3].Value);
                        lbl2.Text = conectar_.RUTINAS_.Fields[1].Value;
                        if (conectar_.RUTINAS_.EOF == true)
                        {
                            break;
                        }
                        else
                        {
                            detalle++;
                            conectar_.RUTINAS_.MoveNext();
                        }
                    }
                    else if (pic3.Image == null)
                    {
                        pic3.Image = System.Drawing.Image.FromFile(System.IO.Directory.GetCurrentDirectory() + @"\IMG_RUTINAS\" + conectar_.RUTINAS_.Fields[3].Value);
                        lbl3.Text = conectar_.RUTINAS_.Fields[1].Value;
                        if (conectar_.RUTINAS_.EOF == true)
                        {
                            break;
                        }
                        else
                        {
                            detalle++;
                            conectar_.RUTINAS_.MoveNext();
                        }
                    }
                    else if (pic4.Image == null)
                    {
                        pic4.Image = System.Drawing.Image.FromFile(System.IO.Directory.GetCurrentDirectory() + @"\IMG_RUTINAS\" + conectar_.RUTINAS_.Fields[3].Value);
                        lbl4.Text = conectar_.RUTINAS_.Fields[1].Value;
                        if (conectar_.RUTINAS_.EOF == true)
                        {
                            break;
                        }
                        else
                        {
                            detalle++;
                            conectar_.RUTINAS_.MoveNext();
                        }
                    }
                    else if (pic5.Image == null)
                    {
                        pic5.Image = System.Drawing.Image.FromFile(System.IO.Directory.GetCurrentDirectory() + @"\IMG_RUTINAS\" + conectar_.RUTINAS_.Fields[3].Value);
                        lbl5.Text = conectar_.RUTINAS_.Fields[1].Value;
                        if (conectar_.RUTINAS_.EOF == true)
                        {
                            break;
                        }
                        else
                        {
                            detalle++;
                            conectar_.RUTINAS_.MoveNext();
                        }
                    }
                    else if (pic6.Image == null)
                    {
                        pic6.Image = System.Drawing.Image.FromFile(System.IO.Directory.GetCurrentDirectory() + @"\IMG_RUTINAS\" + conectar_.RUTINAS_.Fields[3].Value);
                        lbl6.Text = conectar_.RUTINAS_.Fields[1].Value;
                        if (conectar_.RUTINAS_.EOF == true)
                        {
                            break;
                        }
                        else
                        {
                            detalle++;
                            conectar_.RUTINAS_.MoveNext();
                        }
                    }
                    else if (pic7.Image == null)
                    {
                        pic7.Image = System.Drawing.Image.FromFile(System.IO.Directory.GetCurrentDirectory() + @"\IMG_RUTINAS\" + conectar_.RUTINAS_.Fields[3].Value);
                        lbl7.Text = conectar_.RUTINAS_.Fields[1].Value;
                        if (conectar_.RUTINAS_.EOF == true)
                        {
                            break;
                        }
                        else
                        {
                            detalle++;
                            conectar_.RUTINAS_.MoveNext();
                        }
                    }
                    else if (pic8.Image == null)
                    {
                        if (conectar_.RUTINAS_.EOF == false)
                        {
                            pic8.Image = System.Drawing.Image.FromFile(System.IO.Directory.GetCurrentDirectory() + @"\IMG_RUTINAS\" + conectar_.RUTINAS_.Fields[3].Value);
                            lbl8.Text = conectar_.RUTINAS_.Fields[1].Value;
                            detalle++;
                            break;
                        }
                        else
                        {
                            break;
                        }
                    }
                }

            }
            catch { }
            try
            {
                if (detalle >= conectar_.RUTINAS_.RecordCount)
                {
                    lblsiguiente.Enabled = false;
                }
                else
                {
                    lblsiguiente.Enabled = true;
                }
                lblanerior.Enabled = true;
            }
            catch { }
            //lblsiguiente.Enabled = false;

        }

        private void lblanerior_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            pic1.Image = null;
            pic2.Image = null;
            pic3.Image = null;
            pic4.Image = null;
            pic5.Image = null;
            pic6.Image = null;
            pic7.Image = null;
            pic8.Image = null;


            try
            {
              conectar_.RUTINAS_.MoveFirst();
              detalle = 0;
              contador = 0;
                
                while (!(conectar_.RUTINAS_.EOF))
                {
                    if (pic1.Image == null)
                    {
                        pic1.Image = System.Drawing.Image.FromFile(System.IO.Directory.GetCurrentDirectory() + @"\IMG_RUTINAS\" + conectar_.RUTINAS_.Fields[3].Value);
                        lbl1.Text = conectar_.RUTINAS_.Fields[1].Value;
                        conectar_.RUTINAS_.MoveNext();
                        detalle++;
                    }
                    else if (pic2.Image == null)
                    {
                        pic2.Image = System.Drawing.Image.FromFile(System.IO.Directory.GetCurrentDirectory() + @"\IMG_RUTINAS\" + conectar_.RUTINAS_.Fields[3].Value);
                        lbl2.Text = conectar_.RUTINAS_.Fields[1].Value;
                        if (conectar_.RUTINAS_.EOF == true)
                        {
                            break;
                        }
                        else
                        {
                            detalle++;
                            conectar_.RUTINAS_.MoveNext();
                        }
                    }
                    else if (pic3.Image == null)
                    {
                        pic3.Image = System.Drawing.Image.FromFile(System.IO.Directory.GetCurrentDirectory() + @"\IMG_RUTINAS\" + conectar_.RUTINAS_.Fields[3].Value);
                        lbl3.Text = conectar_.RUTINAS_.Fields[1].Value;
                        if (conectar_.RUTINAS_.EOF == true)
                        {
                            break;
                        }
                        else
                        {
                            detalle++;
                            conectar_.RUTINAS_.MoveNext();
                        }
                    }
                    else if (pic4.Image == null)
                    {
                        pic4.Image = System.Drawing.Image.FromFile(System.IO.Directory.GetCurrentDirectory() + @"\IMG_RUTINAS\" + conectar_.RUTINAS_.Fields[3].Value);
                        lbl4.Text = conectar_.RUTINAS_.Fields[1].Value;
                        if (conectar_.RUTINAS_.EOF == true)
                        {
                            break;
                        }
                        else
                        {
                            detalle++;
                            conectar_.RUTINAS_.MoveNext();
                        }
                    }
                    else if (pic5.Image == null)
                    {
                        pic5.Image = System.Drawing.Image.FromFile(System.IO.Directory.GetCurrentDirectory() + @"\IMG_RUTINAS\" + conectar_.RUTINAS_.Fields[3].Value);
                        lbl5.Text = conectar_.RUTINAS_.Fields[1].Value;
                        if (conectar_.RUTINAS_.EOF == true)
                        {
                            break;
                        }
                        else
                        {
                            detalle++;
                            conectar_.RUTINAS_.MoveNext();
                        }
                    }
                    else if (pic6.Image == null)
                    {
                        pic6.Image = System.Drawing.Image.FromFile(System.IO.Directory.GetCurrentDirectory() + @"\IMG_RUTINAS\" + conectar_.RUTINAS_.Fields[3].Value);
                        lbl6.Text = conectar_.RUTINAS_.Fields[1].Value;
                        if (conectar_.RUTINAS_.EOF == true)
                        {
                            break;
                        }
                        else
                        {
                            detalle++;
                            conectar_.RUTINAS_.MoveNext();
                        }
                    }
                    else if (pic7.Image == null)
                    {
                        pic7.Image = System.Drawing.Image.FromFile(System.IO.Directory.GetCurrentDirectory() + @"\IMG_RUTINAS\" + conectar_.RUTINAS_.Fields[3].Value);
                        lbl7.Text = conectar_.RUTINAS_.Fields[1].Value;
                        if (conectar_.RUTINAS_.EOF == true)
                        {
                            break;
                        }
                        else
                        {
                            detalle++;
                            conectar_.RUTINAS_.MoveNext();
                        }
                    }
                    else if (pic8.Image == null)
                    {
                        if (conectar_.RUTINAS_.EOF == false)
                        {
                            pic8.Image = System.Drawing.Image.FromFile(System.IO.Directory.GetCurrentDirectory() + @"\IMG_RUTINAS\" + conectar_.RUTINAS_.Fields[3].Value);
                            lbl8.Text = conectar_.RUTINAS_.Fields[1].Value;
                            detalle++;
                            break;
                        }
                        else
                        {
                            break;
                        }
                    }
                }

            }
            catch { }


            lblanerior.Enabled = false;
            lblsiguiente.Enabled = true;

        }

        private void lbladd1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            nombre_add = lbl1.Text;
            lista_add_rutina.Items.Add(lbl1.Text);
            VER_CAMPOS();
        }

        private void lbladd2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            nombre_add = lbl2.Text;
            lista_add_rutina.Items.Add(lbl2.Text);
            VER_CAMPOS();
        }

        private void lbladd3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            nombre_add = lbl3.Text;
            lista_add_rutina.Items.Add(lbl3.Text);
            VER_CAMPOS();
        }

        private void lbladd4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            nombre_add = lbl4.Text;
            lista_add_rutina.Items.Add(lbl4.Text);
            VER_CAMPOS();
        }

        private void lbladd5_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            nombre_add = lbl5.Text;
            lista_add_rutina.Items.Add(lbl5.Text);
            VER_CAMPOS();
        }

        private void lbladd6_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            nombre_add = lbl6.Text;
            lista_add_rutina.Items.Add(lbl6.Text);
            VER_CAMPOS();
        }

        private void lbladd7_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            nombre_add = lbl7.Text;
            lista_add_rutina.Items.Add(lbl7.Text);
            VER_CAMPOS();
        }

        private void lbladd8_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            nombre_add = lbl8.Text;
            lista_add_rutina.Items.Add(lbl8.Text);
            VER_CAMPOS();
          
        }

        private void VER_CAMPOS()
        {
            if (USER_RUTINAS_IZQUIERDA.APELLIDO_ == "" || USER_RUTINAS_IZQUIERDA.NOMBRE_ == "" || USER_RUTINA_ARRIBA.VARIABLE_DIA == "")
            {
                MessageBox.Show("NO SE HA GENERADO TODOS LOS CAMPOS\n\nLO MAS PROBABLE ES QUE EL NOMBRE, APELLIDO O DIA DE LA RUTINA ESTE EN BLANCO", "ERROR NO HAY CAMPOS");
                return;
            }
            else
            {
                RUTINA_ADD RUTINA_ADD = new RUTINA_ADD();
                RUTINA_ADD.Show();
            }
        }

        private void cmd_cerrar_Click(object sender, EventArgs e)
        {
            CERRAR_ = 1;
        }

        private void cmd_reset_Click(object sender, EventArgs e)
        {
            lista_add_rutina.Items.Clear();
            lista_add_rutina.Items.Add("EJERCICIO:");
        }

    }
}

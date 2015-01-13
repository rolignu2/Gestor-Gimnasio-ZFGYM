using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.IO;
using ZONE_FITNESS_3._0_FINAL.CLASES;

namespace ZONE_FITNESS_3._0_FINAL
{
    public partial class seleccion_nombre_gym : Form
    {
        public seleccion_nombre_gym()
        {
            InitializeComponent();
        }


        private int CONTADOR = 20;

        private void seleccion_nombre_gym_Load(object sender, EventArgs e)
        {
            
            this.Height = 253;
            this.Width = 436;
            this.Location = new Point(500, 400);
            this.Opacity = 0.92;
            this.Text = "SELECCIONA EL NOMBRE AL GESTOR DE GIMNASIOS";
            this.BackColor = Color.DarkBlue;
            this.ControlBox = false;

            this.Left = 93;
            this.Top = 154;

            txtnombre_.MaxLength = 20;

            label7.Visible = false;

            img_previa.SizeMode = PictureBoxSizeMode.StretchImage;
            
        }

        private void button1_Click(object sender, EventArgs e)
        {

            try
            {
                if (File.Exists(System.IO.Directory.GetCurrentDirectory() + @"\gym.txt"))
                {
                    File.Delete(System.IO.Directory.GetCurrentDirectory() + @"\gym.txt");
                    //File.Create(System.IO.Directory.GetCurrentDirectory() + @"\gym.txt");
                }
            }
            catch (Exception ex_) { MessageBox.Show(ex_.Message); }

            try{
               
            StreamWriter escribir = File.AppendText(System.IO.Directory.GetCurrentDirectory() + @"\gym.txt"); // new StreamWriter(System.IO.Directory.GetCurrentDirectory() + @"\gym.txt");
            escribir.WriteLine(txtnombre_.Text);
            escribir.WriteLine(txt_admin.Text);
            escribir.WriteLine(txtimg_.Text);
            escribir.Close();

            if (NOMBRE_GYM.todo_texto[0] == null)
            {
                NOMBRE_GYM.OutNombreGym();
            }
            DialogResult RESTART = MessageBox.Show("PARA VER LOS CAMBIOS SE NECESITA REINICIAR LA APLICACION. ¿DESEA REINICIAR LA APLICACION?", "MENSAJE", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (DialogResult.Yes == RESTART) Application.Restart();
            else this.Close();
            }
            catch(Exception ex){
                 MessageBox.Show("ERROR ... " + ex.Message + "  " ,"ERROR",MessageBoxButtons.OK , MessageBoxIcon.Error);
                this.Close();
            }

        }

        private void cmd_buscarimg_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog abrir_directorio = new OpenFileDialog();
                string[] seleccionar_ruta = new string[5000];
                object rutas;
                abrir_directorio.ValidateNames = true;
                abrir_directorio.Title = "AGREGA TU LOGOTIPO";
                //abrir_directorio.Filter = "(*.jpg)|(*.gif)|(*.png)|(*.gif)";
                abrir_directorio.ShowDialog();

                if (abrir_directorio.FileName != null)
                {
                    seleccionar_ruta = abrir_directorio.FileNames;
                    foreach (object rutas_loopVariable in seleccionar_ruta)
                    {
                        rutas = rutas_loopVariable;//variable objeto dandole un ciclo booleano al object
                        System.IO.FileInfo fileinfo = new System.IO.FileInfo(Convert.ToString(rutas));//como es un objeto necesitamos hacerlo string o cadena
                        txtimg_.Text = Convert.ToString(fileinfo);

                        img_previa.Image = null;
                        img_previa.Image = System.Drawing.Image.FromFile(Convert.ToString(fileinfo));

                        this.Height = 253;
                        this.Width = 697;
                        return;
                    }
                }
                else
                {
                    return;
                }

            }
            catch { }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label7.Text = "top = " + this.Top + " left = " + this.Left;
            label7.Visible = false;
        }

        private void txtnombre__TextChanged(object sender, EventArgs e)
        {
            lblcont.Text = Convert.ToString(CONTADOR - txtnombre_.Text.Length);
        }

        private void link_cerrar_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Close();
        }
    }

}

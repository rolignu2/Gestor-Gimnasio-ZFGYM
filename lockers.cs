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
    public partial class lockers : Form
    {

        string Path = System.IO.Directory.GetCurrentDirectory() + @"\LockConf.txt";
        Controles_dinamicos control = new Controles_dinamicos();

        public lockers()
        {
            InitializeComponent();
        }

        private void lockers_Load(object sender, EventArgs e)
        {
            StreamReader LockLectura = new StreamReader(Path);
            string data = "";

            this.Text = "LOCKERS V1.0";
            this.WindowState = FormWindowState.Normal;
            control.Form_Tam(this.Width, this.Height);
            this.AutoScroll = true;

            data = LockLectura.ReadLine();
            if (data != "" || data != null)
            {
                control.set_cantidad(Convert.ToInt16(data));
                DRAW_CONTROL();
                LockLectura.Close();
            }

        }

        private void sALIRDELPROGRAMAToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OPCIONES OPT = new OPCIONES();
            OPT.salir();
        }

        private void sALIRMENUToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmdadd_Click(object sender, EventArgs e)
        {
            control.set_cantidad(Convert.ToInt32(txtcant.Text));
        }

        private void DRAW_CONTROL()
        {
            grupo_.Visible = false;

            if (control.Is_Null() != true)
            {


                for (int i = 0; i < control.Get_img().Count ; i++)
                {
                    this.Controls.Remove(control.Get_img()[i]);
                    this.Controls.Remove(control.Get_label()[i]);
                    this.Controls.Remove(control.Get_check()[i]);
                }
               // InitializeComponent();
                grupo_.Visible = false;
            }


            foreach (System.Windows.Forms.Label lbl in control.set_label())
            {
                this.Controls.Add(lbl);
            }

            foreach (System.Windows.Forms.CheckBox ch in control.set_checke())
            {
                this.Controls.Add(ch);
            }

            foreach (System.Windows.Forms.PictureBox imagen in control.set_CajaImagen())
            {
                this.Controls.Add(imagen);
            }
        }

        private void cmdver_Click(object sender, EventArgs e)
        {
            DRAW_CONTROL();
        }

        private void cmddel_Click(object sender, EventArgs e)
        {
            control.set_cantidad(Convert.ToInt32(txtdel.Text));
        }

        private void aGREGARCASILLEROSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            grupo_.Visible = true;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.grupo_.Visible = false;
        }

        private void cmdsave_Click(object sender, EventArgs e)
        {
             
            try
            {
                if (!(File.Exists(Path)))
                {
                    File.CreateText(Path);
                }
                StreamWriter LockersWriter = new StreamWriter(Path, true, Encoding.Default);
                LockersWriter.WriteLine(txtcant.Text, 0);
                LockersWriter.Close();

                MessageBox.Show("DATOS GUARDADOS CON EXITO");

            }
            catch
            {
                try
                {
                    StreamWriter LockersWriter = new StreamWriter(Path, true, Encoding.Default);
                    LockersWriter.WriteLine(txtcant.Text, 0);
                    LockersWriter.Close();

                    MessageBox.Show("DATOS GUARDADOS CON EXITO");
                }
                catch(Exception ex) { Console.WriteLine("ERROR DE SOBRECARGADO " + ex.Message); }
            }

            DRAW_CONTROL();

        }

    }
}

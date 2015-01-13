using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

using ADODB;
using System.Data.OleDb;
using ZONE_FITNESS_3._0_FINAL.CLASES;


namespace ZONE_FITNESS_3._0_FINAL
{
    public partial class RUTINAS_CREADAS : Form
    {
        public RUTINAS_CREADAS()
        {
            InitializeComponent();
        }

        private void RUTINAS_CREADAS_Load(object sender, EventArgs e)
        {
            this.Text = "RUTINAS CREADAS";
            this.BackColor = System.Drawing.Color.DarkBlue;
            //
            if (GENERAR_BUSQUEDA() == 1)
            {
                MessageBox.Show("NO EXISTE RUTINAS ... ", "NO ENCONTRADO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();//cierra el frm
            }
        }

        private int GENERAR_BUSQUEDA()
        {
            try
            {
                string directorio = System.IO.Directory.GetCurrentDirectory() + @"\PDF";//directorio donde se guardan los ficheros
                System.IO.FileInfo[] ficheros_;//vector ficheros
                ficheros_ = new DirectoryInfo(directorio).GetFiles("*.pdf");//filtrar ficheros de tipo pdf en la direccion , directorio
                label2.Text = "TOTAL: " + ficheros_.Length;//coloca la cantidad de ficheros q existen en pdf


                /*DirectoryInfo directory = new DirectoryInfo(Directory.GetCurrentDirectory() + @"\IMG_RUTINAS");
                directory.GetFiles();
                IEnumerable<FileInfo> getdir = directory.EnumerateFiles();*/
             

                /*foreach (var x in getdir)
                {
                    listimg.Images.Add(Image.FromFile(Directory.GetCurrentDirectory() + @"\IMG_RUTINAS\" + x.Name));
                }*/

                int k = 0 , z= 0;
                ImageList listimg = new ImageList();
                conectar_.RS_SOCIO = new Recordset();
                conectar_.RS_SOCIO.Open( "SELECT * FROM SOCIOS" ,conectar_.CN, CursorTypeEnum.adOpenDynamic, LockTypeEnum.adLockOptimistic);
                for(int m = 0 ; m < ficheros_.Length ; m++)
                {
                    List<string> nombre = new List<string>();
                    List<string> apellido = new List<string>();
                    for (z = 0; z < ficheros_[k].ToString().Length; z++)
                    {
                        if (ficheros_[k].ToString()[z].ToString() != "_")
                        {
                            nombre.Add(ficheros_[k].ToString()[z].ToString());

                        }
                        else
                            break;
                    }

                    for (int j = z + 1; j < ficheros_[k].ToString().Length; j++)
                    {
                        if (ficheros_[k].ToString()[j].ToString() != ".")
                        {
                            apellido.Add(ficheros_[k].ToString()[j].ToString());

                        }
                        else
                            break;
                    }

                    string Rnombre = string.Join("", nombre);
                    string Rapellido = string.Join("", apellido);

                    do
                    {
                        if (Rnombre == conectar_.RS_SOCIO.Fields[1].Value 
                            && Rapellido == conectar_.RS_SOCIO.Fields[2].Value)
                        {
                            if (conectar_.RS_SOCIO.Fields[16].Value == null || conectar_.RS_SOCIO.Fields[16].Value == "")
                            {
                                listimg.Images.Add(Image.FromFile(Directory.GetCurrentDirectory() + @"\IMG_SOCIOS\NO_IMG.jpg"));
                            }
                            else
                            {
                                listimg.Images.Add(Image.FromFile(Directory.GetCurrentDirectory() + @"\IMG_SOCIOS\" + conectar_.RS_SOCIO.Fields[16].Value));
                            }

                            break;
                        }
                        conectar_.RS_SOCIO.MoveNext();
                    }
                    while (conectar_.RS_SOCIO.EOF != true);

                    conectar_.RS_SOCIO.MoveFirst();
                    k++;
                }


                listimg.ImageSize = new System.Drawing.Size(40, 40);
                listView1.View = View.LargeIcon;
                listView1.LargeImageList = listimg;
                listView1.StateImageList = listimg;
                listView1.SmallImageList = listimg;
                listView1.Alignment = ListViewAlignment.Left;
               
                for (int i = 0; i < ficheros_.Length; i++)//este ciclo agrega cada fichero existente a la lista 
                {
                    listView1.Items.Add(ficheros_[i].ToString(), i);
                }
                if (ficheros_.Length == 0) return 1; //si no existe ningun fichero pdf entonces devuelve 1

            }
            catch//en caso de error
            {
                return 0;
            }

            return 0; //si existe entonces devuelve 0

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();//cierra el formulario
        }

        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            string dato = listView1.FocusedItem.Text;
            try { System.Diagnostics.Process.Start(System.IO.Directory.GetCurrentDirectory() + @"\PDF\" + dato); } //abre el fichero segun la direccion gracias al hilo o proceso
            catch { }   
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            listView1.View = View.Tile;
           
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            listView1.View = View.LargeIcon;
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            listView1.View = View.List;
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            listView1.View = View.SmallIcon;
        }


    }
}

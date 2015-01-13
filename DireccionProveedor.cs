using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace GestorGimnasio
{
    public partial class DireccionProveedor : Form
    {
        public DireccionProveedor()
        {
            InitializeComponent();
        }

        private void cmdbuscar_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog abrir_directorio = new OpenFileDialog();
                string[] seleccionar_ruta = new string[5000];
                object rutas;
                abrir_directorio.ValidateNames = true;
                abrir_directorio.Title = "Provider";
                abrir_directorio.Filter = "(*.accdb)|";
                
                if (abrir_directorio.ShowDialog() == DialogResult.OK)
                {
                    seleccionar_ruta = abrir_directorio.FileNames;
                    foreach (object rutas_loopVariable in seleccionar_ruta)
                    {
                        rutas = rutas_loopVariable;//variable objeto dandole un ciclo booleano al object
                        System.IO.FileInfo fileinfo = new System.IO.FileInfo(Convert.ToString(rutas));//como es un objeto necesitamos hacerlo string o cadena
                        txtdireccion.Text = Convert.ToString(fileinfo);

                    }
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cmdguardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (!File.Exists(Directory.GetCurrentDirectory() + @"\Proveedor.txt"))
                {
                    File.Create(Directory.GetCurrentDirectory() + @"\Proveedor.txt").Close();
                   
                }
            }
            catch{}
            StreamWriter WriteProveedor = new StreamWriter(Directory.GetCurrentDirectory() + @"\Proveedor.txt");
            WriteProveedor.WriteLine(txtdireccion.Text);
            WriteProveedor.Close();

            MessageBox.Show("Direccion del proveedor ha sido cambiada a:\n" + txtdireccion.Text + "\n\n\t\t Zone Fitness Gym se reiniciara ...", "Exito!!");
            Application.Restart();
            this.Close();
        }
    }
}

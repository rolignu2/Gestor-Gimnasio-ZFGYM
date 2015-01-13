using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GestorGimnasio
{
    public partial class EMAILINEXISTENTES : Form
    {
        public EMAILINEXISTENTES()
        {
            InitializeComponent();
        }

        private void EMAILINEXISTENTES_Load(object sender, EventArgs e)
        {
            this.Opacity = 0.92;
            LoadDatos();
        }

        private void LoadDatos()
        {

            DataTable dt = new DataTable(); 
            dt.Columns.Add("Nombre");
            dt.Columns.Add("Apellido");
            dt.Columns.Add("Correo Dañado");

           
            dataGridView1.DataSource = null;
           
            foreach (var Datos in PROMOCIONES.CorreosNoexistentes)
            {
                DataRow row = dt.NewRow();
                string[] split = Datos.Key.Split('=');
                row["Nombre"] = split[0].ToString();
                row["Apellido"] = split[1].ToString();
                row["Correo Dañado"] = Datos.Value;
                dt.Rows.Add(row);
                dataGridView1.DataSource = dt;
                dataGridView1.Update();
            }

            
        }
    }
}

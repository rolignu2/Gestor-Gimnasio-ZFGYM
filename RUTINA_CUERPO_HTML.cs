using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using ZONE_FITNESS_3._0_FINAL.CLASES;
using Microsoft.Win32;

using System.Threading;

using ADODB;

namespace ZONE_FITNESS_3._0_FINAL
{
    public partial class RUTINA_CUERPO_HTML : Form
    {
        private static string SQL_ = "SELECT * FROM RUTINA_HTML";

        public RUTINA_CUERPO_HTML()
        {
            InitializeComponent();
        }

        private void RUTINA_CUERPO_HTML_Load(object sender, EventArgs e)
        {
            this.BackColor = Color.DarkBlue;
            this.Text = "EDITAR CUERPO DEL MENSAJE EN RUTINAS";

            //CONECTAR LA TABLA Y SUS DATOS Y CARGARLOS AL FORMULARIO
            conexion_html();
        }

        private string conexion_html()
        {

            SQL_ = "SELECT * FROM RUTINA_HTML";
            conectar_.RUTINA_HTML = new Recordset();
            conectar_.RUTINA_HTML.Open(SQL_, conectar_.CN, CursorTypeEnum.adOpenDynamic, LockTypeEnum.adLockBatchOptimistic);

            
            if (conectar_.RUTINA_HTML.Fields[1].Value == null || conectar_.RUTINA_HTML.Fields[1].Value == "")
            {
                return "GESTOR DE GIMNASIO";
            }
            else
            {
                conectar_.RUTINA_HTML.MoveLast();
                txtnombre.Text = conectar_.RUTINA_HTML.Fields[1].Value;
                rich_html.Text = conectar_.RUTINA_HTML.Fields[2].Value;
                web_html.DocumentText = conectar_.RUTINA_HTML.Fields[2].Value;
                conectar_.RUTINA_HTML.Close();

            }

            return "NO HAY PROBLEMA";         
        }

        private void button1_Click(object sender, EventArgs e)
        {

                SQL_ = "INSERT INTO RUTINA_HTML (HTML_NOMBRE , HTML_RUTINA) VALUES ('" 
                + txtnombre.Text + "','" 
                + rich_html.Text + "')";

            conectar_.RUTINA_HTML = new Recordset();
            conectar_.RUTINA_HTML.Open(SQL_, conectar_.CN, CursorTypeEnum.adOpenDynamic, LockTypeEnum.adLockBatchOptimistic);

            MessageBox.Show("DATOS GUARDADOS CON EXITO", "EXITO");
            conexion_html();
        }
    }
}

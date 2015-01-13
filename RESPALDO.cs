using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using ADODB;
using ZONE_FITNESS_3._0_FINAL.CLASES;
using System.IO;


namespace ZONE_FITNESS_3._0_FINAL
{
    public partial class RESPALDO : Form
    {
        public RESPALDO()
        {
            InitializeComponent();
        }

        private void RESPALDO_Load(object sender, EventArgs e)
        {
            this.Opacity = 0.92;
            this.BackColor = Color.DarkBlue;
        }

        private void cmd_direccion_Click(object sender, EventArgs e)
        {
            respaldo respaldo = new respaldo();
            respaldo.RESPALDAR_BD();
            txtrespaldo.Text = Convert.ToString (respaldo.direccion_respaldo);
        }

        private void cmdrespaldo_Click(object sender, EventArgs e)
        {
            try
            {
                System.IO.File.Copy(System.IO.Directory.GetCurrentDirectory() + @"\ZFGYM_BD.accdb", txtrespaldo.Text);
                MessageBox.Show("RESPALDO GUARDADO EXITOSAMENTE","EXITO",MessageBoxButtons.OK , MessageBoxIcon.Information);
            }
            catch {

                MessageBox.Show("ERROR AL RESPALDAR ARCHIVO , SI EL ERROR PERSISTE FAVOR COMUNICARSE CON AYUDA Y SOPORTE TECNICO", "ERR RESPALDO");
            }
        }

        private void cmdadd_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult re;
                re = MessageBox.Show("ESTA SEGURO DE AGREGAR ESTA BASE DE DATOS, SE ELIMINARA LA BASE DE DATOS ANTERIOR ", "ADVERTENCIA", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (re == DialogResult.Yes)
                {
                    conectar_.CN.Close();
                    conectar_.mysqlconection.Close();
                    System.IO.File.Delete(System.IO.Directory.GetCurrentDirectory() + @"\ZFGYM_BD.accdb");
                    System.IO.File.Copy(txtagregar_respaldo.Text, System.IO.Directory.GetCurrentDirectory() + @"\ZFGYM_BD.accdb" );
                    MessageBox.Show("RESPALDO AGREGADO CON EXITO; EL PROGRAMA SE CERRARA PARA LOS NUEVOS CAMBIOS", "EXITO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Application.Exit();
                }
                else
                {
                    return;
                }
            }
            catch( Exception ex)
            {

                MessageBox.Show("ERROR AL AGREGAR ARCHIVO , SI EL ERROR PERSISTE FAVOR COMUNICARSE CON AYUDA Y SOPORTE TECNICO\n\n" + ex.Message , "ERR RESPALDO");
            }
        }

        private void cmdbuscar_Click(object sender, EventArgs e)
        {
            respaldo respaldo = new respaldo();
            respaldo.AGREGAR_RESPALDO();
            txtagregar_respaldo.Text = respaldo.direccion_agregar_respaldo;
        }
    }
}

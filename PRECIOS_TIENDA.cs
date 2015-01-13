using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using ZONE_FITNESS_3._0_FINAL.CLASES;
using System.Data.OleDb;
using ADODB;


namespace ZONE_FITNESS_3._0_FINAL
{
    public partial class PRECIOS_TIENDA : Form
    {

        string DEL_DATA = "";
        

        public PRECIOS_TIENDA()
        {
            InitializeComponent();
        }

        private void PRECIOS_TIENDA_Load(object sender, EventArgs e)
        {
            this.Text = "AGREGAR PRECIOS TIENDA";
            conectar_.PRECIOS_TIENDA = new Recordset();
            actualizar();

        }

        private void LINKCLOSE_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Close();
        }

        private void actualizar()
        {
            try
            {
                int n = grilla_tienda.RowCount;
                for (int i = 1; i < n - 1; i++)
                {
                    //este codigo remueve fila por fila 
                    grilla_tienda.Rows.RemoveAt(i);
                }
            }
            catch { }

            if (conectar_.mysqlconection.State == 0) { conectar_.mysqlconection.Open(); }
            else
            {
                conectar_.mysqlconection.Close();
                conectar_.mysqlconection.Open();
            }
            
            conectar_.mysqladapter = new OleDbDataAdapter("SELECT * FROM TIENDA_NOMBRES" , conectar_.mysqlconection);//conecta el adapter con la sentencia sql
            conectar_.ds_oledb = new DataTable();//tabla es = nueva tabla
            conectar_.mysqladapter.Fill(conectar_.ds_oledb);//el adaptador con toda la base de sql se le llenara en la tabla

            grilla_tienda.DataSource = conectar_.ds_oledb;

            grilla_tienda.Columns[0].Visible = false;

        }

        private void LINK_DEL_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            string SQL_PRECIOS_DEL;

            try
            {
                SQL_PRECIOS_DEL = "DELETE FROM TIENDA_NOMBRES WHERE NOMBRE='" + DEL_DATA + "'";
                conectar_.PRECIOS_TIENDA.Open(SQL_PRECIOS_DEL, conectar_.CN, CursorTypeEnum.adOpenDynamic, LockTypeEnum.adLockOptimistic);
                MessageBox.Show("DATO ELIMINADO CON EXITO.", "EXITO");
                actualizar();
                conectar_.PRECIOS_TIENDA.Close();
            }
            catch { }
        }

        private void grilla_tienda_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int i = this.grilla_tienda.CurrentRow.Index;
            DEL_DATA = Convert.ToString(grilla_tienda[1, i].Value);
        }

        private void LINK_ADD_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            if (TXTNOMBRE.Text == "")
            {
                MessageBox.Show("no existe un dato valido");
                return;
            }

            try
            {
                conectar_.PRECIOS_TIENDA.Open("SELECT * FROM TIENDA_NOMBRES", conectar_.CN, CursorTypeEnum.adOpenDynamic, LockTypeEnum.adLockOptimistic);
                conectar_.PRECIOS_TIENDA.AddNew();
                {
                    conectar_.PRECIOS_TIENDA.Fields[1].Value = TXTNOMBRE.Text;
                }
                conectar_.PRECIOS_TIENDA.Update();
                MessageBox.Show("DATOS AGREGADOS CON EXITO.", "EXITO");
                actualizar();
                conectar_.PRECIOS_TIENDA.Close();
            }
            catch
            {
                MessageBox.Show("DEBE LLENAR LA CASILLA", "ADVERTENCIA");
            }
        }
    }
}

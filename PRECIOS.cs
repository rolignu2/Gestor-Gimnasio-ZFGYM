using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using ZONE_FITNESS_3._0_FINAL.CLASES;
using ADODB;


namespace ZONE_FITNESS_3._0_FINAL
{
    public partial class PRECIOS : Form
    {
        public string SQL_PRECIOS_DEL = "";

        public PRECIOS()
        {
            InitializeComponent();
        }

        private void PRECIOS_Load(object sender, EventArgs e)
        {
            this.Opacity = 0.92;
            this.Text = "EDITOR DE PRECIOS";
            //
            actualizar_();
            //
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtcosto.Text == "" || txtprecio.Text == "")
            {
                MessageBox.Show("DEBE LLENAR LAS DOS CASILLAS, PRECIO Y COSTO", "ADVERTENCIA");
                return;
            }

            try
            {
                conectar_.PRECIOS_.AddNew();
                {
                    conectar_.PRECIOS_.Fields[1].Value = txtprecio.Text;
                    conectar_.PRECIOS_.Fields[2].Value = txtcosto.Text;
                }
                conectar_.PRECIOS_.Update();
                MessageBox.Show("DATOS AGREGADOS CON EXITO.", "EXITO");
                actualizar_();
            }
            catch {
                MessageBox.Show("DEBE LLENAR LAS DOS CASILLAS, PRECIO Y COSTO", "ADVERTENCIA");
            }
        }

        private void actualizar_()
        {
            listaprecios.Items.Clear();

            conectar_.PRECIOS_ = new Recordset();
            conectar_.PRECIOS_.Open("SELECT * FROM PRECIOS", conectar_.CN, CursorTypeEnum.adOpenDynamic, LockTypeEnum.adLockOptimistic);

            while (!(conectar_.PRECIOS_.EOF))
            {
                listaprecios.Items.Add(Convert.ToString(conectar_.PRECIOS_.Fields[1].Value) + "| -------------->$" + Convert.ToString(conectar_.PRECIOS_.Fields[2].Value));
                conectar_.PRECIOS_.MoveNext();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {

                string[] PrecioEliminar = listaprecios.SelectedItem.ToString().Split(new string[] { "|" }, StringSplitOptions.None);
                conectar_.PRECIOS_ = new Recordset();
                SQL_PRECIOS_DEL = "DELETE FROM PRECIOS WHERE precio='" + PrecioEliminar[0] + "'";
                conectar_.PRECIOS_.Open(SQL_PRECIOS_DEL, conectar_.CN, CursorTypeEnum.adOpenDynamic, LockTypeEnum.adLockOptimistic);
                MessageBox.Show("DATO ELIMINADO CON EXITO.", "EXITO");
                actualizar_();
            }
            catch { }
        }

        private void LINKCLOSED_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Close();
        }

        private void CMD_PRECIOS_TIENDA_Click(object sender, EventArgs e)
        {
            PRECIOS_TIENDA tienda = new PRECIOS_TIENDA();
            tienda.Show();
        }
    }
}

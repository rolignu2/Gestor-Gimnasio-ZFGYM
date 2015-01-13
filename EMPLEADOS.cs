using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

//using System.Data;
using ADODB;
using System.Data.OleDb;
using ZONE_FITNESS_3._0_FINAL.CLASES;



namespace ZONE_FITNESS_3._0_FINAL
{
    public partial class EMPLEADOS : Form
    {
        public static string SQL_EMPLEADO = "SELECT * FROM EMPLEADOS";
        private string SQL_EDITAR_EMPLEADO = "";
        private string SQL_EIMINAR = string.Empty;
        private int sentinela = 0;

        public EMPLEADOS()
        {
            InitializeComponent();
        }

        private void EMPLEADOS_Load(object sender, EventArgs e)
        {
            this.Text = "CONTROL DE EMPLEADOS 1.0";
            sentinela = 1;
            GRILLA_EMPLEADOS();
        }

        public void GRILLA_EMPLEADOS()
        {
            try
            {
                int n = GRILLA_EMPLEADO.RowCount;
                for (int i = 1; i < n - 1; i++)
                {
                    //este codigo remueve fila por fila 
                    GRILLA_EMPLEADO.Rows.RemoveAt(i);
                }
            }
            catch { }

            if (conectar_.mysqlconection.State == 0) { conectar_.mysqlconection.Open(); }
            else
            {
                conectar_.mysqlconection.Close();
                conectar_.mysqlconection.Open();
            }

            conectar_.mysqladapter = new OleDbDataAdapter(SQL_EMPLEADO, conectar_.mysqlconection);//conecta el adapter con la sentencia sql
            conectar_.ds_oledb = new DataTable();//tabla es = nueva tabla
            conectar_.mysqladapter.Fill(conectar_.ds_oledb);//el adaptador con toda la base de sql se le llenara en la tabla

            GRILLA_EMPLEADO.DataSource = conectar_.ds_oledb;//el DataTable table se pasara a la grilla
            GRILLA_EMPLEADO.Columns[0].Visible = false;

            conectar_.mysqlconection.Close();

        }

        private void cmd_add_Click(object sender, EventArgs e)
        {

            if (sentinela == 1)
            {
                try
                {
                    conectar_.RS_EMPLEADOS = new Recordset();
                    conectar_.RS_EMPLEADOS.Open(SQL_EMPLEADO, conectar_.CN, CursorTypeEnum.adOpenDynamic, LockTypeEnum.adLockOptimistic);

                    conectar_.RS_EMPLEADOS.AddNew();
                    {
                        conectar_.RS_EMPLEADOS.Fields[1].Value = textBox1.Text;
                        conectar_.RS_EMPLEADOS.Fields[2].Value = textBox2.Text;
                        conectar_.RS_EMPLEADOS.Fields[3].Value = textBox3.Text;
                        conectar_.RS_EMPLEADOS.Fields[4].Value = textBox4.Text;
                    }

                    conectar_.RS_EMPLEADOS.Update();
                    MessageBox.Show("EMPLEADO AGREGADO CON EXITO ");
                    GRILLA_EMPLEADOS();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("ERROR EN GUARDAR EMPLEADO " + ex.Message);
                }
            }
            else if (sentinela == 2)
            {
                conectar_.RS_EMPLEADOS = new Recordset();
                int i = this.GRILLA_EMPLEADO.CurrentRow.Index;
                SQL_EDITAR_EMPLEADO = "UPDATE  EMPLEADOS SET Nombre='" + textBox1.Text +
                "', Apellido='" + textBox2.Text + "',Seccion ='" + textBox3.Text + "',Salario='" + textBox4.Text + "'WHERE Nombre='" + Convert.ToString(GRILLA_EMPLEADO[1, i].Value) + "'AND Apellido='" + Convert.ToString(GRILLA_EMPLEADO[2, i].Value) + "'";
                conectar_.RS_EMPLEADOS.Open(SQL_EDITAR_EMPLEADO, conectar_.CN, CursorTypeEnum.adOpenDynamic, LockTypeEnum.adLockOptimistic);
                MessageBox.Show("EMPLEADO EDITADO CON EXITO ", "EXITO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                GRILLA_EMPLEADOS();

                cmd_edit.Enabled = true;
                sentinela = 1;
            }

        }

        private void cmd_edit_Click(object sender, EventArgs e)
        {
            try
            {
                int i = this.GRILLA_EMPLEADO.CurrentRow.Index;

                textBox1.Text = Convert.ToString(GRILLA_EMPLEADO[1, i].Value);
                textBox2.Text = Convert.ToString(GRILLA_EMPLEADO[2, i].Value);
                textBox3.Text = Convert.ToString(GRILLA_EMPLEADO[3, i].Value);
                textBox4.Text = Convert.ToString(GRILLA_EMPLEADO[4, i].Value);


                sentinela = 2;

                cmd_edit.Enabled = false;
            }
            catch { }
        }

        private void GRILLA_EMPLEADO_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (sentinela == 2)
            {
                try
                {
                    int i = this.GRILLA_EMPLEADO.CurrentRow.Index;
                    textBox1.Text = Convert.ToString(GRILLA_EMPLEADO[1, i].Value);
                    textBox2.Text = Convert.ToString(GRILLA_EMPLEADO[2, i].Value);
                    textBox3.Text = Convert.ToString(GRILLA_EMPLEADO[3, i].Value);
                    textBox4.Text = Convert.ToString(GRILLA_EMPLEADO[4, i].Value);
                }
                catch { }
            }
        }

        private void GRILLA_EMPLEADO_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (sentinela == 2)
            {
                try
                {
                    int i = this.GRILLA_EMPLEADO.CurrentRow.Index;
                    textBox1.Text = Convert.ToString(GRILLA_EMPLEADO[1, i].Value);
                    textBox2.Text = Convert.ToString(GRILLA_EMPLEADO[2, i].Value);
                    textBox3.Text = Convert.ToString(GRILLA_EMPLEADO[3, i].Value);
                    textBox4.Text = Convert.ToString(GRILLA_EMPLEADO[4, i].Value);
                }
                catch { }
            }
        }

        private void cmd_del_Click(object sender, EventArgs e)
        {
            DialogResult re = MessageBox.Show("¿ESTA SEGURO QUE DESEA ELIMINAR EL REGISTRO DEL EMPLEADO?", "ELIMINAR", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (re == DialogResult.Yes)
            {
                conectar_.RS_EMPLEADOS = new Recordset();

                try
                {
                    int i = this.GRILLA_EMPLEADO.CurrentRow.Index;
                    SQL_EIMINAR = "DELETE FROM EMPLEADOS WHERE Nombre='" + Convert.ToString(GRILLA_EMPLEADO[1, i].Value) +
                        "'AND Apellido='" + Convert.ToString(GRILLA_EMPLEADO[2, i].Value) +"'";

                    conectar_.RS_EMPLEADOS.Open(SQL_EIMINAR, conectar_.CN, CursorTypeEnum.adOpenDynamic, LockTypeEnum.adLockOptimistic);
                    MessageBox.Show("¿EMPLEADO ELIMINADO CON EXITO ?", "ELIMINAR", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    GRILLA_EMPLEADOS();
                }
                catch { }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

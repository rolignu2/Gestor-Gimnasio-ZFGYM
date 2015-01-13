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
    public partial class RUTINA_ADD : Form
    {

        public RUTINA_ADD()
        {
            InitializeComponent();
        }

        private void RUTINA_ADD_Load(object sender, EventArgs e)
        {
            this.Opacity = 0.92;
            this.Text = "AGREGAR RUTINA";
            this.BackColor = Color.DarkBlue;
            //
            combo_rep.Items.Clear();
            combo_set.Items.Clear();
            //
            for (int i = 1; i <= 50; i++)
            {
                combo_set.Items.Add(i);
                combo_rep.Items.Add(i);
            }
        }

        private void cmd_add_Click(object sender, EventArgs e)
        {

            if (combo_rep.Text == "" || combo_set.Text == "")
            {
                MessageBox.Show("NO SE HA GENERADO LOS SET Y/O LAS REPETICIONES", "STOP", MessageBoxButtons.OK , MessageBoxIcon.Stop);
                return;
            }

            try
            {
                conectar_.SOCIO_RUTINA = new Recordset();
                conectar_.SOCIO_RUTINA.Open("SELECT * FROM SOCIO_RUTINA", conectar_.CN, CursorTypeEnum.adOpenDynamic, LockTypeEnum.adLockOptimistic);

                conectar_.SOCIO_RUTINA.AddNew();
                {
                    conectar_.SOCIO_RUTINA.Fields[1].Value = USER_RUTINAS_IZQUIERDA.NOMBRE_;
                    conectar_.SOCIO_RUTINA.Fields[2].Value = USER_RUTINAS_IZQUIERDA.APELLIDO_;
                    conectar_.SOCIO_RUTINA.Fields[3].Value = USER_RUTINA_ARRIBA.VARIABLE_DIA;
                    conectar_.SOCIO_RUTINA.Fields[4].Value = USER_RUTINA_CENTRO.nombre_add;               
                    conectar_.SOCIO_RUTINA.Fields[5].Value = combo_set.Text;
                    conectar_.SOCIO_RUTINA.Fields[6].Value = combo_rep.Text;

                    conectar_.RUTINAS_.MoveFirst();

                    while (!(conectar_.RUTINAS_.EOF))
                    {
                        if (conectar_.RUTINAS_.Fields[1].Value == USER_RUTINA_CENTRO.nombre_add)
                        {
                            conectar_.SOCIO_RUTINA.Fields[7].Value = conectar_.RUTINAS_.Fields[3].Value;
                            break;
                        }
                        conectar_.RUTINAS_.MoveNext();
                    }
                }

                conectar_.SOCIO_RUTINA.Update();
                this.Close();
            }
            catch
            {
            }
        }
    }
}

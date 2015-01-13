using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using ZONE_FITNESS_3._0_FINAL.CLASES;

namespace ZONE_FITNESS_3._0_FINAL
{
    public partial class USER_RUTINA_ARRIBA : UserControl
    {

        public static string VARIABLE_DIA = "";//variable statica independiente

        public USER_RUTINA_ARRIBA()
        {
            InitializeComponent();
        }

        private void USER_RUTINA_ARRIBA_Load(object sender, EventArgs e)
        {
            lbltitulo.Text = rutina_titulos_arriba.TITULO;//titulo en la clase titulo
        }

        private void COMBO_DIA()//aca agrega el los dias de la semana al combobox
        {
            combo_dia.Items.Clear();
            combo_dia.Items.Add("LUNES");
            combo_dia.Items.Add("MARTES");
            combo_dia.Items.Add("MIERCOLES");
            combo_dia.Items.Add("JUEVES");
            combo_dia.Items.Add("VIERNES");
            combo_dia.Items.Add("SABADO");
            combo_dia.Items.Add("DOMINGO");
        }

        private void combo_dia_SelectedIndexChanged(object sender, EventArgs e)
        {
            VARIABLE_DIA = combo_dia.Text;//la variable independiente dia es = dia.txt
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            COMBO_DIA();//llenado del combobox
            timer1.Enabled = false;
        }
    }
}

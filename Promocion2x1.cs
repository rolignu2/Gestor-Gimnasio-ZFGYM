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
    public partial class Promocion2x1 : Form
    {
        public Promocion2x1()
        {
            InitializeComponent();
        }

        private void Promocion2x1_Load(object sender, EventArgs e)
        {

            if (ZONE_FITNESS_3._0_FINAL.PRINCIPAL.Parametros2x1.Count >= 1)
            {
                try
                {
                    txtnombre.Text = ZONE_FITNESS_3._0_FINAL.PRINCIPAL.Parametros2x1[0];
                    txtapellido.Text = ZONE_FITNESS_3._0_FINAL.PRINCIPAL.Parametros2x1[1];
                }
                catch { }
            }

        }

        private void cmdcerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmdaceptar_Click(object sender, EventArgs e)
        {
            if (ZONE_FITNESS_3._0_FINAL.PRINCIPAL.Parametros2x1.Count >= 1)
                ZONE_FITNESS_3._0_FINAL.PRINCIPAL.Parametros2x1.Clear();

            ZONE_FITNESS_3._0_FINAL.PRINCIPAL.Parametros2x1.Add(txtnombre.Text);
            ZONE_FITNESS_3._0_FINAL.PRINCIPAL.Parametros2x1.Add(txtapellido.Text);

            this.Close();
        }
    }
}

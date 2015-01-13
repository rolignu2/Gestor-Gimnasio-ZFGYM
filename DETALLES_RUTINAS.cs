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

    public partial class DETALLES_RUTINAS : Form
    {
        private int conteo = 0;

        public DETALLES_RUTINAS()
        {
            InitializeComponent();
        }

        private void DETALLES_RUTINAS_Load(object sender, EventArgs e)
        {
            txtdetalles.ReadOnly = true;
            try
            {
                conectar_.RUTINAS_.MoveFirst();
                conteo = conectar_.RUTINAS_.RecordCount;
                //
                while (!(conectar_.RUTINAS_.EOF))
                {
                    if (USER_RUTINA_CENTRO.nombre_detalle == conectar_.RUTINAS_.Fields[1].Value)
                    {
                        lbldetalle.Text = conectar_.RUTINAS_.Fields[1].Value;
                        txtdetalles.Text = conectar_.RUTINAS_.Fields[4].Value;
                        picdetalles.Image = System.Drawing.Image.FromFile(System.IO.Directory.GetCurrentDirectory() + @"\IMG_RUTINAS\" + conectar_.RUTINAS_.Fields[3].Value);
                    }

                    conectar_.RUTINAS_.MoveNext();
                }
            }
            catch { }
        }

        private void lbldetalle_Click(object sender, EventArgs e)
        {

        }
    }
}

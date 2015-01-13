using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using ZONE_FITNESS_3._0_FINAL.CLASES;

namespace ZONE_FITNESS_3._0_FINAL
{

    public partial class CONTRASEÑA_LOGIN : Form
    {

        public int x, y;

        public CONTRASEÑA_LOGIN()
        {
            InitializeComponent();
        }

        private void CONTRASEÑA_LOGIN_Load(object sender, EventArgs e)
        {

            this.Text = "SEGURIDAD";
            this.Opacity = 0.92;
            this.BackColor = Color.DarkBlue;
           
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            contraseña_segura.seguridad_caracter(LOGIN.txt_login_contra_segura, progreso_, link);

            //
            x = LOGIN.x_ + 277;
            y = LOGIN.y_ - 2;
            this.Location = new Point(x, y);

        }
    }
}

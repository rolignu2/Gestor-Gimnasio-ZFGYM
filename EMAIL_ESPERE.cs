using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ZONE_FITNESS_3._0_FINAL
{
    public partial class EMAIL_ESPERE : Form
    {
        public EMAIL_ESPERE()
        {
            InitializeComponent();
        }

        private void EMAIL_ESPERE_Load(object sender, EventArgs e)
        {
            this.BackColor = Color.DarkBlue;
            this.Opacity = 0.82;
        }

        private void EMAIL_ESPERE_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Close();
        }

        private void EMAIL_ESPERE_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Close();
        }


    }
}

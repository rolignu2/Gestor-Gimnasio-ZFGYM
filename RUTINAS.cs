using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace ZONE_FITNESS_3._0_FINAL
{
    public partial class RUTINAS : Form
    {
        USER_RUTINAS_IZQUIERDA RUTINA_IZQUIERDA = new USER_RUTINAS_IZQUIERDA();
        USER_RUTINA_CENTRO RUTINA_CENTRO = new USER_RUTINA_CENTRO ();
        USER_RUTINA_ARRIBA RUTINA_ARRIBA = new USER_RUTINA_ARRIBA();

        public RUTINAS()
        {
            InitializeComponent();
        }

        private void RUTINAS_Load(object sender, EventArgs e)
        {

            /*en este formulario se haran las conexiones de los controles de usuario o user control 
             * 
             * cada user control esta declarado en este formulario el de la izquierda , arriba y central 
             * 
             * USER_RUTINAS_IZQUIERDA RUTINA_IZQUIERDA = new USER_RUTINAS_IZQUIERDA();
             * USER_RUTINA_CENTRO RUTINA_CENTRO = new USER_RUTINA_CENTRO ();
               USER_RUTINA_ARRIBA RUTINA_ARRIBA = new USER_RUTINA_ARRIBA();
             * 
             */

            this.Opacity = 0.92;
            //this.BackColor = Color.DarkBlue ;
            //
            //
            panelizquierdo.Controls.Add(RUTINA_IZQUIERDA);
            panel_arriba.Controls.Add(RUTINA_ARRIBA);
            panel_Centro.Controls.Add(RUTINA_CENTRO);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (USER_RUTINA_CENTRO.CERRAR_ == 1)
            {
                this.Close();
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            panelizquierdo.Controls.Add(RUTINA_IZQUIERDA);
            panel_arriba.Controls.Add(RUTINA_ARRIBA);
            panel_Centro.Controls.Add(RUTINA_CENTRO);
            timer2.Enabled = false;
        }

    }
}

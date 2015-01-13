using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Globalization;

using ZONE_FITNESS_3._0_FINAL.CLASES;

namespace ZONE_FITNESS_3._0_FINAL
{
    public partial class REGISTRAR_ : Form
    {
        public REGISTRAR_()
        {
            InitializeComponent();
        }

        private void LINK_CANCELAR_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Close();
        }

        private void REGISTRAR__Load(object sender, EventArgs e)
        {
            txtreg1.MaxLength = 6;
            txtreg2.MaxLength = 6;
            txtreg3.MaxLength = 6;

            version_gestor ver = new version_gestor();

            this.BackColor = Color.DarkBlue;
            this.Text = "REGISTRAR " + ver.Nombre ;

            LINK_CANCELAR.Text = "CANCELAR";

            VERIFICAR_REGISTRO();

            

         
        }

        private void VERIFICAR_REGISTRO()
        {
            string[] temp1 = new string[6];
            string[] temp2 = new string[6];
            string[] temp3 = new string[6];

            string clave = "";
            int cont = 0;


            if (ClaveProd.GET_PRODUCTO() == "NO REGISTRADO")
            {
                link_reg.Text = "SU PRODUCTO NO HA SIDO REGISTRADO...";
                txtreg1.Enabled = true;
                txtreg2.Enabled = true;
                txtreg3.Enabled = true;
                CMDREGISTRAR.Enabled = true;
                LINK_CANCELAR.Text = "CANCELAR";
                LBL_INFO.Text = "EL PROGRAMA ES GRATIS , SOLO TIENES QUE REGISTRARTE EN NUESTRA PAGINA WEB Y TE OTORGAREMOS UNA CLAVE DEL PRODUCTO.";
            }
            else
            {
          
                clave = ClaveProd.GET_PRODUCTO();

                for (int i = 0; i < 18; i++)
                {
                    if (i >= 0 && i < 6)
                    {
                        temp1[cont] = clave[i].ToString();
                        cont++;
                    }
                    else if (i >= 6 && i < 12)
                    {
                        if (i == 6) cont = 0;
                        temp2[cont] = clave[i].ToString();
                        cont++;
                    }
                    else
                    {
                        if (i == 12) cont = 0;
                        temp3[cont] = clave[i].ToString();
                        cont++;
                    }

                }

                txtreg1.Text = string.Join("", temp1);
                txtreg2.Text = string.Join("", temp2);
                txtreg3.Text = string.Join("", temp3);

                link_reg.Text = "GESTOR DE GIMNASIO ACTIVADO ... ";
                LBL_INFO.Text = "GRACIAS POR REGISTRARTE , DISFRUTA LA APLICACION GESTOR DE GIMANSIO ... ";
                txtreg1.Enabled = false;
                txtreg2.Enabled = false;
                txtreg3.Enabled = false;
                CMDREGISTRAR.Enabled = false;
                LINK_CANCELAR.Text = "CERRAR";
            }

        }

        private void CMDREGISTRAR_Click(object sender, EventArgs e)
        {
            string key = txtreg1.Text.ToUpper() + txtreg2.Text.ToUpper() + txtreg3.Text.ToUpper();

            ClaveProd.SET_ENCRIPT_PRODUCTO(key);
            VERIFICAR_REGISTRO();
        }

        private void txtreg1_TextChanged(object sender, EventArgs e)
        {
            if (txtreg1.TextLength == 6)
            {
                txtreg2.Focus();
                CMDREGISTRAR.Enabled = false;
            }
        }

        private void txtreg2_TextChanged(object sender, EventArgs e)
        {
            if (txtreg2.TextLength == 6)
            {
                txtreg3.Focus();
                CMDREGISTRAR.Enabled = false;
            }
        }

        private void txtreg3_TextChanged(object sender, EventArgs e)
        {
            if (txtreg2.TextLength == 6)
            {
                CMDREGISTRAR.Enabled = true;
            }
            else
            {
                CMDREGISTRAR.Enabled = false;
            }
        }

        private void LINKHELP_ONLINE_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("iexplore.exe", Ayuda.direccion_ayuda_online());
        }

        private void LINKCLAVE_ONLINE_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("iexplore.exe", Ayuda.Registro());
        }
   
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Windows.Forms;

namespace ZONE_FITNESS_3._0_FINAL.CLASES
{

    //seguridad de contraseña ---> funcion version prueba
    class contraseña_segura
    {

        //contadores
        private static int a = 20, b = 20, c = 30, d = 30;

        //cuenta los carteres del password
        public static bool contador_caracter(TextBox txt_contra)
        {

            int contador = txt_contra.TextLength;
            if (contador <= 3)
            {
                MessageBox.Show("LA CONTRASEÑA  " + txt_contra.Text + " SU NIVEL DE SEGURIDAD ES MUY BAJO ...\n\nFAVOR AGREGAR UNA CONTRASEÑA CON MAS DE 4 CARACTERES", "INSEGURO!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
            else
            {
                return true;
            }
        }

        //verifica mediante los caracteres contados si el password es seguro o no !
        public static void seguridad_caracter(TextBox txt_contra , ProgressBar progreso__ , LinkLabel link)
        {

            
            int contador_ = txt_contra.TextLength;

            if (contador_ <= 3)
            {
                progreso__.Increment(a);
                link.Text = "INSEGURA";
                a = 0;
            }
            if (contador_ >= 5 && contador_ < 8)
            {
                progreso__.Increment(b);
                link.Text = "POCO SEGURA";
                b = 0;
            }
             if (contador_ >= 8 && contador_ < 9)
            {
                progreso__.Increment(c);
                link.Text = "SEGURA";
                c = 0;
            }
            if (contador_ >= 10)
            {
                progreso__.Increment(d);
                link.Text = "MUY SEGURA";
                d = 0;
            }
        }
       
    }
}

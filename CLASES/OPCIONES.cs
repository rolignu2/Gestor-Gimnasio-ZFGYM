using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Windows.Forms;
using System.IO;

using ZONE_FITNESS_3._0_FINAL.CLASES;

namespace ZONE_FITNESS_3._0_FINAL.CLASES
{

    class OPCIONES
    {
       
       public static int PRIVILEGIOS_ESTATUS;

       //salida del programa 
       public void salir()
        {
            DialogResult diag = MessageBox.Show("¿ DESEA SALIR DEL PROGRAMA ?","SALIR", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (diag == DialogResult.Yes)
            {
                Application.Exit();
                MATAR_PROCESO kill_ = new MATAR_PROCESO();
                kill_.KillProcess();
            }

            else { goto estatus_q; }

        estatus_q:
            Console.WriteLine("se anulo la salida");
            Console.ReadLine();
        }

    }


    //LA CLASE NOMBRE GYM OTORGA EL NOMBRE DADO POR EL USUARIO ... 
    class NOMBRE_GYM
    {

        public static string[] todo_texto = new string[3];

        public static int GetNombreGym()
        {
            int cantidad_ln = 0;
            StreamReader lector = new StreamReader(System.IO.Directory.GetCurrentDirectory() + @"\gym.txt");

            try
            {
                cantidad_ln = lector.ReadToEnd().Length;
                if (cantidad_ln == 0) {
                    lector.Close();
                    return 0; }
                else {
                    lector.Close();
                    return 1; 
                }
            
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return 2;
            }
        }

        public static void OutNombreGym()
        {
            StreamReader lector = new StreamReader(System.IO.Directory.GetCurrentDirectory() + @"\gym.txt");
            int i = 0;
            for (i = 0; i < 3; i++)
                {            
                        todo_texto[i] = lector.ReadLine();
                        lector.Peek();
                }

                lector.Close();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ZONE_FITNESS_3._0_FINAL.CLASES
{
    class respaldo
    {

        /*CLASE DONDE RESPALDAMOS LA BASE DE DATOS EN CASO DE ERRORES EN EL SISTEMA*/

        public static string direccion_respaldo = "";
        public static string direccion_agregar_respaldo = "";

        public int RESPALDAR_BD()
        {

            try
            {

                SaveFileDialog abrir_directorio = new SaveFileDialog();
                string[] seleccionar_ruta = new string[5000];
                object rutas;
                abrir_directorio.ValidateNames = true;
                abrir_directorio.Title = "RESPALDAR";
                abrir_directorio.FileName = "ZFGYM_BD.accdb";
                abrir_directorio.ShowDialog();

                if (abrir_directorio.FileName != null)
                { 
                    seleccionar_ruta = abrir_directorio.FileNames;
                    foreach (object rutas_loopVariable in seleccionar_ruta)
                    {
                        rutas = rutas_loopVariable;//variable objeto dandole un ciclo booleano al object
                        System.IO.FileInfo fileinfo = new System.IO.FileInfo(Convert.ToString(rutas));//como es un objeto necesitamos hacerlo string o cadena
                        direccion_respaldo = Convert.ToString(fileinfo);
                    }             
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return 0;
        }

        public int AGREGAR_RESPALDO()
        {
            try
            {
                OpenFileDialog abrir_directorio = new OpenFileDialog();
                string[] seleccionar_ruta = new string[5000];
                object rutas;
                abrir_directorio.ValidateNames = true;
                abrir_directorio.Title = "RESPALDAR";
                abrir_directorio.Filter = "(*.accdb)|";
                abrir_directorio.ShowDialog();

                if (abrir_directorio.ShowDialog() == DialogResult.OK)
                {
                    seleccionar_ruta = abrir_directorio.FileNames;
                    foreach (object rutas_loopVariable in seleccionar_ruta)
                    {
                        rutas = rutas_loopVariable;//variable objeto dandole un ciclo booleano al object
                        System.IO.FileInfo fileinfo = new System.IO.FileInfo(Convert.ToString(rutas));//como es un objeto necesitamos hacerlo string o cadena
                        direccion_agregar_respaldo  = Convert.ToString(fileinfo);

                    }
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return 0;
        }

    }
}

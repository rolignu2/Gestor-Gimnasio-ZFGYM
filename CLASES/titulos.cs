using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Windows.Forms;
using System.Reflection.Cache;


namespace ZONE_FITNESS_3._0_FINAL.CLASES
{
    //CLASE ABSTRACTA UTILIZADA COMO PLANILLA
    abstract class titulos
    {

        titulos() { }//CONSTRUCTOR
        ~titulos() { }//DESTRUCTOR

        public abstract void obtener_titulo();//ANSTRACTA
    }

    /*ACA AGREGARAN LOS TITULOS POR SI DESEAN CAMBIAR EL NOMBRE AL PROGRAMA ... MUCHO MAS SENCILLO
     * ESTAS CLASES ESTAN VINCULADAS CON LA CLASE NOMBRE_GYM EN EL MODULO OPCIONES */

    class version_gestor
    {
        
        public List<string> GetVersion()
        {
            string location =  System.Reflection.Assembly.GetExecutingAssembly().Location;
            string fileversion = System.Diagnostics.FileVersionInfo.GetVersionInfo(location).FileVersion;
            string name = System.Diagnostics.FileVersionInfo.GetVersionInfo(location).FileName;
            string description = System.Diagnostics.FileVersionInfo.GetVersionInfo(location).FileDescription;
            string build = Convert.ToString(System.Diagnostics.FileVersionInfo.GetVersionInfo(location).FileBuildPart);
            List<string> data = new List<string>();
            data.Add(location);
            data.Add(fileversion);
            data.Add(name);
            data.Add(description);
            data.Add(build);
            return data;
        }

        ~version_gestor() {}

        public string Nombre = "GESTOR DE GIMNASIO";
        public string Copyright = "2011";
        public string Compañia = "Wildsoft 2011";
    }

    class titulos_principal
    {

          public static string TITULO_PRINCIPAL = NOMBRE_GYM.todo_texto[0].ToString();
          public static string TAB_3 = "TIENDA " + NOMBRE_GYM.todo_texto[0].ToString();
          public static string TAB_4 = "OPCIONES " + NOMBRE_GYM.todo_texto[0].ToString();

          public static  string TAB_1 = "SOCIOS ";
          public static string TAB_2 = "SOCIOS EXPIRADOS ";
          public static  string TAB_5 = "IMC";              
    }

    //la rutina existe un titulo donde lo podemos modificar 
    class rutina_titulos_arriba//TITULO DE LA RUTINA 
    {
        public static string TITULO =  NOMBRE_GYM.todo_texto[0].ToString() + " RUTINAS ";
    }

    //quien envia el correo ... sujeto = nombre del gimnasio 
    class correo_titulos {//SUJETO DEL CORREO DONDE ENVIARA CON EL NOMBRE DEL GIMNASIO
        public static string subject__ = NOMBRE_GYM.todo_texto[0].ToString();
    }


}

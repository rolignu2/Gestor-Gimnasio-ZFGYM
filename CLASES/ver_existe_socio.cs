using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Windows.Forms;

namespace ZONE_FITNESS_3._0_FINAL.CLASES
{
    abstract class ver_existe_socio
    {
        public abstract bool GET_EXISTE();
    }

    class EXISTE_REGISTRADO
    {
        public static bool Existe(DataGridView GRILLA , TextBox txt_nombre , TextBox txt_apellido)
        {
            int cont = GRILLA.RowCount;

            for (int i = 0; i < cont; i++)
            {
                if (GRILLA[1, i].Value.ToString() == txt_nombre.Text && GRILLA[1, i].Value.ToString() == txt_apellido.Text)
                {
                    return true;
                }
            }

            return false;
        }

        public static bool ExisteSocio(string Nombre1, string Apellido1 , string Nombre2 , string Apellido2 )
        {

            int i,j;

            List<char> Comparacion1 = new List<char>();
            List<char> Comparacion2 = new List<char>();

            //primero verificamos si la cantidad de letras son iguales en las cadenas
            if (Nombre1.Count() != Nombre2.Count() || Apellido1.Count() != Apellido2.Count()) return false;

            //comparacion de los nombres ....
            for (i = 0; i < Nombre1.Count(); i++)
            {

                Comparacion1.Add(char.ToUpper(Nombre1[i]));
                Comparacion2.Add(char.ToUpper(Nombre2[i]));
            }
            for (j = 0; j < Comparacion1.Count; j++)
            {
                if (Comparacion1[j] != Comparacion2[j])
                {
                    return false;
                }
            }

            //comparacion de los apellidos

            Comparacion1 = new List<char>();
            Comparacion2 = new List<char>();

            for (i = 0; i < Apellido1.Count(); i++)
            {

                Comparacion1.Add(char.ToUpper(Apellido1[i]));
                Comparacion2.Add(char.ToUpper(Apellido2[i]));
            }
            for (j = 0; j < Comparacion1.Count; j++)
            {
                if (Comparacion1[j] != Comparacion2[j])
                {
                    return false;
                }
            }
            //si al final todos los caracteres son iguales entonces existe este socio .... enviamos cierto al valor booleano
            return true;
        }

    }

}

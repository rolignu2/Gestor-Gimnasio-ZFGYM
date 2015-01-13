using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ADODB;
using System.Data.OleDb;
using ZONE_FITNESS_3._0_FINAL.CLASES;

namespace ZONE_FITNESS_3._0_FINAL.CLASES
{
    class inicio_comprobar
    {
        //comprueba si existe un administrador al inicio del programa sino existe creara uno sin necesidad de permisos
        public int ExisteAdmin()
        {
            int contador = 0;

            try
            {
                conectar_.RS_LOGIN = new Recordset();
                conectar_.RS_LOGIN.Open("SELECT * FROM LOGIN WHERE prioridad ='ADMINISTRADOR'", conectar_.CN, CursorTypeEnum.adOpenDynamic, LockTypeEnum.adLockOptimistic);

                while (!(conectar_.RS_LOGIN.EOF))
                {
                    if (!(conectar_.RS_LOGIN.Fields[1].Value == "admin" && conectar_.RS_LOGIN.Fields[2].Value == "root"))
                    {
                        contador++;
                    }
                    conectar_.RS_LOGIN.MoveNext();
                }
            }
            catch {
            }
            return contador;
        }

    }
}

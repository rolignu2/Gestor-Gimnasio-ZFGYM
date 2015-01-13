using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Windows.Forms;

namespace GestorGimnasio.CLASES
{
   static class BuferGrid
    {

       public static void DoubleBuffered(this DataGridView dgv, bool setting)
       {
           try
           {
               Type dgvType = dgv.GetType();
               PropertyInfo pi = dgvType.GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
               pi.SetValue(dgv, setting, null);
           }
           catch { }
       }


    }
}

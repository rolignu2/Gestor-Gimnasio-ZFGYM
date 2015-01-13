using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Threading;

namespace ZONE_FITNESS_3._0_FINAL.CLASES
{
    class MATAR_PROCESO
    {

        private const string NOMBRE = "GESTOR_GIMNASIO_3.1.vshost";
        System.Diagnostics.Process[] p = System.Diagnostics.Process.GetProcesses();
        
        public void KillProcess()
        {

            for (int i = 0; i < p.GetLength(0); i++)
            {
                if (p[i].ProcessName == NOMBRE)
                {
                    p[i].Kill();
                    i = p.GetLength(0);
                    Thread.Sleep(1000);
                }
            }
        }

    }
}

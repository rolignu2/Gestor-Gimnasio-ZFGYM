using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ADODB;
using System.Windows.Forms;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Threading;


namespace ZONE_FITNESS_3._0_FINAL.CLASES
{
    class conectar_
    {


        #region CONEXIONES METODOS ESTATICOS
        public static Connection CN;       
        public static Recordset RS_LOGIN;
        public static Recordset RS_SOCIO;
        public static Recordset RS_SOCIO_AGREGAR;
        public static Recordset RS_SOCIO_EDITAR;
        public static Recordset RS_CONTADOR;
        public static Recordset RS_EMPLEADOS;
        public static Recordset RS_EXISTE_SOCIO;
        public static Recordset RS_EXPIRADOS_;
        public static Recordset RS_CORREO_;
        public static Recordset RS_TIENDA;
        public static Recordset RS_PAGO;
        public static Recordset PRECIOS_;
        public static Recordset RUTINAS_;
        public static Recordset SOCIO_RUTINA;
        public static Recordset RUTINA_HTML;
        public static Recordset PRECIOS_TIENDA;

        public static OleDbConnection mysqlconection;
        public static OleDbDataAdapter mysqladapter;
        public static DataTable ds_oledb;
        public static OleDbDataAdapter mysql_data_adapter_exp;
        public static DataTable ds_oledb_exp;
        public static OleDbDataAdapter mysql_data_adapter_rutina;
        public static DataTable ds_oledb_rutina;
        #endregion


        string MyConString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + System.IO.Directory.GetCurrentDirectory() + @"\ZFGYM_BD.accdb";
        string MyOLEDB = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + System.IO.Directory.GetCurrentDirectory() + @"\ZFGYM_BD.accdb";

        private System.Threading.Tasks.Task TareaAdo;
        private System.Threading.Tasks.Task Tareaoled;

        public conectar_()
        {
           /* if (!File.Exists(Directory.GetCurrentDirectory() + @"\Proveedor.txt")) return;
            else
            {
                StreamReader ReadProvider = new StreamReader(Directory.GetCurrentDirectory() + @"\Proveedor.txt");
                var directorio = ReadProvider.ReadLine();
                MyConString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + directorio;
                MyOLEDB = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + directorio;
            }*/
        }

        public void GetAdodbConection()
        {
            TareaAdo = System.Threading.Tasks.Task.Factory.StartNew(conectar_adodb);
            return;
        }

        protected void conectar_adodb()
        {
            CN = new Connection();

            try
            {
                CN.ConnectionString = MyConString;
                CN.CommandTimeout = 300;
                CN.CursorLocation = CursorLocationEnum.adUseClient;
                CN.Open();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        public void GetoledbConection()
        {
            Tareaoled = System.Threading.Tasks.Task.Factory.StartNew(conectar_oledb);
            return ;
        }

        public bool IsaliveConect()
        {
            try
            {
                if (TareaAdo.IsCompleted == true && Tareaoled.IsCompleted == true)
                    return true;
                else
                    return false;
            }
            catch { return true; }
        }

        protected void conectar_oledb()
        {
            try
            {
                mysqlconection = new OleDbConnection() ;

                if (mysqlconection.State == 0)
                {
                    mysqlconection.ConnectionString = MyOLEDB;
                    mysqlconection.Open();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

using System.Data.OleDb;
using System.Threading;


namespace ZONE_FITNESS_3._0_FINAL.CLASES
{
    class Buscador
    {

        //atributos del motor de busqueda


        #region Motores Obsoletos


        #region Atributos
        private static string SQL_ = "";

        private const string ERR_00 = "ERROR CONTADOR GRILLA";
        private const string ERR_02 = "ERROR AL VACIAR FILAS ";

        private const int ERR_001 = 1;
        private static int NO_ENCONTRADO = 0;

        private static int longitud = 0;


        private static string PROVEDOR = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=";
        private static string DIRECCION = System.IO.Directory.GetCurrentDirectory();
       

        private static OleDbConnection CONEXION;
        private static OleDbDataAdapter DATA_CONEXION;
        private static DataTable TABLA_OBJETOS;

        #endregion

        #region motores de busqueda por base de datos
        //motor de busqueda con parametro otorgados al formulario ... 
        [Obsolete("funcion obsoleta", true)]
        public static int CONEXION_SQL(string BASE_DATOS)
        {
            try
            {
                CONEXION = new OleDbConnection(PROVEDOR + DIRECCION + @"\" + BASE_DATOS);
                if (CONEXION.State == 0){CONEXION.Open();}
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return ERR_001;
            }

            return 0;
        }
        [Obsolete("funcion obsoleta", true)]
        public static int  Count_Sql()
        {
            return longitud;
        }
        [Obsolete("funcion obsoleta", true)]
        public static void Buscar_Sql(DataGridView  GRILLA_ , string TABLA ,  int COLUMNA_INVISIBLE  , ToolStripTextBox txt_tool , params string[] BUSCADOR_NOMBRE_TABLA )
        {

            int contador = BUSCADOR_NOMBRE_TABLA.Length;


 ERROR_:
            for (int j = 0; j < contador; j++)
            {

                if (BUSCADOR_NOMBRE_TABLA[j] == null) { break; }

                SQL_ = "SELECT * FROM " + TABLA + " WHERE " + BUSCADOR_NOMBRE_TABLA[j] + "='" + txt_tool.Text.ToString() + "'";//SQL_ PARAMETROS DEL BUSCADOR

                try
                {
                   // GRILLA_ = new DataGridView();

                    DATA_CONEXION = new OleDbDataAdapter(SQL_, CONEXION);
                    TABLA_OBJETOS = new DataTable();
                    DATA_CONEXION.Fill(TABLA_OBJETOS);
                    GRILLA_.DataSource = TABLA_OBJETOS;

                    longitud = GRILLA_.Rows.Count - 1;

                    if (GRILLA_.Rows.Count >= 2) { break; }

                }
                catch  {
                    GRILLA_ = new DataGridView();
                    goto ERROR_;
                    //MessageBox.Show("ERROR EN LAGUN PARAMETRO DE LA BASE DE DATOS\n\n" + e.Message, "ERR_"); 
                }
            }

 try
 {

     if (GRILLA_.Rows.Count == 1)
     {

         SQL_ = "SELECT * FROM " + TABLA;

         if (CONEXION.State == 0) { CONEXION.Open(); }
         else
         {
             CONEXION.Close();
             CONEXION.Open();
         }


         DATA_CONEXION = new OleDbDataAdapter(SQL_, CONEXION);
         TABLA_OBJETOS = new DataTable();
         DATA_CONEXION.Fill(TABLA_OBJETOS);


         GRILLA_.DataSource = TABLA_OBJETOS;
     }


     //columna x en modo no visible 

     for (int k = 0; k < GRILLA_.ColumnCount; k++)
     {
         if (k == COLUMNA_INVISIBLE)
         {
             GRILLA_.Columns[k].Visible = false;
             break;
         }
     }
 }
 catch { }

        }
        [Obsolete("funcion obsoleta", true)]
        public static int Buscar_Sql(DataGridView GRILLA_, string TABLA, int COLUMNA_INVISIBLE, TextBox txt, params string[] BUSCADOR_NOMBRE_TABLA)
        {

            int contador = BUSCADOR_NOMBRE_TABLA.Length;
            

            for (int j = 0; j < contador; j++)
            {

                if (BUSCADOR_NOMBRE_TABLA[j] == null) { break; }

                SQL_ = "SELECT * FROM " + TABLA + " WHERE " + BUSCADOR_NOMBRE_TABLA[j] + "='" + txt.Text + "'";//SQL_ PARAMETROS DEL BUSCADOR

                try
                {

                    DATA_CONEXION = new OleDbDataAdapter(SQL_, CONEXION);
                    TABLA_OBJETOS = new DataTable();
                    DATA_CONEXION.Fill(TABLA_OBJETOS);

                    //TABLA_OBJETOS.Select("Nombre LIKE '%" + txt.Text + "%'");
                    try
                    {
                        GRILLA_.DataSource = TABLA_OBJETOS;
                    }
                    catch {

                        GRILLA_ = new DataGridView();
                        GRILLA_.DataSource = TABLA_OBJETOS;
                    }

                    NO_ENCONTRADO = 0;

                    if (GRILLA_.Rows.Count >= 2) { break; }

                }
                catch (Exception e) { MessageBox.Show("ERROR EN LAGUN PARAMETRO DE LA BASE DE DATOS\n\n" + e.Message, "ERR_"); }
            }



            if (GRILLA_.Rows.Count == 1)
            {

                SQL_ = "SELECT * FROM " + TABLA;

                if (CONEXION.State == 0) { CONEXION.Open(); }
                else
                {
                    CONEXION.Close();
                    CONEXION.Open();
                }


                DATA_CONEXION = new OleDbDataAdapter(SQL_, CONEXION);
                TABLA_OBJETOS = new DataTable();
                DATA_CONEXION.Fill(TABLA_OBJETOS);


                GRILLA_.DataSource = TABLA_OBJETOS;

                NO_ENCONTRADO  = 1;
            }


            //columna x en modo no visible 

            for (int k = 0; k < GRILLA_.ColumnCount; k++)
            {
                if (k == COLUMNA_INVISIBLE)
                {
                    GRILLA_.Columns[k].Visible = false;
                    break;
                }
            }


            return NO_ENCONTRADO;
            
        }
        [Obsolete("funcion obsoleta", true)]
        public static void Buscar_Exp(DataGridView GRILLA_, string TABLA, int COLUMNA_INVISIBLE, TextBox txt, params string[] BUSCADOR_NOMBRE_TABLA)
        {

            int contador = BUSCADOR_NOMBRE_TABLA.Length;

            for (int j = 0; j < contador; j++)
            {

                if (BUSCADOR_NOMBRE_TABLA[j] == null) { break; }

                SQL_ = "SELECT * FROM " + TABLA + " WHERE " + BUSCADOR_NOMBRE_TABLA[j] + "='" + txt.Text + "'AND Fecha_expiracion <= date()";   //SQL_ PARAMETROS DEL BUSCADOR

                try
                {

                    DATA_CONEXION = new OleDbDataAdapter(SQL_, CONEXION);
                    TABLA_OBJETOS = new DataTable();
                    DATA_CONEXION.Fill(TABLA_OBJETOS);

                    GRILLA_.DataSource = TABLA_OBJETOS;

                    if (GRILLA_.Rows.Count >= 2) { break; }

                }
                catch (Exception e) { MessageBox.Show("ERROR EN LAGUN PARAMETRO DE LA BASE DE DATOS\n\n" + e.Message, "ERR_"); }
            }



            if (GRILLA_.Rows.Count == 1)
            {

                try
                {
                    SQL_ = "SELECT * FROM " + TABLA + "WHERE Nombre='" + txt.Text + "'AND Fecha_expiracion <= date() ";

                    if (CONEXION.State == 0) { CONEXION.Open(); }
                    else
                    {
                        CONEXION.Close();
                        CONEXION.Open();
                    }


                    DATA_CONEXION = new OleDbDataAdapter(SQL_, CONEXION);
                    TABLA_OBJETOS = new DataTable();
                    DATA_CONEXION.Fill(TABLA_OBJETOS);


                    GRILLA_.DataSource = TABLA_OBJETOS;
                }
                catch { }
            }


            //columna x en modo no visible 

            for (int k = 0; k < GRILLA_.ColumnCount; k++)
            {
                if (k == COLUMNA_INVISIBLE)
                {
                    GRILLA_.Columns[k].Visible = false;
                    break;
                }
            }



        }
        #endregion

        #region motores de busqueda por grilla o dtagridview
        
        //motores de busqueda por medio de grilla o datagridview
        public static int Buscar_(DataGridView GRILLA, TextBox txt)
        {

            string CADENA = txt.Text;
            int Contador = 0;

            try
            {
                for (int k = 0; k < GRILLA.Rows.Count - 1; k++)
                {
                    GRILLA.CurrentCell = null;
                    GRILLA.Rows[k].Visible = false;
                }
            }
            catch { }

            for (int i = 1; i < GRILLA.ColumnCount; i++)
            {
                for (int j = 0; j < GRILLA.RowCount - 1; j++)
                {
                    if (Convert.ToString(GRILLA[i, j].Value).ToUpper() == CADENA.ToUpper() && CADENA != "")
                    {
                        GRILLA.Rows[j].Visible = true;
                        Contador++;
                    }
                }
            }

            if (Contador == 0)
            {
                try
                {
                    for (int k = 0; k < GRILLA.Rows.Count - 1; k++)
                    {
                        GRILLA.CurrentCell = null;
                        GRILLA.Rows[k].Visible = true;
                    }
                }
                catch { }
            }

            return Contador;
        }//buscar por medio de caracteres 

        public static void Buscar_(DataGridView GRILLA, ToolStripTextBox txt)
        {
            HiloBusqueda = new Thread(delegate()
            {
                string CADENA = txt.Text;
                int Contador = 0;

                try
                {
                    for (int k = 0; k < GRILLA.Rows.Count - 1; k++)
                    {
                        GRILLA.CurrentCell = null;
                        GRILLA.Rows[k].Visible = false;
                    }
                }
                catch { }

                for (int i = 1; i < GRILLA.ColumnCount; i++)
                {
                    for (int j = 0; j < GRILLA.RowCount - 1; j++)
                    {
                        if (Convert.ToString(GRILLA[i, j].Value).ToUpper() == CADENA.ToUpper() && CADENA != "")
                        {
                            GRILLA.Rows[j].Visible = true;
                            Contador++;
                        }
                    }
                }

                if (Contador == 0)
                {
                    try
                    {
                        for (int k = 0; k < GRILLA.Rows.Count - 1; k++)
                        {
                            GRILLA.CurrentCell = null;
                            GRILLA.Rows[k].Visible = true;
                        }
                    }
                    catch { }
                }
            });

        }

        public static void Buscar_Caracter(DataGridView GRILLA, ToolStripTextBox txt)
        {

            HiloBusqueda = new Thread(delegate()
            {
                string CADENA_GRILLA = "";
                string CADENA_TEXTO = txt.Text;
                int contador = 0;
                List<char> reordenar = new List<char>();
                List<int> RowsAfected = new List<int>();

                try
                {
                        GRILLA.CurrentCell = null;

                        for (int k = 0; k < GRILLA.RowCount - 1; k++)
                        {
                            DataGridViewBand band = GRILLA.Rows[k];
                            band.Visible = false;
                        }
                }
                catch { }

                try
                {
                    for (int i = 1; i < GRILLA.ColumnCount - 10; i++)
                    {

                        for (int j = 0; j < GRILLA.RowCount - 1; j++)
                        {

                            CADENA_GRILLA = Convert.ToString(GRILLA[i, j].Value);
                            reordenar.Clear();

                            if (CADENA_GRILLA.Length < CADENA_TEXTO.Length)
                            {
                                break;
                            }
                            else
                            {
                                for (int n = 0; n < CADENA_TEXTO.Length; n++)
                                {
                                    reordenar.Add(CADENA_GRILLA[n]);
                                }
                            }

                            if (string.Join("", reordenar).ToUpper() == CADENA_TEXTO.ToUpper())
                            {
                                GRILLA.Rows[j].Visible = true;
                                RowsAfected.Add(j);
                                contador++;
                            }

                        }

                    }

                }
                catch { }

                if (contador == 0)
                {

                    for (int i = 1; i < GRILLA.ColumnCount - 1; i++)
                    {
                        for (int j = 0; j < GRILLA.RowCount - 1; j++)
                        {
                            if (Convert.ToString(GRILLA[i, j].Value).ToUpper() == CADENA_TEXTO.ToUpper() && CADENA_TEXTO != "")
                            {
                                GRILLA.Rows[j].Visible = true;
                                contador++;
                            }
                        }

                    }

                    if (contador == 0)
                    {
                        try
                        {
                            for (int k = 0; k < GRILLA.Rows.Count - 1; k++)
                            {
                                GRILLA.CurrentCell = null;
                                GRILLA.Rows[k].Visible = true;
                            }

                            contador = 0;
                        }
                        catch { }
                    }
                }

            });
            HiloBusqueda.Start();
        }

        public static int Buscar_Caracter(DataGridView GRILLA, TextBox txt)
        {


            string CADENA_GRILLA = "";
            string CADENA_TEXTO = txt.Text;
            int contador = 0;
            List<char> reordenar = new List<char>();


            try
            {
                for (int k = 0; k < GRILLA.Rows.Count - 1; k++)
                {
                    GRILLA.CurrentCell = null;
                    GRILLA.Rows[k].Visible = false;
                }
            }
            catch { }


            for (int i = 1; i < GRILLA.ColumnCount - 10; i++)
            {

                for (int j = 0; j < GRILLA.RowCount - 1; j++)
                {
                    CADENA_GRILLA = Convert.ToString(GRILLA[i, j].Value);
                    reordenar.Clear();


                    if (CADENA_GRILLA.Length < CADENA_TEXTO.Length)
                    {
                        break;
                    }
                    else
                    {
                        for (int n = 0; n < CADENA_TEXTO.Length; n++)
                        {
                            reordenar.Add(CADENA_GRILLA[n]);
                        }
                    }

                    if (string.Join("", reordenar).ToUpper() == CADENA_TEXTO.ToUpper())
                    {
                        GRILLA.Rows[j].Visible = true;
                        contador++;
                    }

                }

            }

            if (contador == 0)
            {

                for (int i = 1; i < GRILLA.ColumnCount - 1; i++)
                {
                    for (int j = 0; j < GRILLA.RowCount - 1; j++)
                    {
                        if (Convert.ToString(GRILLA[i, j].Value).ToUpper() == CADENA_TEXTO.ToUpper() && CADENA_TEXTO != "")
                        {
                            GRILLA.Rows[j].Visible = true;
                            contador++;
                        }
                    }
                }

                if (contador == 0)
                {
                    try
                    {
                        for (int k = 0; k < GRILLA.Rows.Count - 1; k++)
                        {
                            GRILLA.CurrentCell = null;
                            GRILLA.Rows[k].Visible = true;
                        }
                    }
                    catch { }

                    contador = 0;
                }
            }


            return contador;
        }

        #endregion

        #endregion


        #region Motor de busqueda 1.0

        private static Thread HiloBusqueda;

        public static string Cadena = "";
        public static DataGridView Datagrid = null;
        public static int Total = 0;

        private static int Cont = 0;
        private static Control ctl;
        private static PictureBox picture;


        private static DataTable TablaTemporal;
        private static DataView DataviewTemporal;

        public static void InitFunction()
        {
            if (DataviewTemporal == null)
            {
                TablaTemporal = (DataTable)Datagrid.DataSource;
                DataviewTemporal = new DataView(TablaTemporal);
            }
        }

        public static void GetBusqueda()
        {


            try
            {
                DataTable tabla = new DataTable();
                tabla = TablaTemporal;
                DataView dv = new DataView(tabla);
                dv.RowFilter = "Nombre Like '" + Cadena + "'";
                Datagrid.DataSource = dv;

                if (Datagrid.RowCount - 1 >= 1)
                    return;
                else
                {
                    Datagrid.DataSource = DataviewTemporal;
                    GetParametroBusqueda();
                }
                   
            }
            catch { }
        
   
        }

        private static void GetParametroBusqueda()
        {
            HiloBusqueda = new Thread(delegate()
            {
                try
                {
                    Cont = 0;
                    Datagrid.CurrentCell = null;
                    for (int k = 0; k < Datagrid.RowCount - 1; k++)
                    {
                        DataGridViewBand band = Datagrid.Rows[k];
                        band.Visible = false;
                    }
                }
                catch { }


                try
                {
                    for (int i = 1; i < Datagrid.ColumnCount - 10; i++)
                    {
                        for (int j = 0; j < Datagrid.RowCount - 1; j++)
                        {
                            var comparar = Datagrid[i, j].Value.ToString();
                            if (string.Equals(Cadena, comparar, StringComparison.OrdinalIgnoreCase))
                            {
                                Datagrid.Rows[j].Visible = true;
                                Cont++;
                            }
                            var cadena = Datagrid[i, j].Value.ToString();
                            List<string> concatenar = new List<string>();
                            List<string> caracteres__ = new List<string>();

                            int c = 0;
                            int q = 0;
                            for (c = 0; c < cadena.Length; c++)
                            {
                                if (!(cadena[c].ToString() == " "))
                                    caracteres__.Add(cadena[c].ToString());
                                else
                                    break;
                            }

                            concatenar.Add(string.Join("", caracteres__));
                            caracteres__ = new List<string>();

                            if ((c) == cadena.Length)
                            {
                                if (string.Equals(Cadena, concatenar[0].ToString(), StringComparison.OrdinalIgnoreCase))
                                {
                                    Datagrid.Rows[j].Visible = true;
                                    Cont++;
                                }
                            }
                            else
                            {
                                for (q = c + 1; q < cadena.Length; q++)
                                {
                                    if (cadena[q].ToString() != " ")
                                        caracteres__.Add(cadena[q].ToString());
                                    else
                                        break;
                                }

                                concatenar.Add(string.Join("", caracteres__));
                                caracteres__ = new List<string>();

                                for (int z = 0; z < concatenar.Count; z++)
                                {
                                    if (string.Equals(Cadena, concatenar[z].ToString(), StringComparison.OrdinalIgnoreCase))
                                    {
                                        Datagrid.Rows[j].Visible = true;
                                        Cont++;
                                    }
                                }

                            }

                        }
                    }

                    if (Cadena == string.Empty || Cont == 0)
                    {
                        try
                        {
                            Datagrid.CurrentCell = null;
                            for (int k = 0; k < Datagrid.RowCount - 1; k++)
                            {
                                DataGridViewBand band = Datagrid.Rows[k];
                                band.Visible = false;
                            }
                        }
                        catch { }
                        return;
                    }


                }
                catch { }
            });
            HiloBusqueda.Priority = ThreadPriority.Highest;
            HiloBusqueda.Start();
        }

        public static void WaitTimeConfig( Control control , Point p)
        {
            ctl = control;
            picture = new PictureBox();
            picture.Image = Image.FromFile(System.IO.Directory.GetCurrentDirectory() + @"/url.gif");
            picture.Show();
            picture.Size = new Size(100, 100);
            picture.SizeMode = PictureBoxSizeMode.StretchImage;
            picture.Location = p;
            picture.BringToFront();
            ctl.Controls.Add(picture);
        }

        public static bool IsaliveThreat()
        {
            return HiloBusqueda.IsAlive;
        }

        public static bool WaitTime()
        {
            try
            {
                if (HiloBusqueda.IsAlive == false)
                {
                    ctl.Controls.Remove(picture);
                    return true;
                }
                else
                    return false;
            }
            catch
            {
                return true;
            }
        }

        #endregion

    }


    class BuscarSocio
    {
        #region Motor de busqueda 2.0

        private static Thread HiloBusqueda;

        public static string Cadena = "";
        public static DataGridView Datagrid = null;
        public static int Total = 0;
        public static List<string> ParametrosBusqueda = new List<string>();

        private static int Cont = 0;
        private static Control ctl;
        private static PictureBox picture;
        private static int Contador = 0;

        private static DataTable TablaTemporal;
        private static DataView DataviewTemporal;

        public void InitFunction()
        {
            try
            {
                TablaTemporal = (DataTable)Datagrid.DataSource;
                DataviewTemporal = new DataView(TablaTemporal);
            }
            catch { }
        }

        public void InitFunction(bool CambioDatagrid)
        {
            if (DataviewTemporal == null || CambioDatagrid == true)
            {
                TablaTemporal = (DataTable)Datagrid.DataSource;
                DataviewTemporal = new DataView(TablaTemporal);
            }
        }

        public void GetBusquedaAvanzada_()
        {


            try
            {
                DataTable tabla = new DataTable();
                tabla = TablaTemporal;
                DataView dv = new DataView(tabla);

                foreach (var N in ParametrosBusqueda)
                {
                    try
                    {
                        dv.RowFilter = N + " Like '" + Cadena + "%'";
                        if (dv.Count != 0 || dv.Count == TablaTemporal.Rows.Count)
                            break;
                    }
                    catch
                    {
                        try
                        {
                            int i = 0;
                            bool resultado = int.TryParse(Cadena, out i);
                            if (resultado == true)
                            {
                                dv.RowFilter = N + " =" + i + "";
                                if (dv.Count != 0 || dv.Count == TablaTemporal.Rows.Count)
                                    break;
                            }
                        }
                        catch { }
                    }
                }

                Datagrid.DataSource = dv;

                if (Datagrid.RowCount - 1 >= 1)
                {
                    return;
                }
                else
                {
                    Datagrid.DataSource = DataviewTemporal;
                    GetParametroBusqueda();
                }

            }
            catch{ }
        }

        public void GetBusqueda()
        {
            Contador = 0;

            try
            {
                DataTable tabla = new DataTable();
                tabla = TablaTemporal;
                DataView dv = new DataView(tabla);

                foreach (var N in ParametrosBusqueda)
                {
                    try
                    {
                        dv.RowFilter = N + " Like '" + Cadena + "%'";
                        if (dv.Count != 0 || dv.Count == TablaTemporal.Rows.Count)
                            break;
                    }
                    catch
                    {
                        try
                        {
                            int i = 0;
                            bool resultado = int.TryParse(Cadena, out i);
                            if (resultado == true)
                            {
                                dv.RowFilter = N + " =" + i + "";
                                if (dv.Count != 0 || dv.Count == TablaTemporal.Rows.Count)
                                    break;
                            }
                        }
                        catch { }
                    }
                }

                Contador = dv.Count;
                Datagrid.DataSource = dv;

            }
            catch { }
        }

        public int TotalBusqueda()
        {
            return Contador;
        }

        private void GetParametroBusqueda()
        {
            HiloBusqueda = new Thread(delegate()
            {
                try
                {
                    Cont = 0;
                    Datagrid.CurrentCell = null;
                    for (int k = 0; k < Datagrid.RowCount - 1; k++)
                    {
                        DataGridViewBand band = Datagrid.Rows[k];
                        band.Visible = false;
                    }
                }
                catch { }


                try
                {
                    for (int i = 1; i < Datagrid.ColumnCount - 10; i++)
                    {
                        for (int j = 0; j < Datagrid.RowCount - 1; j++)
                        {
                            var comparar = Datagrid[i, j].Value.ToString();
                            if (string.Equals(Cadena, comparar, StringComparison.OrdinalIgnoreCase))
                            {
                                Datagrid.Rows[j].Visible = true;
                                Cont++;
                            }
                            var cadena = Datagrid[i, j].Value.ToString();
                            List<string> concatenar = new List<string>();
                            List<string> caracteres__ = new List<string>();

                            int c = 0;
                            int q = 0;
                            for (c = 0; c < cadena.Length; c++)
                            {
                                if (!(cadena[c].ToString() == " "))
                                    caracteres__.Add(cadena[c].ToString());
                                else
                                    break;
                            }

                            concatenar.Add(string.Join("", caracteres__));
                            caracteres__ = new List<string>();

                            if ((c) == cadena.Length)
                            {
                                if (string.Equals(Cadena, concatenar[0].ToString(), StringComparison.OrdinalIgnoreCase))
                                {
                                    Datagrid.Rows[j].Visible = true;
                                    Cont++;
                                }
                            }
                            else
                            {
                                for (q = c + 1; q < cadena.Length; q++)
                                {
                                    if (cadena[q].ToString() != " ")
                                        caracteres__.Add(cadena[q].ToString());
                                    else
                                        break;
                                }

                                concatenar.Add(string.Join("", caracteres__));
                                caracteres__ = new List<string>();

                                for (int z = 0; z < concatenar.Count; z++)
                                {
                                    if (string.Equals(Cadena, concatenar[z].ToString(), StringComparison.OrdinalIgnoreCase))
                                    {
                                        Datagrid.Rows[j].Visible = true;
                                        Cont++;
                                    }
                                }

                            }

                        }
                    }

                    if (Cadena == string.Empty || Cont == 0)
                    {
                        try
                        {
                            Datagrid.CurrentCell = null;
                            for (int k = 0; k < Datagrid.RowCount - 1; k++)
                            {
                                DataGridViewBand band = Datagrid.Rows[k];
                                band.Visible = false;
                            }
                        }
                        catch { }
                        return;
                    }


                }
                catch { }
            });
            HiloBusqueda.Priority = ThreadPriority.Highest;
            HiloBusqueda.Start();
        }

        public void WaitTimeConfig(Control control, Point p)
        {
            ctl = control;
            picture = new PictureBox();
            picture.Image = Image.FromFile(System.IO.Directory.GetCurrentDirectory() + @"/url.gif");
            picture.Show();
            picture.Size = new Size(100, 100);
            picture.SizeMode = PictureBoxSizeMode.StretchImage;
            picture.Location = p;
            picture.BringToFront();
            ctl.Controls.Add(picture);
        }

        public bool IsaliveThreat()
        {
            try
            {
                return HiloBusqueda.IsAlive;
            }
            catch
            {
                return false;
            }
        }

        public bool WaitTime()
        {
            try
            {
                if (HiloBusqueda.IsAlive == false)
                {
                    ctl.Controls.Remove(picture);
                    return true;
                }
                else
                    return false;
            }
            catch
            {
                return true;
            }
        }

        #endregion

    }

}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;


using ADODB;
using System.Data.OleDb;
using ZONE_FITNESS_3._0_FINAL.CLASES;

using System.Web;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;

using System.Threading;
using System.IO;
using System.Runtime;
using Microsoft.Win32;

using ZONE_FITNESS_3._0_FINAL;
using GestorGimnasio.CLASES;
using System.Drawing.Printing;
using System.Windows.Forms.DataVisualization.Charting;
using System.Threading.Tasks;
using System.Reflection;



namespace ZONE_FITNESS_3._0_FINAL
{
    
    public partial class PRINCIPAL : Form
    {
       

        private string SQL_SOCIOS_GRILLA = "SELECT * FROM SOCIOS ORDER BY Fecha_inicio ";
        private const string SQL_SOCIOS_CONST = "SELECT * FROM SOCIOS";
        private string SQL_EMPLEADOS = "SELECT * FROM EMPLEADOS";
        private string SQL_EDITAR_SOCIO = "";
        private string SQL_EDITAR_EXPIRADOS = "";
        private string SQL_ELIMINAR = "";
        private const string SQL_EXPIRADOS = "SELECT * FROM SOCIOS WHERE DATE() >= Fecha_expiracion - 5  ORDER BY Fecha_expiracion";
        private string SQL_TIENDA = "SELECT * FROM TIENDA WHERE Fecha >= DATE()";
        
        private List<string> Precios__ = new List<string>();
        private List<string> Empleados__ = new List<string>();


        int sentinela = 0;
        int status = 0;
        int tiempo_cursor = 0;

        public static int X_;
        public static int Y_;
     
        static string NOMBRE_EDIT;
        static string APELLIDO_EDIT;
        //static string IMAGEN_ELIMINAR;

        static string AUXILIAR;

        public static string CORREO_SERVIDOR;
        public static string PASSWORD_SERVIDOR;
        public static string CUERPO_CORREO;
        public static string FROM_CORREO;

        public static OleDbDataAdapter oled_data_adapter_tienda;
        public static DataTable ds_oledb_tienda;
        public static DataTable dt_mysql_socios = new DataTable();
        public static DataTable dt_mysql_expirados = new DataTable();
        public static DataGridView GRILLA_SOCIOS_EXTERNA;
        public static string WebcamCapture = string.Empty;
        public static bool EnableTimeWebCam = false;

        public static List<string> Parametros2x1 = new List<string>();

        byte[] ImagenCliente;
        long LongImagen;

        GroupBox FrmVistaPrevia = new GroupBox();
        PictureBox imagenVp = new PictureBox();
        LinkLabel linkNombre = new LinkLabel();
        LinkLabel linkApellido = new LinkLabel();
        LinkLabel linkcuota = new LinkLabel();
        LinkLabel linkFechaExpiracion = new LinkLabel();

        /*HILOS PARA SUB-PROCESOS */

        private Thread HiloGuardarFtp;
        private Thread Hilo_expirado;
        private Thread Hilo_Socio;
        private Thread HiloTienda;
        private Thread HiloSocioP;
        private Thread Hilo;
        private Thread HiloExpiradoPorDia;

        BuscarSocio BuscarSocio = new BuscarSocio();
        DataGridViewPrinter MyDataGridViewPrinter;


        public PRINCIPAL()
        {
            InitializeComponent();
        }

        private void ADD_TITULOS()
        {


            tabPage1.Text = titulos_principal.TAB_1;
            tabPage2.Text = titulos_principal.TAB_2;
            tabPage3.Text = titulos_principal.TAB_5;
            tabPage6.Text = titulos_principal.TAB_4;
            tabPage5.Text = titulos_principal.TAB_3;
       
        }

        private void FUNCION_TIENDA()
        {

            int x = 0;
           
           
            try
            {
               
                combovendedor_tienda.Items.Clear();
                combocantidad_tienda.Items.Clear();
                COMBO_PRODUCTO.Items.Clear();
                comboBox3.Items.Clear();

                try
                {

                    conectar_.RS_EMPLEADOS = new Recordset();

                    conectar_.RS_EMPLEADOS.Open(SQL_EMPLEADOS, conectar_.CN, CursorTypeEnum.adOpenDynamic, LockTypeEnum.adLockOptimistic);
                    conectar_.RS_EMPLEADOS.MoveFirst();

                    while (!(conectar_.RS_EMPLEADOS.EOF))
                    {
                        combovendedor_tienda.Items.Add(conectar_.RS_EMPLEADOS.Fields[1].Value);
                        conectar_.RS_EMPLEADOS.MoveNext();
                    }

                    conectar_.RS_EMPLEADOS.Close();
                 
                }
                catch { }


                try
                {
                    conectar_.PRECIOS_TIENDA = new Recordset();
                    COMBO_PRODUCTO.Items.Clear();

                    conectar_.PRECIOS_TIENDA.Open("SELECT * FROM TIENDA_NOMBRES", conectar_.CN, CursorTypeEnum.adOpenDynamic, LockTypeEnum.adLockOptimistic);
                    conectar_.PRECIOS_TIENDA.MoveFirst();

                    while (!(conectar_.PRECIOS_TIENDA.EOF))
                    {
                        COMBO_PRODUCTO.Items.Add(conectar_.PRECIOS_TIENDA.Fields[1].Value);
                        conectar_.PRECIOS_TIENDA.MoveNext();
                    }

                    conectar_.PRECIOS_TIENDA.Close();
                }
                catch { }

                for (x = 1; x <= 10; x++)
                {
                    combocantidad_tienda.Items.Add(x);
                }


            }
            catch { }

            //----------------------------------------------------------------------------------------------------------------------------

            try
            {
                if (conectar_.mysqlconection.State == 0) { conectar_.mysqlconection.Open(); }
                else
                {
                    conectar_.mysqlconection.Close();
                    conectar_.mysqlconection.Open();
                }

                SQL_TIENDA = "SELECT * FROM TIENDA WHERE Fecha LIKE DATE()";
                oled_data_adapter_tienda = new OleDbDataAdapter(SQL_TIENDA, conectar_.mysqlconection);//conecta el adapter con la sentencia sql
                ds_oledb_tienda = new DataTable();//tabla es = nueva tabla
                oled_data_adapter_tienda.Fill(ds_oledb_tienda);//el adaptador con toda la base de sql se le llenara en la tabla

            }
            catch( Exception ex)
            {
                GestorGimnasio.CLASES.Informe_Errores.Enviar_Error(ex.Message);
            }

        }

        private void TiendaThreat()
        {

            decimal dato = 0;
            GRILLA_TIENDA.DataSource = ds_oledb_tienda;

            if (!(GRILLA_TIENDA.RowCount == 1))
            {
                for (int i = 0; i < this.GRILLA_TIENDA.RowCount - 1; i++)
                {
                    if (GRILLA_TIENDA[2, i].Value != null)
                    {
                        dato += Convert.ToDecimal(FiltroDatosTienda(GRILLA_TIENDA[2, i].Value.ToString()));
                    }
                }
                 
                lbltotal.Text = "Total vendido: $" + Convert.ToString(dato) + " Dolares";
            }


            GRILLA_TIENDA.Columns[0].HeaderText = "Producto";
            GRILLA_TIENDA.Columns[1].HeaderText = "Cantidad";
            GRILLA_TIENDA.Columns[2].HeaderText = "Costo";
            GRILLA_TIENDA.Columns[3].HeaderText = "Vendedor";
            GRILLA_TIENDA.Columns[4].HeaderText = "Fecha de venta";
            GRILLA_TIENDA.Columns[5].Visible = false;
        }

        private object FiltroDatosTienda(string Filtro)
        {
            if (Filtro.Contains("$") == true)
                Filtro = Filtro.Replace('$', ' ');
            if (Filtro.Contains(".") == true)
                Filtro = Filtro.Replace('.', ',');
            float Flote = float.Parse(Filtro);
            Filtro = null;
            return Flote;
        }

        private void GetEstadisticasTienda()
        {

            List<string> Secuencia = new List<string>();
            List<decimal> Resultados = new List<decimal>();


            SQL_TIENDA = "SELECT * FROM TIENDA";
            OleDbDataAdapter adapter = new OleDbDataAdapter(SQL_TIENDA, conectar_.mysqlconection);
            DataTable table = new DataTable();
            adapter.Fill(table);

            GRILLA_TIENDA.DataSource = table;

              
            decimal subtotal = 0;


            for (int i = 0; i < GRILLA_TIENDA.RowCount - 1; i++)
            {
                for (int j = i; j < GRILLA_TIENDA.RowCount - 1; j++)
                {
                    if (string.Compare( GRILLA_TIENDA[4, i].Value.ToString() ,GRILLA_TIENDA[4, j].Value.ToString()) == 0)
                    {
                        subtotal += Convert.ToDecimal(FiltroDatosTienda(GRILLA_TIENDA[2, j].Value.ToString()));
                    }
                }

                if (Secuencia.Contains(GRILLA_TIENDA[4, i].Value.ToString()) == false)
                {
                    if (subtotal == 0)
                        Resultados.Add(Convert.ToDecimal(FiltroDatosTienda(GRILLA_TIENDA[2, i].Value.ToString())));
                    else
                        Resultados.Add(subtotal);

                    Secuencia.Add(GRILLA_TIENDA[4, i].Value.ToString());
                }
                subtotal = 0;
            }

            CharTienda.Titles.Clear();
            CharTienda.Titles.Add("Ganancias por dias");
            CharTienda.Series.Clear();

            for (int k = 0; k < Secuencia.Count; k++)
            {
                Series ChartSeries = CharTienda.Series.Add(Secuencia[k]);
                ChartSeries.BorderColor = Color.Red;
                ChartSeries.MarkerStyle = MarkerStyle.Circle;
                ChartSeries.Points.Add(Convert.ToDouble(Resultados[k]));
            }

            adapter.Dispose();
            table.Dispose();
            GRILLA_TIENDA.DataSource = null;
            
        }

        private void VaciarGrillaSocios()
        {
            try
            {
                GRILLA_SOCIOS.DataSource = null;
            }
            catch { }
        }

        private void RowsPatronesSocios()
        {
            try
            {
                GRILLA_SOCIOS.DataSource = dt_mysql_socios;//el DataTable table se pasara a la grilla
                GRILLA_SOCIOS.Columns[0].Visible = false;
                GRILLA_SOCIOS.Columns[11].Visible = false;
                GRILLA_SOCIOS.Columns[12].Visible = false;
                GRILLA_SOCIOS.Columns[16].Visible = false;

               // STRIP_TXT_BUSCAR.AutoCompleteCustomSource.Clear();
               // STRIP_TXT_BUSCAR.AutoCompleteMode = AutoCompleteMode.Suggest;
               

                try
                {
                    int cexp = 0;
                    int cact = 0;

                    for (int i = 0; i < GRILLA_SOCIOS.RowCount - 1; i++)
                    {

                        string fechas_ex = GRILLA_SOCIOS[4, i].Value.ToString();
                        DateTime fecha = Convert.ToDateTime(fechas_ex);


                     
                        if (DateTime.Now.Date.AddDays(5) < fecha)
                        {
                            GRILLA_SOCIOS.Rows[i].DefaultCellStyle.BackColor = Color.White;
                            GRILLA_SOCIOS.Rows[i].DefaultCellStyle.ForeColor = Color.Black;
                            cact++;
                        }
                        else
                        {
                            GRILLA_SOCIOS.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                            GRILLA_SOCIOS.Rows[i].DefaultCellStyle.ForeColor = Color.DarkBlue;
                            cexp++;
                        }

                    }

                    lblsociosExp.Text = "Socios Expirados: " + cexp.ToString();
                    lblsociosactiv.Text ="Socios Activos: " + cact.ToString();

                    UpdateBuscador();
                    CONTADOR_SOCIOS();
                }
                catch { }
            }
            catch
            {
                return;
            }

        }

        private void RowsColorSocios()
        {
            try
            {
                int cexp = 0;
                int cact = 0;

                for (int i = 0; i < GRILLA_SOCIOS.RowCount - 1; i++)
                {

                    string fechas_ex = GRILLA_SOCIOS[4, i].Value.ToString();
                    DateTime fecha = Convert.ToDateTime(fechas_ex);


                    if (DateTime.Now.Date.AddDays(5) < fecha)
                    {
                        GRILLA_SOCIOS.Rows[i].DefaultCellStyle.BackColor = Color.White;
                        GRILLA_SOCIOS.Rows[i].DefaultCellStyle.ForeColor = Color.Black;
                        cact++;
                    }
                    else
                    {
                        GRILLA_SOCIOS.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                        GRILLA_SOCIOS.Rows[i].DefaultCellStyle.ForeColor = Color.WhiteSmoke;
                        cexp++;
                    }
                }

                lblsociosExp.Text = "Socios Expirados: " + cexp.ToString();
                lblsociosactiv.Text = "Socios Activos: " + cact.ToString();
            }
            catch { }
        }

        private void View_SociosEliminados()
        {
            try
            {
                
                for (int i = 0; i < GRILLA_SOCIOS.RowCount - 1; i++)
                {

                    string fechas_ex = GRILLA_SOCIOS[4, i].Value.ToString();
                    DateTime fecha = Convert.ToDateTime(fechas_ex);

                    if ( DateTime.Now.Date.AddDays(5) > fecha)
                    {
                        GRILLA_SOCIOS.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                        GRILLA_SOCIOS.Rows[i].DefaultCellStyle.ForeColor = Color.DarkBlue;
                    }
                    else
                    {
                        GRILLA_SOCIOS.Rows[i].DefaultCellStyle.BackColor = Color.White;
                        GRILLA_SOCIOS.Rows[i].DefaultCellStyle.ForeColor = Color.Black;
                    }
                }
            }
            catch { }
        }

        private void UpdateBuscador()
        {
            try
            {
                BuscarSocio.Datagrid = GRILLA_SOCIOS;
                BuscarSocio.InitFunction(true);
            }
            catch { }
        }

        private void GET_GRILL_SOCIOS()
        {

            try
            {
                dt_mysql_socios.Clear();
                if (conectar_.mysqlconection.State == ConnectionState.Closed)
                {
                    conectar_.mysqlconection.Open();
                }
                else if (conectar_.mysqlconection.State == ConnectionState.Connecting)
                {

                    conectar_.mysqladapter = new OleDbDataAdapter(SQL_SOCIOS_GRILLA, conectar_.mysqlconection);//conecta el adapter con la sentencia sql
                    conectar_.ds_oledb = new DataTable();//tabla es = nueva tabla
                    conectar_.mysqladapter.Fill(dt_mysql_socios);//el adaptador con toda la base de sql se le llenara en la tabla
                    return;
                }
                conectar_.mysqladapter = new OleDbDataAdapter(SQL_SOCIOS_GRILLA, conectar_.mysqlconection);//conecta el adapter con la sentencia sql
                conectar_.ds_oledb = new DataTable();//tabla es = nueva tabla
                conectar_.mysqladapter.Fill(dt_mysql_socios);//el adaptador con toda la base de sql se le llenara en la tabla
            }
            catch
            {
                try
                {
                    conectar_.mysqlconection.Close();
                    conectar_.mysqlconection.Open();
                    conectar_.mysqladapter = new OleDbDataAdapter(SQL_SOCIOS_GRILLA, conectar_.mysqlconection);//conecta el adapter con la sentencia sql
                    conectar_.ds_oledb = new DataTable();//tabla es = nueva tabla
                    conectar_.mysqladapter.Fill(dt_mysql_socios);//el adaptador con toda la base de sql se le llenara en la tabla
                    return;
                }
                catch { }
            }
            try
            {
                conectar_.mysqlconection.Close();
            }
            catch { }

        }


        private void FUNCIONSOCIOS()
        {

            if (conectar_.mysqlconection.State == 0) { conectar_.mysqlconection.Open(); }
            else
            {
                conectar_.mysqlconection.Close();
                conectar_.mysqlconection.Open();
            }

            conectar_.mysqladapter = new OleDbDataAdapter(SQL_SOCIOS_GRILLA, conectar_.mysqlconection);//conecta el adapter con la sentencia sql
            conectar_.ds_oledb = new DataTable();//tabla es = nueva tabla
            conectar_.mysqladapter.Fill(conectar_.ds_oledb);//el adaptador con toda la base de sql se le llenara en la tabla

            try
            {
                GRILLA_SOCIOS.DataSource = conectar_.ds_oledb;//el DataTable table se pasara a la grilla
                GRILLA_SOCIOS.Columns[0].Visible = false;
                GRILLA_SOCIOS.Columns[8].Visible = false;
                GRILLA_SOCIOS.Columns[9].Visible = false;
                GRILLA_SOCIOS.Columns[10].Visible = false;
                GRILLA_SOCIOS.Columns[11].Visible = false;
                GRILLA_SOCIOS.Columns[12].Visible = false;
                GRILLA_SOCIOS.Columns[16].Visible = false;

                View_SociosEliminados();
            }
            catch
            {
                GRILLA_SOCIOS = new DataGridView();
                return;
            }
        }

        private void CONTADOR_SOCIOS()
        {

            try
            {
                strip_lbl_cantidad_socios.Text = GRILLA_SOCIOS.RowCount - 1 + " SOCIOS";
            }
            catch { }

        }

        private void GRILLA_POS_INI()
        {
            try
            {
                GRILLA_SOCIOS.Location = new Point(3, 6);
                GRILLA_SOCIOS.Width = 792;
                GRILLA_SOCIOS.Height = 308;
               // GRILLA_SOCIOS.Width = 1072;
               // GRILLA_SOCIOS.Height = 379;
            }
            catch { }
        }

        private void GRILLA_POS_FIN()
        {
            try
            {
                GRILLA_SOCIOS.Location = new Point(3, 6);
                GRILLA_SOCIOS.Width = 219;
                GRILLA_SOCIOS.Height = 308;
                //GRILLA_SOCIOS.Width = 292;
                //GRILLA_SOCIOS.Height = 379;
            }
            catch { }
        }

        private void GetParametrosInicialesSocios()
        {
            
            COMBO_PAGO.Items.Clear();
            COMBO_PAGO.Text = "SELECCIONAR PAGO";
            COMBO_PAGO.Items.Add("EFECTIVO");
            COMBO_PAGO.Items.Add("CHEQUE");
            COMBO_PAGO.Items.Add("TARJETA DE CREDITO");
            Precios__.Clear();
            Empleados__.Clear();

                conectar_.PRECIOS_ = new Recordset();
                conectar_.RS_EMPLEADOS = new Recordset();
 
                    try
                    {
                        

                        if (conectar_.PRECIOS_.State == 1) { conectar_.PRECIOS_.Close(); }
        
                        conectar_.PRECIOS_.Open("SELECT * FROM PRECIOS", conectar_.CN, CursorTypeEnum.adOpenDynamic, LockTypeEnum.adLockOptimistic);

                        while (!(conectar_.PRECIOS_.EOF))
                        {
                           // COMBO_PRECIOS.Items.Add(Convert.ToString(conectar_.PRECIOS_.Fields[1].Value));
                            Precios__.Add(Convert.ToString(conectar_.PRECIOS_.Fields[1].Value));
                            conectar_.PRECIOS_.MoveNext();
                        }
                       // conectar_.PRECIOS_.Close();

                        try
                        {
                            SQL_EMPLEADOS = "SELECT * FROM EMPLEADOS";
                            if (conectar_.RS_EMPLEADOS.State == 1) { conectar_.RS_EMPLEADOS.Close(); }
                            conectar_.RS_EMPLEADOS.Open(SQL_EMPLEADOS, conectar_.CN, CursorTypeEnum.adOpenDynamic, LockTypeEnum.adLockOptimistic);

                            while (!(conectar_.RS_EMPLEADOS.EOF))
                            {

                               // COMBO_ATENDIO.Items.Add(conectar_.RS_EMPLEADOS.Fields[1].Value);
                                Empleados__.Add(conectar_.RS_EMPLEADOS.Fields[1].Value);
                                conectar_.RS_EMPLEADOS.MoveNext();
                            }

                           conectar_.RS_EMPLEADOS.Close();
                        }
                        catch { }


                    }
                    catch
                    {

                    }
      
        }

        private void AgregarSocioParamsSubProceso()
        {
            COMBO_ATENDIO.Items.Clear();
            COMBO_PRECIOS.Items.Clear();
            COMBO_PRECIOS.AutoCompleteCustomSource.Clear();
            COMBO_ATENDIO.AutoCompleteCustomSource.Clear();
            COMBO_PRECIOS.Text = "Seleccionar Precio";
            COMBO_ATENDIO.Text = "Empleado";

            COMBO_ATENDIO.AutoCompleteMode = AutoCompleteMode.Append;
            COMBO_ATENDIO.AutoCompleteSource = AutoCompleteSource.CustomSource;

            COMBO_PRECIOS.AutoCompleteMode = AutoCompleteMode.Append;
            COMBO_PRECIOS.AutoCompleteSource = AutoCompleteSource.CustomSource;
          
            foreach (var empleados in Empleados__)
            {
                COMBO_ATENDIO.Items.Add(empleados.ToString());
                COMBO_ATENDIO.AutoCompleteCustomSource.Add(empleados.ToString());
            }

            foreach (var precios in Precios__)
            {
                COMBO_PRECIOS.Items.Add(precios.ToString());
                COMBO_PRECIOS.AutoCompleteCustomSource.Add(precios.ToString());
            }

        }

        private void GUARDAR_SOCIO(string ImagenSocio)
        {
            try
            {

                conectar_.RS_SOCIO_AGREGAR = new Recordset();

                if (conectar_.RS_SOCIO_AGREGAR.State == 1) { conectar_.RS_SOCIO_AGREGAR.Close(); }

                conectar_.RS_SOCIO_AGREGAR.Open(SQL_SOCIOS_CONST, conectar_.CN, CursorTypeEnum.adOpenDynamic, LockTypeEnum.adLockOptimistic);

                conectar_.RS_SOCIO_AGREGAR.AddNew();
                {
                    conectar_.RS_SOCIO_AGREGAR.Fields[1].Value = TXTNOMBRE_SOC1.Text;
                    conectar_.RS_SOCIO_AGREGAR.Fields[2].Value = TXTAPELLIDO_SOC1.Text;
                    conectar_.RS_SOCIO_AGREGAR.Fields[3].Value = DATE_INICIO.Text;
                    conectar_.RS_SOCIO_AGREGAR.Fields[4].Value = DATE_EXPIRAR.Text;
                    conectar_.RS_SOCIO_AGREGAR.Fields[5].Value = Convert.ToDecimal(TXTCUOTA.Text);
                    conectar_.RS_SOCIO_AGREGAR.Fields[6].Value = COMBO_PRECIOS.Text;
                    conectar_.RS_SOCIO_AGREGAR.Fields[7].Value = COMBO_PAGO.Text;

                    if (txtcomentario.Text != "COMENTARIO..." && txtcomentario.Text != null)
                        conectar_.RS_SOCIO_AGREGAR.Fields[8].Value = txtcomentario.Text;
                    else
                        conectar_.RS_SOCIO_AGREGAR.Fields[8].Value = "-";

                    if (Parametros2x1.Count >= 1)
                    {
                        conectar_.RS_SOCIO_AGREGAR.Fields[9].Value = Parametros2x1[0].ToString();
                        conectar_.RS_SOCIO_AGREGAR.Fields[10].Value = Parametros2x1[1].ToString();
                    }
                    else
                    {
                        conectar_.RS_SOCIO_AGREGAR.Fields[9].Value = "-";
                        conectar_.RS_SOCIO_AGREGAR.Fields[10].Value = "-";
                    }

                    conectar_.RS_SOCIO_AGREGAR.Fields[11].Value = "-";
                    conectar_.RS_SOCIO_AGREGAR.Fields[12].Value = "-";
                    conectar_.RS_SOCIO_AGREGAR.Fields[13].Value = Convert.ToDecimal(TXTFACTURA.Text);
                    conectar_.RS_SOCIO_AGREGAR.Fields[14].Value = COMBO_ATENDIO.Text;
                    conectar_.RS_SOCIO_AGREGAR.Fields[15].Value = TXT_MAIL.Text;
                    conectar_.RS_SOCIO_AGREGAR.Fields[16].Value = ImagenSocio;


                    conectar_.RS_SOCIO_AGREGAR.Update();
                }


                DialogResult respuesta = MessageBox.Show(
                    "SOCIO GUARDADO EXITOSAMENTE ¿DESEA AGREGAR OTRO SOCIO?", 
                    "Agregar Socio", MessageBoxButtons.YesNo, 
                    MessageBoxIcon.Information);
               
                if (respuesta == DialogResult.Yes)
                {
                    VaciarGrillaSocios();
                    Time_Socios.Enabled = true;
                    BORRAR_CASILLAS_AGREGAR();
                    sentinela = 1;
                }
                else
                {
                    VaciarGrillaSocios();
                    Hilo_Socio = new Thread(delegate()
                    {
                        GET_GRILL_SOCIOS();
                    });
                    EMAIL.Loop(GRILLA_SOCIOS, new Point(350, 115));
                    Hilo_Socio.Start();
                    Time_Socios.Enabled = true;
                    BORRAR_CASILLAS_AGREGAR();
                    GRILLA_POS_INI();
                    return;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("EL SOCIO NO SE HA PODIDO GUARDAR: " + ex.Message);
            }

        }

        private void BORRAR_CASILLAS_AGREGAR()
        {
            TXT_MAIL.Text = "@hotmail.com";
            TXTAPELLIDO_SOC1.Text = "";
            TXTCUOTA.Text = "";
            TXTFACTURA.Text = "";
            TXTNOMBRE_SOC1.Text = "";
            txtcomentario.Text = "COMENTARIO...";

            //picture_imagen_socio.Image = null;
            //picture_imagen_socio.Image = System.Drawing.Image.FromFile(System.IO.Directory.GetCurrentDirectory() + @"\IMG_SOCIOS\NO_IMG.jpg");
            //lbldireccion.Text = System.IO.Directory.GetCurrentDirectory() + @"\IMG_SOCIOS\NO_IMG.jpg";


        }

        private void FUNCION_PARAMETROS_PERFIL()
        {
            //-------------------------------
            TXT_MAIL.Visible = false;
            LINK_LBL_MAIL.Visible = true;
            LBL_MAIL__.Text = "E-MAIL:";
            //--------
        }

        private void FUNCION_ELIMINAR()
        {
            conectar_.RS_SOCIO = new Recordset();

            try
            {
                SQL_ELIMINAR = "DELETE FROM SOCIOS WHERE Nombre='" + TXTNOMBRE_SOC1.Text +
                    "'AND Apellido='" + TXTAPELLIDO_SOC1.Text + "'";
                conectar_.RS_SOCIO.Open(SQL_ELIMINAR, conectar_.CN, CursorTypeEnum.adOpenDynamic, LockTypeEnum.adLockOptimistic);

            }
            catch { }

        }

        private void FUNCION_EDITAR()
        {
            try
            {
                if (sentinela == 2 || status == 1)
                {

                    int i = this.GRILLA_SOCIOS.CurrentRow.Index;

                    TXTNOMBRE_SOC1.Text = Convert.ToString(GRILLA_SOCIOS[1, i].Value);
                    TXTAPELLIDO_SOC1.Text = Convert.ToString(GRILLA_SOCIOS[2, i].Value);
                    DATE_INICIO.Text = Convert.ToString(GRILLA_SOCIOS[3, i].Value);
                    DATE_EXPIRAR.Text = Convert.ToString(GRILLA_SOCIOS[4, i].Value);
                    TXTCUOTA.Text = Convert.ToString(GRILLA_SOCIOS[5, i].Value);
                    COMBO_PRECIOS.Text = Convert.ToString(GRILLA_SOCIOS[6, i].Value);
                    COMBO_PAGO.Text = Convert.ToString(GRILLA_SOCIOS[7, i].Value);
                    txtcomentario.Text = Convert.ToString(GRILLA_SOCIOS[8, i].Value);
                    TXTFACTURA.Text = Convert.ToString(GRILLA_SOCIOS[13, i].Value);
                    COMBO_ATENDIO.Text = Convert.ToString(GRILLA_SOCIOS[14, i].Value);
                    

                    if (GRILLA_SOCIOS[9, i].Value.ToString() != "-" && GRILLA_SOCIOS[10, i].Value.ToString() != "-")
                    {
                        Parametros2x1.Add(GRILLA_SOCIOS[9, i].Value.ToString());
                        Parametros2x1.Add(GRILLA_SOCIOS[10, i].Value.ToString());
                    }
                    else
                    {
                        if (Parametros2x1.Count >= 1)
                            Parametros2x1.Clear();
                    }

                    try
                    {
                        
                        Thread hiloImagen = new Thread(delegate()
                        {
                            try
                            {
                                picture_imagen_socio.Image = GestorGimnasio.Properties.Resources.cargando;
                                Seguridad.DownLoadImage(picture_imagen_socio, Convert.ToString(GRILLA_SOCIOS[16, i].Value));
                                lbldireccion.Text = Convert.ToString(GRILLA_SOCIOS[16, i].Value);
                            }
                            catch { }
                        });
                        hiloImagen.Start();
                    }
                    catch 
                    {
                        AUXILIAR = Convert.ToString(GRILLA_SOCIOS[16, i].Value);
                    }


                    if (sentinela == 2)
                    {
                        TXT_MAIL.Text = Convert.ToString(GRILLA_SOCIOS[15, i].Value);
                        NOMBRE_EDIT = Convert.ToString(GRILLA_SOCIOS[1, i].Value);
                        APELLIDO_EDIT = Convert.ToString(GRILLA_SOCIOS[2, i].Value);
                       // IMAGEN_ELIMINAR = System.IO.Directory.GetCurrentDirectory() + @"\IMG_SOCIOS\" + Convert.ToString(GRILLA_SOCIOS[16, i].Value);
                    }
                    else
                    {
                        LINK_LBL_MAIL.Text = Convert.ToString(GRILLA_SOCIOS[15, i].Value);
                    }
                }
            }
            catch { }
        }

        private void Delete_Grill_socios_exp()
        {
                try
                {

                    GRILLA_EXPIRADOS.DataSource = null;
                }
                catch { }
        }

        private void Get_Expirados()
        {
            GRILLA_EXPIRADOS.DataSource = dt_mysql_expirados;//el DataTable table se pasara a la grilla

            GRILLA_EXPIRADOS.Columns[0].Visible = false;
            GRILLA_EXPIRADOS.Columns[11].Visible = false;
            GRILLA_EXPIRADOS.Columns[12].Visible = false;
            GRILLA_EXPIRADOS.Columns[16].Visible = false;


            if (conectar_.RS_EXPIRADOS_.RecordCount >= 1)
            {
                lista_mail_1.Items.Clear();
                list_mail_2.Items.Clear();

                while (!(conectar_.RS_EXPIRADOS_.EOF))
                {
                    if (!(conectar_.RS_EXPIRADOS_.Fields[15].Value == "NO HAY CORREO" || conectar_.RS_EXPIRADOS_.Fields[15].Value == ""))
                    {
                        lista_mail_1.Items.Add(conectar_.RS_EXPIRADOS_.Fields[1].Value + " " + conectar_.RS_EXPIRADOS_.Fields[2].Value + " -" + conectar_.RS_EXPIRADOS_.Fields[15].Value);
                        list_mail_2.Items.Add(conectar_.RS_EXPIRADOS_.Fields[1].Value + " " + conectar_.RS_EXPIRADOS_.Fields[2].Value);
                    }
                    conectar_.RS_EXPIRADOS_.MoveNext();
                }

            }

            if (GRILLA_EXPIRADOS.RowCount >= 2)
            {
                STRIP_LABEL_EXPIRADO.Text = "EXISTEN " + Convert.ToString(GRILLA_EXPIRADOS.Rows.Count - 1) + " SOCIO EXPIRADOS";
                lbl_socios_exp_2.Text = Convert.ToString(GRILLA_EXPIRADOS.Rows.Count - 1) + " SOCIO EXPIRADOS";
            }
            else
            {
                STRIP_LABEL_EXPIRADO.Text = "0 SOCIOS EXPIRADOS";
                lbl_socios_exp_2.Text = "0 SOCIOS EXPIRADOS";
            }

            if (lista_mail_1.Items.Count == 0)
            {
                lista_mail_1.Items.Add("NO EXISTEN CORREOS A ENVIAR");
            }

            TimeLineExpirados();
        }

        private void FUNCION_SOCIOS_EXPIRADOS()
        {
            try
            {
                dt_mysql_expirados.Clear();
                if (conectar_.mysqlconection.State == 0) { conectar_.mysqlconection.Open(); }
                else
                {
                    conectar_.mysqlconection.Close();
                    conectar_.mysqlconection.Open();
                }

                conectar_.mysql_data_adapter_exp = new OleDbDataAdapter(SQL_EXPIRADOS, conectar_.mysqlconection);//conecta el adapter con la sentencia sql
                conectar_.ds_oledb_exp = new DataTable();//tabla es = nueva tabla
                conectar_.mysql_data_adapter_exp.Fill(dt_mysql_expirados);//el adaptador con toda la base de sql se le llenara en la tabla

                conectar_.RS_EXPIRADOS_ = new Recordset();
                conectar_.RS_EXPIRADOS_.Open(SQL_EXPIRADOS, conectar_.CN, CursorTypeEnum.adOpenDynamic, LockTypeEnum.adLockOptimistic);

                conectar_.mysqlconection.Close();

            }
            catch
            {
               // GestorGimnasio.CLASES.Informe_Errores.Enviar_Error(ex.Message);
            }

        }

        private void REGISTRAR_GESTOR()
        {
            try
            {
                RegistryKey registro = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Run", true);

                if (registro.GetValue("ZONE_FITNESS") == null)
                  {
                      registro.CreateSubKey("ZONE_FITNESS");
                      registro.SetValue("ZONE_FITNESS", Convert.ToString(System.IO.Directory.GetCurrentDirectory() + @"\gestor de gimnasios 3.0.exe"));
                  }      
            }

            catch { }
        }

        private void GetNumeber_Expirado()
        {
            try
            {
                conectar_.RS_EXPIRADOS_ = new Recordset();
                conectar_.RS_EXPIRADOS_.Open(SQL_EXPIRADOS, conectar_.CN, CursorTypeEnum.adOpenDynamic, LockTypeEnum.adLockOptimistic);
                if (conectar_.RS_EXPIRADOS_.RecordCount >= 1)
                {
                    STRIP_LABEL_EXPIRADO.Text = "EXISTEN " + conectar_.RS_EXPIRADOS_.RecordCount + " SOCIO EXPIRADOS";
                    lbl_socios_exp_2.Text = conectar_.RS_EXPIRADOS_.RecordCount + " SOCIO EXPIRADOS";
                }

                conectar_.RS_EXPIRADOS_.Close();
            }
            catch { }
        }

        private void TimeLineExpirados()
        {

            for (int i = 0; i < GRILLA_EXPIRADOS.RowCount - 1; i++)
            {

                string fechas_ex = GRILLA_EXPIRADOS[4, i].Value.ToString();
                DateTime fecha = Convert.ToDateTime(fechas_ex);

                TimeSpan TiempoDiferencial = DateTime.Now - fecha;
                var day = TiempoDiferencial.Days;
                var month = TiempoDiferencial.TotalDays;
                
                if ( day <= -1 || day >= 5 && day <= 6)
                {
                    GRILLA_EXPIRADOS.Rows[i].DefaultCellStyle.BackColor = Color.Green;
                    GRILLA_EXPIRADOS.Rows[i].DefaultCellStyle.ForeColor = Color.Black;
                }
                else if (day >= 365)
                {
                    GRILLA_EXPIRADOS.Rows[i].DefaultCellStyle.BackColor = Color.DarkGray;
                    GRILLA_EXPIRADOS.Rows[i].DefaultCellStyle.ForeColor = Color.Red;
                }
                else if (day >= 62 && day <= 364)
                {
                    GRILLA_EXPIRADOS.Rows[i].DefaultCellStyle.BackColor = Color.DarkBlue;
                    GRILLA_EXPIRADOS.Rows[i].DefaultCellStyle.ForeColor = Color.Yellow;
                }
                else if (day >= 31 && day <= 62)
                {
                    GRILLA_EXPIRADOS.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                    GRILLA_EXPIRADOS.Rows[i].DefaultCellStyle.ForeColor = Color.DarkBlue;
                }
                else if ( day >= 7 && day <= 30 || day < 31)
                {
                    GRILLA_EXPIRADOS.Rows[i].DefaultCellStyle.BackColor = Color.Orange;
                    GRILLA_EXPIRADOS.Rows[i].DefaultCellStyle.ForeColor = Color.Black;
                }
 
            }

            

            picexp1.BackColor = Color.Green;
            picexp2.BackColor = Color.Orange;
            picexp3.BackColor = Color.Red;
            picexp4.BackColor = Color.DarkGray;
            picexp5.BackColor = Color.DarkBlue;
        }

        private void SociosExpiradosPorDia()
        {
            try
            {
                if (HiloExpiradoPorDia.IsAlive) Hilo_expirado.Abort();
            }
            catch { }

            HiloExpiradoPorDia = new Thread(delegate()
            {

                try
                {
                    var Cont = 0;
                    var SQL = "SELECT * FROM SOCIOS WHERE Fecha_expiracion = DATE()";

                    conectar_.RS_EXPIRADOS_ = new Recordset();
                    conectar_.RS_EXPIRADOS_.Open(SQL, conectar_.CN, CursorTypeEnum.adOpenDynamic, LockTypeEnum.adLockOptimistic);
                    if (conectar_.RS_EXPIRADOS_.RecordCount >= 1)
                    {

                        while (!conectar_.RS_EXPIRADOS_.EOF)
                        {
                            Cont++;
                            conectar_.RS_EXPIRADOS_.MoveNext();
                        }

                        LinkSociosExpiradosDia.Text = "Este Dia Expiran " + Cont + " Socios ";
                    }
                    else
                    {
                        LinkSociosExpiradosDia.Text = "Ningùn Socio Expirado Este dia";
                    }

                    conectar_.RS_EXPIRADOS_.Close();
                }
                catch { 

                    LinkSociosExpiradosDia.Text = "Zone Fitness Gym ...";
                }

            });
            HiloExpiradoPorDia.Start();
        }



        private void PRINCIPAL_Load(object sender, EventArgs e)
        {
            version_gestor ver = new version_gestor();
            VaciarGrillaSocios();
           
            //BuferGrid.DoubleBuffered(GRILLA_SOCIOS , true);
            GRILLA_SOCIOS.DoubleBuffered(true);
            Hilo_Socio = new Thread(delegate()
            {
                 GET_GRILL_SOCIOS();              
            });
            Hilo_Socio.Priority = ThreadPriority.Highest;
            Hilo_Socio.Start();
            EMAIL.Loop(GRILLA_SOCIOS, new Point(360,115));
            Time_Socios.Enabled = true;

            this.Opacity = 0.92;
            this.Text = ver.Nombre + " | USUARIO ACTUAL " + LOGIN.Usuario;

            conectar_.RS_SOCIO = new Recordset();
            conectar_.RS_CONTADOR = new Recordset();
            conectar_.RS_SOCIO_AGREGAR = new Recordset();
            conectar_.RS_SOCIO_EDITAR = new Recordset();
            conectar_.RS_EMPLEADOS = new Recordset();


           // Thread h = new Thread(delegate()
           // {
                GetNumeber_Expirado();
                SociosExpiradosPorDia();
           // });
           // h.Start();

             CONTADOR_SOCIOS();
             GRILLA_POS_INI();

            DATE_EXPIRAR.Format = DateTimePickerFormat.Custom;
            DATE_INICIO.Format = DateTimePickerFormat.Custom;
            DATE_INICIO.CustomFormat = "yyy/MM/dd";
            DATE_EXPIRAR.CustomFormat = "yyy/MM/dd";
            DATE_EXP_MEMBRESIA.Format = DateTimePickerFormat.Custom;
            DATE_EXP_MEMBRESIA.CustomFormat = "yyy/MM/dd";
            lbl_socios_exp_2.Text = "CARGANDO SOCIOS...";

            if (OPCIONES.PRIVILEGIOS_ESTATUS == 1)
            {
                strip_eliminar.Enabled = true;
                strip_editar.Enabled = true;
                aToolStripMenuItem1.Visible = true;
                button5.Enabled = true;
                //RESPALDO_TOOL.Visible = true;
            }
            else if (OPCIONES.PRIVILEGIOS_ESTATUS == 2)
            {
                strip_eliminar.Enabled = false;
                strip_editar.Enabled = false;
                aToolStripMenuItem1.Visible = false;
                button5.Enabled = false;
               // RESPALDO_TOOL.Visible = false;
            }

            lbl_aviso_correo.Visible = false;
            lista_mail_1.HorizontalScrollbar = true;
            picture_imagen_socio.SizeMode = PictureBoxSizeMode.StretchImage;
           // FuncionVistaPrevia();
            Checkupdate();


        }

        private void Checkupdate()
        {
            GestorGimnasio.CLASES.CheckUpdate update = new GestorGimnasio.CLASES.CheckUpdate(this);
            update.Setconfig("https://dl.dropbox.com/u/75344773/Zonefitness_update/", "update.xml", false);
            update.Iniciar();
        }

        private void FuncionVistaPrevia()
        {
            FrmVistaPrevia.Size = new Size(265, 115);
            FrmVistaPrevia.BringToFront();
            GRILLA_SOCIOS.Controls.Add(FrmVistaPrevia);
            FrmVistaPrevia.Hide();
            imagenVp.Size = new Size(70, 70);
            imagenVp.Location = new Point(10, 30);
            imagenVp.BackColor = Color.Black;
            imagenVp.SizeMode = PictureBoxSizeMode.StretchImage;
            linkNombre.Location = new Point(80, 30);
            linkNombre.Text = "Nombre";
            linkNombre.AutoSize = true;
            linkApellido.Location = new Point(80, 47);
            linkApellido.Text = "Apellido";
            linkApellido.AutoSize = true;
            linkFechaExpiracion.Location = new Point(80, 62);
            linkFechaExpiracion.Text = "Expira";
            linkFechaExpiracion.AutoSize = true;
            linkcuota.Location = new Point(80, 77);
            linkcuota.Text = "Cuota";
            linkcuota.AutoSize = true;
            FrmVistaPrevia.Controls.Add(imagenVp);
            FrmVistaPrevia.Controls.Add(linkNombre);
            FrmVistaPrevia.Controls.Add(linkApellido);
            FrmVistaPrevia.Controls.Add(linkFechaExpiracion);
            FrmVistaPrevia.Controls.Add(linkcuota);
        }

        private void tiempo_real_Tick(object sender, EventArgs e)
        {
            STRIP_LABEL_HORA.Text = "HORA : " + DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString() + ":" + DateTime.Now.Second.ToString();
            STRIP_LABEL_FECHA.Text = "FECHA : " + DateTime.Now.Day.ToString() + "/" + DateTime.Now.Month.ToString() + "/" + DateTime.Now.Year.ToString();
            CONTADOR_SOCIOS();
        }

        private void strip_agregar_Click(object sender, EventArgs e)
        {
            
                GRUPO_SOCIOS.Text = "AGREGAR UN NUEVO SOCIO";
                CMD_ENVIAR.Text = "GUARDAR";
                CMD_ENVIAR.Visible = true;

                cmd_buscar_imagen.Visible = true;

                if (Parametros2x1.Count >= 1)
                    Parametros2x1.Clear();

                try{
                    BORRAR_CASILLAS_AGREGAR();
                    picture_imagen_socio.Image = GestorGimnasio.Properties.Resources.NO_IMG1;
                }
                catch {}

            
                GRILLA_POS_FIN();
                sentinela = 1;

                LINK_LBL_MAIL.Visible = false;
                LBL_MAIL__.Text = "E-MAIL(OPCIONAL):";
                TXT_MAIL.Visible = true;

                try
                {
                    if (HiloSocioP.IsAlive)
                    {
                        HiloSocioP.Abort();
                        TimerParamSocios.Enabled = false;
                    }
                    
                }
                catch { }

                HiloSocioP = new Thread(delegate()
                {
                    GetParametrosInicialesSocios();
                });
                HiloSocioP.Start();
                TimerParamSocios.Enabled = true;
 
        }

        private void STRIP_CMD_RESTAURAR_Click(object sender, EventArgs e)
        {

            GRILLA_POS_INI();

            /*SUB PROCESO SOCIOS */
            VaciarGrillaSocios();

            try
            {
                if (Hilo_Socio.IsAlive) Hilo_Socio.Abort();
            }
            catch { }

            Hilo_Socio = new Thread(delegate()
            {
                GET_GRILL_SOCIOS();
            });
            EMAIL.Loop(GRILLA_SOCIOS, new Point(350, 115));
            Hilo_Socio.Start();
            Time_Socios.Enabled = true;

        }

        private void CMD_ENVIAR_Click(object sender, EventArgs e)
        {
            string ImagenSocio = Seguridad.ObtenerImagen(lbldireccion.Text);
            
            if (sentinela == 1)
            {
               
                HiloGuardarFtp = new Thread(delegate()
                {
                    if(Seguridad.ExisteImagen(ImagenSocio) == false)
                         Seguridad.UploadImagen(lbldireccion.Text);
                });
                HiloGuardarFtp.Start();
                BloqueoGuardando();
                TiempoRespuestaFtp.Enabled = true;
            }
            else if (sentinela == 2)
            {
                HiloGuardarFtp = new Thread(delegate()
                {
                    if (Seguridad.ExisteImagen(ImagenSocio) == false)
                        Seguridad.UploadImagen(lbldireccion.Text);
                });
                HiloGuardarFtp.Start();
                BloqueoGuardando();
                TiempoRespuestaFtp.Enabled = true;
            }
           
        }

        private void HiloGuardar() 
        {

            string ImagenSocio = Seguridad.ObtenerImagen(lbldireccion.Text);

            conectar_.RS_EXISTE_SOCIO = new Recordset();
            try
            {
                conectar_.RS_EXISTE_SOCIO.Open(SQL_SOCIOS_CONST, conectar_.CN, CursorTypeEnum.adOpenDynamic, LockTypeEnum.adLockOptimistic);
                conectar_.RS_EXISTE_SOCIO.MoveFirst();
                while (!(conectar_.RS_EXISTE_SOCIO.EOF))
                {

                    string Nombre_ = conectar_.RS_EXISTE_SOCIO.Fields[1].Value;
                    string Apellido_ = conectar_.RS_EXISTE_SOCIO.Fields[2].Value;

                    if (EXISTE_REGISTRADO.ExisteSocio(Nombre_, Apellido_, TXTNOMBRE_SOC1.Text, TXTAPELLIDO_SOC1.Text) == true)
                    {
                        MessageBox.Show("Este Socio ya existe en la base de datos.\n Socio registrado el:\n\n\t\t"
                            + conectar_.RS_EXISTE_SOCIO.Fields[3].Value, "Socio registrado");
                        return;
                    }
                    else
                    {
                        conectar_.RS_EXISTE_SOCIO.MoveNext();
                    }

                }
            }
            catch { }

            try
            {
                if (TXT_MAIL.Text == "" || TXT_MAIL.Text == "@hotmail.com") { TXT_MAIL.Text = "NO HAY CORREO"; }
                if (COMBO_PRECIOS.Text == "" || COMBO_PRECIOS.Text == "SELECCIONAR PRECIO") { COMBO_PRECIOS.Text = "PRECIO AGREGADO MANUAL"; }

                try
                {
                    if (ImagenSocio != "NO_IMG.jpg" && ImagenSocio != "")
                    {
                        GestorGimnasio.CLASES.Ftp.rutaArchivo = lbldireccion.Text;
                        Thread hiloftp = new Thread(GestorGimnasio.CLASES.Ftp.upload);
                        hiloftp.Start();
                    }
                }
                catch { }

                GUARDAR_SOCIO(ImagenSocio);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void HiloEditar()
        {

              string ImagenSocio = Seguridad.ObtenerImagen(lbldireccion.Text);
               conectar_.RS_SOCIO = new Recordset();

                try
                {
                    if (Parametros2x1.Count == 0)
                    {
                        Parametros2x1.Add("-");
                        Parametros2x1.Add("-");
                    }

                    SQL_EDITAR_SOCIO = "UPDATE  SOCIOS SET Nombre='" + TXTNOMBRE_SOC1.Text +
                        "',Apellido='" + TXTAPELLIDO_SOC1.Text +
                        "',Fecha_inicio='" + DATE_INICIO.Text +
                        "',Fecha_expiracion='" + DATE_EXPIRAR.Text +
                        "',Cuota='" + Convert.ToDecimal(TXTCUOTA.Text) +
                        "',Precio='" + COMBO_PRECIOS.Text +
                        "',Pago='" + COMBO_PAGO.Text +
                        "',Comentario='" + txtcomentario.Text +
                        "',Nombre_socio2 ='" +  Parametros2x1[0].ToString() +
                        "',Apellido_socio2 ='" + Parametros2x1[1].ToString() +
                        "',Numero_factura='" + Convert.ToDecimal(TXTFACTURA.Text) +
                        "',Atendio ='" + COMBO_ATENDIO.Text +
                        "',Imagen ='" + ImagenSocio +
                        "',E_mail='" + TXT_MAIL.Text + "'WHERE Nombre='" + NOMBRE_EDIT + "'AND Apellido='" + APELLIDO_EDIT + "'";

                    conectar_.RS_SOCIO.Open(SQL_EDITAR_SOCIO, conectar_.CN, CursorTypeEnum.adOpenDynamic, LockTypeEnum.adLockOptimistic);

                    MessageBox.Show("SOCIO EDITADO CON EXITO", "EXITO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    VaciarGrillaSocios();
                    Hilo_Socio = new Thread(delegate()
                    {
                        GET_GRILL_SOCIOS();
                    });
                    EMAIL.Loop(GRILLA_SOCIOS, new Point(350, 115));
                    Hilo_Socio.Start();
                    Time_Socios.Enabled = true;
                }
                catch (Exception EX) { MessageBox.Show(EX.Message); }
            }

        private void BloqueoGuardando()
        {
            STRIP_HERRAMIENTAS.Enabled = false;
            toolStrip1.Enabled = false;
            CMD_ENVIAR.Enabled = false;
            tabControl1.Enabled = false;
        }

        private void DesbloqueoGuardando()
        {
            STRIP_HERRAMIENTAS.Enabled = true;
            toolStrip1.Enabled = true;
            CMD_ENVIAR.Enabled = true;
            tabControl1.Enabled = true;
        }
         
        private void tiempo_add_Tick(object sender, EventArgs e)
        {

            if (TXTNOMBRE_SOC1.Text == "" || TXTAPELLIDO_SOC1.Text == ""
                || DATE_INICIO.Text == "" || DATE_EXPIRAR.Text == "" 
                || COMBO_PAGO.Text == "" || COMBO_PAGO.Text == "SELECCIONAR PAGO" 
                || COMBO_ATENDIO.Text == "" || TXTCUOTA.Text == "" || TXTFACTURA.Text == "")
            {
                CMD_ENVIAR.Enabled = false;
            }
            else
            {
                CMD_ENVIAR.Enabled = true;
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            VaciarGrillaSocios();
            Hilo_Socio = new Thread(delegate()
            {
                GET_GRILL_SOCIOS();
            });
            EMAIL.Loop(GRILLA_SOCIOS, new Point(350, 115));
            Hilo_Socio.Start();
            Time_Socios.Enabled = true;
        }

        private void COMBO_PRECIOS_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                conectar_.PRECIOS_.MoveFirst();
                while (!(conectar_.PRECIOS_.EOF))
                {
                    if (COMBO_PRECIOS.Text == conectar_.PRECIOS_.Fields[1].Value)
                    {
                        TXTCUOTA.Text = conectar_.PRECIOS_.Fields[2].Value;
                        break;
                    }
                    conectar_.PRECIOS_.MoveNext();
                }
            }
            catch { }
        }

        private void strip_editar_Click(object sender, EventArgs e)
        {
            sentinela = 2;

            cmd_buscar_imagen.Visible = true;

            GRUPO_SOCIOS.Text = "EDITAR SOCIOS";

            LINK_LBL_MAIL.Visible = false;
            LBL_MAIL__.Text = "E-MAIL(OPCIONAL):";
            TXT_MAIL.Visible = true;

            CMD_ENVIAR.Text = "GUARDAR";
            CMD_ENVIAR.Visible = true;

            BORRAR_CASILLAS_AGREGAR();
            GRILLA_POS_FIN();
            FUNCION_EDITAR();

            try
            {
                if (HiloSocioP.IsAlive)
                {
                    HiloSocioP.Abort();
                }
            }
            catch { }

            HiloSocioP = new Thread(delegate()
            {
                GetParametrosInicialesSocios();
            });
            HiloSocioP.Start();
            TimerParamSocios.Enabled = true;
           
        }

        private void GRILLA_SOCIOS_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            FUNCION_EDITAR();
        }

        private void strip_perfil_Click(object sender, EventArgs e)
        {
            GRUPO_SOCIOS.Text = "PERFIL SOCIO";
            sentinela = 1;
            status = 1;
            GRILLA_POS_FIN();
            FUNCION_EDITAR();
            TXT_MAIL.Visible = false;
            LINK_LBL_MAIL.Visible = true;
            LBL_MAIL__.Text = "E-MAIL";
            CMD_ENVIAR.Visible = false;
            cmd_buscar_imagen.Visible = false;
        }

        private void strip_eliminar_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult respuesta = MessageBox.Show("¿DESEA ELIMINAR ESTE SOCIO?", "ELIMINAR", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                if (respuesta == DialogResult.Yes)
                {
                    status = 1;
                    FUNCION_EDITAR();
                    FUNCION_ELIMINAR();
                    MessageBox.Show("SOCIO ELIMINADO CON EXITO....", "ELIMINADO");
                    VaciarGrillaSocios();
                    Hilo_Socio = new Thread(delegate()
                    {
                        GET_GRILL_SOCIOS();
                    });
                    EMAIL.Loop(GRILLA_SOCIOS, new Point(350, 115));
                    Hilo_Socio.Start();
                    Time_Socios.Enabled = true;
                    CONTADOR_SOCIOS();
                }
                else
                {
                    return;
                }
            }
            catch (Exception EX) { MessageBox.Show("NO SE HA SELECCIONADO AL SOCIO QUE DESEA ELIMINAR\n\n" + EX.Message, "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Error); }

        }

        private void check_seleccionar_todo_CheckedChanged(object sender, EventArgs e)
        {
            if (check_seleccionar_todo.Checked == true)
            {
                for (int i = 0; i < lista_mail_1.Items.Count; i++)
                {
                    lista_mail_1.SetItemChecked(i, true);
                }
            }
            else
            {
                for (int i = 0; i < lista_mail_1.Items.Count; i++)
                {
                    lista_mail_1.SetItemChecked(i, false);
                }
            }
        }

        private void CMD_ENVIAR_CORREO_Click(object sender, EventArgs e)
        {
            EMAIL.Loop(lista_mail_1, new Point(80, 10));
            Hilo = new Thread(new ThreadStart(Envio_Hilo_Correo));
            Hilo.Priority = ThreadPriority.Highest;
            Hilo.Start();
        }

        private void Envio_Hilo_Correo()
        {
            EMAIL.EnviarCorreoSmtp(lista_mail_1);
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                switch (tabControl1.SelectedIndex)
                {

                    case 0:
                    FUNCION_EDITAR();
                    GRILLA_POS_INI();
                    VaciarGrillaSocios();

                    try
                    {
                        if (Hilo_Socio.IsAlive) Hilo_Socio.Abort();
                    }
                    catch { }


                    Hilo_Socio = new Thread(delegate()
                    {
                        GET_GRILL_SOCIOS();
                    });
                    EMAIL.Loop(GRILLA_SOCIOS, new Point(350, 115));
                    Hilo_Socio.Start();
                    Time_Socios.Enabled = true;
                    break;
                    case 1:
                    Delete_Grill_socios_exp();
                    try
                    {
                        if (Hilo_expirado.IsAlive) Hilo_expirado.Abort();
                    }
                    catch { }

                    Hilo_expirado = new Thread(delegate()
                    {
                        FUNCION_SOCIOS_EXPIRADOS();
                    });
                    EMAIL.Loop(GRILLA_EXPIRADOS, new Point(200, 30));
                    Hilo_expirado.Start();
                    Time_Expirados.Enabled = true;
                    break;
                    case 2:
                    try
                    {
                        if (HiloTienda.IsAlive) HiloTienda.Abort();
                    }
                    catch { }

                    HiloTienda = new Thread(delegate()
                    {
                        GetEstadisticasTienda();
                        FUNCION_TIENDA();
                    });
                    HiloTienda.Start();
                    TiempoTienda.Enabled = true;
                        break;
                    case 3:
                        //no hace nada ...
                        break;
                    default:
                        break;

                }
            }
            catch { }
            
        }

        private void ChartSocios()
        {
         
        }

        private void STRIP_PROPIEDADES_Click(object sender, EventArgs e)
        {
            PROPIEDADES propiedades = new PROPIEDADES();
            propiedades.Show();
        }

        private void CMD_ENVIAR_SOCIO_EXP_2_Click(object sender, EventArgs e)
        {

            try
            {
                conectar_.RS_EXPIRADOS_ = new Recordset();

                int i = this.GRILLA_EXPIRADOS.CurrentRow.Index;

                if (!(Convert.ToString(GRILLA_EXPIRADOS[4, i].Value) == DATE_EXP_MEMBRESIA.Text))
                {
                    SQL_EDITAR_EXPIRADOS = "UPDATE  SOCIOS SET Fecha_expiracion='" + DATE_EXP_MEMBRESIA.Text +
                     "'WHERE Nombre='" + Convert.ToString(GRILLA_EXPIRADOS[1, i].Value) + "'AND Apellido='" + Convert.ToString(GRILLA_EXPIRADOS[2, i].Value) + "'";

                    conectar_.RS_EXPIRADOS_.Open(SQL_EDITAR_EXPIRADOS, conectar_.CN, CursorTypeEnum.adOpenDynamic, LockTypeEnum.adLockOptimistic);
                    MessageBox.Show("LA MEMBRESIA HA SIDO RENOVADA CON EXITO", "EXITO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    FUNCION_SOCIOS_EXPIRADOS();
                }
                else
                {
                    MessageBox.Show("LA FECHA ASIGANADA ES IGUAL A LA FECHA EXPIRADA", "FALLO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
            catch
            {
                MessageBox.Show("ERROR AL CAMBIAR LA MEMBRESIA", "ERR", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void STRIP_TXT_BUSCAR_TextChanged(object sender, EventArgs e)
        {

            if (BuscarSocio.ParametrosBusqueda.Count >= 1)
                BuscarSocio.ParametrosBusqueda.Clear();

            BuscarSocio.ParametrosBusqueda.Add("Nombre");
            BuscarSocio.ParametrosBusqueda.Add("Apellido");
            BuscarSocio.ParametrosBusqueda.Add("Nombre_socio2");
            BuscarSocio.ParametrosBusqueda.Add("Apellido_socio2");
            BuscarSocio.ParametrosBusqueda.Add("Cuota");
            BuscarSocio.ParametrosBusqueda.Add("Precio");
            BuscarSocio.ParametrosBusqueda.Add("Pago");


            BuscarSocio.Cadena = STRIP_TXT_BUSCAR.Text;
            BuscarSocio.GetBusqueda();
            RowsColorSocios();

            int total = BuscarSocio.TotalBusqueda();

            if (total == 0)
            {
                toolcmd_buscar.Enabled = true;
                toolcmd_buscar.Text = "Ningun Resultado, Click busqueda avanzada";
            }
            else
            {
                toolcmd_buscar.Enabled = false;
                toolcmd_buscar.Text = "Resultados: " + total + " Socios";
            }
            
        }

        private void txtbuscar_exp_TextChanged(object sender, EventArgs e)
        {
            //Buscador.Buscar_Caracter(GRILLA_EXPIRADOS, txtbuscar_exp);
        }

        private void cmdcalcularimc_Click(object sender, EventArgs e)
        {
            
            decimal peso;
            decimal altura;
            decimal resultado;
            decimal altura2;

            peso = Convert.ToDecimal(txtpeso.Text);
            altura = (Convert.ToDecimal(txtaltura.Text) * 1) / 100;
            altura2 = altura * altura;
            resultado = peso / altura2;
            txtresultado_imc.Text = " SU INDICE DE MASA CORPORAL ES = " + Math.Round(resultado , 2);
            txtresultado_imc.ReadOnly = true;
        }

        private void cmdclimpiarimc_Click(object sender, EventArgs e)
        {
            txtpeso.Text = "";
            txtaltura.Text = "";
            txtresultado_imc.Text = "";
        }

        private void TXTFACTURA_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsNumber(e.KeyChar))
            {
                e.Handled = false;
            }
            if (Char.IsLetter(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void TXTCUOTA_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsNumber(e.KeyChar) || Char.IsPunctuation(e.KeyChar))
            {
                e.Handled = false;
            }
            if (Char.IsLetter(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void comboBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsNumber(e.KeyChar) || Char.IsPunctuation(e.KeyChar))
            {
                e.Handled = false;
            }
            if (Char.IsLetter(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtpeso_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsNumber(e.KeyChar))
            {
                e.Handled = false;
            }
            if (Char.IsLetter(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtaltura_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsNumber(e.KeyChar))
            {
                e.Handled = false;
            }
            if (Char.IsLetter(e.KeyChar))
            {
                e.Handled = true;
            }
        }



        private void LINK_LBL_MAIL_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start("mailto:" + LINK_LBL_MAIL.Text + "?subject=" + correo_titulos.subject__);
            }
            catch{}
        }

        private void PRINCIPAL_Activated(object sender, EventArgs e)
        {
            if (OPCIONES.PRIVILEGIOS_ESTATUS == 1)
            {
                strip_eliminar.Enabled = true;
                strip_editar.Enabled = true;
                button5.Enabled = true;
                //RESPALDO_TOOL.Visible = true;
            }
            else if (OPCIONES.PRIVILEGIOS_ESTATUS == 2)
            {
                strip_eliminar.Enabled = false;
                strip_editar.Enabled = false;
                button5.Enabled = false;
                //RESPALDO_TOOL.Visible = false;
            }
        }

        private void toolcalendario_Click(object sender, EventArgs e)
        {
            calendario calendario = new calendario();
            calendario.Show();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            conectar_.RS_TIENDA = new Recordset();

            try
            {
                SQL_TIENDA = "SELECT * FROM TIENDA";
                conectar_.RS_TIENDA.Open(SQL_TIENDA, conectar_.CN, CursorTypeEnum.adOpenDynamic, LockTypeEnum.adLockOptimistic);

                conectar_.RS_TIENDA.AddNew();
                {
                    conectar_.RS_TIENDA.Fields[0].Value = COMBO_PRODUCTO.Text;
                    conectar_.RS_TIENDA.Fields[1].Value = combocantidad_tienda.Text;
                    conectar_.RS_TIENDA.Fields[2].Value = comboBox3.Text;
                    conectar_.RS_TIENDA.Fields[3].Value = combovendedor_tienda.Text;
                    conectar_.RS_TIENDA.Fields[4].Value = Convert.ToString( DateTime.Now.Day.ToString() + "/" + DateTime.Now.Month.ToString() + "/" + DateTime.Now.Year.ToString());
                    conectar_.RS_TIENDA.Update();
                }
                MessageBox.Show("PRODUCTO REGISTRADO CON EXITO", "EXITO", MessageBoxButtons.OK);
                HiloTienda = new Thread(delegate()
                {
                    FUNCION_TIENDA();
                });
                HiloTienda.Start();
                TiempoTienda.Enabled = true;
            }
            catch { }
        }

        private void pAGOSMENSUALESToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 fomulario2 = new Form2();
            fomulario2.Show();
        }
        

        public static void ExportDataGridViewTo_Excel12(DataGridView dataGridView1)
        {

          /*  int k = 1;

            Microsoft.Office.Interop.Excel._Application app = new Microsoft.Office.Interop.Excel.Application();

            Microsoft.Office.Interop.Excel._Workbook workbook = app.Workbooks.Add(Type.Missing);

            Microsoft.Office.Interop.Excel._Worksheet worksheet = null;

            app.Visible = true;

            try
            {

                try
                {
                    worksheet = workbook.Sheets["Hoja1"];
                }
                catch
                {
                    worksheet = workbook.Sheets["Sheet1"];
                }

                worksheet = workbook.ActiveSheet;


                worksheet.Name = titulos_principal.TITULO_PRINCIPAL;


                for (int i = 1; i < dataGridView1.Columns.Count + 1; i++ , k++)
                {
                    switch (i)
                    {

                        case 1:
                            k -= 1;
                            break;
                        case 8:
                            k -= 1;
                            break;
                        case 9:
                            k -= 1;
                            break;
                        case 10:
                            k -= 1;
                            break;
                        case 11:
                            k -= 1;
                            break;
                        case 12:
                            k -= 1;
                            break;
                        case 13:
                            k -= 1;
                            break;
                        case 17:
                            k -= 1;
                            break;
                        default:
                            worksheet.Cells[1, k] = dataGridView1.Columns[i - 1].HeaderText;
                            break;
                    }

                }



                for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                {
                    k = 0;
                    for (int j = 0; j < dataGridView1.Columns.Count - 1; j++ ,k++)
                    {
                        switch (j)
                        {
                            case 0:
                                k -= 1;
                                break;
                            case 8:
                                k -= 1;
                                break;
                            case 9:
                                k -= 1;
                                break;
                            case 10:
                                k -= 1;
                                break;
                            case 11:
                                k -= 1;
                                break;
                            case 12:
                                k -= 1;
                                break;
                            case 13:
                                k -= 1;
                                break;
                            case 17:
                                k -= 1;
                                break;
                            default:
                                worksheet.Cells[i + 2, k + 1] = dataGridView1.Rows[i].Cells[j].Value.ToString();
                                break;
                        }
                     
                    }

                }
            }
            catch { MessageBox.Show("PROBLEMAS CON COMPATIBILIDAD DE MICROSOFT OFFICES, ASEGURECE QUE USTED TENGA INSTALADO OFFICES 2010", "ERROR OFFICES", MessageBoxButtons.OK, MessageBoxIcon.Error); }*/

        }

        private void CMD_REPORTE_EXP_Click(object sender, EventArgs e)
        {
            ExportDataGridViewTo_Excel12(GRILLA_EXPIRADOS);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ExportDataGridViewTo_Excel12(GRILLA_TIENDA);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            DialogResult diag = MessageBox.Show("ADVERTENCIA SE ELIMINARA TODO REGISTRO DE VENTAS EN TIENDA , ANTES DE ELIMINAR CREAR UN REPORTE DE LOS PRODUCTOS VENDIDOS (PRECIONE CANCELAR PARA CREAR EL REPORTE Y ACEPTAR PARA ELIMINAR)", "ELIMINAR", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (diag == DialogResult.Yes)
            {
                COMBO_PRODUCTO.Text = "";
                comboBox3.Text = "";
                combocantidad_tienda.Text = "";
                combovendedor_tienda.Text = "";
                //
                conectar_.RS_TIENDA = new Recordset();

                try
                {
                    conectar_.RS_TIENDA.Open(SQL_TIENDA, conectar_.CN, CursorTypeEnum.adOpenDynamic, LockTypeEnum.adLockOptimistic);
                    conectar_.RS_TIENDA.MoveFirst();
                    while (!(conectar_.RS_TIENDA.EOF))
                    {
                        conectar_.RS_TIENDA.Delete();
                        conectar_.RS_TIENDA.MoveNext();
                    }
                }
                catch { }
                FUNCION_TIENDA();
            }
            else
            {
                ExportDataGridViewTo_Excel12(GRILLA_TIENDA);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("calc.exe");
        }

        private void mONTOTOTALToolStripMenuItem_Click(object sender, EventArgs e)
        {
          /*  decimal suma =0;
            decimal total = 0;
            try
            {

                for (int i = 0; i < GRILLA_SOCIOS.RowCount - 1; i++)
                {
                    suma = Convert.ToDecimal(GRILLA_SOCIOS[5, i].Value);
                    total += suma;
                }

            }
            catch { }
            MessageBox.Show("EL MONTO TOTAL DE LOS SOCIOS REGISTRADOS ES = $" + total, "TOTAL ADMIN");*/

            GestorGimnasio.MontoTotal monto = new GestorGimnasio.MontoTotal();
            monto.GrillaInformacion = GRILLA_SOCIOS;
            monto.Show();
        }

        private void sALIRToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OPCIONES opt = new OPCIONES();
            opt.salir();
        }

        private void stripayuda_Click(object sender, EventArgs e)
        {
            AYUDA ayuda = new AYUDA();
            ayuda.Show();
            return;
        }

        private void rUTINASToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RUTINAS RUTINAS = new RUTINAS();
            RUTINAS.Show();
        }

        private void tabControl1_MouseLeave(object sender, EventArgs e)
        {
            //Cursor.Show();
        }


        private void actualizar_Tick(object sender, EventArgs e)
        {
            tiempo_cursor += 1000;
            if (tiempo_cursor == 190000)
            {
                try
                {
                    Time_Socios.Enabled = true;
                    FUNCION_SOCIOS_EXPIRADOS();
                    strip_lbl_actualizar.Text = "ACTUALIZADO: " + DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString() + ":" + DateTime.Now.Second.ToString();
                }
                catch { 
                    strip_lbl_actualizar.Text = "ERROR AL ACTUALIZAR";
                }
                tiempo_cursor = 0;
            }

        }

        private void sOCIOSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                ExportDataGridViewTo_Excel12(GRILLA_SOCIOS);
            }
            catch { MessageBox.Show("SE NECESITA DE MISCROSOFT OFFICES 2007 EN ADELANTE", "ERROR MICROSOFT", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void sOCIOSEXPIRADOSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                ExportDataGridViewTo_Excel12(GRILLA_EXPIRADOS);
            }
            catch { MessageBox.Show("SE NECESITA DE MISCROSOFT OFFICES 2007 EN ADELANTE", "ERROR MICROSOFT", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void tIENDAZONEFITNESSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                ExportDataGridViewTo_Excel12(GRILLA_TIENDA);
            }
            catch { MessageBox.Show("SE NECESITA DE MISCROSOFT OFFICES 2007 EN ADELANTE", "ERR MICROSOFT", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void PRINCIPAL_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void RESPALDO_TOOL_Click(object sender, EventArgs e)
        {
            //RESPALDO respaldo = new RESPALDO();
            //respaldo.Show();
        }

        private void aGREGARNUEVOSPRECIOSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PRECIOS PRECIOS = new PRECIOS();
            PRECIOS.Show();
        }

        private void gRAFICAPAGOSMENSUALESToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("NO DISPONIBLE EN ESTA VERSION ","GRAFICAS" , MessageBoxButtons.OK , MessageBoxIcon.Hand);
        }

        private void cONTROLDEEMPLEADOSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EMPLEADOS empleado = new EMPLEADOS();
            empleado.Show();
        }

        private void pROPIEDADESRUTINASToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EDITAR_RUTINA rutinas = new EDITAR_RUTINA();
            rutinas.Show();
        }

        private void vERRUTINASCREADASToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RUTINAS_CREADAS RUTINA_CREADA = new RUTINAS_CREADAS();
            RUTINA_CREADA.Show();
        }

        private void cAMBIARNOMBREDELGIMNASIOToolStripMenuItem_Click(object sender, EventArgs e)
        {
            seleccion_nombre_gym gym = new seleccion_nombre_gym();
            gym.Show();
        }

        private void PRINCIPAL_LocationChanged(object sender, EventArgs e)
        {
            X_ = this.Location.X;
            Y_ = this.Location.Y;
        }

        private void toolcmd_buscar_Click(object sender, EventArgs e)
        {

            try
            {

                if (BuscarSocio.ParametrosBusqueda.Count >= 1)
                    BuscarSocio.ParametrosBusqueda.Clear();

                BuscarSocio.ParametrosBusqueda.Add("Nombre");
                BuscarSocio.ParametrosBusqueda.Add("Apellido");
                BuscarSocio.ParametrosBusqueda.Add("Nombre_socio2");
                BuscarSocio.ParametrosBusqueda.Add("Apellido_socio2");
                BuscarSocio.ParametrosBusqueda.Add("Cuota");
                BuscarSocio.ParametrosBusqueda.Add("Precio");
                BuscarSocio.ParametrosBusqueda.Add("Pago");

         
                BuscarSocio.Cadena = STRIP_TXT_BUSCAR.Text;
              
                BuscarSocio.GetBusquedaAvanzada_();

                if (BuscarSocio.IsaliveThreat() == true)
                {
                    TimeBusqueda.Enabled = true;
                    BuscarSocio.WaitTimeConfig(GRILLA_SOCIOS, new Point(350, 115));
                }
                else
                {
                    View_SociosEliminados();
                }

            }
            catch { }
        }

        private void cmd_buscar_imagen_Click(object sender, EventArgs e)
        {
            try
            {
                System.Windows.Forms.OpenFileDialog abrir_directorio = new System.Windows.Forms.OpenFileDialog();
                string[] seleccionar_ruta = new string[5000];
                object rutas;
                abrir_directorio.ValidateNames = true;
                abrir_directorio.Title = "Buscar Imagen del socio";
                abrir_directorio.Filter = "(*.jpg)|";

                if (abrir_directorio.ShowDialog() == DialogResult.OK)
                {
                    seleccionar_ruta = abrir_directorio.FileNames;
                    foreach (object rutas_loopVariable in seleccionar_ruta)
                    {
                        rutas = rutas_loopVariable;//variable objeto dandole un ciclo booleano al object
                        System.IO.FileInfo fileinfo = new System.IO.FileInfo(Convert.ToString(rutas));//como es un objeto necesitamos hacerlo string o cadena
                        lbldireccion.Text = Convert.ToString(fileinfo);
                        picture_imagen_socio.Image = System.Drawing.Image.FromFile(lbldireccion.Text);

                        LongImagen = fileinfo.Length;
                        ImagenCliente = new byte[LongImagen];
                        FileStream fs = new FileStream(abrir_directorio.FileName, FileMode.Open, FileAccess.Read, FileShare.Read);
                        fs.Read(ImagenCliente, 0, Convert.ToInt32(LongImagen));
                        fs.Flush();
                        fs.Close();
                        return;
                    }

                   
                }
            }

            catch { }
        }


        private void PRINCIPAL_VisibleChanged(object sender, EventArgs e)
        {

            try { icono_notificacion.BalloonTipTitle = titulos_principal.TITULO_PRINCIPAL; }
            catch { icono_notificacion.BalloonTipTitle = "GESTOR DE GIMNASIO"; }


            if (this.Visible)
            {
                icono_notificacion.BalloonTipText = STRIP_LABEL_EXPIRADO.Text ;              
            }
            else
            {
                icono_notificacion.BalloonTipText = "EL PROGRAMA SEGUIRA EJECUTANDOCE ";
            }

            icono_notificacion.ShowBalloonTip(100000);

            mcdactualizarToolStripMenuItem.Text = strip_lbl_actualizar.Text;
            mcdsociosexpToolStripMenuItem.Text = STRIP_LABEL_EXPIRADO.Text;
        }

        private void mcdsalirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OPCIONES opt = new OPCIONES();
            opt.salir();
        }


        private void GRILLA_EXPIRADOS_CellClick(object sender, DataGridViewCellEventArgs e)
        {



            string Fecha_exp = "";
            string fecha_inicio = "";

            List<char> get_exp = new List<char>();
            List<char> get_fecha = new List<char>();
            
            try
            {
                int i = this.GRILLA_EXPIRADOS.CurrentRow.Index;

                lblsocio_exp_1.Text = Convert.ToString(GRILLA_EXPIRADOS[1, i].Value);
                fecha_inicio = Convert.ToString(GRILLA_EXPIRADOS[4, i].Value);
                Fecha_exp = Convert.ToString(GRILLA_EXPIRADOS[4, i].Value);

                try
                {

                    DateTime fecha_T = Convert.ToDateTime(GRILLA_EXPIRADOS[4, i].Value);
                    TimeSpan promedio = new TimeSpan();

                    if (fecha_T.Month <= DateTime.Now.Month)
                    {
                        promedio = DateTime.Now - fecha_T;

                        if (Convert.ToInt32(promedio.TotalDays) <= 0)
                        {
                            int prom2 = fecha_T.Day - DateTime.Now.Day;
                            linkdias.Text = "Faltan " + Math.Abs(prom2).ToString() + " Dias para expirar";
                        }
                        else
                            linkdias.Text = "Expiro Hace " + Convert.ToInt32(promedio.TotalDays).ToString() + " Dia(s)";
                    }
                    else if (fecha_T.Day >= (DateTime.Now.Day) && fecha_T.Month == DateTime.Now.Month)
                    {
                        promedio = DateTime.Now - fecha_T;
                        linkdias.Text = "Expiro Hace " + Convert.ToInt32(promedio.TotalDays).ToString() + " Dia(s)";
                    }
                    else
                    {
                        promedio = DateTime.Now - fecha_T;

                        if (promedio.Days < 0)
                        {
                            linkdias.Text = "Faltan " + Math.Abs(promedio.Days).ToString() + " Dias para expirar";
                        }
                        else if (promedio.Days == 0)
                        {
                            linkdias.Text = "Faltan " + Math.Abs(promedio.Days + 1).ToString() + " Dias para expirar";
                        }
                        else
                        {
                            linkdias.Text = "Faltan " + promedio.TotalDays.ToString() + " Dias para expirar";
                        }

                    }
                }
                catch { }


            }
            catch { }
        }

        private void lOCKERSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lockers lock_ = new lockers();
            lock_.Show();
        }

        private void cERRARSESIONToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Restart();
        }

        private void DATE_EXPIRAR_ValueChanged(object sender, EventArgs e)
        {
           /* if (DATE_INICIO.Text == DATE_EXPIRAR.Text)
            {
                MessageBox.Show("Las fechas de Inicio y de Expiracion no pueden ser las mismas", "Error En fechas");
                IsDate = false;
                return;
            }
            else IsDate = true;*/
        }

        private void GRILLA_SOCIOS_CellMouseMove(object sender, DataGridViewCellMouseEventArgs e)
        {
           /* try
            {
                FrmVistaPrevia.Location = new Point(e.X + 8, e.Y + 4);
                int i = e.RowIndex;
                FrmVistaPrevia.Show();
                FrmVistaPrevia.BringToFront();
                linkNombre.Text = "Nombre: " + GRILLA_SOCIOS[1, i].Value.ToString();
                linkApellido.Text= "Apellido: " + GRILLA_SOCIOS[2, i].Value.ToString();
                linkFechaExpiracion.Text = "Expira: " + GRILLA_SOCIOS[4, i].Value.ToString();
                linkcuota.Text = "Cuota: $" + GRILLA_SOCIOS[5, i].Value.ToString();
                string Imgp = GRILLA_SOCIOS[16, i].Value.ToString();

                if (Imgp != "NO_IMG.jpg" && Imgp != "")
                {
                    try
                    {
                        Image imagenprevia = Image.FromFile(System.IO.Directory.GetCurrentDirectory() + @"\IMG_SOCIOS\" + Imgp);
                        imagenVp.Image = imagenprevia;
                    }
                    catch
                    {
                        Image imagenprevia = Image.FromFile(System.IO.Directory.GetCurrentDirectory() + @"\IMG_SOCIOS\NO_IMG.jpg");
                        imagenVp.Image = imagenprevia;
                    }
                }
                else
                {
                    Image imagenprevia = Image.FromFile(System.IO.Directory.GetCurrentDirectory() + @"\IMG_SOCIOS\NO_IMG.jpg");
                    imagenVp.Image = imagenprevia;
                }
                
            }
            catch { }*/
        }

        private void GRILLA_SOCIOS_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            //FrmVistaPrevia.Hide();
        }

     

        private void control_hilos_Tick(object sender, EventArgs e)
        {
            try
            {
                if (Hilo.IsAlive == false)
                {
                    EMAIL.EndLoop();
                }
            }
            catch { }
        }

        private void linksesion_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LoginFacebook();
        }

        private void LoginFacebook()
        {
          //  WinformsSample.FriendViewer frmfriend = new WinformsSample.FriendViewer();
           // FrmVistaPrevia.Show();
            MessageBox.Show("FACEBOOK NO HABILITADO PARA ESTA VERSION ");
        }

        private void Time_Expirados_Tick(object sender, EventArgs e)
        {
            try
            {
                if (Hilo_expirado.IsAlive != true)
                {
                    Get_Expirados();
                    EMAIL.EndLoop();
                    Time_Expirados.Enabled = false;
                }
            }
            catch { }
         
        }

        private void Time_Socios_Tick(object sender, EventArgs e)
        {
            try
            {
                if (Hilo_Socio.IsAlive != true)
                {
                    RowsPatronesSocios();
                    Thread h = new Thread(delegate()
                    {
                        GetNumeber_Expirado();
                    });
                    h.Start();
                    EMAIL.EndLoop();
                    Time_Socios.Enabled = false;
                }
            }
            catch { }
        }

        private void TimeBusqueda_Tick(object sender, EventArgs e)
        {
            try
            {
                if (BuscarSocio.WaitTime() == true)
                {
                    View_SociosEliminados();
                    TimeBusqueda.Enabled = false;
                }
            }
            catch { }
        }

        private void cmdprom2x1_Click(object sender, EventArgs e)
        {
            GestorGimnasio.Promocion2x1 prom2x1 = new GestorGimnasio.Promocion2x1();
            prom2x1.Show();
        }

        private void TiempoRespuestaFtp_Tick(object sender, EventArgs e)
        {
            switch (sentinela)
            {
                case 1:
                    try
                    {
                        if (!HiloGuardarFtp.IsAlive)
                        {
                            TiempoRespuestaFtp.Enabled = false;
                            EMAIL.EndLoop();
                            HiloGuardar();
                            DesbloqueoGuardando();
                        }
                        else
                        {
                            return;
                        }
                    }
                    catch { DesbloqueoGuardando(); }
                    break;
                case 2:
                    try
                    {
                        if (!HiloGuardarFtp.IsAlive)
                        {
                            TiempoRespuestaFtp.Enabled = false;
                            HiloEditar();
                            DesbloqueoGuardando();
                        }
                        else
                        {
                            return;
                        }
                    }
                    catch { DesbloqueoGuardando(); }
                    break;
            }
        }

        private void cmdbuscar_expirado_Click(object sender, EventArgs e)
        {
            if (BuscarSocio.ParametrosBusqueda.Count >= 1)
                BuscarSocio.ParametrosBusqueda.Clear();

            BuscarSocio.ParametrosBusqueda.Add("Nombre");
            BuscarSocio.ParametrosBusqueda.Add("Apellido");
            BuscarSocio.ParametrosBusqueda.Add("Nombre_socio2");
            BuscarSocio.ParametrosBusqueda.Add("Apellido_socio2");
            BuscarSocio.ParametrosBusqueda.Add("Cuota");
            BuscarSocio.ParametrosBusqueda.Add("Precio");
            BuscarSocio.ParametrosBusqueda.Add("Pago");

            BuscarSocio.Datagrid = GRILLA_EXPIRADOS;
            BuscarSocio.Cadena = txtbuscar_exp.Text;

            BuscarSocio.InitFunction();
            BuscarSocio.GetBusquedaAvanzada_();

            if (BuscarSocio.IsaliveThreat() == true)
            {
                TimeBusquedaExpirados.Enabled = true;
                BuscarSocio.WaitTimeConfig(GRILLA_EXPIRADOS, new Point(200, 30));
            }
            else
            {

                TimeLineExpirados();
            }
        }

        private void TimeBusquedaExpirados_Tick(object sender, EventArgs e)
        {
            try
            {
                if (BuscarSocio.WaitTime() == true)
                {
                    TimeLineExpirados();
                    TimeBusquedaExpirados.Enabled = false;
                }
            }
            catch { }
        }

        private void cmdrefleshexp_Click(object sender, EventArgs e)
        {
            Delete_Grill_socios_exp();

            try
            {
                if (Hilo_expirado.IsAlive) Hilo_expirado.Abort();
            }
            catch { }

            Hilo_expirado = new Thread(delegate()
            {
                FUNCION_SOCIOS_EXPIRADOS();
            });
            EMAIL.Loop(GRILLA_EXPIRADOS, new Point(200, 30));
            Hilo_expirado.Start();
            Time_Expirados.Enabled = true;
        }


        private void pROMOCIONESToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GestorGimnasio.PROMOCIONES promociones = new GestorGimnasio.PROMOCIONES();
            promociones.Show();
        }

        private void TXT_MAIL_TextChanged(object sender, EventArgs e)
        {
        }

        private void TiempoTienda_Tick(object sender, EventArgs e)
        {

            try
            {
                if (!HiloTienda.IsAlive)
                {
                    TiendaThreat();
                    TiempoTienda.Enabled = false;
                }
            }
            catch { }

        }

        private void fechatienda_ValueChanged(object sender, EventArgs e)
        {
            HiloTienda = new Thread(delegate()
            {
                SeleccionarTienda(fechatienda.Text);
            });
            HiloTienda.Start();
        }

        private void SeleccionarTienda(string Fecha)
        {

            try
            {
                if (conectar_.mysqlconection.State == 0) { conectar_.mysqlconection.Open(); }
                else
                {
                    conectar_.mysqlconection.Close();
                    conectar_.mysqlconection.Open();
                }

                var Nfecha = Convert.ToDateTime(Fecha).Year +
                    "-" + Convert.ToDateTime(Fecha).Month + 
                    "-" + Convert.ToDateTime(Fecha).Day;

                SQL_TIENDA = "SELECT * FROM TIENDA WHERE Fecha Like '" + Nfecha + "'";
                oled_data_adapter_tienda = new OleDbDataAdapter(SQL_TIENDA, conectar_.mysqlconection);
                ds_oledb_tienda = new DataTable();
                oled_data_adapter_tienda.Fill(ds_oledb_tienda);

            }
            catch (Exception ex)
            {
                GestorGimnasio.CLASES.Informe_Errores.Enviar_Error(ex.Message);
            }

            GRILLA_TIENDA.DataSource = ds_oledb_tienda;
            conectar_.mysqlconection.Close();
         
        }

        private void TimerParamSocios_Tick(object sender, EventArgs e)
        {
            try
            {
                if (!HiloSocioP.IsAlive)
                {
                    AgregarSocioParamsSubProceso();
                    TimerParamSocios.Enabled = false;
                }
                else {
                    COMBO_ATENDIO.Text = "Cargando ...";
                    COMBO_PRECIOS.Text = "Cargando ...";
                    return;
                }
            }
            catch { }
        }

        private void cmdwebcam_Click(object sender, EventArgs e)
        {
            GestorGimnasio.WebCamCapture webcam = new GestorGimnasio.WebCamCapture();
            webcam.Show();
        }

        private void TimeWebCam_Tick(object sender, EventArgs e)
        {
            if (EnableTimeWebCam)
            {
                picture_imagen_socio.Image = Image.FromFile(PRINCIPAL.WebcamCapture);
                lbldireccion.Text = PRINCIPAL.WebcamCapture;
                EnableTimeWebCam = false;
            }
        }



        private bool SetupThePrinting(DataGridView MyDataGridView , string Tema)
        {
            PrintDialog MyPrintDialog = new PrintDialog();
            MyPrintDialog.AllowCurrentPage = false;
            MyPrintDialog.AllowPrintToFile = false;
            MyPrintDialog.AllowSelection = false;
            MyPrintDialog.AllowSomePages = false;
            MyPrintDialog.PrintToFile = false;
            MyPrintDialog.ShowHelp = false;
            MyPrintDialog.ShowNetwork = false;

           if (MyPrintDialog.ShowDialog() != DialogResult.OK)
                return false;


            MyPrintDocument.DocumentName = "Zone Fitness Gym Reporte " + Tema;
            MyPrintDocument.PrinterSettings =
                                MyPrintDialog.PrinterSettings;
            MyPrintDocument.DefaultPageSettings =
            MyPrintDialog.PrinterSettings.DefaultPageSettings;
            MyPrintDocument.DefaultPageSettings.Margins =
                             new System.Drawing.Printing.Margins(40, 40, 40, 40);

            if (MessageBox.Show("¿Desea que el reporte se centre automaticamente a la pagina?",
                "Centrar", MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) == DialogResult.Yes)
            {
                MyDataGridViewPrinter = new DataGridViewPrinter(MyDataGridView,
                MyPrintDocument, true, true, Tema, new Font("Tahoma", 18,
                FontStyle.Bold, GraphicsUnit.Point), Color.Black, true);
            }
            else
                MyDataGridViewPrinter = new DataGridViewPrinter(MyDataGridView,
                MyPrintDocument, false, true, Tema, new Font("Tahoma", 18,
                FontStyle.Bold, GraphicsUnit.Point), Color.Black, true);

            return true;
        }


        private void MyPrintDocument_PrintPage_1(object sender, PrintPageEventArgs e)
        {
            bool more = MyDataGridViewPrinter.DrawDataGridView(e.Graphics);
            if (more == true)
                e.HasMorePages = true;
        }

        private void toolprint_Click(object sender, EventArgs e)
        {

            switch (tabControl1.SelectedIndex)
            {
                case 0:
                    if (SetupThePrinting(GRILLA_SOCIOS, " Socios"))
                    {
                        PrintPreviewDialog MyPrintPreviewDialog = new PrintPreviewDialog();
                        MyPrintPreviewDialog.Document = MyPrintDocument;
                        MyPrintPreviewDialog.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("No se ha detectado dispositivo de impresion", "Error Impresion", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    break;
                case 1:
                    if (SetupThePrinting(GRILLA_EXPIRADOS, " Socios Expirados"))
                    {
                        PrintPreviewDialog MyPrintPreviewDialog = new PrintPreviewDialog();
                        MyPrintPreviewDialog.Document = MyPrintDocument;
                        MyPrintPreviewDialog.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("No se ha detectado dispositivo de impresion", "Error Impresion", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    break;
                case 2:
                    if (SetupThePrinting(GRILLA_TIENDA, " Tienda"))
                    {
                        PrintPreviewDialog MyPrintPreviewDialog = new PrintPreviewDialog();
                        MyPrintPreviewDialog.Document = MyPrintDocument;
                        MyPrintPreviewDialog.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("No se ha detectado dispositivo de impresion", "Error Impresion", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    break;
                default:
                    break;
            }
           
        }

        private void TimerAutoCompletar_Tick(object sender, EventArgs e)
        {

            if (GRILLA_SOCIOS.RowCount >= 2)
            {
                STRIP_TXT_BUSCAR.AutoCompleteMode = AutoCompleteMode.Suggest;
                STRIP_TXT_BUSCAR.AutoCompleteSource = AutoCompleteSource.CustomSource;
                string nombre = null;
                if (STRIP_TXT_BUSCAR.AutoCompleteCustomSource.Count >= 1) STRIP_TXT_BUSCAR.AutoCompleteCustomSource.Clear();
                for (int i = 0; i < GRILLA_SOCIOS.RowCount - 1; i++)
                {
                    nombre = GRILLA_SOCIOS[1, i].Value.ToString();
                    if (!STRIP_TXT_BUSCAR.AutoCompleteCustomSource.Contains(nombre))
                    {
                        STRIP_TXT_BUSCAR.AutoCompleteCustomSource.Add(nombre);
                    }
                }
                TimerAutoCompletar.Enabled = false;
            }
        }

        private void linkreflescarsocios_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (Parametros2x1.Count >= 1)
                Parametros2x1.Clear();

            try { BORRAR_CASILLAS_AGREGAR(); }
            catch { }

            try
            {
                if (HiloSocioP.IsAlive)
                {
                    HiloSocioP.Abort();
                    TimerParamSocios.Enabled = false;
                }

            }
            catch { }

            HiloSocioP = new Thread(delegate()
            {
                GetParametrosInicialesSocios();
            });
            HiloSocioP.Start();
            TimerParamSocios.Enabled = true;

        }

        private void cAMBIARDIRECCIONBASEDEDATOSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GestorGimnasio.DireccionProveedor dir = new GestorGimnasio.DireccionProveedor();
            dir.Show();
        }

        private void CmdVerificarCorreo_Click(object sender, EventArgs e)
        {
            GestorGimnasio.MensajesTiempoReal mensaje = new GestorGimnasio.MensajesTiempoReal();
            GestorGimnasio.MensajesTiempoReal.CuerpoMensaje = "Verificando Correo ...";
            mensaje.Show();

            MessageBox.Show("Se procedera a verificar el correo, esto podra tardar unos segundos", "Verificar correo", MessageBoxButtons.OK, MessageBoxIcon.Information);
           

            if (!Seguridad.IsEmail(TXT_MAIL.Text))
            {
                mensaje.Close();
                MessageBox.Show("El correo electronico " + TXT_MAIL.Text + " NO EXISTE ", "Error Email", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                bool Validado = EMAIL.VerificarCorreoActivo(TXT_MAIL.Text);
                if (Validado == true)
                {
                    mensaje.Close();
                    TXT_MAIL.BackColor = Color.White;
                    MessageBox.Show("El correo electronico " + TXT_MAIL.Text + " EXISTE Y ESTA LISTO PARA USARSE", "Error Email", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    mensaje.Close();
                    TXT_MAIL.BackColor = Color.Red;
                    MessageBox.Show("El correo electronico " + TXT_MAIL.Text + " NO EXISTE ", "Error Email", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
        }

        private void GRILLA_SOCIOS_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int i = GRILLA_SOCIOS.CurrentRow.Index;

            try
            {

                EnvioDatos_Socios.Datos_Socios.Clear();
                EnvioDatos_Socios.Datos_Socios.Add(Convert.ToString(GRILLA_SOCIOS[1, i].Value));
                EnvioDatos_Socios.Datos_Socios.Add(Convert.ToString(GRILLA_SOCIOS[2, i].Value));
                EnvioDatos_Socios.Datos_Socios.Add(Convert.ToString(GRILLA_SOCIOS[3, i].Value));
                EnvioDatos_Socios.Datos_Socios.Add(Convert.ToString(GRILLA_SOCIOS[4, i].Value));
                EnvioDatos_Socios.Datos_Socios.Add(Convert.ToString(GRILLA_SOCIOS[5, i].Value));
                EnvioDatos_Socios.Datos_Socios.Add(Convert.ToString(GRILLA_SOCIOS[7, i].Value));
                EnvioDatos_Socios.Datos_Socios.Add(Convert.ToString(GRILLA_SOCIOS[8, i].Value));
                EnvioDatos_Socios.Datos_Socios.Add(Convert.ToString(GRILLA_SOCIOS[9, i].Value));
                EnvioDatos_Socios.Datos_Socios.Add(Convert.ToString(GRILLA_SOCIOS[10, i].Value));
                EnvioDatos_Socios.Datos_Socios.Add(Convert.ToString(GRILLA_SOCIOS[13, i].Value));
                EnvioDatos_Socios.Datos_Socios.Add(Convert.ToString(GRILLA_SOCIOS[15, i].Value));
                EnvioDatos_Socios.Datos_Socios.Add(Convert.ToString(GRILLA_SOCIOS[16, i].Value));

                EnvioDatos_Socios.Error = false;
            }
            catch
            {
                EnvioDatos_Socios.Error = true;
            }

            GestorGimnasio.DATOS_SOCIOS datos = new GestorGimnasio.DATOS_SOCIOS();
            datos.Show();
        }



    


     

    }
}

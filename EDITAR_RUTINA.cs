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


namespace ZONE_FITNESS_3._0_FINAL
{
    public partial class EDITAR_RUTINA : Form
    {

        private string SQL_RUTINA = "SELECT * FROM SOCIO_RUTINA";
        public static string SERVIDOR_RUTINA_MAIL = "";
        public static string SERVIDOR_RUTINA_PASSWORD = "";
        private const string SQL_ = "SELECT * FROM CORREO";
          
        public EDITAR_RUTINA()
        {
            InitializeComponent();
        }

        private void EDITAR_RUTINA_Load(object sender, EventArgs e)
        {
            this.Text = "PROPIEDADES RUTINA";
            //
            CONEXION_RUTINA();
            COMBO_BOX();
            ACTUALIZAR();
            GRILLA();
        }


        private void COMBO_BOX()
        {
            combo_ejercicio.Items.Clear();
            combo_ejercicio.Items.Add("ABDOMINALES INFERIORES");
            combo_ejercicio.Items.Add("ABDOMINALES SUPERIORES");
            combo_ejercicio.Items.Add("ANTEBRAZOS");
            combo_ejercicio.Items.Add("BICEPS");
            combo_ejercicio.Items.Add("CADERAS");
            combo_ejercicio.Items.Add("CUADRICEPS");
            combo_ejercicio.Items.Add("DORSALES");
            combo_ejercicio.Items.Add("ESPALDA");
            combo_ejercicio.Items.Add("EXTERIOR FEMORAL");
            combo_ejercicio.Items.Add("GEMELOS");
            combo_ejercicio.Items.Add("GLUTEOS");
            combo_ejercicio.Items.Add("HOMBROS");
            combo_ejercicio.Items.Add("LATERALES");
            combo_ejercicio.Items.Add("LUMBARES");
            combo_ejercicio.Items.Add("PECTORALES");
            combo_ejercicio.Items.Add("TRAPECIOS");
            combo_ejercicio.Items.Add("TRASEROS");
            combo_ejercicio.Items.Add("TRICEPS");
            combo_ejercicio.Items.Add("OTROS");
        }

        private void ELIMINAR_EN_GRILLA()
        {
            DialogResult re = MessageBox.Show("¿SEGURO QUE DESEA ELIMINAR LA RUTINA DE ESTE SOCIO?", "ELIMINAR", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (re == DialogResult.Yes)
            {

                try
                {
                    int i = this.grilla_socios.CurrentRow.Index;
                    try
                    {
                        conectar_.SOCIO_RUTINA = new Recordset();
                        try
                        {
                            System.IO.File.Delete(System.IO.Directory.GetCurrentDirectory() + @"\PDF\" + Convert.ToString(grilla_socios[1, i].Value) + "_" + Convert.ToString(grilla_socios[2, i].Value) + ".pdf");
                        }
                        catch(Exception ex)
                        {
                            MessageBox.Show("NO SE PUEDE ELIMINAR EL USUARIO YA QUE EL ARCHIVO PDF ESTA SIENDO USADO POR OTRO PROCESO\n\n" + ex.Message + "\n\nINTENTAR MAS TARDE");
                            return;
                        }

                        string SQL_eliminar_ = "DELETE FROM SOCIO_RUTINA WHERE Nombre ='" + Convert.ToString(grilla_socios[1, i].Value) +"'AND Apellido='" + Convert.ToString(grilla_socios[2, i].Value) + "'";
                        conectar_.SOCIO_RUTINA.Open(SQL_eliminar_, conectar_.CN, CursorTypeEnum.adOpenDynamic, LockTypeEnum.adLockOptimistic);
                        MessageBox.Show("RUTINA ELIMINADA CON EXITO", "EXITO");
                        temporixador_grilla.Enabled = true;
                    }
                    catch { }

                }
                catch { }
            }
            else { return; }
        }

        private void ELIMINAR_EN_LISTA()
        {
            DialogResult re = MessageBox.Show("¿SEGURO QUE DESEA ELIMINAR LA RUTINA?" ,"ELIMINAR", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (re == DialogResult.Yes)
            {

                try
                {
                   
                    try
                    {
                        conectar_.RUTINAS_ = new Recordset();
                        string SQL_eliminar_ = "DELETE FROM RUTINAS WHERE Categoria ='" + combo_ejercicio.Text +
                        "'AND Nombre='" + lista_ejercicio.SelectedItem + "'";
                        conectar_.RUTINAS_.Open(SQL_eliminar_, conectar_.CN, CursorTypeEnum.adOpenDynamic, LockTypeEnum.adLockOptimistic);
                        MessageBox.Show("RUTINA ELIMINADA CON EXITO", "EXITO");
                        lista_index();
                    }
                    catch { }

                }
                catch { }
            }
            else { return; }
        }

        private void CONEXION_RUTINA()
        {

            conectar_.mysql_data_adapter_rutina = new OleDbDataAdapter(SQL_RUTINA, conectar_.mysqlconection);//conecta el adapter con la sentencia sql
            conectar_.ds_oledb_rutina = new DataTable();//tabla es = nueva tabla
            conectar_.mysql_data_adapter_rutina.Fill(conectar_.ds_oledb_rutina);//el adaptador con toda la base de sql se le llenara en la tabla
            
        }

        private void GRILLA()
        {
            int n = grilla_socios.RowCount;
            int  y = 0;
            try
            {
                for (int i = 1; i < n - 1; i++)
                {
                    grilla_socios.Rows.RemoveAt(i);
                }
            }
            catch { }
            try
            {

                grilla_socios.DataSource = conectar_.ds_oledb_rutina;
                grilla_socios.Columns[0].Visible = false;
                grilla_socios.ReadOnly = false;
                for (int i = 0; i <= grilla_socios.ColumnCount - 1; i++)
                {
                    if (!(i == 1 || i == 2))
                    {
                        grilla_socios.Columns[i].Visible = false;
                    }
                }
               
                for (int j = 0; j < grilla_socios.RowCount - 1; j++)
                {
                    while( y  == 0)
                    {
                    if (Convert.ToString(grilla_socios[1,j].Value) == Convert.ToString(grilla_socios[1, j + 1].Value) && Convert.ToString(grilla_socios[2,j].Value) == Convert.ToString(grilla_socios[2, j + 1].Value))
                    {
                       grilla_socios.Rows.RemoveAt(j + 1);
                    }
                    else 
                    {
                        y = 1;
                    }
                    }
                    y = 0;
                }
            }
            catch { }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                conectar_.RS_CORREO_.AddNew();
                {
                    conectar_.RS_CORREO_.Fields[1].Value = txtproxy.Text;
                    conectar_.RS_CORREO_.Fields[2].Value = txtpass_.Text;
                    conectar_.RS_CORREO_.Fields[3].Value = "_";

                    conectar_.RS_CORREO_.Update();
                }

                MessageBox.Show("CAMBIOS GUARDADOS CON EXITO", "EXITO");
                ACTUALIZAR();
            }
            catch { MessageBox.Show("NO SE LOGRARON GUARDAR LOS CAMBIOS , VEA SI TODOS LOS CAMPOS ESTAN LLENOS"); }
        }

        private void ACTUALIZAR()
        {
            this.BackColor = Color.DarkBlue;
            try
            {
                conectar_.RS_CORREO_ = new Recordset();

                if (conectar_.RS_CORREO_.State == 1)
                {
                    conectar_.RS_CORREO_.Close();
                }

                conectar_.RS_CORREO_.Open(SQL_, conectar_.CN, CursorTypeEnum.adOpenDynamic, LockTypeEnum.adLockOptimistic);

                txtproxy.Text = conectar_.RS_CORREO_.Fields[1].Value;
                txtpass_.Text = conectar_.RS_CORREO_.Fields[2].Value;
            }
            catch { }
        }

        private void AGREGAR_EJERCICIO()
        {
            string IMAGEN_RUTA = string.Empty;
            string[] VECTOR_IMG = new string[100];

            for(int y = txtimg.TextLength ; y >= 0  ; y--)
            {
                if(txtimg.Text[y - 1] != '\\')
                {
                    VECTOR_IMG[y] = Convert.ToString(txtimg.Text[y - 1]);
                }
                else if (txtimg.Text[y - 1] == '\\')
                {
                    IMAGEN_RUTA = string.Join("", VECTOR_IMG);
                    break;
                }
            }

            System.IO.File.Copy(txtimg.Text , System.IO.Directory.GetCurrentDirectory() + @"\IMG_RUTINAS\" + Convert.ToString(IMAGEN_RUTA));
            conectar_.RUTINAS_ = new Recordset();
            conectar_.RUTINAS_.Open("SELECT * FROM RUTINAS", conectar_.CN, CursorTypeEnum.adOpenDynamic, LockTypeEnum.adLockOptimistic);

            conectar_.RUTINAS_.AddNew();
            {
                conectar_.RUTINAS_.Fields[2].Value = combo_ejercicio.Text;
                conectar_.RUTINAS_.Fields[1].Value = txtnombre.Text;
                conectar_.RUTINAS_.Fields[3].Value = IMAGEN_RUTA;
                conectar_.RUTINAS_.Fields[4].Value = txtdetalle.Text;
            }

            conectar_.RUTINAS_.Update();
            MessageBox.Show("RUTINA AGREGADA CON EXITO", "EXITO RUTINA", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        private void cmd_add_Click(object sender, EventArgs e)
        {
            try
            {
                AGREGAR_EJERCICIO();
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR " + ex.Message);
            }
        }

        private void cmd_buscar_img_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog abrir_directorio = new OpenFileDialog();
                string[] seleccionar_ruta = new string[5000];
                object rutas;
                abrir_directorio.ValidateNames = true;
                abrir_directorio.Title = "BUSCAR UNA IMAGEN RUTINA";
                //abrir_directorio.Filter = "(*.jpeg)|(*.gif)|(*.png)|(*.gif)";
                abrir_directorio.ShowDialog();

                if (abrir_directorio.FileName != null)
                {
                    seleccionar_ruta = abrir_directorio.FileNames;
                    foreach (object rutas_loopVariable in seleccionar_ruta)
                    {
                        rutas = rutas_loopVariable;//variable objeto dandole un ciclo booleano al object
                        System.IO.FileInfo fileinfo = new System.IO.FileInfo(Convert.ToString(rutas));//como es un objeto necesitamos hacerlo string o cadena
                        txtimg.Text = Convert.ToString(fileinfo);
                        return;
                    }
                }
                else
                {
                    return;
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void combo_ejercicio_SelectedIndexChanged(object sender, EventArgs e)
        {
            lista_index();
        }

        private void lista_index()
        {
            lista_ejercicio.Items.Clear();

            conectar_.RUTINAS_ = new Recordset();
            conectar_.RUTINAS_.Open("SELECT * FROM RUTINAS WHERE Categoria ='" + combo_ejercicio.Text + "'" , conectar_.CN, CursorTypeEnum.adOpenDynamic, LockTypeEnum.adLockOptimistic);
           
            while (!(conectar_.RUTINAS_.EOF))
            {
                lista_ejercicio.Items.Add(conectar_.RUTINAS_.Fields[1].Value);
                conectar_.RUTINAS_.MoveNext();
            }

        }

        private void cmd_editar_Click(object sender, EventArgs e)
        {
            RUTINA_CUERPO_HTML rutina_html_ = new RUTINA_CUERPO_HTML();
            rutina_html_.Show();
        }

        private void txtbuscar_TextChanged(object sender, EventArgs e)
        {
            Buscador.Buscar_(grilla_socios, txtbuscar);
        }

        private void link_eliminar_grilla_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {        
            ELIMINAR_EN_GRILLA();
        }

        private void link_eliminar_listbox_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ELIMINAR_EN_LISTA();
        }

        private void temporixador_grilla_Tick(object sender, EventArgs e)
        {
            GRILLA();
            temporixador_grilla.Enabled = false;
        }
        
        }

    }

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;

using ZONE_FITNESS_3._0_FINAL.CLASES;


using ADODB;

namespace ZONE_FITNESS_3._0_FINAL
{
    public partial class LOGIN : Form
    {

        #region Variables
        OPCIONES OPCIONES = new OPCIONES();
        inicio_comprobar inicio_comprobar = new inicio_comprobar();
        CONTRASEÑA_LOGIN contra_segura = new CONTRASEÑA_LOGIN();

        public static int x_, y_;
        public static int activate_ = 0;

        public static string Usuario = "";

        public static TextBox txt_login_contra_segura;

        public int sentinela = 0;
        private static string SQL_LOGIN = "";

        private int contador__ = 0;
        private static int sentinela_administrador = 0;

        protected ToolTip textoiconos = new ToolTip();



        #endregion

        #region constructor_destructor
        ~LOGIN() { }

        public LOGIN()
        {
            InitializeComponent();
        }

        #endregion

        #region metodos_privados
        private void LOGIN_Load(object sender, EventArgs e)
        {
            try
            {
                switch (NOMBRE_GYM.GetNombreGym())
                {
                    case 0:
                        iniciar_seleccion_nombre_gym();
                        break;
                    case 1:
                        NOMBRE_GYM.OutNombreGym();
                        break;
                    default:
                        return;
                }
            }
            catch
            {
                seleccion_nombre_gym nombre_gym = new seleccion_nombre_gym();
                nombre_gym.Show();
            }



            Linknombre.Text = "Zone Fitness Gym Login ...";
     
            txt_login_contra_segura = txtpass;
            conectar_.RS_LOGIN = new Recordset();

            this.Opacity = 0.92;
            this.Visible = true;

            combo_user.Items.Clear();
            combo_user.Visible = false;

            sentinela = 1;
            txtpass.PasswordChar = '●';
            txtpass2.PasswordChar = '●';
            ParametrosIniciales();
            txtnombre.Items.Clear();
            BUSCAR_USUARIOS();
            lbladvertencia.Text = "";
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;


        }

        private void iniciar_seleccion_nombre_gym()
        {
            seleccion_nombre_gym gym = new seleccion_nombre_gym();
            gym.Show();
        }

        private void salir_strip_Click(object sender, EventArgs e)
        {
            OPCIONES.salir();
        }

        private void cmdenviar_Click(object sender, EventArgs e)
        {
            if (sentinela == 1)
            {
                conectar_.RS_LOGIN = new Recordset();

                try
                {
                   // SQL_LOGIN = "SELECT * FROM LOGIN WHERE nombre ='" + txtnombre.Text + "'AND password='" + txtpass.Text + "'AND prioridad ='" + combo_user.Text + "'";
                    SQL_LOGIN = "SELECT * FROM LOGIN WHERE nombre ='" + txtnombre.Text + "'AND password='" + txtpass.Text + "'";
                    //
                    if (conectar_.RS_LOGIN.State == 1)
                    {
                        conectar_.RS_LOGIN.Close();
                    }
                    //
                    conectar_.RS_LOGIN.Open(SQL_LOGIN, conectar_.CN, CursorTypeEnum.adOpenDynamic, LockTypeEnum.adLockOptimistic);
                    if (conectar_.RS_LOGIN.RecordCount == 0)
                    {
                        MessageBox.Show("ESTE USUARIO NO EXISTE, FAVOR VERIFICAR EL NOMBRE O CONTRASEÑA", "NO LOGIN", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    else
                    {
                        Usuario = conectar_.RS_LOGIN.Fields[1].Value;

                        if (conectar_.RS_LOGIN.Fields[3].Value == "ENTRENADOR")
                        {
                            OPCIONES.PRIVILEGIOS_ESTATUS = 2;
                            //
                        }
                        else if (conectar_.RS_LOGIN.Fields[3].Value == "ADMINISTRADOR")
                        {
                            OPCIONES.PRIVILEGIOS_ESTATUS = 1;
                            //
                        }

                        MessageBox.Show("Bienvenido a Zone Fitness Gym (" + conectar_.RS_LOGIN.Fields[1].Value + ")", "USUARIO " + combo_user.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        PRINCIPAL PRINCIPAL = new PRINCIPAL();
                        contra_segura.Hide();
                        contador__ = 0;
                        this.Hide();
                        this.Close();
                        PRINCIPAL.Show();

                    }

                }
                catch (Exception ex)
                {
                    GestorGimnasio.CLASES.Informe_Errores.Enviar_Error(ex.Message);
                }

            }
            else if (sentinela == 2)
            {
                conectar_.RS_LOGIN = new Recordset();

                try
                {
                    SQL_LOGIN = "SELECT * FROM LOGIN";
                    //
                    if (conectar_.RS_LOGIN.State == 1)
                    {
                        conectar_.RS_LOGIN.Close();
                    }
                    //
                    conectar_.RS_LOGIN.Open(SQL_LOGIN, conectar_.CN, CursorTypeEnum.adOpenDynamic, LockTypeEnum.adLockOptimistic);
                    //

                    if (contraseña_segura.contador_caracter(txtpass) == false)
                    {
                        return;
                    }

                    conectar_.RS_LOGIN.AddNew();
                    {
                        conectar_.RS_LOGIN.Fields[1].Value = txtnombre.Text;
                        conectar_.RS_LOGIN.Fields[2].Value = txtpass.Text;
                        conectar_.RS_LOGIN.Fields[3].Value = combo_user.Text;
                        conectar_.RS_LOGIN.Update();
                    }
                    MessageBox.Show("USUARIO AGREGADO EXITOSAMENTE ", "EXITO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ParametrosIniciales();
                    contra_segura.Hide();
                    BORRAR_();
                    BUSCAR_USUARIOS();
                    /*combo_user.Items.Clear();
                    combo_user.Text = "SELECCIONAR";
                    combo_user.Items.Add("ENTRENADOR");
                    combo_user.Items.Add("ADMINISTRADOR");*/
                    combo_user.Visible = false;
                }
                catch (Exception ex)
                {
                    GestorGimnasio.CLASES.Informe_Errores.Enviar_Error(ex.Message);
                }


            }
            else if (sentinela == 3)
            {
                conectar_.RS_LOGIN = new Recordset();

                try
                {
                    SQL_LOGIN = "SELECT * FROM LOGIN WHERE nombre ='" + txtnombre.Text + "'AND password='" + txtpass.Text + "'AND prioridad ='ADMINISTRADOR'";
                    //
                    if (conectar_.RS_LOGIN.State == 1)
                    {
                        conectar_.RS_LOGIN.Close();
                    }
                    //
                    conectar_.RS_LOGIN.Open(SQL_LOGIN, conectar_.CN, CursorTypeEnum.adOpenDynamic, LockTypeEnum.adLockOptimistic);
                    if (conectar_.RS_LOGIN.RecordCount == 0)
                    {
                        MessageBox.Show("ESTE USUARIO NO EXISTE, FAVOR VERIFICAR EL NOMBRE O CONTRASEÑA", "NO LOGIN", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    else
                    {
                        MessageBox.Show("ADMINISTRADOR ACEPTADO , PERMISOS DESBLOQUEADOS", "EXITO PERMISOS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        combo_user.Items.Clear();
                        combo_user.Text = "SELECCIONAR";
                        combo_user.Items.Add("ENTRENADOR");
                        combo_user.Items.Add("ADMINISTRADOR");
                        BORRAR_();
                        ParametrosFinales();
                    }
                }
                catch { }
            }
        }

        private void strip_entrenador_Click(object sender, EventArgs e)
        {
            ParametrosFinales();
            combo_user.Items.Clear();
            combo_user.Text = "SELECCIONAR";
            combo_user.Items.Add("ENTRENADOR");
        }

        private void strip_administrador_Click(object sender, EventArgs e)
        {
            DialogResult diag = MessageBox.Show("SE NECESITA PERMISO DE ADMINISTRADOR PARA AGREGAR NUEVO ADMINISTRADOR", "PERMISO AVANZADO", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
            if (diag == DialogResult.Yes)
            {
                cmdenviar.Text = "APLICAR PERMISOS";
                combo_user.Items.Add("ADMINISTRADOR");
                ParametrosIniciales();
                sentinela = 3;
            }
            else if (diag == DialogResult.Cancel)
            {
                return;
            }
        }

        private void ParametrosIniciales()
        {
            txtpass2.Visible = false;
            lblpass2.Visible = false;
            sentinela = 1;
        }

        private void ParametrosFinales()
        {
            txtpass2.Visible = true;
            lblpass2.Visible = true;
            sentinela = 2;
        }

        private void BORRAR_()
        {
            txtnombre.Text = "";
            txtpass.Text = "";
            txtpass2.Text = "";
        }

        private void BUSCAR_USUARIOS()
        {

            try
            {
                SQL_LOGIN = "SELECT * FROM LOGIN ";
                //
                try
                {
                    if (conectar_.RS_LOGIN.State == 1)
                    {
                        conectar_.RS_LOGIN.Close();
                    }
                }
                catch 
                {
                    if (conectar_.RS_LOGIN.State != 1)
                    {
                        conectar_.RS_LOGIN.Close();
                    }
                }
                //
                conectar_.RS_LOGIN.Open(SQL_LOGIN, conectar_.CN, CursorTypeEnum.adOpenDynamic, LockTypeEnum.adLockOptimistic);
                while (!(conectar_.RS_LOGIN.EOF))
                {
                    if (conectar_.RS_LOGIN.Fields[1].Value == "admin" || conectar_.RS_LOGIN.Fields[1].Value == "user") { conectar_.RS_LOGIN.MoveNext(); }
                    else
                    {
                        txtnombre.Items.Add(conectar_.RS_LOGIN.Fields[1].Value);
                        conectar_.RS_LOGIN.MoveNext();
                    }
                }

            }
            catch { }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (txtnombre.Text == "" || txtpass.Text == "" )
            {
                cmdenviar.Enabled = false;
            }
            else if (sentinela == 2)
            {

                if (txtpass2.Text == txtpass.Text)
                {
                    cmdenviar.Enabled = true;
                    lbladvertencia.Text = "";
                }
                else
                {
                    cmdenviar.Enabled = false;
                    lbladvertencia.Text = "password no coinciden";
                }

            }
            else
            {
                cmdenviar.Enabled = true;
            }
        }

        private void cANCELARToolStripMenuItem_Click(object sender, EventArgs e)
        {
            combo_user.Items.Clear();
            combo_user.Text = "SELECCIONAR";
            combo_user.Items.Add("ENTRENADOR");
            combo_user.Items.Add("ADMINISTRADOR");
            //
            lbladvertencia.Text = "";
            //
            ParametrosIniciales();
            BORRAR_();
            //
            contra_segura.Hide();
            contador__ = 0;

        }

        private void LOGIN_Activated(object sender, EventArgs e)
        {
            if (activate_ == 0)//activate sirve para verificar una vez por cada seccion iniciada... ya que la variable es estatica 
            {

                if (inicio_comprobar.ExisteAdmin() == 0 && sentinela_administrador == 0)//si el valor de retorno es 0 que no existe administrador entonces crea uno sin permisos extra
                {
                   /* sentinela_administrador = 1;
                    DialogResult dialogo_resultado = new DialogResult();
                    dialogo_resultado = MessageBox.Show("NO EXISTE UN ADMINISTRADOR CREADO.\n\n ¿DESEA CREAR UNO? ", "GESTOR DE GIMNASIOS", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dialogo_resultado == DialogResult.Yes)
                    {
                        combo_user.Items.Clear();
                        combo_user.Items.Add("ADMINISTRADOR");
                        sentinela = 2;
                    }
                    else
                    {
                        sentinela_administrador = 1;
                        activate_++;
                        return;
                    }
                    activate_++;//el activador es >1*/
                }
                else
                {
                    // grupo_mensaje_.Visible = false;//visible NO!
                    activate_++;//el activador > 1
                }
            }

        }

        private void txtpass_TextChanged(object sender, EventArgs e)
        {

            if (contador__ == 0 && sentinela == 2)
            {
                contador__++;
                contra_segura.Show();
                this.txtpass.Focus();
            }
        }

        private void LOGIN_LocationChanged(object sender, EventArgs e)
        {

            x_ = this.Location.X;
            y_ = this.Location.Y;

        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            try
            {
                if (NOMBRE_GYM.todo_texto[2] != null)
                {
                    pictureBox1.Image = System.Drawing.Image.FromFile(NOMBRE_GYM.todo_texto[2].ToString());
                    timer2.Enabled = false;
                }
            }
            catch { }
        }

        private void rEGISTRARPRODUCTOToolStripMenuItem_Click(object sender, EventArgs e)
        {
            REGISTRAR_ REG = new REGISTRAR_();
            REG.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            lockers lockers = new lockers();
            lockers.Show();
        }

        #endregion

        private void cmdadd_Click(object sender, EventArgs e)
        {
            ParametrosFinales();
            combo_user.Items.Clear();
            combo_user.Text = "SELECCIONAR";
            combo_user.Items.Add("ENTRENADOR");
            combo_user.Visible = true;
        }

        private void cmdaddadmin_Click(object sender, EventArgs e)
        {
            DialogResult diag = MessageBox.Show("SE NECESITA PERMISO DE ADMINISTRADOR PARA AGREGAR NUEVO ADMINISTRADOR", "PERMISO AVANZADO", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
            if (diag == DialogResult.Yes)
            {
                cmdenviar.Text = "";
                combo_user.Items.Add("ADMINISTRADOR");
                combo_user.Visible = true;
                ParametrosIniciales();
                sentinela = 3;
            }
            else if (diag == DialogResult.Cancel)
            {
                combo_user.Visible = false;
                return;
                }
        }

        private void cmdacerca_Click(object sender, EventArgs e)
        {
            REGISTRAR_ REG = new REGISTRAR_();
            REG.Show();
        }

        private void cmdquit_Click(object sender, EventArgs e)
        {
            combo_user.Items.Clear();
            combo_user.Text = "SELECCIONAR";
            combo_user.Items.Add("ENTRENADOR");
            combo_user.Items.Add("ADMINISTRADOR");
            combo_user.Visible = false;
            //
            lbladvertencia.Text = "";
            //
            ParametrosIniciales();
            BORRAR_();
            //
            contra_segura.Hide();
            contador__ = 0;
        }

        private void cmdenviar_MouseMove(object sender, MouseEventArgs e)
        {
            textoiconos.SetToolTip(this.cmdenviar, "ENTRAR AL PROGRAMA");
            textoiconos.UseAnimation = true;
           
        }

        private void cmdadd_MouseMove(object sender, MouseEventArgs e)
        {
            textoiconos.SetToolTip(this.cmdadd, "AGREGAR RECEPCIONISTA");
            textoiconos.UseAnimation = true;
        }

        private void cmdaddadmin_MouseMove(object sender, MouseEventArgs e)
        {
            textoiconos.SetToolTip(this.cmdaddadmin, "AGREGAR ADMINISTRADOR");
            textoiconos.UseAnimation = true;
        }

        private void cmdacerca_MouseMove(object sender, MouseEventArgs e)
        {
            textoiconos.SetToolTip(this.cmdacerca, "TOOLS");
            textoiconos.UseAnimation = true;
        }

        private void cmdquit_MouseMove(object sender, MouseEventArgs e)
        {
            textoiconos.SetToolTip(this.cmdquit, "CANCELAR");
            textoiconos.UseAnimation = true;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {

            GestorGimnasio.CLASES.Seguridad.DownLoadImage(pictureBox1, "");

        }

        private void cmdCerrar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }


        private void cmdproveedor_Click(object sender, EventArgs e)
        {
            //GestorGimnasio.DireccionProveedor dir = new GestorGimnasio.DireccionProveedor();
           // dir.Show();
        }



   


    }
}

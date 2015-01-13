using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ZONE_FITNESS_3._0_FINAL.CLASES;
using Microsoft.Win32;
using System.Threading;
using ADODB;
using System.IO;

namespace ZONE_FITNESS_3._0_FINAL
{
    public partial class PROPIEDADES : Form
    {

        public PROPIEDADES()
        {
            InitializeComponent();
        }

        const string SQL = "SELECT * FROM CORREO";

        private void PROPIEDADES_Load(object sender, EventArgs e)
        {


            conectar_.RS_CORREO_ = new Recordset();

            cmd_eliminar.Hide();//boton eliminar escondido
            TXT_PASS_.PasswordChar = '*';//pasword *
            txtpass.PasswordChar = '*';

            combo_servidor.Text = "SELECCIONE EL SERVIDOR";
            combo_servidor.Items.Add("HOTMAIL");
            combo_servidor.Items.Add("GMAIL");
            combo_servidor.Items.Add("OTROS");
            txtotroservidor.Enabled = false;

            try
            {
                RegistryKey registro = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Run", true);//registro del programa para que inicie con windows 


                if (conectar_.RS_CORREO_.State == 1)
                {
                    conectar_.RS_CORREO_.Close();
                }

                conectar_.RS_CORREO_.Open(SQL, conectar_.CN, CursorTypeEnum.adOpenDynamic, LockTypeEnum.adLockOptimistic);

                conectar_.RS_CORREO_.MoveLast();
                TXTPROXY_.Text = conectar_.RS_CORREO_.Fields[1].Value;//txtproxy genera el proxy o direccion electronica
                TXT_PASS_.Text = conectar_.RS_CORREO_.Fields[2].Value;//genera el pasword 

                TXT_CUERPO.DocumentText = conectar_.RS_CORREO_.Fields[3].Value;//genera el cuerpo del mensaje en html
                richTextBox1.Text = conectar_.RS_CORREO_.Fields[3].Value;//vista previa 



                if (registro.GetValue("GIMNASIO") == null)
                {
                    // registro.CreateSubKey("ZONE_FITNESS");
                    //  registro.SetValue("ZONE_FITNESS", Convert.ToString(System.IO.Directory.GetCurrentDirectory() + @"\ZONE_FITNESS_3.0_FINAL.exe"));
                    chek_ejecutar.Checked = false;
                }
                else if (registro.GetValue("GIMNASIO") != null)//si existe el registro entonces el cheke esta activo
                {
                    chek_ejecutar.Checked = true;
                }
            }
            catch { }
            
        }

        private void ACTUALIZAR()
        {
            this.BackColor = Color.DarkBlue;//actualiza
            TXT_PASS_.PasswordChar = '*';//actualiza
            try
            {
                conectar_.RS_CORREO_.MoveLast();
                TXTPROXY_.Text = conectar_.RS_CORREO_.Fields[1].Value;
                TXT_PASS_.Text = conectar_.RS_CORREO_.Fields[2].Value;

                TXT_CUERPO.DocumentText = conectar_.RS_CORREO_.Fields[3].Value;
                richTextBox1.Text = conectar_.RS_CORREO_.Fields[3].Value;
            }
            catch { }
        }

        private void GUARDAR_CAMBIOS()
        {
            try
            {
                conectar_.RS_CORREO_.AddNew(); //guarda los nuevos datos o registros en la base de datos
                {
                    conectar_.RS_CORREO_.Fields[1].Value = TXTPROXY_.Text;
                    conectar_.RS_CORREO_.Fields[2].Value = TXT_PASS_.Text;
                    conectar_.RS_CORREO_.Fields[3].Value = richTextBox1.Text;

                    if (combo_servidor.Text == "HOTMAIL")
                    {
                        conectar_.RS_CORREO_.Fields[4].Value = "smtp.live.com";
                    }
                    else if (combo_servidor.Text == "GMAIL")
                    {
                        conectar_.RS_CORREO_.Fields[4].Value = "smtp.gmail.com";
                    }
                    else
                    {
                        conectar_.RS_CORREO_.Fields[4].Value = txtotroservidor.Text;
                    }


                    conectar_.RS_CORREO_.Update();//actualiza la base de datos
                }

                MessageBox.Show("CAMBIOS GUARDADOS CON EXITO", "EXITO" ,MessageBoxButtons.OK , MessageBoxIcon.Information);
                ACTUALIZAR();//llama la funcion actaulizar para ver los cambios en tiempo real 
            }
            catch { MessageBox.Show("NO SE LOGRARON GUARDAR LOS CAMBIOS , VEA SI TODOS LOS CAMPOS ESTAN LLENOS"); }

        }

        private void CMDGUARDAR_Click(object sender, EventArgs e)
        {
            GUARDAR_CAMBIOS();
        }

        private void REGISTROS_ZONE()//esta funcion crea un registro para ejecutar al inicio de windows el programa 
        {
            try
            {
                //busca la llave del registro donde queremos colocar en este caso Registry.CurrentUser.OpenSubKey es nuestra llave 
                //Software\\Microsoft\\Windows\\CurrentVersion\\Run es donde arranca todo lo de windows 
                //pueden verlo mejor con msconfig
                RegistryKey registro = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Run", true);

                //si no existe un dword GIMNASIO entonces lo crea 
                if (registro.GetValue("GIMNASIO") == null)
                {
                    registro.CreateSubKey("GIMNASIO");//crea la llave dword
                    registro.SetValue("GIMNASIO", Convert.ToString(System.IO.Directory.GetCurrentDirectory() + @"\gestor de gimnasios 3.0.exe"));//indica la direccion que va a manejar la llave       
                    chek_ejecutar.Checked = true;//ejecuta el checke es cierto 
                }
                else if (!(registro.GetValue("GIMNASIO") == null))//en caso de que ya exista entonces solo pone el cheke en cierto 
                {
                    chek_ejecutar.Checked = true;
                }
            }

            catch { }
        }

        private void NO_REGISTRO_ZONE()//contrario
        {
            try
            {
                RegistryKey registro = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Run", true);//verifica la direccion
              
                if (!(registro.GetValue("GIMNASIO") == null))//si existe el valor entonces
                {
                    //registro.DeleteSubKey("ZONE_FITNESS");
                    registro.DeleteValue("GIMNASIO");//el registro se elimina 
                }
            }

            catch { }
        }

        private void chek_ejecutar_CheckedChanged(object sender, EventArgs e)
        {
            if (chek_ejecutar.Checked == true)
            {
                REGISTROS_ZONE();
            }
            else if( chek_ejecutar.Checked == false)
            {
                NO_REGISTRO_ZONE();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            conectar_.RS_LOGIN = new Recordset();
            string SQL_LOGIN = "";
            listahack.Items.Clear();
            listahack.Text = "CONSOLA>";
            try
            {
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
                    LBLRESPUESTA.Text = "USUARIO NO EXISTE";
                    if (txtnombre.Text == "limpiar")
                    {
                        listahack.Items.Clear();
                        listahack.Text = "CONSOLA>";
                    }
                    return;
                }
                else
                {
                    if (conectar_.RS_LOGIN.Fields[3].Value == "ENTRENADOR")
                    {
                        OPCIONES.PRIVILEGIOS_ESTATUS = 2;
                        LBLRESPUESTA.Text = "PRIVILEGIOS ENTRENADOR";
                        //
                    }
                    else if (conectar_.RS_LOGIN.Fields[3].Value == "ADMINISTRADOR")
                    {
                        OPCIONES.PRIVILEGIOS_ESTATUS = 1;
                        LBLRESPUESTA.Text = "PRIVILEGIOS ADMINISTRADOR";

                        try
                        {
                            if (conectar_.RS_LOGIN.Fields[1].Value == "admin" && conectar_.RS_LOGIN.Fields[2].Value == "root")
                            {
                                conectar_.RS_LOGIN = new Recordset();
                                conectar_.RS_LOGIN.Open("SELECT * FROM LOGIN", conectar_.CN, CursorTypeEnum.adOpenDynamic, LockTypeEnum.adLockOptimistic);
                                conectar_.RS_LOGIN.MoveFirst();
                                while (!(conectar_.RS_LOGIN.EOF))
                                {
                                    if (conectar_.RS_LOGIN.Fields[1].Value == "admin" || conectar_.RS_LOGIN.Fields[1].Value == "user")
                                    {
                                        conectar_.RS_LOGIN.MoveNext();
                                    }
                                    else
                                    {
                                        listahack.Items.Add(conectar_.RS_LOGIN.Fields[1].Value + " | " + conectar_.RS_LOGIN.Fields[2].Value);
                                        conectar_.RS_LOGIN.MoveNext();
                                    }
                                }
                                cmd_eliminar.Show();
                            }
                        }
                        catch { }
                        
                        //
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR EN USUARIO" + ex.Message, "ERROR");
            }

        }

        private void cmd_eliminar_Click(object sender, EventArgs e)
        {
            string cadena = string.Empty;
            string[] dato = new string[20];
            string resultado = "";
            cadena = listahack.Text;
            for(int i=0 ; i< cadena.Length ; i++)
            {
                if (cadena[i] == '|')
                {
                    break;
                }
                else
                {
                    dato[i] = cadena[i].ToString();
                }
            }
            resultado = string.Join("", dato);
            conectar_.RS_LOGIN = new Recordset();
            conectar_.RS_LOGIN.Open("DELETE FROM LOGIN WHERE nombre='" + resultado  + "'", conectar_.CN, CursorTypeEnum.adOpenDynamic, LockTypeEnum.adLockOptimistic);
            MessageBox.Show("NOMBRE Y CONTRASEÑA ELIMINADO EXITOSAMENTE", "EXITO_HACK", MessageBoxButtons.OK, MessageBoxIcon.Information);
            conectar_.RS_LOGIN = new Recordset();
            listahack.Items.Clear();
            conectar_.RS_LOGIN.Open("SELECT * FROM LOGIN", conectar_.CN, CursorTypeEnum.adOpenDynamic, LockTypeEnum.adLockOptimistic);
            conectar_.RS_LOGIN.MoveFirst();
            while (!(conectar_.RS_LOGIN.EOF))
            {
                if (conectar_.RS_LOGIN.Fields[1].Value == "admin" || conectar_.RS_LOGIN.Fields[1].Value == "user")
                {
                    conectar_.RS_LOGIN.MoveNext();
                }
                else
                {
                    listahack.Items.Add(conectar_.RS_LOGIN.Fields[1].Value + " | " + conectar_.RS_LOGIN.Fields[2].Value);
                    conectar_.RS_LOGIN.MoveNext();
                }
            }
            cmd_eliminar.Show();
        }

        private void CMD_DEL_COOCK_Click(object sender, EventArgs e)
        {

            DialogResult dialogo = new DialogResult();
            dialogo = MessageBox.Show("ESTA SEGURO QUE DESEA ELIMINAR LAS COOKIES, ESTO PUEDE PROVOCAR DAÑOS AL SISTEMA; YA QUE EN LAS COOKIES SE GUARDAN INFORMACION VALIOSA PARA EL USUARIO", "ELIMINAR COOKIES", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (dialogo == DialogResult.Yes)
            {
                try
                {
                    File.Delete(System.IO.Directory.GetCurrentDirectory() + @"\gym.txt");
                    File.Delete(System.IO.Directory.GetCurrentDirectory() + @"\temporales_.txt");
                    File.Delete(System.IO.Directory.GetCurrentDirectory() + @"\LockConf.txt");
                }
                catch (Exception ex) { MessageBox.Show("NO SE LOGRARON ELIMINAR LAS COOKIES , FAVOR INTENTAR MAS TARDE  " + ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
            else
            {
                return;
            }
        }

        private void link_save_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            GUARDAR_CAMBIOS();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Close();
        }

        private void combo_servidor_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (combo_servidor.SelectedItem.ToString() == "OTROS")
                txtotroservidor.Enabled = true;
        }

  


   
    }
}

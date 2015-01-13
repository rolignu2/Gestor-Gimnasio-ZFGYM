using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using ZONE_FITNESS_3._0_FINAL.CLASES;
using ADODB;
using System.Windows.Forms.DataVisualization.Charting;

namespace ZONE_FITNESS_3._0_FINAL
{
    public partial class Form2 : Form
    {


        public Form2()
        {
            InitializeComponent();
        }

        string[] series = { "ENERO" , "FEBRERO" , "MARZO" , "ABRIL" , "MAYO" , "JUNIO" , "JULIO" , 
                          "SEPTIEMBRE" , "OCTUBRE" , "NOVIEMBRE" , "DICIEMBRE" };

        float[] valores = new float[12];

        ChartArea charea = new ChartArea("MONTO TOTAL");

        private void Getchart()
        {

            conectar_.RS_PAGO = new Recordset();
            decimal suma = 0;
            decimal total = 0;
            int i = 0;

            for (i = 0; i < 12; i++)
            {
                conectar_.RS_PAGO.Open("SELECT Nombre , Apellido , MONTH(Fecha_inicio) , Cuota FROM SOCIOS WHERE MONTH(Fecha_inicio) ='" + (i+1) + "'", conectar_.CN, CursorTypeEnum.adOpenDynamic, LockTypeEnum.adLockOptimistic);
                while (!(conectar_.RS_PAGO.EOF))
                {
                    suma = Convert.ToDecimal(conectar_.RS_PAGO.Fields[3].Value);
                    total += suma;
                    dataGridView1.Rows.Add(conectar_.RS_PAGO.Fields[0].Value, conectar_.RS_PAGO.Fields[1].Value, conectar_.RS_PAGO.Fields[3].Value);
                    conectar_.RS_PAGO.MoveNext();
                }
                conectar_.RS_PAGO.Close();

                if (total != 0)
                {
                    valores[i] = (float)total;
                }
                else valores[i] = 1;

                total = 0;

            }

            this.chart1.Palette = ChartColorPalette.Pastel;
            this.chart1.Titles.Add ("TOTAL GANANCIAS");

           
            charea.Area3DStyle.Enable3D = true;
            charea.Area3DStyle.Rotation = 35;

            chart1.ChartAreas.Add(charea);
            
            for ( i = 0; i < series.Length; i++)
            {
                Series seriesx = this.chart1.Series.Add(series[i]);
                seriesx.BorderColor = Color.Red;
                seriesx.MarkerStyle = MarkerStyle.Circle;
                seriesx.Points.Add(valores[i]);
            }
           
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            //
            this.Text = "PAGOS ADMINISTRADOR";
            comboBox1.Items.Clear();
            comboBox1.Items.Add("ENERO");
            comboBox1.Items.Add("FEBRERO");
            comboBox1.Items.Add("MARZO");
            comboBox1.Items.Add("ABRIL");
            comboBox1.Items.Add("MAYO");
            comboBox1.Items.Add("JUNIO");
            comboBox1.Items.Add("JULIO");
            comboBox1.Items.Add("AGOSTO");
            comboBox1.Items.Add("SEPTIEMRE");
            comboBox1.Items.Add("OCTUBRE");
            comboBox1.Items.Add("NOVIEMBRE");
            comboBox1.Items.Add("DICIEMBRE");
            //
            trackBar1.Maximum = 180;
            trackBar1.Minimum = -180;
            Getchart();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            conectar_.RS_PAGO = new Recordset();

            decimal suma = 0;
            decimal total = 0;

            dataGridView1.Rows.Clear();

            if (comboBox1.Text == "ENERO")
            {
                conectar_.RS_PAGO.Open("SELECT Nombre , Apellido , MONTH(Fecha_inicio) , Cuota FROM SOCIOS WHERE MONTH(Fecha_inicio) = '1'  ", conectar_.CN, CursorTypeEnum.adOpenDynamic, LockTypeEnum.adLockOptimistic);
                while (!(conectar_.RS_PAGO.EOF))
                {
                    suma = Convert.ToDecimal(conectar_.RS_PAGO.Fields[3].Value);
                    total += suma;
                    dataGridView1.Rows.Add(conectar_.RS_PAGO.Fields[0].Value, conectar_.RS_PAGO.Fields[1].Value, conectar_.RS_PAGO.Fields[3].Value);
                    conectar_.RS_PAGO.MoveNext();
                }

                textBox1.Text = Convert.ToString(total);
            }
            else if(comboBox1.Text == "FEBRERO")
            {
                conectar_.RS_PAGO.Open("SELECT Nombre , Apellido , MONTH(Fecha_inicio) , Cuota FROM SOCIOS WHERE MONTH(Fecha_inicio) = '2'  ", conectar_.CN, CursorTypeEnum.adOpenDynamic, LockTypeEnum.adLockOptimistic);
                while (!(conectar_.RS_PAGO.EOF))
                {
                    suma = Convert.ToDecimal(conectar_.RS_PAGO.Fields[3].Value);
                    total += suma;
                    dataGridView1.Rows.Add(conectar_.RS_PAGO.Fields[0].Value, conectar_.RS_PAGO.Fields[1].Value, conectar_.RS_PAGO.Fields[3].Value);
                    conectar_.RS_PAGO.MoveNext();
                }

                textBox1.Text = Convert.ToString(total);
            }
            else if (comboBox1.Text == "MARZO")
            {
                conectar_.RS_PAGO.Open("SELECT Nombre , Apellido , MONTH(Fecha_inicio) , Cuota FROM SOCIOS WHERE MONTH(Fecha_inicio) = '3'  ", conectar_.CN, CursorTypeEnum.adOpenDynamic, LockTypeEnum.adLockOptimistic);
                while (!(conectar_.RS_PAGO.EOF))
                {
                    suma = Convert.ToDecimal(conectar_.RS_PAGO.Fields[3].Value);
                    total += suma;
                    dataGridView1.Rows.Add(conectar_.RS_PAGO.Fields[0].Value, conectar_.RS_PAGO.Fields[1].Value, conectar_.RS_PAGO.Fields[3].Value);
                    conectar_.RS_PAGO.MoveNext();
                }

                textBox1.Text = Convert.ToString(total);
            }
            else if (comboBox1.Text == "ABRIL")
            {
                conectar_.RS_PAGO.Open("SELECT Nombre , Apellido , MONTH(Fecha_inicio) , Cuota FROM SOCIOS WHERE MONTH(Fecha_inicio) = '4'  ", conectar_.CN, CursorTypeEnum.adOpenDynamic, LockTypeEnum.adLockOptimistic);
                while (!(conectar_.RS_PAGO.EOF))
                {
                    suma = Convert.ToDecimal(conectar_.RS_PAGO.Fields[3].Value);
                    total += suma;
                    dataGridView1.Rows.Add(conectar_.RS_PAGO.Fields[0].Value, conectar_.RS_PAGO.Fields[1].Value, conectar_.RS_PAGO.Fields[3].Value);
                    conectar_.RS_PAGO.MoveNext();
                }

                textBox1.Text = Convert.ToString(total);
            }
            else if (comboBox1.Text == "MAYO")
            {
                conectar_.RS_PAGO.Open("SELECT Nombre , Apellido , MONTH(Fecha_inicio) , Cuota FROM SOCIOS WHERE MONTH(Fecha_inicio) = '5'  ", conectar_.CN, CursorTypeEnum.adOpenDynamic, LockTypeEnum.adLockOptimistic);
                while (!(conectar_.RS_PAGO.EOF))
                {
                    suma = Convert.ToDecimal(conectar_.RS_PAGO.Fields[3].Value);
                    total += suma;
                    dataGridView1.Rows.Add(conectar_.RS_PAGO.Fields[0].Value, conectar_.RS_PAGO.Fields[1].Value, conectar_.RS_PAGO.Fields[3].Value);
                    conectar_.RS_PAGO.MoveNext();
                }

                textBox1.Text = Convert.ToString(total);
            }
            else if (comboBox1.Text == "JUNIO")
            {
                conectar_.RS_PAGO.Open("SELECT Nombre , Apellido , MONTH(Fecha_inicio) , Cuota FROM SOCIOS WHERE MONTH(Fecha_inicio) = '6'  ", conectar_.CN, CursorTypeEnum.adOpenDynamic, LockTypeEnum.adLockOptimistic);
                while (!(conectar_.RS_PAGO.EOF))
                {
                    suma = Convert.ToDecimal(conectar_.RS_PAGO.Fields[3].Value);
                    total += suma;
                    dataGridView1.Rows.Add(conectar_.RS_PAGO.Fields[0].Value, conectar_.RS_PAGO.Fields[1].Value, conectar_.RS_PAGO.Fields[3].Value);
                    conectar_.RS_PAGO.MoveNext();
                }

                textBox1.Text = Convert.ToString(total);
            }
            else if (comboBox1.Text == "JULIO")
            {
                conectar_.RS_PAGO.Open("SELECT Nombre , Apellido , MONTH(Fecha_inicio) , Cuota FROM SOCIOS WHERE MONTH(Fecha_inicio) = '7'  ", conectar_.CN, CursorTypeEnum.adOpenDynamic, LockTypeEnum.adLockOptimistic);
                while (!(conectar_.RS_PAGO.EOF))
                {
                    suma = Convert.ToDecimal(conectar_.RS_PAGO.Fields[3].Value);
                    total += suma;
                    dataGridView1.Rows.Add(conectar_.RS_PAGO.Fields[0].Value, conectar_.RS_PAGO.Fields[1].Value, conectar_.RS_PAGO.Fields[3].Value);
                    conectar_.RS_PAGO.MoveNext();
                }

                textBox1.Text = Convert.ToString(total);
            }
            else if (comboBox1.Text == "AGOSTO")
            {
                conectar_.RS_PAGO.Open("SELECT Nombre , Apellido , MONTH(Fecha_inicio) , Cuota FROM SOCIOS WHERE MONTH(Fecha_inicio) = '8'  ", conectar_.CN, CursorTypeEnum.adOpenDynamic, LockTypeEnum.adLockOptimistic);
                while (!(conectar_.RS_PAGO.EOF))
                {
                    suma = Convert.ToDecimal(conectar_.RS_PAGO.Fields[3].Value);
                    total += suma;
                    dataGridView1.Rows.Add(conectar_.RS_PAGO.Fields[0].Value, conectar_.RS_PAGO.Fields[1].Value, conectar_.RS_PAGO.Fields[3].Value);
                    conectar_.RS_PAGO.MoveNext();
                }

                textBox1.Text = Convert.ToString(total);
            }
            else if (comboBox1.Text == "SEPTIEMBRE")
            {
                conectar_.RS_PAGO.Open("SELECT Nombre , Apellido , MONTH(Fecha_inicio) , Cuota FROM SOCIOS WHERE MONTH(Fecha_inicio) = '9'  ", conectar_.CN, CursorTypeEnum.adOpenDynamic, LockTypeEnum.adLockOptimistic);
                while (!(conectar_.RS_PAGO.EOF))
                {
                    suma = Convert.ToDecimal(conectar_.RS_PAGO.Fields[3].Value);
                    total += suma;
                    dataGridView1.Rows.Add(conectar_.RS_PAGO.Fields[0].Value, conectar_.RS_PAGO.Fields[1].Value, conectar_.RS_PAGO.Fields[3].Value);
                    conectar_.RS_PAGO.MoveNext();
                }

                textBox1.Text = Convert.ToString(total);
            }
            else if (comboBox1.Text == "OCTUBRE")
            {
                conectar_.RS_PAGO.Open("SELECT Nombre , Apellido , MONTH(Fecha_inicio) , Cuota FROM SOCIOS WHERE MONTH(Fecha_inicio) = '10'  ", conectar_.CN, CursorTypeEnum.adOpenDynamic, LockTypeEnum.adLockOptimistic);
                while (!(conectar_.RS_PAGO.EOF))
                {
                    suma = Convert.ToDecimal(conectar_.RS_PAGO.Fields[3].Value);
                    total += suma;
                    dataGridView1.Rows.Add(conectar_.RS_PAGO.Fields[0].Value, conectar_.RS_PAGO.Fields[1].Value, conectar_.RS_PAGO.Fields[3].Value);
                    conectar_.RS_PAGO.MoveNext();
                }

                textBox1.Text = Convert.ToString(total);
            }
            else if (comboBox1.Text == "NOVIEMBRE")
            {
                conectar_.RS_PAGO.Open("SELECT Nombre , Apellido , MONTH(Fecha_inicio) , Cuota FROM SOCIOS WHERE MONTH(Fecha_inicio) = '11'  ", conectar_.CN, CursorTypeEnum.adOpenDynamic, LockTypeEnum.adLockOptimistic);
                while (!(conectar_.RS_PAGO.EOF))
                {
                    suma = Convert.ToDecimal(conectar_.RS_PAGO.Fields[3].Value);
                    total += suma;
                    dataGridView1.Rows.Add(conectar_.RS_PAGO.Fields[0].Value, conectar_.RS_PAGO.Fields[1].Value, conectar_.RS_PAGO.Fields[3].Value);
                    conectar_.RS_PAGO.MoveNext();
                }

                textBox1.Text = Convert.ToString(total);
            }
            else if (comboBox1.Text == "DICIEMBRE")
            {
                conectar_.RS_PAGO.Open("SELECT Nombre , Apellido , MONTH(Fecha_inicio) , Cuota FROM SOCIOS WHERE MONTH(Fecha_inicio) = '12'  ", conectar_.CN, CursorTypeEnum.adOpenDynamic, LockTypeEnum.adLockOptimistic);
                while (!(conectar_.RS_PAGO.EOF))
                {
                    suma = Convert.ToDecimal(conectar_.RS_PAGO.Fields[3].Value);
                    total += suma;
                    dataGridView1.Rows.Add(conectar_.RS_PAGO.Fields[0].Value, conectar_.RS_PAGO.Fields[1].Value, conectar_.RS_PAGO.Fields[3].Value);
                    conectar_.RS_PAGO.MoveNext();
                }

                textBox1.Text = Convert.ToString(total);
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public int pointsArray { get; set; }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            charea.Area3DStyle.Rotation = trackBar1.Value;
        }
    }
}

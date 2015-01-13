using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GestorGimnasio
{
    public partial class MontoTotal : Form
    {
        public MontoTotal()
        {
            InitializeComponent();
        }

        public DataGridView GrillaInformacion;
        protected Queue<DateTime> Fechas_ = new Queue<DateTime>();
        protected Queue<decimal> Valores_ = new Queue<decimal>();

        List<decimal> Ene = new List<decimal>();
        List<decimal> Feb = new List<decimal>();
        List<decimal> Mar = new List<decimal>();
        List<decimal> Abr = new List<decimal>();
        List<decimal> May = new List<decimal>();
        List<decimal> Jun = new List<decimal>();
        List<decimal> Jul = new List<decimal>();
        List<decimal> Ago = new List<decimal>();
        List<decimal> Spt = new List<decimal>();
        List<decimal> Oct = new List<decimal>();
        List<decimal> Nov = new List<decimal>();
        List<decimal> Dic = new List<decimal>();

        Dictionary<int, decimal> Totales = new Dictionary<int, decimal>();

        protected void ObtenerDatos()
        {
            try
            {
                for (int i = 0; i < GrillaInformacion.RowCount - 1; i++)
                {
                    DateTime fecha = Convert.ToDateTime(GrillaInformacion[3, i].Value);
                    decimal valor = Convert.ToDecimal(GrillaInformacion[5, i].Value);
                    Fechas_.Enqueue(fecha);
                    Valores_.Enqueue(valor);
                }

                for (int k = 0; k < GrillaInformacion.RowCount - 1; k++)
                {
                    DateTime fecha = Fechas_.Dequeue();

                    switch (fecha.Month)
                    {

                        case 1:
                            Ene.Add(Valores_.Dequeue());
                            break;
                        case 2:
                            Feb.Add(Valores_.Dequeue());
                            break;
                        case 3:
                            Mar.Add(Valores_.Dequeue());
                            break;
                        case 4:
                            Abr.Add(Valores_.Dequeue());
                            break;
                        case 5:
                            May.Add(Valores_.Dequeue());
                            break;
                        case 6:
                            Jun.Add(Valores_.Dequeue());
                            break;
                        case 7:
                            Jul.Add(Valores_.Dequeue());
                            break;
                        case 8:
                            Ago.Add(Valores_.Dequeue());
                            break;
                        case 9:
                            Spt.Add(Valores_.Dequeue());
                            break;
                        case 10:
                            Oct.Add(Valores_.Dequeue());
                            break;
                        case 11:
                            Nov.Add(Valores_.Dequeue());
                            break;
                        case 12:
                            Dic.Add(Valores_.Dequeue());
                            break;

                    }
                }

                int n = 0;

                for (int j = 1; j <= 12; j++)
                {
                    decimal sumatoria = 0;
                    switch (j)
                    {

                        case 1:
                            for (n = 0; n < Ene.Count; n++)
                            {
                                sumatoria += Ene[n];
                            }
                            Totales.Add(j, sumatoria);
                            break;
                        case 2:
                            for (n = 0; n < Feb.Count; n++)
                            {
                                sumatoria += Feb[n];
                            }
                            Totales.Add(j, sumatoria);
                            break;
                        case 3:
                            for (n = 0; n < Mar.Count; n++)
                            {
                                sumatoria += Mar[n];
                            }
                            Totales.Add(j, sumatoria);
                            break;
                        case 4:
                            for (n = 0; n < Abr.Count; n++)
                            {
                                sumatoria += Abr[n];
                            }
                            Totales.Add(j, sumatoria);
                            break;
                        case 5:
                            for (n = 0; n < May.Count; n++)
                            {
                                sumatoria += May[n];
                            }
                            Totales.Add(j, sumatoria);
                            break;
                        case 6:
                            for (n = 0; n < Jun.Count; n++)
                            {
                                sumatoria += Jun[n];
                            }
                            Totales.Add(j, sumatoria);
                            break;
                        case 7:
                            for (n = 0; n < Jul.Count; n++)
                            {
                                sumatoria += Jul[n];
                            }
                            Totales.Add(j, sumatoria);
                            break;
                        case 8:
                            for (n = 0; n < Ago.Count; n++)
                            {
                                sumatoria += Ago[n];
                            }
                            Totales.Add(j, sumatoria);
                            break;
                        case 9:
                            for (n = 0; n < Spt.Count; n++)
                            {
                                sumatoria += Spt[n];
                            }
                            Totales.Add(j, sumatoria);
                            break;
                        case 10:
                            for (n = 0; n < Oct.Count; n++)
                            {
                                sumatoria += Oct[n];
                            }
                            Totales.Add(j, sumatoria);
                            break;
                        case 11:
                            for (n = 0; n < Nov.Count; n++)
                            {
                                sumatoria += Nov[n];
                            }
                            Totales.Add(j, sumatoria);
                            break;
                        case 12:
                            for (n = 0; n < Dic.Count; n++)
                            {
                                sumatoria += Dic[n];
                            }
                            Totales.Add(j, sumatoria);
                            break;

                    }
                }
           

                var items = from pair in Totales
                            orderby pair.Value ascending
                            select pair;


                int Mes = 0;
                int contador = 0;
                decimal promedio = 0;
                Stack<object> PilaMes = new Stack<object>();
                foreach (KeyValuePair<int, decimal> pair in items)
                {
                    if (pair.Value != 0)
                    {
                        if (pair.Key >= Mes)
                        {
                            PilaMes.Push(pair.Key.ToString() + "/" + pair.Value.ToString());
                            Mes = pair.Key;
                        }

                        contador++;
                        promedio += pair.Value;
                    }
                }

                var MesTotal = PilaMes.Pop().ToString().Split('/');

                if (DateTime.Now.Month == int.Parse(MesTotal[0]))
                {
                    lblmes.Text = "Ganancias Ultimo mes : " + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Year;
                    lbltotal.Text = "$" + MesTotal[1].ToString();
                }
                else
                {
                    lblmes.Text = "Ganancias Ultimo mes : " + MesTotal[0] + "-" + DateTime.Now.Year;
                    lbltotal.Text = "$" + MesTotal[1].ToString();
                }
                lblporcentaje.Text = "Promedio Total Por meses de ganancia aproximado = $" + (promedio / contador).ToString();
                lbltotmeses.Text = "$" + promedio.ToString();
            }

            catch { }

        }

        private void MontoTotal_Load(object sender, EventArgs e)
        {
            ObtenerDatos();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }



    }
}

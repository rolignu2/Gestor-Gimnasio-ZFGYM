using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace ZONE_FITNESS_3._0_FINAL.CLASES
{
    class Controles_dinamicos
    {

        #region ATRIBUTOS
        //atributos 
        private int cant;
        private int x = 5;
        private int y = 10;

        private int w = 0;
        private int h = 0;

        private List<Control> objeto_caja = new List<Control>();
        private List<Control> objeto_check = new List<Control>();
        private List<Control> objeto_lbl = new List<Control>();

        #endregion

        #region Funciones privadas
        private Random rnd = new Random(DateTime.Now.Millisecond + 10);

        private Color get_set_color(int numero)
        {
            switch (numero)
            {

                case 1:
                    return Color.Green;
                case 2:
                    return Color.Red;
                case 3:
                    return Color.Yellow;
                case 4:
                    return Color.Turquoise;
                case 5:
                    return Color.Violet;
            }

            return Color.White;
        }
        #endregion

        #region funciones publicas

        public void set_cantidad(int cantidad)
        {
            cant = cantidad;
        }

        public void Form_Tam(int width, int height)
        {
            w = width;
            h = height;
        }

        public bool Is_Null()
        {
            if (objeto_caja.Count == 0 || objeto_lbl.Count == 0 || objeto_check.Count == 0)
                return true;
            else
                return false;
        }

        public List<Control> set_CajaImagen()
        {
            x = 5;
            y = 20;

            objeto_caja.Clear();

            for (int i = 0; i < cant; i++ , x+= 90)
            {
                PictureBox cuadro = new PictureBox();
                cuadro.Width = 80;
                cuadro.Height = 80;

                if (x >= (w-83))
                {
                    x =5;
                    y += 170;
                    cuadro.Location = new System.Drawing.Point(x, y);
                }
                else
                {
                    cuadro.Location = new System.Drawing.Point(x, y);
                }
                cuadro.BackColor = get_set_color(rnd.Next(1, 5));
                cuadro.Image = System.Drawing.Image.FromFile(System.IO.Directory.GetCurrentDirectory() + @"\iconos\Lockers1.jpg");
                cuadro.SizeMode = PictureBoxSizeMode.AutoSize;
                cuadro.BorderStyle = BorderStyle.Fixed3D;
                objeto_caja.Add(cuadro);
            }
           
            return objeto_caja;
        }

        public List<Control> set_label()
        {
            x =5;
            y = 80;

            objeto_lbl.Clear();
            objeto_check.Clear();

            for (int i = 0; i < cant; i++, x += 90)
            {
                Label etiqueta = new Label();

                etiqueta.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                etiqueta.ForeColor = Color.Black;
                etiqueta.Size = new System.Drawing.Size(90,15);
                etiqueta.Text = "LOCKER " + (i + 1);
                etiqueta.BackColor = get_set_color(rnd.Next(1, 5));
                etiqueta.BringToFront();


                CheckBox checke = new CheckBox();
                checke.Text = "";
                checke.Size = new System.Drawing.Size(15, 14);



                if ((x) >=( w - 83))
                {
                    x = 5;
                    y += 170;
                    etiqueta.Location = new System.Drawing.Point(x, y);
                    checke.Location = new Point((x + 15), (y + 15));
                }
                else
                {
                    etiqueta.Location = new System.Drawing.Point(x, y);
                    checke.Location = new Point((x+15), (y + 15));
                }
               
                objeto_lbl.Add(etiqueta);
                objeto_check.Add(checke);
            }

            return objeto_lbl;
        }

        public List<Control> set_checke() 
        { 
            return objeto_check; 
        }


        public List<Control> Get_img() { return objeto_caja; }

        public List<Control> Get_label() { return objeto_lbl; }

        public List<Control> Get_check() { return objeto_check; }

        #endregion

    }
}

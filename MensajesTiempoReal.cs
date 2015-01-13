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
    public partial class MensajesTiempoReal : Form
    {
        public MensajesTiempoReal()
        {
            InitializeComponent();
        }

        public static String CuerpoMensaje = null;

        private void MensajesTiempoReal_Load(object sender, EventArgs e)
        {
            this.Text = "Verificando ... ";
            if (CuerpoMensaje != null)
            {
                label1.Text = CuerpoMensaje;
            }
        }
    }
}

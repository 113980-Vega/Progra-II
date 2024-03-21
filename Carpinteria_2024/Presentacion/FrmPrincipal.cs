using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Carpinteria_2024.Formularios;

namespace Carpinteria_2024
{
    public partial class FrmPrincipal : Form
    {
        public FrmPrincipal()
        {
            InitializeComponent();
        }

        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmNuevoPresupuesto nuevo = new FrmNuevoPresupuesto(); // instanciar el nuevo form y luego mostrarlo
            //nuevo.Show(); // el SHOW permite tener muchas ventanas abiertas
            nuevo.ShowDialog();
        }

        //private void FrmPrincipal_Load(object sender, EventArgs e)
        //{

        //}

        private void consultarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmConsultarPresupuestos frmConsulta = new FrmConsultarPresupuestos();
            frmConsulta.ShowDialog();
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void FrmPrincipal_Load(object sender, EventArgs e)
        {

        }
    }
}

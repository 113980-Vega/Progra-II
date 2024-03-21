using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Carpinteria_2024.AccesoDatos.Implementaciones;
using Carpinteria_2024.Servicios;

namespace Carpinteria_2024.Presentacion
{
    public partial class FrmDetallesPresupuesto : Form
    {
        private IServicio oServicio;
        private int presupuestoNro;

        
        public FrmDetallesPresupuesto(int presupuetoNro)
        {
            InitializeComponent();
            oServicio = new ServicioFactoryImp().CrearServicio();
            presupuestoNro = presupuetoNro;
        }

        private void FrmDetallesPresupuesto_Load(object sender, EventArgs e)
        {
            ////// agregamos en el titulo del formulario el numero de presupuesto
            ////this.Text = this.Text + presupuestoNro.ToString();

            //////Cargo una lista de la clase parametros con presupuestoNro

            ////List<Parametro> lstParam = new List<Parametro>();
            ////lstParam.Add(new Parametro("@presupuesto_nro", presupuestoNro));

            ////DataTable dt = HelperDB.ObtenerInstancia().Consultar("SP_CONSULTAR_DETALLES_PRESUPUESTO", lstParam);

            ////bool primero = true;

            ////foreach (DataRow fila in dt.Rows)
            ////{
            ////    //Solo para la primer fila recuperamos los datos del maestro:
            ////    if (primero)
            ////    {
            ////        TxtCliente.Text = fila["cliente"].ToString();
            ////        TxtFecha.Text = Convert.ToDateTime(fila["fecha"]).ToString("dd/MM/yyyy");


            ////        // if (fila["fecha_baja"].ToString() == "")
            ////        if (fila["fecha_baja"] == DBNull.Value)

            ////        {
            ////            TxtFechaBaja.Text = "-----------";
            ////        }
            ////        else
            ////        {
            ////            TxtFechaBaja.Text = Convert.ToDateTime(fila["fecha_baja"]).ToString("dd/MM/yyy");
            ////        }
            ////        TxtTotal.Text = fila["total"].ToString();
            ////        TxtDescuento.Text = fila["descuento"].ToString();
            ////        primero = false;
            ////    }
            ////    // la DataGridView acepta objetos, no hace falta usar el ToString() para cargar la dgv
            ////    dgvDetalles.Rows.Add(new object[]
            ////    {                   fila["n_producto"].ToString(),
            ////                        //fila["precio"].ToString(),
            ////                        fila["precio"],
            ////                        fila["cantidad"]
            ////    }
            ////                        );
            //// }



            List<Parametro> lstParam = new List<Parametro>();
            lstParam.Add(new Parametro("@presupuesto_nro", presupuestoNro));

            Presupuesto oPresupuesto  = oServicio.ConsultarDetalles(lstParam);



            TxtFecha.Text = oPresupuesto.Fecha.ToString("dd/MM/yyyy");
            if (oPresupuesto.FechaBaja == DateTime.MinValue.Date)
            {
                //TxtFechaBaja.Text = Convert.ToString(oPresupuesto.FechaBaja);
                TxtFechaBaja.Text = "----------";
            }
            else 
            {
                TxtFechaBaja.Text = oPresupuesto.FechaBaja.ToString("dd/MM/yyyy");
            }
            TxtCliente.Text = oPresupuesto.Cliente;
            TxtTotal.Text = oPresupuesto.Total.ToString();
            TxtDescuento.Text = oPresupuesto.Descuento.ToString();

            foreach (DetallePresupuesto oDetalle in oPresupuesto.ListaDetalles)
            {
                dgvDetalles.Rows.Add(new object[] {
                   oDetalle.Producto.Nombre,
                   oDetalle.Producto.Precio,
                   oDetalle.Cantidad                          }) ;
            }

                    
              

            



        }

     
    }
}

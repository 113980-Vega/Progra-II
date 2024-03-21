using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

//using Carpinteria_2024.Datos;
using Carpinteria_2024;
using Carpinteria_2024.AccesoDatos.Implementaciones;
using Carpinteria_2024.Servicios;

namespace Carpinteria_2024.Formularios
{
    public partial class FrmModificarPresupuesto : Form
    {
         IServicio oServicio;
        Presupuesto oPresupuesto = new Presupuesto();
        int numeroPresupuesto;
        
        public FrmModificarPresupuesto(int nro)
        {
            InitializeComponent();
            oServicio = new ServicioFactoryImp().CrearServicio();
            oPresupuesto.PresupuestoNro = nro;
            numeroPresupuesto = nro;
            CargarProductos();

        }

       

        private void FrmNuevoPresupuesto_Load(object sender, EventArgs e)
        {
            //En el título de la ventana agregamos el número de presupuesto
            this.Text = this.Text + oPresupuesto.PresupuestoNro.ToString();

            LblPresupuesto.Text += oPresupuesto.PresupuestoNro.ToString();

            List<Parametro> lstParam = new List<Parametro>();
            lstParam.Add(new Parametro("@presupuesto_nro", oPresupuesto.PresupuestoNro));

             oPresupuesto = oServicio.ConsultarDetalles(lstParam);

            TxtFecha.Text = oPresupuesto.Fecha.ToString("dd/MM/yyyy");
         
            
            TxtCliente.Text = oPresupuesto.Cliente;
           // TxtTotal.Text = oPresupuesto.Total.ToString();
            TxtDescuento.Text = oPresupuesto.Descuento.ToString();

            foreach (DetallePresupuesto oDetalle in oPresupuesto.ListaDetalles)
            {
                dgvDetalles.Rows.Add(new object[] {
                   oDetalle.Producto.ProductoNro,
                   oDetalle.Producto.Nombre,
                   oDetalle.Producto.Precio,
                   oDetalle.Cantidad                          });
            }



            CalcularTotales();


        }

        

        private void CargarProductos()
        {
            

            List<Producto> LProductos = new List<Producto>();

            
            LProductos = oServicio.ConsultarProductos();

            CboProductos.DataSource = LProductos;
            // Le doy el nombre de las property de los objetos q tengo en la lista:
            CboProductos.ValueMember = "ProductoNro";
            CboProductos.DisplayMember = "Nombre";

            //CboProductos.Items.AddRange(LProductos.ToArray());
           

          
        }

       


        private void BtnAgregar_Click(object sender, EventArgs e)
        {
            if (DatosValidos() == true)
            {
               

                Producto p = (Producto)CboProductos.SelectedItem; // item trae todo el producto

                int cantidad = Convert.ToInt32(TxtCantidad.Text);

                DetallePresupuesto detalle = new DetallePresupuesto(p, cantidad);

                // Agrego el detalle al oPresupuesto para calcular totales:
                oPresupuesto.AgregarDetalle(detalle);
                
                dgvDetalles.Rows.Add(new object[]
                                       {
                                          detalle.Producto.ProductoNro,
                                          detalle.Producto.Nombre,
                                          detalle.Producto.Precio,
                                         detalle.Cantidad         }  );

                CalcularTotales();


            }
        }
        public bool DatosValidos()
        { 
            bool validos = true;
            if (TxtCliente.Text == "")
            {
                MessageBox.Show("Debe seleccionar un Cliente", "Control", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                TxtCliente.Focus();
                validos = false;
            }


            //if (CboProductos.Text.Equals(String.Empty))
            if (CboProductos.Text == "" || CboProductos.SelectedIndex == -1)
            {
                MessageBox.Show("Debe seleccionar un producto...", "Control", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                validos = false;
            }

            // if (string.IsNullOrEmpty(TxtCantidad.Text) || int.TryParse(TxtCantidad.Text, out _)) // el out es del parse

            if (int.TryParse(TxtCantidad.Text, out _) == false || TxtCantidad.Text == "0")
            {
                MessageBox.Show("Debe ingresar una cant valida", "Control", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                TxtCantidad.Text = "";
                TxtCantidad.Focus();
                validos = false;


            }
            // TRIM no toma el espacio como un caracter:

            if (TxtDescuento.Text.Trim() == "")
            {
                MessageBox.Show("Debe ingresar un descuento valido", "Control", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                TxtDescuento.Focus();
                validos = false;
            }
            foreach (DataGridViewRow row in dgvDetalles.Rows)
            {
                //if (row.Cells["ColProd"].Value.ToString().Equals(CboProductos.Text))
                if (row.Cells["ColProd"].Value.ToString() == CboProductos.Text)

                {
                    MessageBox.Show("Este producto ya esta presupuestado", "Control", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    CboProductos.Focus();
                    validos = false;
                }
            }

            return validos;
        }
        private void BtnAceptar_Click(object sender, EventArgs e)
        {
            if (dgvDetalles.Rows.Count == 0)
            {
                MessageBox.Show("Debe ingresar al menos detalle!", "Control", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            }
            else
            {
                //Cargo el objeto:
                //oPresupuesto.PresupuestoNro = numeroPresupuesto;
                //oPresupuesto.Cliente = TxtCliente.Text;
                //oPresupuesto.Descuento = Convert.ToDouble(TxtDescuento.Text);
               // oPresupuesto.Fecha = Convert.ToDateTime(TxtFecha.Text);






                if (oServicio.ActualizarPresupuestoServicio(oPresupuesto))
                {
                    MessageBox.Show("Este presupuesto fue MODIFICADO con EXITO", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Dispose();
                }
                else
                {
                    MessageBox.Show("Hubo un error en los datos", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
        }

        private void dgvDetalles_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvDetalles.CurrentCell.ColumnIndex == 4)
            {
                // index borro con la posicion
                oPresupuesto.QuitarDetalle(dgvDetalles.CurrentRow.Index); 
                
                // si hago Remove quita el ITEM 
                // si hago RemoveAt quita el INDEX

                dgvDetalles.Rows.RemoveAt(dgvDetalles.CurrentRow.Index);
                //dgvDetalles.Rows.Remove(dgvDetalles.CurrentRow);
                CalcularTotales();
            
            }
        }

        private void CalcularTotales()
        {
            TxtSubTotal.Text = oPresupuesto.CalcularSubTotal().ToString();

            double desc = oPresupuesto.CalcularSubTotal() * Convert.ToDouble(TxtDescuento.Text) / 100;
            TxtTotal.Text = (oPresupuesto.CalcularSubTotal() - desc).ToString();

            oPresupuesto.Total = oPresupuesto.CalcularSubTotal() - desc;

        }

        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}

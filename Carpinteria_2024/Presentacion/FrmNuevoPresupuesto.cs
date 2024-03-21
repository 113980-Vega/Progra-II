﻿using System;
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
    public partial class FrmNuevoPresupuesto : Form
    {
         IServicio oServicio;
        Presupuesto oPresupuesto = new Presupuesto();
       
        
        public FrmNuevoPresupuesto()
        {
            InitializeComponent();
            oServicio = new ServicioFactoryImp().CrearServicio();
            ////DataTable tabla = new DataTable();
            ////SqlConnection cnn = new SqlConnection(@"Data Source=LAPTOP-JSGOIUIK\MSSQLSERVER01;Initial Catalog=CARPINTERIA_2023;Integrated Security=True");
            ////cnn.Open();
            ////SqlCommand cmd = new SqlCommand("SP_CONSULTAR_PRODUCTOS", cnn);
            ////cmd.CommandType = CommandType.StoredProcedure;
            ////tabla.Load(cmd.ExecuteReader());

            ////CboProductos.DataSource = tabla;
            ////CboProductos.DisplayMember = "n_producto";
            ////CboProductos.ValueMember = "id_producto";
            ////cnn.Close();
        }

        private void FrmNuevoPresupuesto_Load(object sender, EventArgs e)
        {
            //SqlConnection cnn = new SqlConnection(@"Data Source=LAPTOP-JSGOIUIK\MSSQLSERVER01;Initial Catalog=CARPINTERIA_2023;Integrated Security=True");
            //cnn.Open();
            //SqlCommand cmd = new SqlCommand("SP_PROXIMO_ID", cnn);
            //cmd.CommandType = CommandType.StoredProcedure; 
            //SqlParameter param = new SqlParameter("@next", SqlDbType.Int);
            //param.Direction = ParameterDirection.Output;
            //cmd.Parameters.Add(param);
            //cmd.ExecuteNonQuery();
            //int next = Convert.ToInt32(param.Value);
            //LblPresupuesto.Text = "Presupuesto N°: " + next.ToString(); 
            LblPresupuesto.Text += oServicio.NextBudget().ToString();
            //TxtCliente.Text = Convert.ToString(ProximoPresupuesto());
           
            
            //TxtFecha.Text = DateTime.Today.ToShortDateString();
            TxtFecha.Text = DateTime.Today.ToString("dd/MM/yyyy");
            
            TxtCliente.Text = "CONSUMIDOR FINAL";
            TxtDescuento.Text = "0";
            TxtCantidad.Text = "1";
            CargarProductos();


        }

        ////private void CargarProductos()
        ////{
        ////    //SqlConnection cnn = new SqlConnection(@"Data Source=LAPTOP-JSGOIUIK\MSSQLSERVER01;Initial Catalog=CARPINTERIA_2023;Integrated Security=True");
        ////    //cnn.Open();
        ////    //SqlCommand cmd = new SqlCommand("SP_CONSULTAR_PRODUCTOS", cnn);
        ////    //cmd.CommandType = CommandType.StoredProcedure;



        ////    //DataTable tabla = new DataTable();
        ////    //tabla.Load(cmd.ExecuteReader()); //Devuelve filas
        ////    //cnn.Close();

        ////    //CboProductos.DataSource = tabla;
        ////    //CboProductos.DisplayMember = "n_producto";
        ////    //CboProductos.ValueMember = "id_producto" ;

        ////    DataTable tabla = oHelper.Consultar("SP_CONSULTAR_PRODUCTOS");
        ////    CboProductos.DataSource = tabla;
        ////    CboProductos.DisplayMember = "n_producto";
        ////    CboProductos.ValueMember = "id_producto";
        ////}

        private void CargarProductos()
        {
            //PresupuestoDAO DAO = new PresupuestoDAO();

            List<Producto> LProductos = new List<Producto>();

            //LProductos = DAO.GetProductos();
            LProductos = oServicio.ConsultarProductos();

            CboProductos.DataSource = LProductos;
            // Le doy el nombre de las property de los objetos q tengo en la lista:
            CboProductos.ValueMember = "ProductoNro";
            CboProductos.DisplayMember = "Nombre";

            //CboProductos.Items.AddRange(LProductos.ToArray());
           

          
        }

        //private int ProximoPresupuesto()
        //{
        //    /*SP 
        //     CREATE PROCEDURE SP_PROXIMO_ID
        //     @next int output
        //    AS
        //    BEGING
        //       SET @next (SELECT MAX(presupuesto_nro) +1 FROM T_PRESUPUESTO)); 
        //     END
        //     */
        //     SqlConnection cnn = new SqlConnection(@"Data Source=LAPTOP-JSGOIUIK\MSSQLSERVER01;Initial Catalog=CARPINTERIA_2023;Integrated Security=True");
        //    cnn.Open();
        //    SqlCommand cmd = new SqlCommand("SP_PROXIMO_ID", cnn);
        //    cmd.CommandType = CommandType.StoredProcedure;

        //    SqlParameter param = new SqlParameter("@next", SqlDbType.Int);
        //    param.Direction = ParameterDirection.Output;
        //    cmd.Parameters.Add(param);

        //    cmd.ExecuteNonQuery(); // vamos a ejecutar un SP que devuelve valores como parametros NO FILAS
        //    cnn.Close();

        //    return (int)param.Value;

        //}


        private void BtnAgregar_Click(object sender, EventArgs e)
        {
            if (DatosValidos() == true)
            {
                //foreach (DataGridViewRow row in dgvDetalles.Rows)
                //{
                //    //if (row.Cells["ColProd"].Value.ToString().Equals(CboProductos.Text))
                //    if (row.Cells["ColProd"].Value.ToString() == CboProductos.Text)
                        
                //    {
                //        MessageBox.Show("Este producto ya esta presupuestado", "Control", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                //        return;
                //    }
                //}

                ////DataRowView item = (DataRowView)CboProductos.SelectedItem; // item trae todo el producto
                ////int prodNro = Convert.ToInt32(item.Row.ItemArray[0]);
                ////string nom = Convert.ToString(item.Row.ItemArray[1]);
                ////double pre = Convert.ToDouble(item.Row.ItemArray[2]);

                ////Producto p = new Producto(prodNro, nom, pre);

                ////int cantidad = Convert.ToInt32(TxtCantidad.Text);

                ////DetallePresupuesto detalle = new DetallePresupuesto(p, cantidad);

                ////oPresupuesto.AgregarDetalle(detalle);

                //////dgvDetalles.Rows.Add(new object[] { item.Row.ItemArray[0],
                //////                                     item.Row.ItemArray[1],
                //////                                     item.Row.ItemArray[2],
                //////                                     TxtCantidad.Text });


                /////*dgvDetalles.Rows.Add(new object[]
                ////                       {
                ////                           detalle.Producto.ProductoNro,
                ////                           detalle.Producto.Nombre,
                ////                           detalle.Producto.Precio,
                ////                           detalle.Cantidad         }  ); */

                ////dgvDetalles.Rows.Add(new object[] { prodNro, nom, pre, cantidad });

                ////CalcularTotales();

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
            // pasar datos al objeto:
            //oPresupuesto.PresupuestoNro = oHelper.ProximoPresupuesto();
            //oPresupuesto.Cliente = TxtCliente.Text;
            //oPresupuesto.Descuento = Convert.ToDouble(TxtDescuento.Text);

            //oPresupuesto.Fecha = Convert.ToDateTime(TxtFecha.Text);

            //oPresupuesto.Total = Convert.ToDouble(TxtTotal.Text);

            //if (oPresupuesto.ConfirmarPresupuesto())
            //{
            //    MessageBox.Show("Este presupuesto fue cargado con EXITO", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    this.Dispose();
            //}
            //else
            //{
            //    MessageBox.Show("Hubo un error en los datos", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    return;
            //}
            ////Cargo el OBJETO:
            ////oPresupuesto.PresupuestoNro = oHelper.ProximoPresupuesto();
            ////oPresupuesto.Cliente = TxtCliente.Text;
            ////oPresupuesto.Descuento = Convert.ToDouble(TxtDescuento.Text);

            ////oPresupuesto.Fecha = Convert.ToDateTime(TxtFecha.Text);


            ////////Cargo el presupuesto en BD con el metodo CONFIRMARPRESUPUESTO al cual le ingreso un oPresupuesto
            ////if (oHelper.ConfirmarPresupuesto(oPresupuesto))
            ////{
            ////    MessageBox.Show("Este presupuesto fue cargado con EXITO", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            ////    this.Dispose();
            ////}
            ////else
            ////{
            ////    MessageBox.Show("Hubo un error en los datos", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            ////    return;
            ////}

            //Cargo el objeto:
            if (dgvDetalles.Rows.Count == 0)
            {
                MessageBox.Show("Debe ingresar al menos detalle!", "Control", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            else
            {
                oPresupuesto.PresupuestoNro = oServicio.NextBudget();
                oPresupuesto.Cliente = TxtCliente.Text;
                oPresupuesto.Descuento = Convert.ToDouble(TxtDescuento.Text);
                oPresupuesto.Fecha = Convert.ToDateTime(TxtFecha.Text);

                if (oServicio.ConfirmarPresupuesto(oPresupuesto))
                {
                    MessageBox.Show("Este presupuesto fue cargado con EXITO", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

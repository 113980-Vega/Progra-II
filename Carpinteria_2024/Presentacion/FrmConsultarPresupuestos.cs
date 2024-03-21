using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

using Carpinteria_2024.AccesoDatos.Implementaciones;
using Carpinteria_2024.Servicios;
using Carpinteria_2024.Presentacion;

namespace Carpinteria_2024.Formularios
{
    public partial class FrmConsultarPresupuestos : Form
    {
        // el tener un intermediario entre la presentacion y el AccesoDAtos

        // Elimino la siguiente linea:
        //private PresupuestoServicio oServicio = new PresupuestoServicio();

        /* Elimino la linea (private PresupuestoServicio oServicio = new PresupuestoServicio();
         * ) y creo la linea ( private IServicio oServicio = new PresupuestoServicio(); )
           para separar la INTERFACE de la IMPLEMENTACION: */
        


        private IServicio oServicio;

        //private ServicioFactory factory;
        
        
        // Para no usar new: 
        //public FrmConsultarPresupuestos(ServicioFactory factory)
        public FrmConsultarPresupuestos()
        {
            
            InitializeComponent();
            oServicio = new ServicioFactoryImp().CrearServicio();
            // para no usar new se usa la siguiente linea:
            //oServicio = factory.CrearServicio();
        }

        private void FrmConsultarPresupuestos_Load(object sender, EventArgs e)
        {
            dtpDesde.Value = DateTime.Today.AddDays(-7);
            dtpHasta.Value = DateTime.Today;
        }

        ////private void btnConsultar_Click(object sender, EventArgs e)
        ////{
        ////    //Valido datos:
        ////    if (ValidarDatos() == true)
        ////    {
        ////        string datosBaja = "";

        ////        ////Cargo un LISTA PARAMETROS con los campos del form (txt, dtp,etc):
        ////        if (chkBaja.Checked == true)
        ////        {
        ////            datosBaja = "S";
        ////        }
        ////        else
        ////        {
        ////            datosBaja = "N";
        ////        }
        ////        List<Parametro> lParametros = new List<Parametro>();
        ////        //lParametros.Add(new Parametro("@fecha_desde", dtpDesde.Value.ToString("yyyyMMdd")));
        ////        //lParametros.Add(new Parametro("@fecha_hasta", dtpHasta.Value.ToString("yyyyMMdd")));
        ////        lParametros.Add(new Parametro("@fecha_desde", dtpDesde.Value));
        ////        lParametros.Add(new Parametro("@fecha_hasta", dtpHasta.Value));
        ////        lParametros.Add(new Parametro("@cliente", txtCliente.Text));
        ////        lParametros.Add(new Parametro("@datos_baja", datosBaja));

        ////        ////Cargo una TABLA con un SP y una lista(a travez del metodo consultar)

        ////        //DataTable tabla = new HelperDB().Consultar("SP_CONSULTAR_PRESUPUESTOS", lParametros);
        ////        DataTable tabla = oHelperDB.Consultar("SP_CONSULTAR_PRESUPUESTOS_BAJA_05", lParametros);

        ////        ////Limpio la DataGriedView
        ////        dgvPresupuestos.Rows.Clear();

        ////        ////Cargo el DataGriedView con la tabla con ForEach porq son varios:

        ////        foreach (DataRow fila in tabla.Rows)
        ////        {
        ////            dgvPresupuestos.Rows.Add(new object[] {fila[0].ToString(),
        ////                                                   fila[1].ToString(),
        ////                                                   fila[2].ToString(),
        ////                                                   fila[3].ToString(),
        ////                                                   fila[4].ToString(),
        ////                                                   fila[5].ToString(),
        ////                                                   "Ver Detalle" });
        ////        }



        ////    }

        ////}
        private void btnConsultar_Click(object sender, EventArgs e)
        {
            //Valido datos:
            if (ValidarDatos() == true)
            {
                string datosBaja = "";

                ////Cargo un LISTA PARAMETROS con los campos del form (txt, dtp,etc):
                if (chkBaja.Checked == true)
                {
                    datosBaja = "S";
                }
                else
                {
                    datosBaja = "N";
                }
                List<Parametro> lParametros = new List<Parametro>();
                //lParametros.Add(new Parametro("@fecha_desde", dtpDesde.Value.ToString("yyyyMMdd")));
                //lParametros.Add(new Parametro("@fecha_hasta", dtpHasta.Value.ToString("yyyyMMdd")));
                lParametros.Add(new Parametro("@fecha_desde", dtpDesde.Value));
                lParametros.Add(new Parametro("@fecha_hasta", dtpHasta.Value));
                lParametros.Add(new Parametro("@cliente", txtCliente.Text));
                lParametros.Add(new Parametro("@datos_baja", datosBaja));


                /*+++++++++++++++++++++++++++++++++++++++++++++++++*/

                ///Cargo una lista con del metodo consultar presupuestos q me devuelve una lista
                List<Presupuesto> lPresupuestos = oServicio.ConsultarPresupuestos(lParametros);

                
                ////Limpio las filas del DataGriedView del formulario
                dgvPresupuestos.Rows.Clear();

                ////Cargo la dgvPresupuestos con cada objeto de la lPresupuestos:

                foreach (Presupuesto oPresupuesto in lPresupuestos)
                {
                    dgvPresupuestos.Rows.Add(new object[] {
                         
                                                            oPresupuesto.PresupuestoNro,
                                                            oPresupuesto.Fecha.ToString("dd/MM/yyyy"),
                                                            oPresupuesto.Cliente,
                                                            oPresupuesto.Descuento,
                                                            oPresupuesto.FechaBaja.ToString("dd/MM/yyyy"),
                                                            oPresupuesto.Total
                                                             }) ;
                }

            }

        }

        private bool ValidarDatos()
        {
            bool valido = true;
            if (dtpDesde.Value < dtpHasta.Value)
            {
                valido = true;
               
            }
            else
            {
                valido = false;
                MessageBox.Show("La fecha DESDE debe ser MENOR que la fecha HASTA", "Control", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                dtpDesde.Focus();
            }
            return valido;
        }

      

        private void btnSalir_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Esta seguro que desea salir? ", "SALIR", MessageBoxButtons.YesNo,
                MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                this.Dispose();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            // dame la fila seleccionada (CurrentRow)
            DataGridViewRow fila = dgvPresupuestos.CurrentRow;// fila seleccionada en la grilla


            if (fila != null)
            {
                int presupuestoNro = Convert.ToInt32(fila.Cells["ColId"].Value);
                //int presupuestoNro = Convert.ToInt32(dgvPresupuestos.CurrentRow.Cells["ColId"].Value);

                if (MessageBox.Show("Seguro que desea eliminar el elemento seleccionado?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    bool respuesta = oServicio.RegistrarBajaPresupuesto(presupuestoNro);
                    //SqlConnection cnn = new SqlConnection(@"Data Source=LAPTOP-JSGOIUIK\MSSQLSERVER01;Initial Catalog=CARPINTERIA_2023;Integrated Security=True");
                    //SqlTransaction t = null;
                    //int filasAfectadas = 0;

                    //try
                    //{
                    //    cnn.Open();
                    //    t = cnn.BeginTransaction();
                    //    SqlCommand cmd = new SqlCommand("SP_ELIMINAR_PRESUPUESTO_UPDATE", cnn, t);
                    //    cmd.CommandType = CommandType.StoredProcedure;
                    //    cmd.Parameters.AddWithValue("presupuesto_nro", presupuestoNro);

                    //    filasAfectadas = cmd.ExecuteNonQuery();
                    //    t.Commit();




                    //}
                    //catch (Exception ex)
                    //{
                    //    t.Rollback();

                    //}
                    //finally
                    //{
                    //    if (cnn != null && cnn.State == ConnectionState.Open)
                    //    {
                    //        cnn.Close();
                    //    }
                    //}
                    //if (filasAfectadas == 1)
                    if (respuesta)
                    {
                        MessageBox.Show("Presupuesto Eliminado", "INFORMACION", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
                        this.btnConsultar_Click(sender, e);
                        // sender es el objeto q disparo el evento
                        // e es la informacion del evento pero no la veridica
                        // Puedo poner null y null :
                        //this.btnConsultar_Click(null, null);
                    }
                    else
                    {
                        MessageBox.Show("No se pudo eliminar el PRESUPUESTO", "ADVERTENCIA", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                    }

                }




            }
            else
            {
                MessageBox.Show("La fila esta vacia", "ADVERTENCIA");
            }
        }

        private void dgvPresupuestos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvPresupuestos.CurrentCell.ColumnIndex == 6)
            {
                //int nro = int.Parse(dgvPresupuestos.CurrentRow.Cells["ColId"].Value.ToString());
                int nro = Convert.ToInt32(dgvPresupuestos.CurrentRow.Cells["ColId"].Value);
                
                new FrmDetallesPresupuesto(nro).ShowDialog();

            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
           
            
                int nroPresupuesto = Convert.ToInt32(dgvPresupuestos.CurrentRow.Cells["ColId"].Value);

                FrmModificarPresupuesto nuevo = new FrmModificarPresupuesto(nroPresupuesto); // instanciar el nuevo form y luego mostrarlo
                 //nuevo.Show(); // el SHOW permite tener muchas ventanas abiertas
                nuevo.ShowDialog();
                this.btnConsultar_Click(null, null);

        }

     
    }
    
}

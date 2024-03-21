using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using Carpinteria_2024.AccesoDatos.Interfaces;

namespace Carpinteria_2024.AccesoDatos.Implementaciones
{
    class PresupuestoDAO : IPresupuestoDAO
    {
        public Presupuesto CheckDetails(List<Parametro> LParam)
        {
           Presupuesto oPresupuesto = new Presupuesto();
            DataTable tabla = HelperDB.ObtenerInstancia().Consultar("SP_CONSULTAR_DETALLES_PRESUPUESTO", LParam);

            bool primero = true;
            // paso la tabla a una lista:
            foreach (DataRow fila in tabla.Rows)
            {
                if (primero == true)
                {
                    oPresupuesto.Fecha = Convert.ToDateTime(fila["fecha"]);
                   
                    if (fila["fecha_baja"] == DBNull.Value)
                    {
                        //oPresupuesto.FechaBaja = Convert.ToDateTime("01/01/0001");

                        oPresupuesto.FechaBaja = DateTime.MinValue.Date;

                        //oPresupuesto.FechaBaja = Convert.ToDateTime(DateTime.MinValue.ToString("ddMMyyyy"));

                    }
                    else
                    {
                        oPresupuesto.FechaBaja = Convert.ToDateTime(fila["fecha_baja"]).Date;
                    }
                    ///// verificar esa carga de num de presu:
                    oPresupuesto.PresupuestoNro = Convert.ToInt32(fila["presupuesto_nro"]);
                    oPresupuesto.Cliente = fila["cliente"].ToString();
                    oPresupuesto.Descuento = Convert.ToDouble(fila["descuento"]);
                    oPresupuesto.Total = Convert.ToInt32(fila["total"]);
                  
                    primero = false;
                }
                int nroProducto = Convert.ToInt32(fila["id_producto"]);
                string nombreProducto = Convert.ToString(fila["n_producto"]);
                double precio = Convert.ToDouble(fila["precio"]);

                Producto oProducto = new Producto(nroProducto, nombreProducto, precio);

                int cantidad = Convert.ToInt32(fila["cantidad"]);

             

                DetallePresupuesto oDetalle = new DetallePresupuesto(oProducto, cantidad);
               

                oPresupuesto.AgregarDetalle(oDetalle);

            }

            return oPresupuesto;
        }




        public bool ConfirmQuote(Presupuesto oPresupuesto)
        {
            SqlConnection cnn = new SqlConnection(@"Data Source=LAPTOP-JSGOIUIK\MSSQLSERVER01;Initial Catalog=CARPINTERIA_2023;Integrated Security=True");
            bool resultado = true;

            SqlTransaction t = null;
            try
            {
                cnn.Open();
                t = cnn.BeginTransaction();

                ////Cargo el COMANDO con el SP, la CONEXION y la TRANSACCION:

                SqlCommand comando = new SqlCommand("SP_INSERTAR_MAESTRO", cnn, t);
                //comando.Connection = cnn;
                //comando.Transaction = t;
                //comando.CommandText = "SP_INSERTAR_MAESTRO";

                comando.CommandType = CommandType.StoredProcedure;

                ////Cargo el command con los PARAMETROS del SP con el metodo Parameters:

                comando.Parameters.AddWithValue("@cliente", oPresupuesto.Cliente);
                comando.Parameters.AddWithValue("@dto", oPresupuesto.Descuento);
                comando.Parameters.AddWithValue("@total", oPresupuesto.Total);
                //comando.Parameters.AddWithValue("@total", (oPresupuesto.CalcularSubTotal() - ((oPresupuesto.CalcularSubTotal() * oPresupuesto.Descuento) / 100)));

                ////Cargo el parametro de salida con la variable parametro de SqlParameter:

                SqlParameter parametro = new SqlParameter("@presupuesto_nro", SqlDbType.Int);
                //parametro.ParameterName = "@presupuesto_nro";
                //parametro.SqlDbType = SqlDbType.Int;
                parametro.Direction = ParameterDirection.Output;
                comando.Parameters.Add(parametro);

                comando.ExecuteNonQuery();
                int presupuestoNro = (int)parametro.Value;

                int detalleNro = 1;
                SqlCommand cmdDetalle;
                //// Cargo el comand de detalle con el SP, la CONEXION, y la TRANSACCION
                /// Cargo el command con los parametros del SP con el metodo Parameters del command.
                /// y con el Foreach porq son varios detalles
                foreach (DetallePresupuesto dp in oPresupuesto.ListaDetalles)
                {
                    cmdDetalle = new SqlCommand("SP_INSERTAR_DETALLE", cnn, t);
                    cmdDetalle.CommandType = CommandType.StoredProcedure;

                    cmdDetalle.Parameters.AddWithValue("@presupuesto_nro", presupuestoNro);
                    cmdDetalle.Parameters.AddWithValue("@detalle_nro", detalleNro);
                    cmdDetalle.Parameters.AddWithValue("@id_producto", dp.Producto.ProductoNro);
                    cmdDetalle.Parameters.AddWithValue("@cantidad", dp.Cantidad);
                    cmdDetalle.ExecuteNonQuery();
                    detalleNro++;
                }
                t.Commit();
            }
            catch
            {
                if (t != null)
                    t.Rollback();
                resultado = false;
            }
            finally
            {
                if ((cnn != null) && (cnn.State == ConnectionState.Open))
                    cnn.Close();
            }

            return resultado;
        }

        public bool Delete(int nro)
        {
            bool auxiliar = false;
            SqlConnection cnn = new SqlConnection(@"Data Source=LAPTOP-JSGOIUIK\MSSQLSERVER01;Initial Catalog=CARPINTERIA_2023;Integrated Security=True");
            SqlTransaction t = null;
            // int filasAfectadas = 0;

            try
            {
                cnn.Open();
                t = cnn.BeginTransaction();
                SqlCommand cmd = new SqlCommand("SP_ELIMINAR_PRESUPUESTO_UPDATE", cnn, t);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("presupuesto_nro", nro);

                //filasAfectadas = cmd.ExecuteNonQuery();
                //if (filasAfectadas == 1)
                //{ 
                //  auxiliar = true;
                //}

                auxiliar = cmd.ExecuteNonQuery() == 1;

                t.Commit();
            }
            catch //(Exception ex)
            {
                t.Rollback();
            }
            finally
            {
                if (cnn != null && cnn.State == ConnectionState.Open)
                {
                    cnn.Close();
                }
            }
            return auxiliar;
        }

        public List<Presupuesto> GetByFilters(List<Parametro> lCriterios)
        {
            List<Presupuesto> list = new List<Presupuesto>();
            DataTable tabla = new DataTable();
            SqlConnection cnn = new SqlConnection(@"Data Source=LAPTOP-JSGOIUIK\MSSQLSERVER01;Initial Catalog=CARPINTERIA_2023;Integrated Security=True");
            try
            {
                cnn.Open();
                SqlCommand cmd = new SqlCommand("SP_CONSULTAR_PRESUPUESTOS_BAJA_05", cnn);
                cmd.CommandType = CommandType.StoredProcedure;

                foreach (Parametro p in lCriterios)
                {
                    cmd.Parameters.AddWithValue(p.Nombre, p.Valor);
                }
                tabla.Load(cmd.ExecuteReader());

                foreach (DataRow fila in tabla.Rows)
                {
                    // por cada registro(fila) creamos un objeto del dominio
                    Presupuesto oPresupuesto = new Presupuesto();
                    oPresupuesto.PresupuestoNro = Convert.ToInt32(fila["presupuesto_nro"]);
                    oPresupuesto.Fecha = Convert.ToDateTime(fila["fecha"]).Date;
                    oPresupuesto.Cliente = fila["cliente"].ToString();

                    oPresupuesto.Descuento = Convert.ToDouble(fila["descuento"]);


                    if (fila["fecha_baja"] == DBNull.Value)
                    {
                        //oPresupuesto.FechaBaja = Convert.ToDateTime("01/01/0001");

                        oPresupuesto.FechaBaja = DateTime.MinValue.Date;

                        //oPresupuesto.FechaBaja = Convert.ToDateTime(DateTime.MinValue.ToString("ddMMyyyy"));

                    }
                    else
                    {
                        oPresupuesto.FechaBaja = Convert.ToDateTime(fila["fecha_baja"]).Date;
                    }
                    oPresupuesto.Total = Convert.ToDouble(fila["total"]);

                    list.Add(oPresupuesto);

                }
                
                cnn.Close();

            }
            catch (SqlException ex)
            {


            }

            return list;
        }

        public List<Producto> GetProductos()
        {
            List<Producto> List = new List<Producto>();

            //SqlConnection cnn = new SqlConnection(@"Data Source=LAPTOP-JSGOIUIK\MSSQLSERVER01;Initial Catalog=CARPINTERIA_2023;Integrated Security=True");
            try
            {
                //cnn.Open();
                //SqlCommand cmd = new SqlCommand("SP_CONSULTAR_PRODUCTOS", cnn);
                //cmd.CommandType = CommandType.StoredProcedure;

                //DataTable tabla = new DataTable();
                //tabla.Load(cmd.ExecuteReader());

                
                DataTable tabla = new DataTable();
                tabla = HelperDB.ObtenerInstancia().ConsultarSimple("SP_CONSULTAR_PRODUCTOS");

                foreach (DataRow fila in tabla.Rows)
                {
                    // por cada registro(fila) creamos un objeto del dominio
                    Producto oProducto = new Producto();
                    oProducto.ProductoNro = Convert.ToInt32(fila["id_producto"]);
                    oProducto.Nombre = Convert.ToString(fila["n_producto"]);
                    oProducto.Precio = Convert.ToDouble(fila["precio"]);
                    //EQUALS devuelve true o false:
                    oProducto.Activo = fila["activo"].Equals("S");
                    //oProducto.Activo = Convert.ToBoolean(fila["activo"].ToString());

                    List.Add(oProducto);

                }
                //cnn.Close();



            }
            catch (Exception ex)
            {
                _ = ex.Message;
            }

            return List;
        }

        public int ProximoPresupuesto()
        {
            SqlConnection cnn = new SqlConnection(@"Data Source=LAPTOP-JSGOIUIK\MSSQLSERVER01;Initial Catalog=CARPINTERIA_2023;Integrated Security=True");
            cnn.Open();
            SqlCommand comando = new SqlCommand("SP_PROXIMO_ID", cnn);
          


            comando.CommandType = CommandType.StoredProcedure;

            SqlParameter parametro = new SqlParameter("@next", SqlDbType.Int);
          
            parametro.Direction = ParameterDirection.Output;
            comando.Parameters.Add(parametro);
            comando.ExecuteNonQuery();

            cnn.Close();

            return Convert.ToInt32(parametro.Value);
        }

        public bool ActualizarPresupuestoDAO(Presupuesto oPresupuesto)
        { 
          return HelperDB.ObtenerInstancia().ActualizarPresupuestoHELPER(oPresupuesto);
        }
    }
}

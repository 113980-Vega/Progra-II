using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Carpinteria_2024.AccesoDatos.Implementaciones
{
    internal class HelperDB
    {
        private static HelperDB instancia;
        private string cadenaConexion;
        private SqlConnection cnn;
        private SqlCommand cmd;

        private HelperDB()
        {
            //cadenaConexion = @"Data Source=LAPTOP-JSGOIUIK\MSSQLSERVER01;Initial Catalog=CARPINTERIA_2023;Integrated Security=True";
            cadenaConexion = Properties.Resources.cadenaConexion;

            cnn = new SqlConnection(cadenaConexion);
            cmd = new SqlCommand();
        }
        public static HelperDB ObtenerInstancia()
        {
            if (instancia == null)
            { 
              instancia = new HelperDB();
            
            }
            return instancia;
        
        }

        public void Conectar()
        {
            cnn.Open();
            cmd.Connection = cnn;
            cmd.CommandType = CommandType.StoredProcedure;

        }

        public void Desconectar()
        {
            if (cnn != null && cnn.State == ConnectionState.Open)
            { 
            cnn.Close();
            }
        }

        ////public int ProximoPresupuesto()
        ////{
        ////    cnn.Open();
        ////     SqlCommand comando = new SqlCommand("SP_PROXIMO_ID", cnn);
        ////    //comando.CommandText = "SP_PROXIMO_ID";
        ////    //comando.Connection = cnn;
            
            
        ////    comando.CommandType = CommandType.StoredProcedure;
            
        ////    SqlParameter parametro = new SqlParameter("@next", SqlDbType.Int);
        ////    //parametro.ParameterName = "@next";
        ////    //parametro.SqlDbType = SqlDbType.Int;
        ////    parametro.Direction = ParameterDirection.Output;
        ////    comando.Parameters.Add(parametro);
        ////    comando.ExecuteNonQuery();

        ////    cnn.Close();

        ////    return Convert.ToInt32(parametro.Value);
        ////}

        public DataTable ConsultarSimple(string nombreSP)
        {
            ////cnn.Open();
            ////SqlCommand cmd = new SqlCommand();
            ////cmd.Connection = cnn;
            ////cmd.CommandType = CommandType.StoredProcedure;
            Conectar();
            cmd.CommandText = nombreSP;
            
            DataTable tabla = new DataTable();
            tabla.Load(cmd.ExecuteReader());
            
            cmd.ExecuteNonQuery();
            Desconectar();
            return tabla;
        }

        public DataTable Consultar(string nombreSP, List<Parametro> lstParametros)
        {
            //Conectar();
            if (cnn != null && cnn.State == ConnectionState.Open)
            {
                cnn.Close();
            }
            cnn.Open();
            cmd.Connection = cnn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = nombreSP;

            cmd.Parameters.Clear();
           
            foreach (Parametro p in lstParametros)
            {
                cmd.Parameters.AddWithValue(p.Nombre, p.Valor);
            }

            DataTable tabla = new DataTable();
            tabla.Load(cmd.ExecuteReader());
            cmd.ExecuteNonQuery();
            //Desconectar();
            if (cnn != null && cnn.State == ConnectionState.Open)
            {
                cnn.Close();
            }
            return tabla;
        }

        public bool ConfirmarPresupuesto(Presupuesto oPresupuesto)
        {
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

        public bool ActualizarPresupuestoHELPER(Presupuesto oPresupuesto)

        {
            bool resultado = true;

            SqlTransaction t = null;
            try
            {
                cnn.Open();
                t = cnn.BeginTransaction();

                ////Cargo el COMANDO con el SP, la CONEXION y la TRANSACCION:

                SqlCommand comando = new SqlCommand("SP_MODIFICAR_MAESTRO", cnn, t);
                //comando.Connection = cnn;
                //comando.Transaction = t;
                //comando.CommandText = "SP_INSERTAR_MAESTRO";

                comando.CommandType = CommandType.StoredProcedure;

                ////Cargo el command con los PARAMETROS del SP con el metodo Parameters:

                comando.Parameters.AddWithValue("@cliente", oPresupuesto.Cliente);
                comando.Parameters.AddWithValue("@dto", oPresupuesto.Descuento);
                comando.Parameters.AddWithValue("@total", oPresupuesto.Total);
                comando.Parameters.AddWithValue("@presupuesto_nro", oPresupuesto.PresupuestoNro);
               

                comando.ExecuteNonQuery();
               
                
               

                int detalleNro = 1;
                SqlCommand cmdDetalle;
                //// Cargo el comand de detalle con el SP, la CONEXION, y la TRANSACCION
                /// Cargo el command con los parametros del SP con el metodo Parameters del command.
                /// y con el Foreach porq son varios detalles
                foreach (DetallePresupuesto dp in oPresupuesto.ListaDetalles)
                {
                    cmdDetalle = new SqlCommand("SP_INSERTAR_DETALLE", cnn, t);
                    cmdDetalle.CommandType = CommandType.StoredProcedure;

                    cmdDetalle.Parameters.AddWithValue("@presupuesto_nro", oPresupuesto.PresupuestoNro);
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
    }
}

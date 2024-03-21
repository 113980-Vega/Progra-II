using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carpinteria_2024
{
    public class Presupuesto
    {
        public int PresupuestoNro { get; set; }
        public DateTime Fecha { get; set; }
        public string Cliente { get; set; }
        //public double CostoMO { get; set; }
        public double Descuento { get; set; }
        public DateTime FechaBaja { get; set; }

        public double Total { get; set; }
       
        public List<DetallePresupuesto> ListaDetalles { get; set; }

        public Presupuesto()
        {
            ListaDetalles = new List<DetallePresupuesto>();
        }

        public void AgregarDetalle(DetallePresupuesto detalle)
        { 
            ListaDetalles.Add(detalle);
           
        
        }

        public void QuitarDetalle(int indice)
        {
            // si hago Remove quita el ITEM (objeto)
            // si hago RemoveAt quita el INDEX
            
            ListaDetalles.RemoveAt(indice);
           
        
        }

        public double CalcularSubTotal()
        {
           double total = 0;
            //for (int i = 0; i < ListaDetalles.Count; i++)
            //{ 
            //   total = total+  ListaDetalles[i].CalcularSubTotal();

            //}
            //return total;

            foreach (DetallePresupuesto detalleItem in ListaDetalles)
            { 
               total = total + detalleItem.CalcularSubTotalProducto();
            }

            return total;
        }

        //public void ConfirmarPresupuesto()
        //{

        //    SqlConnection cnn = new SqlConnection(@"Data Source=LAPTOP-JSGOIUIK\MSSQLSERVER01;Initial Catalog=CARPINTERIA_2023;Integrated Security=True");
        //    cnn.Open();
        //    SqlCommand cmd = new SqlCommand("SP_INSERTAR_MAESTRO", cnn);
        //    cmd.CommandType = CommandType.StoredProcedure;

        //    cmd.Parameters.AddWithValue("@cliente", this.Cliente);
        //    cmd.Parameters.AddWithValue("@dto", this.Descuento);
        //    cmd.Parameters.AddWithValue("@total", this.CalcularTotal()- this.Descuento);//mal

        //    SqlParameter param = new SqlParameter("@presupuesto_nro", SqlDbType.Int);
        //    param.Direction = ParameterDirection.Output;
        //    cmd.Parameters.Add(param);
        //    cmd.ExecuteNonQuery();
        //    int presupuestoNro = Convert.ToInt32(param.Value);
        //    int cDetalles = 1;
        //    foreach (DetallePresupuesto det in ListaDetalles)
        //    {
        //        SqlCommand cmdDet = new SqlCommand("SP_INSERTAR_DETALLE", cnn);
        //        cmdDet.CommandType = CommandType.StoredProcedure;
        //        cmdDet.Parameters.AddWithValue("@presupuesto_nro", presupuestoNro);
        //        cmdDet.Parameters.AddWithValue("@detalle_nro", cDetalles);
        //        cmdDet.Parameters.AddWithValue("@id_producto", det.Producto.ProductoNro);
        //        cmdDet.Parameters.AddWithValue("@cantidad", det.Cantidad);
        //        cmdDet.ExecuteNonQuery();
        //        cDetalles++;
        //    }
        //       cnn.Close();

        //}

        ////public bool ConfirmarPresupuesto()
        ////{
        ////    bool flag = true;

        ////    SqlTransaction t = null;
        ////    SqlConnection cnn = new SqlConnection(@"Data Source=LAPTOP-JSGOIUIK\MSSQLSERVER01;Initial Catalog=CARPINTERIA_2023;Integrated Security=True");
        ////    try
        ////    {

        ////        /*ALTER PROCEDURE [dbo].[SP_INSERTAR_MAESTRO] 
        ////         @cliente varchar(255), 
        ////         @dto numeric(5,2),
        ////         @total numeric(8,2),
        ////         @presupuesto_nro int OUTPUT
        ////        AS
        ////        BEGIN
        ////        INSERT INTO T_PRESUPUESTOS(fecha, cliente, descuento, total)
        ////       VALUES (GETDATE(), @cliente, @dto, @total);
        ////       --Asignamos el valor del último ID autogenerado (obtenido --  
        ////       --mediante la función SCOPE_IDENTITY() de SQLServer)	
        ////       SET @presupuesto_nro = SCOPE_IDENTITY();

        ////       END */

        ////        /* ALTER PROCEDURE [dbo].[SP_INSERTAR_DETALLE] 
        ////         @presupuesto_nro int,
        ////         @detalle_nro int, 
        ////         @id_producto int, 
        ////         @cantidad int
        ////         AS
        ////         BEGIN
        ////         INSERT INTO T_DETALLES_PRESUPUESTO(presupuesto_nro,detalle_nro, id_producto, cantidad)
        ////         VALUES (@presupuesto_nro, @detalle_nro, @id_producto, @cantidad);

        ////         END
        ////         */


        ////        cnn.Open();
        ////        t = cnn.BeginTransaction();

        ////        SqlCommand cmd = new SqlCommand("SP_INSERTAR_MAESTRO", cnn, t);
        ////        cmd.CommandType = CommandType.StoredProcedure;

        ////        cmd.Parameters.AddWithValue("@cliente", this.Cliente);
        ////        cmd.Parameters.AddWithValue("@dto", this.Descuento);
        ////        //cmd.Parameters.AddWithValue("@total", this.CalcularSubTotal() - this.Descuento);
        ////        cmd.Parameters.AddWithValue("@total", this.Total);

        ////        SqlParameter param = new SqlParameter("@presupuesto_nro", SqlDbType.Int);
        ////        param.Direction = ParameterDirection.Output;
        ////        // param es una variable de salida y guarda en numero de presupuesto (id de la tabla presupestos)
        ////        cmd.Parameters.Add(param);
        ////        cmd.ExecuteNonQuery();
        ////        int presupuestoNro = Convert.ToInt32(param.Value);

        ////        int cDetalles = 1;

        ////        foreach (DetallePresupuesto det in ListaDetalles)
        ////        {
        ////            SqlCommand cmdDet = new SqlCommand("SP_INSERTAR_DETALLE", cnn, t);
        ////            cmdDet.CommandType = CommandType.StoredProcedure;

        ////            cmdDet.Parameters.AddWithValue("@presupuesto_nro", presupuestoNro);
        ////            cmdDet.Parameters.AddWithValue("@detalle_nro", cDetalles);
        ////            cmdDet.Parameters.AddWithValue("@id_producto", det.Producto.ProductoNro);
        ////            cmdDet.Parameters.AddWithValue("@cantidad", det.Cantidad);
        ////            cmdDet.ExecuteNonQuery();
        ////            cDetalles++;
        ////        }

        ////        /* cada vez q vos abris una transaccion el motor va ejecutando los comandos y va guardando como en un archivo de log,
        ////           el estado anterior y el estado actual de los registros*/
        ////        // cuando haces comit le decis el estado actual bajalo a disco porque hasta ese entonces lo tiene en un archivo temporal
        ////        // como si fuese una tabla temporarl al estado anterior d la fila q esta por afectar
        ////        // cuando vos decis commit materializa, baja al disco y escribi y en los archivos q tienen q ver con esa tabla escribi efectivamente el nuevo estado
        ////        // por eso es q yo le puedo decir hace un ROLLBACK el motor va a ver los log, va al archivo temporal donde esta la foto anterior
        ////        // de ese registro y le dice rollback me vuelvo para atraz 

        ////        t.Commit();
        ////        flag = true;
        ////        // COMMIT: confirmacion, si no se produjo ningun error recien en ese momento se efectivizan las modificaciones
        ////        // en la base de datos, hasta haora por mas q hayan sido ejecutodos todos los procedimientos
        ////        // no se confirmaron los vambios en la Base de datos

        ////    }
        ////    catch (Exception ex)

        ////    {
        ////        // si se da un error:
        ////        t.Rollback();
        ////        flag = false;

        ////        // el Rollback me deja la bd como estaba

        ////    }

        ////    finally
        ////    {
        ////        if (cnn != null && cnn.State == ConnectionState.Open)
        ////        {
        ////            cnn.Close();
        ////        }
        ////    }
        ////    return flag;

        ////}
    }
}

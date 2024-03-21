using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carpinteria_2024
{
    public class DetallePresupuesto
    {
       

        public Producto Producto  { get; set; }
        public int Cantidad { get; set; }

        //constructor:
        public DetallePresupuesto(Producto producto, int cantidad)
        {
            Producto = producto;
            Cantidad = cantidad;
        }

        public double CalcularSubTotalProducto()
        { 
           return Producto.Precio * Cantidad;
        }

    }
}

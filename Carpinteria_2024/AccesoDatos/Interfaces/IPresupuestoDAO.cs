using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Carpinteria_2024.AccesoDatos.Implementaciones;

namespace Carpinteria_2024.AccesoDatos.Interfaces
{
     interface IPresupuestoDAO
    {
        // es public y abstract por defecto:
         bool Delete(int nro);
        // el DAO se compromete a devolverme todos los presupuestos(o los criterios q busco)
        //  La consulta me deberia devolver una lista 
        // el DAO deberia comprometerse a devolverme una lista de objetos preuspuestos

        // reciebe una lista de filtros:
        List<Presupuesto> GetByFilters(List <Parametro> lCriterios);

        List<Producto> GetProductos();

        int ProximoPresupuesto();

        bool ConfirmQuote( Presupuesto oPresupuesto);

        Presupuesto CheckDetails(List<Parametro> LParam);


        bool ActualizarPresupuestoDAO(Presupuesto oPresupuesto);
       



      }
}

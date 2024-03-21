using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Carpinteria_2024.AccesoDatos.Implementaciones;

namespace Carpinteria_2024.Servicios
{
      public interface IServicio
    {  // PARA SEPARAR EL SERVICIO DEL ACCESO A DATOS:
        /* Creo el  metodo RegistrarBajaPresupuesto en IServicio
           LLamo el RegistrarBajaPresupuesto en PresupuestoServicio(y lo "Configuro")
           En Presupuesto servicio dentro de RegistrarBajaPresupuesto llamo al metodo Detele 
           Detele esta creado en IPresupuestoDAO
           LLamo el Delete en PresupuestoDAO (y lo "Configuro")
           */
        /* el metodo RegistrarBajaPresupuesto esta creado en la IServicio(interface)
         * el metodo RegistrarBajaPresupuesto esta configurado en PresupuestoServicio(Clase) 
         * y dentro de RegistrarBajaPresupuesto esta el metodo Delete,
         * el metodo Delete esta  creado en IPresupuestoDAO(Interface)
         * el metodo Delete esta configurado en PresupuestoDAO(clase)
           */
        bool RegistrarBajaPresupuesto(int presupuesto);

        List<Presupuesto> ConsultarPresupuestos(List<Parametro> lParam);

        List<Producto> ConsultarProductos();

        int NextBudget();

        bool ConfirmarPresupuesto(Presupuesto oPresupuesto);

        Presupuesto  ConsultarDetalles(List<Parametro> lParam);

        bool ActualizarPresupuestoServicio(Presupuesto oPresupuesto);

            
        


      }
}

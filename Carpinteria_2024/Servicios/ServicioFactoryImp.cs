using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carpinteria_2024.Servicios
{
     public class ServicioFactoryImp : ServicioFactory
    {
        public override IServicio CrearServicio()
        {
            return new PresupuestoServicio();
        }
    }
}

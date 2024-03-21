using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Carpinteria_2024.AccesoDatos.Implementaciones;
using Carpinteria_2024.AccesoDatos.Interfaces;

namespace Carpinteria_2024.Servicios
{
    //PresupuestoServicio implementa IServicio:
     public class PresupuestoServicio: IServicio

    {
        private IPresupuestoDAO DAO;


        public PresupuestoServicio()
        {
            DAO = new PresupuestoDAO();
        }

        public List<Presupuesto>ConsultarPresupuestos(List<Parametro> lParam)
        {
            return DAO.GetByFilters(lParam);
        }

        public List<Producto> ConsultarProductos()
        {
            return DAO.GetProductos();
        }



        // vamos a ir a la bd solamente vamos a exponer servicios,
        // los service van a ser clases q exponen todos los metodos que necesitaria la pantalla para poder hacer su trabajo
        // 
        public bool RegistrarBajaPresupuesto(int presupuestoNro)
        {
            // este metododo va a delegar a otro objeto la baja
            // y ese otro objeto va a ser un objeto especifico de Acceso a Datos
           
             return DAO.Delete(presupuestoNro);
            
        }

        public int NextBudget()
        {
            return DAO.ProximoPresupuesto();
        }

        public bool ConfirmarPresupuesto(Presupuesto oPresupuesto)
        {
            return DAO.ConfirmQuote(oPresupuesto);
        }
        public bool ActualizarPresupuestoServicio(Presupuesto oPresupuesto)
        { 
           return DAO.ActualizarPresupuestoDAO(oPresupuesto);
        }

        public Presupuesto ConsultarDetalles(List<Parametro> lParam)
        {
            return DAO.CheckDetails(lParam);
        }
    }
}

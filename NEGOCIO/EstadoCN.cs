using DATOS;
using ENTIDAD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEGOCIO
{
    public class EstadoCN
    {
        private static EstadoDAL obj = new EstadoDAL();
        public static List<ESTADO> ListarEstado()
        {
            return obj.ListEstado();
        }

        public static void AgregarEstado(ESTADO un)
        {
            obj.AddEstado(un);
        }

        public static ESTADO DetalleEstado(int id)
        {
            return obj.DetailEstado(id);
        }

        public static void EditarEstado(ESTADO un)
        {
            obj.EditEstado(un);
        }

        public static void EliminarEstado(int id)
        {
            obj.DeleteEstado(id);
        }
    }
}

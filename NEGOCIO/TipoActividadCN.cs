using DATOS;
using ENTIDAD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEGOCIO
{
    public class TipoActividadCN
    {
        private static TipoActividadDAL obj = new TipoActividadDAL();
        public static List<TIPOACTIVIDAD> ListarTipoActividad()
        {
            return obj.ListTipoActividad();
        }

        public static void AgregarTipoActividad(TIPOACTIVIDAD un)
        {
            obj.AddTipoActividad(un);
        }

        public static TIPOACTIVIDAD DetalleTipoActividad(int id)
        {
            return obj.DetailTipoActividad(id);
        }

        public static void EditarTipoActividad(TIPOACTIVIDAD un)
        {
            obj.EditTipoActividad(un);
        }

        public static void EliminarTipoActividad(int id)
        {
            obj.DeleteTipoActividad(id);
        }
    }
}

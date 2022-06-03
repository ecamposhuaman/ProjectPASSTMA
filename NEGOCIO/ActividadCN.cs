using DATOS;
using ENTIDAD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEGOCIO
{
    public class ActividadCN
    {
        private static ActividadDAL obj = new ActividadDAL();
        public static List<ACTIVIDADCE> ListarActividad()
        {
            return obj.ListActividad();
        }

        public static void AgregarActividad(ACTIVIDAD ac)
        {
            obj.AddActividad(ac);
        }

        public static ACTIVIDADCE DetalleActividad(int id)
        {
            return obj.DetailActividad(id);
        }

        public static void EditarActividad(ACTIVIDADCE ac)
        {
            obj.EditActividad(ac);
        }

        public static void EliminarActividad(int id)
        {
            obj.DeleteActividad(id);
        }

        public static List<ACTIVIDAD> ListarCMBActividad()
        {
            return obj.ListCMBActividad();
        }

        public static List<ACTIVIDAD> ListarCMBActividadByTipo(int idtipo)
        {
            return obj.ListCMBActividadByTipo(idtipo);
        }
    }   
}

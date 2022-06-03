using DATOS;
using ENTIDAD;
using System.Collections.Generic;

namespace NEGOCIO
{
    public class Sem_ActividadCN
    {
        private static Sem_ActividadDAL obj = new Sem_ActividadDAL();

        public static int ObtenerUsuario(string user)
        {
            return obj.ObtenerIdUsuario(user);
        }

        public static List<SEM_ACTIVIDADCE> ListarSem_Actividad()
        {
            return obj.ListSem_Actividad();
        }
        public static List<SEM_ACTIVIDADCE> ListarSem_Actividad_xUsuMes(int idus, int cmbmeses)
        {
            return obj.ListSem_Actividad_xUsuMes(idus, cmbmeses);
        }
        public static List<SEM_ACTIVIDADCE> ListarSem_Actividad_xSemMes(int IdSem, int cmbmeses)
        {
            return obj.ListSem_Actividad_xSemMes(IdSem, cmbmeses);
        }

        public static void AgregarSem_Actividad(SEM_ACTIVIDAD sa, INFORME Informe)
        {
            obj.AddSem_Actividad(sa, Informe);
        }

        public static SEM_ACTIVIDAD DetalleSem_Actividad_Asignacion(int id)
        {
            return obj.DetailSem_Actividad_Asignacion(id);
        }

        public static SEM_ACTIVIDADCE DetalleSem_Actividad(int id)
        {
            return obj.DetailSem_Actividad(id);
        }

        public static void EditarSem_Actividad(SEM_ACTIVIDADCE sa, INFORME Informe)
        {
            obj.EditSem_Actividad(sa, Informe);
        }

        public static void EjecutarSem_Actividad(SEM_ACTIVIDADCE sa, INFORME Informe)
        {
            obj.EjectSem_Actividad(sa, Informe);
        }

        public static void EliminarSem_Actividad(int id, int idarch)
        {
            obj.DeleteSem_Actividad(id, idarch);
        }

        public static void RevisaSem_Actividad(int id, int idestado, int idrevisor)
        {
            obj.RevSem_Actividad(id, idestado, idrevisor);
        }

        public static void ObservarSem_Actividad(int id, int idestado, int idrevisor, string com)
        {
            obj.ObsSem_Actividad(id, idestado, idrevisor, com);
        }

        public static List<SEMACTByIdSEM> DataExportByIdSEM(int idsem, int Anio)
        {
            return obj.DataExport_ByIdSEM(idsem, Anio);
        }
    }
}

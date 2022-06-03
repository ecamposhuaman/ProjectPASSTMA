using DATOS;
using ENTIDAD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEGOCIO
{
    public class SEMCN
    {
        private static SEMDAL obj = new SEMDAL();

        public static List<SEMCE> ListarSEM()
        {
            return obj.ListSEM();
        }

        public static void AgregarSEM(SEM sm)
        {
            obj.AddSEM(sm);
        }

        public static SEMCE DetalleSEM(int id)
        {
            return obj.DetailSEM(id);
        }

        public static void EditarSEM(SEM sm)
        {
            obj.EditSEM(sm);
        }

        public static void EliminarSEM(int id)
        {
            obj.DeleteSEM(id);
        }

        public static List<SEM> ListarCMBSEM()
        {
            return obj.ListCMBSEM();
        }

        public static List<SEM> ListarCMBSEMByIdUUNN(int iduunn)
        {
            return obj.ListCMBSEMByIdUUNN(iduunn);
        }

        public static List<SEM> ListarCMBSEMByUser(string user)
        {
            return obj.ListCMBSEMByUser(user);
        }
    }
}

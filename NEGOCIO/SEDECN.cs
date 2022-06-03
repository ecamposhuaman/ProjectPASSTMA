using DATOS;
using ENTIDAD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEGOCIO
{
    public class SEDECN
    {
        private static SEDEDAL obj = new SEDEDAL();
        public static List<SEDE> ListarSEDE()
        {
            return obj.ListSEDE();
        }

        public static void AgregarSEDE(SEDE sd)
        {
            obj.AddSEDE(sd);
        }

        public static SEDE DetalleSEDE(int id)
        {
            return obj.DetailSEDE(id);
        }

        public static void EditarSEDE(SEDE sd)
        {
            obj.EditSEDE(sd);
        }

        public static void EliminarSEDE(int id)
        {
            obj.DeleteSEDE(id);
        }
    }
}

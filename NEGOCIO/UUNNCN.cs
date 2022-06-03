using DATOS;
using ENTIDAD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEGOCIO
{
    public class UUNNCN
    {
        private static UUNNDAL obj = new UUNNDAL();
        public static List<UUNNCE> ListarUUNN()
        {
            return obj.ListUUNN();
        }

        public static void AgregarUUNN(UUNN un)
        {
            obj.AddUUNN(un);
        }

        public static UUNNCE DetalleUUNN(int id)
        {
            return obj.DetailUUNN(id);
        }

        public static void EditarUUNN(UUNN un)
        {
            obj.EditUUNN(un);
        }

        public static void EliminarUUNN(int id)
        {
            obj.DeleteUUNN(id);
        }
    }
}

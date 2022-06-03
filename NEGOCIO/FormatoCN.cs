using DATOS;
using ENTIDAD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEGOCIO
{
    public class FormatoCN
    {
        private static FormatoDAL obj = new FormatoDAL();
        public static List<FORMATO> ListarFormato()
        {
            return obj.ListFormato();
        }

        public static void AgregarFormato(FORMATO ac)
        {
            obj.AddFormato(ac);
        }

        public static FORMATO DetalleFormato(int id)
        {
            return obj.DetailFormato(id);
        }

        public static void EditarFormato(FORMATO ac)
        {
            obj.EditFormato(ac);
        }

        public static void EliminarFormato(int id)
        {
            obj.DeleteFormato(id);
        }
    }
}

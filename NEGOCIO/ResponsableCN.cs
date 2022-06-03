using DATOS;
using ENTIDAD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEGOCIO
{
    public class ResponsableCN
    {
        private static ResponsableDAL obj = new ResponsableDAL();
        public static List<RESPONSABLE> ListarResponsable()
        {
            return obj.ListResponsable();
        }

        public static void AgregarResponsable(RESPONSABLE rs)
        {
            obj.AddResponsable(rs);
        }

        public static RESPONSABLE DetalleResponsable(int id)
        {
            return obj.DetailResponsable(id);
        }

        public static RESPONSABLE DetalleResponsableByEmail(string email)
        {
            return obj.DetailResponsableByEmail(email);
        }

        public static void EditarResponsable(RESPONSABLE rs, int action)
        {
            obj.EditResponsable(rs, action);
        }

        public static void EliminarResponsable(int id)
        {
            obj.DeleteResponsable(id);
        }

        public static RESPONSABLE VerificarUsuario(string email)
        {
            return obj.VerificateUser(email);
        }
        public static void EditarUsuario(int idresp, string key)
        {
            obj.EditUser(idresp, key);
        }

        public static List<RESPONSABLE> ListarCMBResponsable()
        {
            return obj.ListCMBResponsable();
        }
    }
}

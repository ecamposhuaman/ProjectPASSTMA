using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTIDAD
{
    public class SEM_ACTIVIDADCE
    {
        public int IdSEMACT { get; set; }
        public int IdSEM { get; set; }
        public string NombreSEM { get; set; }
        public int IdActividad { get; set; }
        public string NombreActividad { get; set; }
        public int Periodo { get; set; }
        public string NombrePeriodo { get; set; }
        public int IdEstado { get; set; }
        public string NombreEstado { get; set; }
        public int IdInforme { get; set; }
        public string NombreInforme { get; set; }
        public Nullable<System.DateTime> FEjec { get; set; }
        public Nullable<int> IdRevisor { get; set; }
        public string NombreRevisor { get; set; }
        public int Cantidad { get; set; }
        public string Comunicado { get; set; }
        public string Observacion { get; set; }
        public string NombreFormato { get; set; }
    }
}

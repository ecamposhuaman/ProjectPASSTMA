using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTIDAD
{
    public class ACTIVIDADCE
    {
        public int IdActividad { get; set; }
        public string NombreActividad { get; set; }
        public int IdTipo { get; set; }
        public string NombreTipo { get; set; }
        public Nullable<int> IdFormato { get; set; }
        public string NombreFormato { get; set; }
    }
}

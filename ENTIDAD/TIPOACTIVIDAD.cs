//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ENTIDAD
{
    using System;
    using System.Collections.Generic;
    
    public partial class TIPOACTIVIDAD
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TIPOACTIVIDAD()
        {
            this.ACTIVIDAD = new HashSet<ACTIVIDAD>();
        }
    
        public int IdTipo { get; set; }
        public string NombreTipo { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ACTIVIDAD> ACTIVIDAD { get; set; }
    }
}

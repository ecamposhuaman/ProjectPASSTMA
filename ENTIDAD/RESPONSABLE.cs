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
    
    public partial class RESPONSABLE
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public RESPONSABLE()
        {
            this.SEM = new HashSet<SEM>();
        }
    
        public int IdResponsable { get; set; }
        public string NombreResponsable { get; set; }
        public string EmailResponsable { get; set; }
        public string IdAspNetUser { get; set; }
        public string NombreRol { get; set; }
    
        public virtual AspNetUsers AspNetUsers { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SEM> SEM { get; set; }
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RaceControl.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class TecnicaRevision
    {
        public long idRevision { get; set; }
        public long idTecnica { get; set; }
        public long idElemRevision { get; set; }
    
        public virtual Elem_Revision Elem_Revision { get; set; }
        public virtual Tecnica Tecnica { get; set; }
    }
}

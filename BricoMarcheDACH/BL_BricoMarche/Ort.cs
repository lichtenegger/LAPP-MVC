//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BL_BricoMarche
{
    using System;
    using System.Collections.Generic;
    
    public partial class Ort
    {
        public Ort()
        {
            this.VieleBenutzer = new HashSet<Benutzer>();
        }
    
        public int ID { get; set; }
        public string Bezeichnung { get; set; }
        public string PLZ { get; set; }
        public int Land_ID { get; set; }
    
        public virtual ICollection<Benutzer> VieleBenutzer { get; set; }
        public virtual Land EinLand { get; set; }
    }
}

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
    
    public partial class Land
    {
        public Land()
        {
            this.VieleOrte = new HashSet<Ort>();
        }
    
        public int ID { get; set; }
        public string Bezeichnung { get; set; }
    
        public virtual ICollection<Ort> VieleOrte { get; set; }
    }
}
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
    
    public partial class Schlagwort
    {
        public Schlagwort()
        {
            this.VieleVideos = new HashSet<Video>();
        }
    
        public int ID { get; set; }
        public string Bezeichnung { get; set; }
    
        public virtual ICollection<Video> VieleVideos { get; set; }
    }
}

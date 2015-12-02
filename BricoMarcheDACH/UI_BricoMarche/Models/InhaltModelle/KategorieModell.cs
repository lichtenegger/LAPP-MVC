using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UI_BricoMarche.Models.InhaltModelle
{
    public class KategorieModell
    {
        [Required]
        public int ID { get; set; }
        [Required]
        public string Bezeichnung { get; set; }
    }
}
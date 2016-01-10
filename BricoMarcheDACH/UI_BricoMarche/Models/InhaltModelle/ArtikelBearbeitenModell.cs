using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UI_BricoMarche.Models.InhaltModelle
{
    public class ArtikelBearbeitenModell : ArtikelDetailModell
    {
        [Required]
        public bool Aktiv { get; set; }
        [Required]
        public int KategorieID { get; set; }
        public List<KategorieModell> Kategorien { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UI_BricoMarche.Models.InhaltModelle
{
    public class VideoDetailModell : VideoModell
    {
        [Display(Name = "Beschreibung")]
        public string Langbeschreibung { get; set; }

        public string Pfad { get; set; }

        [Display(Name = "Schlagwörter")]
        public List<string> Schlagwoerter { get; set; }

        public List<ArtikelModell> verlinkteProdukte { get; set; }

        public bool Gemerkt { get; set; }
    }
}
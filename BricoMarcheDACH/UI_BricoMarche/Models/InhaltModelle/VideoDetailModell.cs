using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UI_BricoMarche.Models.InhaltModelle
{
    public class VideoDetailModell : VideoModell
    {
        public string Langbeschreibung { get; set; }

        public string Pfad { get; set; }

        public List<ArtikelModell> verlinkteProdukte { get; set; }

        public bool Gemerkt { get; set; }
    }
}
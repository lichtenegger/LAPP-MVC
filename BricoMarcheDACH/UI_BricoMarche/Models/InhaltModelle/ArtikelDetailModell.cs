using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UI_BricoMarche.Models.InhaltModelle
{
    public class ArtikelDetailModell : ArtikelModell
    {
        public string Langbeschreibung { get; set; }

        public bool WirdGemerkt { get; set; }

        public List<VideoModell> verlinkteVideos { get; set; }

        public bool Gemerkt { get; set; }
    }
}
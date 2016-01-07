using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UI_BricoMarche.Models.InhaltModelle;

namespace UI_BricoMarche.Models.BenutzerModelle
{
    public class MerklisteModell
    {
        public List <ArtikelModell> Produkte { get; set; }

        public List <VideoModell> Videos { get; set; }
    }
}
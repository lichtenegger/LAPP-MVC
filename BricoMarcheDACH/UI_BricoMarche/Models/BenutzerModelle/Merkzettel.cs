using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UI_BricoMarche.Models.InhaltModelle;

namespace UI_BricoMarche.Models.BenutzerModelle
{
    public class Merkzettel
    {
        public List <InhaltModell> Artikel { get; set; }

        public List <InhaltModell> Videos { get; set; }
    }
}
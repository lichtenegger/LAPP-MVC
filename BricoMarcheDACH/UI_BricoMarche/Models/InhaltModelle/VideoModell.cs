using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UI_BricoMarche.Models.InhaltModelle
{
    public class VideoModell
    {
        public int ID { get; set; }

        public string Bezeichnung { get; set; }

        public string Kategorie { get; set; }

        public List<string> schlagwoerter { get; set; }
    }
}
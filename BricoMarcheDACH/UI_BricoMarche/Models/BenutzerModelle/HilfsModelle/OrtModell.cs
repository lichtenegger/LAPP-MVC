using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UI_BricoMarche.Models.BenutzerModelle.HilfsModelle
{
    public class OrtModell
    {
        public int ID { get; set; }

        public string PLZ { get; set; }
        
        public string Ortsname { get; set; }

        public string Land { get; set; }

        public override string ToString()
        {
            return Land + " - " + PLZ + " " + Ortsname;
        }
    }
}
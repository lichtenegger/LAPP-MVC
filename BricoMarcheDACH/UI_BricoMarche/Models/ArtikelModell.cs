using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UI_BricoMarche.Models
{
    public class ArtikelModell
    {
        public int ID { get; set; }

        public string Bezeichnung { get; set; }

        public decimal Preis { get; set; }

        public bool Aktiv { get; set; }

        public string Langbeschreibung { get; set; }

        public string Kategorie { get; set; }

        public string BildPfad { get; set; }
    }
}
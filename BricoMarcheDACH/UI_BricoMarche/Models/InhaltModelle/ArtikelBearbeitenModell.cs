using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UI_BricoMarche.Models.InhaltModelle
{
    public class ArtikelBearbeitenModell : ArtikelDetailModell
    {
        public byte[] Bild { get; set; }
        public int KategorieID { get; set; }
        public List<KategorieModell> Kategorien { get; set; }
    }
}
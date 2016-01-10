using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UI_BricoMarche.Models.InhaltModelle
{
    public class ArtikelModell
    {
        public int ID { get; set; }
        [Required]
        public string Bezeichnung { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public decimal Preis { get; set; }

        public string Kategorie { get; set; }
    }
}
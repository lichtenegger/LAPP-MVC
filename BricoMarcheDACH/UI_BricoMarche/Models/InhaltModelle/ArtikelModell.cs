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

        [Required(ErrorMessage = "Pflichtfeld")]
        public string Bezeichnung { get; set; }

        [Required(ErrorMessage = "Pflichtfeld")]
        [DataType(DataType.Currency, ErrorMessage = "Bitte geben Sie einen gültigen Preis ein.")]
        [RegularExpression(@"^\d+\,\d+$", ErrorMessage = "Bitte geben Sie einen gültigen Preis ein")]
        public decimal Preis { get; set; }

        public string Kategorie { get; set; }
    }
}
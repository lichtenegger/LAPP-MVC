using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UI_BricoMarche.Models.BenutzerModelle
{
    public class RegistrierungModell : BenutzerModell
    {
        [Required(ErrorMessage = "Pflichtfeld")]
        [DataType(DataType.Password)]
        [RegularExpression(@"[\w+\S+\d+]}{5,12}", ErrorMessage = "Passwort muss 5-12 Zeichen lang sein und aus Buchstaben, Ziffern & Sonderzeichen bestehen.")]
        public string Passwort { get; set; }

        [Required(ErrorMessage = "Pflichtfeld")]
        [DataType(DataType.Password)]
        [Compare("Passwort", ErrorMessage = "Passwörter stimmen nicht überein")]
        public string PasswortWiederholung { get; set; }
    }
}
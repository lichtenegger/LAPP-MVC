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
        [Display(Name = "Passwort wiederholen")]
        [Compare("Passwort", ErrorMessage = "Passwörter stimmen nicht überein")]
        public string PasswortWiederholung { get; set; }

        [MustBeTrue(ErrorMessage = "Bitte AGB akzeptieren")]
        public bool AGB { get; set; }

        [MustBeTrue(ErrorMessage = "Bitte Datenschutz-Erklärung akzeptieren")]
        public bool Datenschutz { get; set; }
    }
}
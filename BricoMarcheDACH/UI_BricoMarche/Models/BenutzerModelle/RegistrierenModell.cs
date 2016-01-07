using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UI_BricoMarche.Models.BenutzerModelle
{
    public class RegistrierenModell : ProfilModell
    {
        [Required(ErrorMessage = "Pflichtfeld")]
        [DataType(DataType.Password)]
        [Display(Name = "Passwort")]
        public string NeuesPasswort { get { return NeuesPasswort; } set { NeuesPasswort = value; base.Passwort = value; } }

        [Required(ErrorMessage = "Pflichtfeld")]
        [DataType(DataType.Password)]
        [Display(Name = "Passwort wiederholen")]
        [Compare("NeuesPasswort", ErrorMessage = "Passwörter stimmen nicht überein")]
        public string PasswortWiederholung { get; set; }

        [Required(ErrorMessage = "Bitte AGB akzeptieren")]
        public bool AGB { get; set; }

        [Required(ErrorMessage = "Bitte Datenschutz-Erklärung akzeptieren")]
        public bool Datenschutz { get; set; }
    }
}
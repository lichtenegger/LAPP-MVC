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


        [DataType(DataType.EmailAddress, ErrorMessage = "Ungültige Email-Adresse")]
        public override string Email { get; set; }

        [Required(ErrorMessage = "Pflichtfeld")]
        [DataType(DataType.Password)]
        [Display(Name = "Passwort")]
        public override string NeuesPasswort { get; set; }

        [Required(ErrorMessage = "Pflichtfeld")]
        [DataType(DataType.Password)]
        [Display(Name = "Passwort wiederholen")]
        [Compare("NeuesPasswort", ErrorMessage = "Passwörter stimmen nicht überein")]
        public string PasswortWiederholung { get; set; }

        [MustBeTrue(ErrorMessage = "Bitte AGB akzeptieren")]
        [Required(ErrorMessage = "Bitte AGB akzeptieren")]
        public bool AGB { get; set; }

        [MustBeTrue(ErrorMessage = "Bitte Datenschutz-Erklärung akzeptieren")]
        [Required(ErrorMessage = "Bitte Datenschutz-Erklärung akzeptieren")]
        public bool Datenschutz { get; set; }
    }
}
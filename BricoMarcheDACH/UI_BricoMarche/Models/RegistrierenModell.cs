using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UI_BricoMarche.Models
{
    public class RegistrierenModell
    {
        [Required(ErrorMessage = "Pflichtfeld")]
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress, ErrorMessage ="Ungültige Email-Adresse")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Pflichtfeld")]
        [DataType(DataType.Password)]
        [RegularExpression(@"[\w+\S+\d+]}{5,12}", ErrorMessage = "Passwort muss 5-12 Zeichen lang sein und aus Buchstaben, Ziffern & Sonderzeichen bestehen.")]
        public string Passwort { get; set; }

        [Required(ErrorMessage = "Pflichtfeld")]
        [DataType(DataType.Password)]
        [Compare("Passwort", ErrorMessage = "Passwörter stimmen nicht überein")]
        public string PasswortWiederholung { get; set; }

        [Required(ErrorMessage = "Pflichtfeld")]
        public string Vorname { get; set; }

        [Required(ErrorMessage = "Pflichtfeld")]
        public string Nachname { get; set; }

        [Required(ErrorMessage = "Pflichtfeld")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Geburtsdatum { get; set; }

        [Required(ErrorMessage = "Pflichtfeld")]
        [DataType(DataType.MultilineText)]
        public string Adresse { get; set; }

        [Required(ErrorMessage = "Pflichtfeld")]
        [Display(Name = "Ort")]
        public int OrtID { get; set; }

        public List<OrtModell> Orte { get; set; }

        [MustBeTrue(ErrorMessage = "Bitte AGB akzeptieren")]
        public bool AGB { get; set; }

        [MustBeTrue(ErrorMessage = "Bitte Datenschutz-Erklärung akzeptieren")]
        public bool Datenschutz { get; set; }
    }
}
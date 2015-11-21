using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using System.Text;


namespace UI_BricoMarche.Models
{
    public class ProfilModell
    {
        [Display(Name = "Benutzer/Email")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Ungültige Email-Adresse")]
        [Editable(false)] /// nicht editierbar!!
        public string Benutzername { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Pflichtfeld")]
        [DataType(DataType.Password)]
        public string AltesPasswort { get; set; }

        [DataType(DataType.Password)]
        [RegularExpression(@"[\w+\S+\d+]}{5,12}", ErrorMessage = "Passwort muss 5-12 Zeichen lang sein und aus Buchstaben, Ziffern & Sonderzeichen bestehen.")]
        public string NeuesPasswort { get; set; }

        [DataType(DataType.Password)]
        [Compare("NeuesPasswort", ErrorMessage = "Passwort und Wiederholung stimmen nicht überein")]
        public string Wiederholung { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Pflichtfeld")]
        public string Vorname { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Pflichtfeld")]
        public string Nachname { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Pflichtfeld")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Geburtsdatum { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Pflichtfeld")]
        [DataType(DataType.MultilineText)]
        public string Adresse { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Pflichtfeld")]
        [Display(Name = "Ort")]
        public int Ort_ID { get; set; }

        public List<OrtModell> Orte { get; set; }
    }
}
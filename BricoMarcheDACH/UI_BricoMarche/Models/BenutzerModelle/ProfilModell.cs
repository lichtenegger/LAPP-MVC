using System;
using System.ComponentModel.DataAnnotations;

namespace UI_BricoMarche.Models.BenutzerModelle
{
    public class ProfilModell : BenutzerModell
    {
        [Display(Name = "Email-Adresse")]
        [Editable(false)]
        public override string Email { get; set; }

        [Required(ErrorMessage = "Pflichtfeld")]
        public override string Vorname { get; set; }

        [Required(ErrorMessage = "Pflichtfeld")]
        public override string Nachname { get; set; }

        [Required(ErrorMessage = "Pflichtfeld")]
        [DataType(DataType.Date, ErrorMessage = "Bitte das Datum im Format JJJ-MM-DD eingeben. Z.B.: 1980-12-27")]
        //[RegularExpression(@"^\d{4}\-(0?[1-9]|1[012])\-(0?[1-9]|[12][0-9]|3[01])", ErrorMessage = "Bitte das Datum im Format JJJJ-MM-DD eingeben. Z.B.: 1980-12-27")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public override DateTime Geburtsdatum { get; set; }

        [Required(ErrorMessage = "Pflichtfeld")]
        [DataType(DataType.MultilineText)]
        public override string Adresse { get; set; }

        [Required(ErrorMessage = "Pflichtfeld")]
        [Display(Name = "Ort")]
        public override int OrtID { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Neues Passwort")]
        [RegularExpression(@"[\w+\S+\d+]{5,12}", ErrorMessage = "Passwort muss 5-12 Zeichen lang sein und aus Buchstaben, Ziffern & Sonderzeichen bestehen.")]
        public virtual string NeuesPasswort { get; set; }
    }
}
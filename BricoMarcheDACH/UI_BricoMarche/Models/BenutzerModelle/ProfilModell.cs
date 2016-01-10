using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

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
        [DataType(DataType.Date)]
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
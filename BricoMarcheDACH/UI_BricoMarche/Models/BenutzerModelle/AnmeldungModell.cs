using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UI_BricoMarche.Models.BenutzerModelle
{
    public class AnmeldenModell
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email-Adresse")]
        public string Benutzername { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Passwort")]
        public string Passwort { get; set; }
    }
}
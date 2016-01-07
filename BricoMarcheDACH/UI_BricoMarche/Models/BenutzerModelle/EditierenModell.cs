﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using System.Text;


namespace UI_BricoMarche.Models.BenutzerModelle
{
    public class EditierenlModell : ProfilModell
    {
        [DataType(DataType.Password)]
        [Display(Name = "Passwort")]
        [Compare("Passwort", ErrorMessage = "Passwörter stimmen nicht überein")]
        public string AltesPasswort { get; set; }

        [Required(ErrorMessage = "Pflichtfeld")]
        [DataType(DataType.Password)]
        [Display(Name = "neues Passwort")]
        private string neuesPasswort;
        public string NeuesPasswort { get { return neuesPasswort; } set { neuesPasswort = value; base.Passwort = value; } }

        [DataType(DataType.Password)]
        [Display(Name = "Passwort wiederholen")]
        [Compare("NeuesPasswort", ErrorMessage = "Passwörter stimmen nicht überein")]
        public string PasswortWiederholung { get; set; }
    }
}
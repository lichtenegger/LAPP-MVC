using System;
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
        [Display(Name = "Altes Passwort")]
        public string AltesPasswort { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Passwort Wiederholung")]
        [Compare("NeuesPasswort", ErrorMessage = "Passwörter stimmen nicht überein")]
        public string PasswortWiederholung { get; set; }
    }
}
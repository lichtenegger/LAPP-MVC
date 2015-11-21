using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using System.Text;


namespace UI_BricoMarche.Models.BenutzerModelle
{
    public class ProfilModell : RegistrierungModell
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Pflichtfeld")]
        [DataType(DataType.Password)]
        public string AltesPasswort { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UI_BricoMarche.Models.InhaltModelle
{
    public class ArtikelDetailModell : ArtikelModell
    {
        [Required]
        [DataType(DataType.MultilineText)]
        public string Langbeschreibung { get; set; }

        public bool Gemerkt { get; set; }

        public List<VideoModell> verlinkteVideos { get; set; }
    }
}
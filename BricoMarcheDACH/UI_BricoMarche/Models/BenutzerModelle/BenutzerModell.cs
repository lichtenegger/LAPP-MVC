using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UI_BricoMarche.Models.BenutzerModelle
{
    public class BenutzerModell
    {
        public virtual string Email { get; set; }

        public virtual string Vorname { get; set; }

        public virtual string Nachname { get; set; }

        public virtual DateTime Geburtsdatum { get; set; }

        public virtual string Adresse { get; set; }

        public virtual int OrtID { get; set; }

        public List<OrtModell> Orte { get; set; }

        public string Passwort { get; set; }

        public bool Aktiv { get; set; }
    }
}
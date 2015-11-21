using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UI_BricoMarche.Models;
using BL_BricoMarche;

namespace UI_BricoMarche.Controllers
{
    /// <summary>
    /// Sämtliche Actions im BenutzerController können OHNE
    /// Authentifizierung durchgeführt werden.
    /// Hinweis: Ausnahmen können immer noch definiert werden.
    /// </summary>

    public class BenutzerController
    {
        /// <summary>
        /// Registrieren Action
        /// </summary>
        /// <returns>Registrieren View</returns>
        /// Mittels HTTP Methoden können Sie festlegen WIE diese
        /// Action ausschließlich aufgerufen werden kann
        
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Registrieren()
        {
            Debug.WriteLine("Benutzer - Registrieren - GET");
            Debug.Indent();
            RegistrierenModell model = new RegistrierenModell;

            /// lade aus der BL alle verfügbare Orte
            List<Ort> orte = BL_BricoMarche.OrtVerwaltung.LadeAlleOrte();

            return View(Model);
        }
    }
}
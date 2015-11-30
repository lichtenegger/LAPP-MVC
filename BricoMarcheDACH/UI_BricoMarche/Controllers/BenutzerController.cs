using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using UI_BricoMarche.Models.BenutzerModelle;
using UI_BricoMarche.Models.BenutzerModelle.HilfsModelle;
using static BL_BricoMarche.DatenVerwaltung.Benutzer;

namespace UI_BricoMarche.Controllers
{
    public class BenutzerController : Controller
    {
        [HttpGet]
        [Authorize]
        public ActionResult Willkommen()
        {
            ViewBag.Benutzer = "benutzer";
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Anmelden(AnmeldenModell daten)
        {
            if (ModelState.IsValid && SindAnmeldeDatenRichtig(daten.Benuztername, daten.Passwort))
            {
                FormsAuthentication.SetAuthCookie(daten.Benuztername, true);
                Debug.WriteLine("AnmeldeDaten sind richtig; AuthCookie gesetzt.");
                return RedirectToAction("Willkommen");
            }
            return RedirectToRoute("/Error");
        }

        [HttpPost]
        [Authorize]
        public ActionResult Abmelden()
        {
            Debug.WriteLine("Benutzer - Abmelden - POST");

            FormsAuthentication.SignOut();

            return RedirectToAction("Willkommen", "Inhalt");
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Registrieren()
        {
            RegistrierungModell modell = new RegistrierungModell();
            Debug.WriteLine("-- START : Benutzer - Registrieren - GET -----------------------------");
            Debug.Indent();
            List<BL_BricoMarche.Ort> orte = Orte.LadeAlleOrte();
            if (orte == null)
            {
                TempData["Fehler"] = "Fehler beim Laden der Orte! Bitte wenden Sie sich an einen Administrator.";
                return RedirectToAction("Willkommen", "Inhalt");
            }
            else
            {
                modell.Orte = new List<OrtModell>();
                foreach (var ort in orte)
                {
                    modell.Orte.Add(new OrtModell()
                    {
                        ID = ort.ID,
                        Ortsname = ort.Bezeichnung,
                        Land = ort.EinLand.Bezeichnung,
                        PLZ = ort.PLZ
                    });
                    Debug.WriteLine(ort.ToString());
                }
                Debug.WriteLine(modell.Orte.Count() + " Orte in Modell geladen.");
            }
            Debug.Unindent();
            Debug.WriteLine("-- START : Benutzer - Registrieren - GET -----------------------------");
            return View(modell);
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Registrieren(RegistrierungModell modell)
        {
            Debug.WriteLine("-- START : Benutzer - Registrieren - POST ------------------");
            Debug.Indent();
            if (ModelState.IsValid)
            {
                if (RegistriereBenutzer(
                    modell.Email,
                    modell.Passwort,
                    modell.Geburtsdatum,
                    modell.Vorname,
                    modell.Nachname,
                    modell.Adresse,
                    modell.OrtID))
                {
                    TempData["Erfolg"] = "Registrierung erfolgreich";
                    Debug.WriteLine("Registrierung erfolgreich");
                    Debug.Unindent();
                    return RedirectToAction("Willkommen", "Inhalt");
                }
                else
                {
                    TempData["Fehler"] = "Fehler beim Registrieren - Daten ungültig";
                }
            }
            Debug.Unindent();
            Debug.WriteLine("-- ENDE : Benutzer - Registrieren - POST -------------------");
            return View("Registrieren", modell);
        }
    }
}
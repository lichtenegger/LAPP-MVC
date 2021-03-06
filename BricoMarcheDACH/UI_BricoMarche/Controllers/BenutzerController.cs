﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using UI_BricoMarche.Models.BenutzerModelle;
using static BL_BricoMarche.DatenVerwaltung.Benutzer;

namespace UI_BricoMarche.Controllers
{
    public class BenutzerController : Controller
    {
        #region Willkommen
        [HttpGet]
        [Authorize]
        public ActionResult Willkommen()
        {
            return View();
        }
        #endregion

        #region Anmelden
        [HttpPost]
        [AllowAnonymous]
        public ActionResult Anmelden(AnmeldenModell daten, string returnUrl)
        {
            if (ModelState.IsValid && SindAnmeldeDatenRichtig(daten.Benuztername, daten.Passwort))
            {
                FormsAuthentication.SetAuthCookie(daten.Benuztername, true);
                Debug.WriteLine("AnmeldeDaten sind richtig; AuthCookie gesetzt.");
                if (IstBenutzerAdministrator(daten.Benuztername))
                {
                    Session["Admin"] = "Ja";
                }
                else
                {
                    Session["Admin"] = "Nein";
                }
                TempData["Erfolg"] = "Anmeldung erfolgt!";
            }
            else
            {
             TempData["Fehler"] = "Fehler beim Anmelden!";
            }
            return Redirect(returnUrl);
        }
        #endregion

        #region Abmelden
        [HttpPost]
        [Authorize]
        public ActionResult Abmelden()
        {
            Debug.WriteLine("Benutzer - Abmelden - POST");

            FormsAuthentication.SignOut();
            Session.Remove("Admin");

            return RedirectToAction("Willkommen", "Inhalt");
        }
        #endregion

        #region Registrieren GET
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Registrieren()
        {
            RegistrierenModell modell = new RegistrierenModell();
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
        #endregion

        #region Registrieren POST
        [HttpPost]
        [AllowAnonymous]
        public ActionResult Registrieren(RegistrierenModell modell)
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

            // NEUE ORTSLISTE FÜR MODELL FÜR VIEW
            List<BL_BricoMarche.Ort> orte = Orte.LadeAlleOrte();
            modell.Orte = new List<OrtModell>();
            foreach (var ort in orte)
            {
                modell.Orte.Add(new OrtModell()
                {
                    ID = ort.ID,
                    Ortsname = ort.Bezeichnung,
                    Land = ort.EinLand.Bezeichnung,
                    PLZ = ort.PLZ,
                });
            }
            Debug.Unindent();
            Debug.WriteLine("-- ENDE : Benutzer - Registrieren - POST -------------------");
            return View("Registrieren", modell);
        }
        #endregion

        #region Editieren GET
        [HttpGet]
        [Authorize]
        public ActionResult Editieren( string benutzerName)
        {
            EditierenlModell modell = null;
            BL_BricoMarche.Benutzer benutzer = null;
            Debug.WriteLine("-- START : Benutzer - Editieren - GET -----------------------");
            Debug.Indent();
            if (benutzerName != null)
            {
                benutzer = LadeBenutzerProfil(benutzerName);
            }
            else
            {
                benutzer = LadeBenutzerProfil(User.Identity.Name);

            }
            if (benutzer != null)
            {
                modell = new EditierenlModell()
                {
                    Email = benutzer.Benutzername,
                    Vorname = benutzer.Vorname,
                    Nachname = benutzer.Nachname,
                    Adresse = benutzer.Adresse,
                    OrtID = benutzer.Ort_ID,
                    Geburtsdatum = benutzer.Geburtsdatum
                };

                modell.Orte = new List<OrtModell>();
                List<BL_BricoMarche.Ort> orte = Orte.LadeAlleOrte();
                foreach (var ort in orte)
                {
                    modell.Orte.Add(new OrtModell()
                    {
                        ID = ort.ID,
                        Ortsname = ort.Bezeichnung,
                        Land = ort.EinLand.Bezeichnung,
                        PLZ = ort.PLZ
                    });
                }
            }
            else
            {
                TempData["Fehler"] = "Fehler beim Laden der Profil-Daten";
                return RedirectToAction("Willkommen", "Inhalt");
            }

            Debug.Unindent();
            Debug.WriteLine("-- ENDE : Benutzer - Editieren - GET -----------------------");
            return View(modell);
        }
        #endregion

        #region Editieren POST
        [HttpPost]
        [Authorize]
        public ActionResult Editieren(EditierenlModell modell)
        {
            Debug.WriteLine("-- START: Benutzer - Editieren - POST -----------------------");
            Debug.Indent();
            if (ModelState.IsValid)
            {
                if (!EditiereBenutzer(
                        modell.Email,
                        modell.AltesPasswort,
                        modell.Passwort,
                        modell.Geburtsdatum,
                        modell.Vorname,
                        modell.Nachname,
                        modell.Adresse,
                        modell.OrtID))
                {
                    TempData["Fehler"] = "Fehler beim Speichern des Profils";
                }
                else
                {
                    TempData["Erfolg"] = "Profil erfolgreich aktualisiert!";
                }
            }

            // NEUE ORTSLISTE FÜR MODELL FÜR VIEW
            List<BL_BricoMarche.Ort> orte = Orte.LadeAlleOrte();
            modell.Orte = new List<OrtModell>();
            foreach (var ort in orte)
            {
                modell.Orte.Add(new OrtModell()
                {
                    ID = ort.ID,
                    Ortsname = ort.Bezeichnung,
                    Land = ort.EinLand.Bezeichnung,
                    PLZ = ort.PLZ,
                });
            }

            // PASSWÖRTER ZURÜCKSETZEN!
            modell.AltesPasswort = "";
            modell.PasswortWiederholung = "";
            modell.Passwort = "";

            Debug.Unindent();
            Debug.WriteLine("-- ENDE: Benutzer - Editieren - POST -----------------------");

            return View(modell);
        }
        #endregion

    }
}
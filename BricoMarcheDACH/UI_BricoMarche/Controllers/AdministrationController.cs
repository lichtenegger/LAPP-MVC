using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BL_BricoMarche;
using UI_BricoMarche.Models.InhaltModelle;
using UI_BricoMarche.Models.BenutzerModelle;
using System.IO;

namespace UI_BricoMarche.Controllers
{
    public class AdministrationController : Controller
    {
        #region PRODUKTVERWALTUNG

        #region HttpGet
        [HttpGet]
        [Authorize]
        public ActionResult ProduktVerwalten(int produktID)
        {
            ArtikelBearbeitenModell modell = null;
            Artikel artikel = BL_BricoMarche.DatenVerwaltung.Artikel.LadeArtikel(produktID);
            List<Kategorie> kategorien = BL_BricoMarche.DatenVerwaltung.Kategorie.LadeAlleKategorien();

            if (artikel != null && kategorien != null)
            {
                modell = new ArtikelBearbeitenModell()
                {
                    ID = artikel.ID,
                    Bezeichnung = artikel.Bezeichnung,
                    Langbeschreibung = artikel.Beschreibung,
                    Preis = artikel.Preis,
                    KategorieID = artikel.Kategorie_ID,
                    Kategorien = new List<KategorieModell>(),
                    Bild = artikel.Bild
                };
                foreach (var kategorie in kategorien)
                {
                    modell.Kategorien.Add(new KategorieModell()
                    {
                        ID = kategorie.ID,
                        Bezeichnung = kategorie.Bezeichnung
                    });
                }

            }
            return View(modell);
        }
        #endregion

        #region HttpPost
        [HttpPost]
        [Authorize]
        public ActionResult ProduktVerwalten(ArtikelBearbeitenModell modell)
        {
           BL_BricoMarche.DatenVerwaltung.Artikel.SpeichereArtikel(modell.ID, modell.Bezeichnung, modell.Langbeschreibung, modell.KategorieID, modell.Preis);

            List<Kategorie> kategorien = BL_BricoMarche.DatenVerwaltung.Kategorie.LadeAlleKategorien();
            modell.Kategorien = new List<KategorieModell>();
            foreach (var kategorie in kategorien)
            {
                modell.Kategorien.Add(new KategorieModell()
                {
                    ID = kategorie.ID,
                    Bezeichnung = kategorie.Bezeichnung
                });
            }

            return View(modell);
        }
        #endregion

        #endregion

        #region BENUTZERVERWALTUNG

        #region Vewalten
        [Authorize]
        public ActionResult BenutzerVerwalten()
        {
            List <BenutzerModell> modell = null;
            List <BL_BricoMarche.Benutzer> geladeneBenutzer = BL_BricoMarche.DatenVerwaltung.Benutzer.LadeAlleBenutzer();

            if (geladeneBenutzer != null && geladeneBenutzer.Count() > 0)
            {
                modell = new List<BenutzerModell>();
                foreach (var benutzer in geladeneBenutzer)
                {
                    modell.Add(new BenutzerModell()
                    {
                        Email = benutzer.Benutzername,
                        Vorname = benutzer.Vorname,
                        Nachname = benutzer.Nachname,
                        Aktiv = benutzer.Aktiv
                    });
                }
            }

            return View(modell);
        }
        #endregion

        #region Bearbeiten
        [Authorize]
        [HttpGet]
        public ActionResult BenutzerBearbeiten(string benutzerName)
        {
            return RedirectToAction("Editieren", "Benutzer", new { benutzerName = benutzerName });
        }
        #endregion

        #region Passwort
        [Authorize]
        public ActionResult PasswortReset(string benutzerName)
        {
            if (!BL_BricoMarche.DatenVerwaltung.Benutzer.PasswortReset(benutzerName))
            {
                TempData["Fehler"] = "Passwort wurde nicht zurückgesetzt";
            }
            else
            {
                TempData["Erfolg"] = "Passwort wurde zurückgesetzt";
            }
            return RedirectToAction("BenutzerVerwalten");
        }
        #endregion
        #endregion
    }
}
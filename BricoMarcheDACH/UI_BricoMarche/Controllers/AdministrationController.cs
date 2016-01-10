using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BL_BricoMarche;
using UI_BricoMarche.Models.InhaltModelle;
using UI_BricoMarche.Models.BenutzerModelle;
using System.IO;
using System.Diagnostics;

namespace UI_BricoMarche.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class AdministrationController : Controller
    {
        #region PRODUKTVERWALTUNG

        #region HttpGet
        [HttpGet]
        public ActionResult ProduktVerwalten(int produktID = 0)
        {
            Artikel artikel = null;
            List<Kategorie> kategorien = null;
            ArtikelBearbeitenModell modell = new ArtikelBearbeitenModell();
            modell.KategorieID = 1;
            modell.Aktiv = true;
            modell.Preis = .0m;
            modell.Kategorien = new List<KategorieModell>();
            if (produktID != 0)
            {
                artikel = BL_BricoMarche.DatenVerwaltung.Artikel.LadeArtikel(produktID);
            }
            if (artikel != null)
            {
                modell.ID = artikel.ID;
                modell.Bezeichnung = artikel.Bezeichnung;
                modell.Langbeschreibung = artikel.Beschreibung;
                modell.Preis = artikel.Preis;
                modell.KategorieID = artikel.Kategorie_ID;
                modell.Aktiv = artikel.Aktiv;
            }
            else
            {
                modell.ID = 0;
            }
            kategorien = BL_BricoMarche.DatenVerwaltung.Kategorie.LadeAlleKategorien();
            if (kategorien != null)
            {
                foreach (var kategorie in kategorien)
                {
                    modell.Kategorien.Add(new KategorieModell()
                    {
                        ID = kategorie.ID,
                        Bezeichnung = kategorie.Bezeichnung
                    });
                }
            }
            else
            {
                modell.Kategorien.Add(new KategorieModell()); 
            }
            return View(modell);
        }
        #endregion

        #region HttpPost
        [HttpPost]
        public ActionResult ProduktVerwalten(ArtikelBearbeitenModell modell, HttpPostedFileBase neuesBild = null)
        {
            int gespeichertesProduktID = 0;
            if (ModelState.IsValid)
            {
                byte[] bildDaten = null;
                if (neuesBild != null)
                {
                    // Convert Image to byte[]
                    using (var binaryReader = new BinaryReader(neuesBild.InputStream))
                    {
                        bildDaten = binaryReader.ReadBytes(neuesBild.ContentLength);
                    }
                }
                if ((bildDaten == null && neuesBild != null) || (modell.ID == 0 && neuesBild == null))
                {
                    TempData["Fehler"] = "Neues Bild konnte nicht hochgeladen werden.";                     
                }
                else
                {
                    gespeichertesProduktID = BL_BricoMarche.DatenVerwaltung.Artikel.SpeichereArtikel(modell.ID, modell.Bezeichnung, modell.Langbeschreibung, modell.KategorieID, modell.Preis, bildDaten, modell.Aktiv);
                }
            }
            else
            {
                TempData["Fehler"] = "Es wurden nicht alle benötgten Felder ausgefüllt.";
            }
            if (gespeichertesProduktID != 0)
            {
                TempData["Erfolg"] = "Produkt erfolgreich gespeichert!";
                return RedirectToAction("ProduktVerwalten", new { produktID = gespeichertesProduktID });
            }
            modell.Kategorien = new List<KategorieModell>();
            List<Kategorie> kategorien = BL_BricoMarche.DatenVerwaltung.Kategorie.LadeAlleKategorien();
            if (kategorien != null)
            {
                foreach (var kategorie in kategorien)
                {
                    modell.Kategorien.Add(new KategorieModell()
                    {
                        ID = kategorie.ID,
                        Bezeichnung = kategorie.Bezeichnung
                    });
                }
            }
            else
            {
                modell.Kategorien.Add(new KategorieModell());
            }
            return View(modell);
        }
        #endregion

        #endregion

        #region BENUTZERVERWALTUNG

        #region Vewalten
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
        [HttpGet]
        public ActionResult BenutzerBearbeiten(string benutzerName)
        {
            return RedirectToAction("Editieren", "Benutzer", new { benutzerName = benutzerName });
        }
        #endregion

        #region Passwort
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
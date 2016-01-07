using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BL_BricoMarche;
using UI_BricoMarche.Models.InhaltModelle;
using UI_BricoMarche.Models.BenutzerModelle;

namespace UI_BricoMarche.Controllers
{
    public class AdministrationController : Controller
    {
        // GET: Administration
        [Authorize]
        public ActionResult Willkommen()
        {
            return View();
        }

        [HttpGet]
        [Authorize]
        public ActionResult ProduktVerwalten(int produktID)
        {
            ArtikelDetailModell modell = null;
            BL_BricoMarche.Artikel artikel = BL_BricoMarche.DatenVerwaltung.Artikel.LadeArtikel(produktID);

            modell = new ArtikelDetailModell()
            {
                ID = artikel.ID,
                Bezeichnung = artikel.Bezeichnung,
                Langbeschreibung = artikel.Beschreibung,
                Preis = artikel.Preis,
                Kategorie = artikel.EineKategorie.Bezeichnung
            };

            return View(modell);
        }

        [HttpPost]
        [Authorize]
        public ActionResult ProduktVerwalten(ArtikelDetailModell modell)
        {
           BL_BricoMarche.DatenVerwaltung.Artikel.SpeichereArtikel(modell.ID, modell.Bezeichnung, modell.Langbeschreibung, modell.Preis);
            return View(modell);
        }

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
                        Nachname = benutzer.Nachname
                    });
                }
            }

            return View(modell);
        }

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
    }
}
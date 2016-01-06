using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using UI_BricoMarche.Models.InhaltModelle;
using BL_BricoMarche.DatenVerwaltung;

namespace UI_BricoMarche.Controllers
{
    /// <summary>
    /// Controller für die Darstellung des Inhaltes
    /// </summary>
    public class InhaltController : Controller
    {
        #region Willkommen
        /// <summary>
        /// Willkommen Action
        /// </summary>
        /// <returns>Willkommen View (Die Startseite der Webanwendung)</returns>
        public ActionResult Willkommen()
        {
            return View();
        }
        #endregion

        #region Kategorien
        /// <summary>
        /// Liefert alle Kategorien an die Kategorien-Teilansicht in der InhaltsNavigation.
        /// </summary>
        /// <param name="inhalt"></param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        [OutputCache(VaryByParam = "id", Duration = 300)]
        public ActionResult Kategorien(string inhalt)
        {
            List<KategorieModell> modell = new List<KategorieModell>();
            List<BL_BricoMarche.Kategorie> alleKategorien = Kategorie.LadeAlleKategorien();
            if (alleKategorien != null)
            {
                foreach (var kategorie in alleKategorien)
                {
                    modell.Add(new KategorieModell
                    {
                        ID = kategorie.ID,
                        Bezeichnung = kategorie.Bezeichnung
                    });
                }
            }
            ViewData["Inhalt"] = inhalt; 
            return PartialView(modell);
        }
        #endregion


        #region -- Produkte Action -------------------------------------------------------

        #region Produkte : Suchbegriff : Seite : Anzahl
        /// <summary>
        /// Gibt eine Anzahl an Artikel einer Seite die einem Suchbegriff entpsrechen an die Sicht weiter.
        /// </summary>
        /// <param name="suchbegriff"></param>
        /// <param name="seite"></param>
        /// <param name="anzahl"></param>
        /// <returns></returns>
        public ActionResult ProdukteSuche(string suchbegriff = "", int seite = 1, int anzahl = 20)
        {
            if (suchbegriff == "")
            {
                return RedirectToAction("Produkte");
            }
            List<BL_BricoMarche.Artikel> geladeneProdukte = Artikel.LadeAlleArtikel(suchbegriff, seite, anzahl);
            if (geladeneProdukte == null)
            {
                TempData["Fehler"] = "Fehler beim Laden gesucher Produkte aus der Datenbank.";
            }
            View("Produkte").ViewBag.Suchbegriff = suchbegriff;
            View("Produkte").ViewBag.AnzahlProdukte = Artikel.ZaehleAlleArtikel(suchbegriff);
            View("Produkte").ViewBag.Seite = 1;
            View("Produkte").ViewBag.AnzahlProSeite = 20;
            List<ArtikelModell> modell = new List<ArtikelModell>();
            foreach (var produkt in geladeneProdukte)
            {
                modell.Add(new ArtikelModell
                {
                    ID = produkt.ID,
                    Bezeichnung = produkt.Bezeichnung,
                    Kategorie = produkt.EineKategorie.Bezeichnung,
                    Preis = produkt.Preis
                });
            }
            return View("Produkte", modell);
        }
        #endregion

        #region Produkte : Kategorie : Seite : Anzahl
        /// <summary>
        /// Gibt eine Anzahl an Produkte einer bestimmten Seite aus einer eventuellen Kategorie an die Sicht weiter.
        /// </summary>
        /// <param name="kategorieID"></param>
        /// <param name="seite" default="1"></param>
        /// <param name="anzahl" default=20></param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Produkte(int kategorieID = -1, int seite = 1, int anzahl = 20) 
        {
            List<ArtikelModell> modell = new List<ArtikelModell>();
            ViewBag.AnzahlProdukte = kategorieID == -1 ? Artikel.ZaehleAlleArtikel() : Artikel.ZaehleAlleArtikel(kategorieID);
            ViewBag.KategorieID = kategorieID;
            ViewBag.AnzahlProSeite = anzahl;
            ViewBag.Seite = seite;
            ViewBag.Suchbegriff = "";
            Debug.WriteLine("-- START: Produkte - GET --------------------------------------------------------------- ");
            Debug.Indent();
            List<BL_BricoMarche.Artikel> geladeneProdukte = kategorieID == -1 ? Artikel.LadeAlleArtikel(seite, anzahl) : Artikel.LadeAlleArtikel(kategorieID, seite, anzahl);
            if (geladeneProdukte == null || geladeneProdukte.Count == 0)
            {
                Debug.WriteLine("Fehler! 0 Produkte in Controller geladen.");
                TempData["Fehler"] = "Fehler beim laden der Produkte aus der Datenbank.";
                return RedirectToAction("Willkommen");
            }
            Debug.WriteLine("Erfolg! " + geladeneProdukte.Count + " Produkte in Controller geladen.");
            foreach (var produkt in geladeneProdukte)
            {
                modell.Add(new ArtikelModell
                {
                    ID = produkt.ID,
                    Bezeichnung = produkt.Bezeichnung,
                    Preis = produkt.Preis,
                    Kategorie = produkt.EineKategorie.Bezeichnung
                });
            }
            Debug.WriteLine("\t -->" + modell.Count + " Produkte in Modell geladen.");
            Debug.Unindent();
            Debug.WriteLine("-- Ende: Produkte - GET --------------------------------------------------------------- ");
            return View(modell);
        }
        #endregion

        #region ProduktDetails
        [HttpGet]
        [AllowAnonymous]
        public ActionResult ProduktDetails(int produktID = -1)
        {
            if (produktID == -1)
            {
                return RedirectToAction("Willkommen", "Inhalt", null);
            }
            ArtikelDetailModell modell = null;
            BL_BricoMarche.Artikel geladenerArtikel = Artikel.LadeArtikel(produktID);
            if (geladenerArtikel == null)
            {
                TempData["Fehler"] = "Fehler beim Laden von Artikel " + produktID + "aus der Datenbank.";
                return RedirectToRoute("~/Error");
            }
            modell = new ArtikelDetailModell
            {
                ID = geladenerArtikel.ID,
                Bezeichnung = geladenerArtikel.Bezeichnung,
                Langbeschreibung = geladenerArtikel.Beschreibung,
                Kategorie = geladenerArtikel.EineKategorie.Bezeichnung,
                Preis = geladenerArtikel.Preis,
                verlinkteVideos = new List<VideoModell>()
            };
            foreach (var video in geladenerArtikel.VerlinkteVideos)
            {
                modell.verlinkteVideos.Add(new VideoModell
                {
                    ID = video.ID,
                    Bezeichnung = video.Bezeichnung,
                    Kategorie = video.EineKategorie.Bezeichnung
                });
            }
            return View(modell);
        }
        #endregion

        #region ProduktBild
        /// <summary>
        /// Holt zu einem bestimmten Produkt das ProduktBild aus der Datenbank.
        /// </summary>
        /// <param name="ProduktID"></param>
        /// <returns>Bild</returns>
        [HttpGet]
        [AllowAnonymous]
        [OutputCache(VaryByParam = "id", Duration = 300)]
        public ActionResult ProduktBild(int ProduktID = -1)
        {
            ActionResult geladenesBild = null;
            ActionResult bild = new FilePathResult(Url.Content("~/Content/images/default-produkt.png"), "image/png");
            if (ProduktID != -1)
            {
                MemoryStream stream = new MemoryStream(Artikel.LadeArtikelBild(ProduktID));
                geladenesBild = new FileStreamResult(stream, "image/png");

            }
            return geladenesBild != null ? geladenesBild : bild;
        }
        #endregion

        #endregion


        #region -- Videos Action ---------------------------------------------------------

        /// <summary>
        /// Videos Action
        /// </summary>
        /// <returns>Alle Videoss</returns>
        #region Videos : Kategorie : Seite : Anzahl
        /// <summary>
        /// Gibt eine Anzahl an Videos einer bestimmten Seite aus einer eventuellen Kategorie an die Sicht weiter.
        /// </summary>
        /// <param name="kategorieID"></param>
        /// <param name="seite" default="1"></param>
        /// <param name="anzahl" default=20></param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Videos(int kategorieID = -1, int seite = 1, int anzahl = 20)
        {
            List<VideoModell> modell = new List<VideoModell>();
            ViewBag.AnzahlVideos = kategorieID == -1 ? Video.ZaehleAlleVideos() : Video.ZaehleAlleVideos(kategorieID);
            ViewBag.KategorieID = kategorieID;
            ViewBag.AnzahlProSeite = anzahl;
            ViewBag.Seite = seite;
            ViewBag.Suchbegriff = "";
            Debug.WriteLine("-- START: Videos - GET --------------------------------------------------------------- ");
            Debug.Indent();
            List<BL_BricoMarche.Video> geladeneVideos = kategorieID == -1 ? Video.LadeAlleVideos(seite, anzahl) : Video.LadeAlleVideos(kategorieID, seite, anzahl);
            if (geladeneVideos == null || geladeneVideos.Count == 0)
            {
                Debug.WriteLine("Fehler! 0 Videos in Controller geladen.");
                TempData["Fehler"] = "Fehler beim laden der Videos aus der Datenbank.";
                return RedirectToAction("Willkommen");
            }
            Debug.WriteLine("Erfolg! " + geladeneVideos.Count + " Videos in Controller geladen.");
            foreach (var video in geladeneVideos)
            {
                modell.Add(new VideoModell
                {
                    ID = video.ID,
                    Bezeichnung = video.Bezeichnung,
                    Kategorie = video.EineKategorie.Bezeichnung
                });
            }
            Debug.WriteLine("\t -->" + modell.Count + " Videos in Modell geladen.");
            Debug.Unindent();
            Debug.WriteLine("-- Ende: Videos - GET --------------------------------------------------------------- ");
            return View(modell);
        }
        #endregion


        #endregion

        #region -- Detail -----------------------------------------------------------------

        /// <summary>
        /// Detail Action
        /// </summary>
        /// <param name="artID"></param>
        /// <returns>Die Detail-Ansicht eines Produkte oder Videoss</returns>
        public ActionResult Details(string artID)
        {
            return View();
        }

        #endregion
    }
}
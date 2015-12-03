using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using UI_BricoMarche.Models.InhaltModelle;
using static BL_BricoMarche.DatenVerwaltung.Inhalt;

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
        public ActionResult Kategorien(string inhalt)
        {
            List<KategorieModell> modell = new List<KategorieModell>();
            List<BL_BricoMarche.Kategorie> alleKategorien = LadeAlleKategorien();
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

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="kategorieID"></param>
        ///// <returns></returns>
        //[HttpGet]
        //[AllowAnonymous]
        //public ActionResult Produkte(int kategorieID = -1)
        //{
        //    Debug.WriteLine("-- START: Produkte - GET --------------------------------------------------------------- ");
        //    Debug.Indent();
        //    List<ArtikelModell> modell = new List<ArtikelModell>();
        //    List<BL_BricoMarche.Artikel> geladeneProdukte = kategorieID == -1 ? LadeAlleArtikel() : LadeAlleArtikel(kategorieID);
        //    if (geladeneProdukte == null || geladeneProdukte.Count == 0)
        //    {
        //        Debug.WriteLine("Fehler! 0 Produkte in Controller geladen.");
        //        TempData["Fehler"] = "Fehler beim laden der Produkte aus der Datenbank.";
        //        return RedirectToAction("Willkommen");
        //    }
        //    Debug.WriteLine("Erfolg! " + geladeneProdukte.Count + " Produkte in Controller geladen.");
        //    foreach (var produkt in geladeneProdukte)
        //    {
        //        modell.Add(new ArtikelModell {
        //            ID = produkt.ID,
        //            Bezeichnung = produkt.Bezeichnung,
        //            Preis = produkt.Preis,
        //            Kategorie = produkt.EineKategorie.Bezeichnung
        //        });
        //    }
        //    Debug.WriteLine("\t -->" + modell.Count + " Produkte in Modell geladen.");
        //    Debug.Unindent();
        //    Debug.WriteLine("-- Ende: Produkte - GET --------------------------------------------------------------- ");
        //    return View(modell);
        //}


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
            ViewBag.AnzahlProdukte = kategorieID == -1 ? ZaehleAlleArtikel() : ZaehleAlleArtikel(kategorieID);
            ViewBag.KategorieID = kategorieID;
            ViewBag.AnzahlProSeite = anzahl;
            ViewBag.Seite = seite;
            Debug.WriteLine("-- START: Produkte - GET --------------------------------------------------------------- ");
            Debug.Indent();
            List<BL_BricoMarche.Artikel> geladeneProdukte = kategorieID == -1 ? LadeAlleArtikel(seite, anzahl) : LadeAlleArtikel(kategorieID, seite, anzahl);
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
                MemoryStream stream = new MemoryStream(LadeArtikelBild(ProduktID));
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
        public ActionResult Videos()
        {
            return View();
        }

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
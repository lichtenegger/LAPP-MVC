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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="kategorieID"></param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Produkte(int kategorieID = -1)
        {
            Debug.WriteLine("-- START: Produkte - GET --------------------------------------------------------------- ");
            Debug.Indent();
            List<ArtikelModell> modell = new List<ArtikelModell>();
            List<BL_BricoMarche.Artikel> geladeneProdukte = kategorieID == -1 ? LadeAlleArtikel() : LadeAlleArtikel(kategorieID);
            if (geladeneProdukte == null || geladeneProdukte.Count == 0)
            {
                Debug.WriteLine("Fehler! 0 Produkte in Controller geladen.");
                TempData["Fehler"] = "Fehler beim laden der Produkte aus der Datenbank.";
                return RedirectToAction("Willkommen");
            }
            Debug.WriteLine("Erfolg! " + geladeneProdukte.Count + " Produkte in Controller geladen.");
            foreach (var produkt in geladeneProdukte)
            {
                modell.Add(new ArtikelModell {
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
         
        /*
        /// <summary>
        /// Produkte Action
        /// </summary>
        /// <param name="anzahl"></param>
        /// <param name="seite"></param>
        /// <returns>Eine Anzahl an Produkte auf einer bestimmten Seite</returns>
        public ActionResult Produkte(int anzahl, int seite)
        {
            return View();
        }


        /// <summary>
        /// Produkte Action
        /// </summary>
        /// <param name="kategorie"></param>
        /// <param name="anzahl"></param>
        /// <param name="seite"></param>
        /// <returns>Eine Anzahl an Produkte einer Kategorie auf einer bestimmten Seite</returns>
        public ActionResult Produkte(string kategorie, int anzahl, int seite)
        {
            return View();
        }
        */

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

        /*
        /// <summary>
        /// Videos Action
        /// </summary>
        /// <param name="kategorie"></param>
        /// <returns>Alle Videoss einer Kategorie</returns>
        public ActionResult Videos(string kategorie)
        {
            return View();
        }

        /// <summary>
        /// Videos Action
        /// </summary>
        /// <param name="anzahl"></param>
        /// <param name="seite"></param>
        /// <returns>Eine Anzahl an Videoss einer bestimmten Seite</returns>
        public ActionResult Videos(int anzahl, int seite)
        {
            return View();
        }

        /// <summary>
        /// Videos Action
        /// </summary>
        /// <param name="kategorie"></param>
        /// <param name="anzahl"></param>
        /// <param name="seite"></param>
        /// <returns>Eine Anzahl Videoss einer Kategorie einer bestimmten Seite</returns>
        public ActionResult Videos(string kategorie, int anzahl, int seite)
        {
            return View();
        }

        /// <summary>
        /// Videos Action
        /// </summary>
        /// <param name="kategorie"></param>
        /// <param name="schlagwort"></param>
        /// <returns>Alle Videoss einer Kategorie die einem bestimmten Schlagwort entsprechen</returns>
        public ActionResult Videos(string kategorie, string schlagwort)
        {
            return View();
        }

        /// <summary>
        /// Videos Action
        /// </summary>
        /// <param name="kategorie"></param>
        /// <param name="schlagwort"></param>
        /// <param name="anzahl"></param>
        /// <param name="seite"></param>
        /// <returns>Eine Anzahl an Videos einer Kategorie, die einem Schlagwort entsprechen, einer bestimmten Seite</returns>
        public ActionResult Videos(string kategorie, string schlagwort, int anzahl, int seite)
        {
            return View();
        }
        */

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
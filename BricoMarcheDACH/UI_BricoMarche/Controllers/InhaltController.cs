using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        /// <summary>
        /// Willkommen Action
        /// </summary>
        /// <returns>Willkommen View (Die Startseite der Webanwendung)</returns>
        public ActionResult Willkommen()
        {
            return View();
        }

        public ActionResult Kategorien(string inhalt)
        {
            List<KategorieModell> modell = new List<KategorieModell>();
            List<BL_BricoMarche.Kategorie> alleKategorien = InhaltKategorien.LadeAlleKategorien();
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

        #region -- Produkte Action -------------------------------------------------------

        /// <summary>
        /// Aritkel Action
        /// </summary>
        /// <returns>Alle Produkte</returns>
        public ActionResult Produkte()
        {
            return View();
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
        /// <returns>Alle Produkte einer Kategorie</returns>
        public ActionResult Produkte(string kategorie)
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
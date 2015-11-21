using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using UI_BricoMarche.Models;
using BL_BricoMarche;

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

        #region -- Artikel Action -------------------------------------------------------

        /// <summary>
        /// Aritkel Action
        /// </summary>
        /// <returns>Alle Artikel</returns>
        public ActionResult Artikel()
        {
            return View();
        }

        /// <summary>
        /// Artikel Action
        /// </summary>
        /// <param name="anzahl"></param>
        /// <param name="seite"></param>
        /// <returns>Eine Anzahl an Artikel auf einer bestimmten Seite</returns>
        public ActionResult Artikel(int anzahl, int seite)
        {
            return View();
        }

        /// <summary>
        /// Artikel Action
        /// </summary>
        /// <param name="kategorie"></param>
        /// <returns>Alle Artikel einer Kategorie</returns>
        public ActionResult Artikel(string kategorie)
        {
            return View();
        }

        /// <summary>
        /// Artikel Action
        /// </summary>
        /// <param name="kategorie"></param>
        /// <param name="anzahl"></param>
        /// <param name="seite"></param>
        /// <returns>Eine Anzahl an Artikel einer Kategorie auf einer bestimmten Seite</returns>
        public ActionResult Artikel(string kategorie, int anzahl, int seite)
        {
            return View();
        }

        #endregion


        #region -- Video Action ---------------------------------------------------------

        /// <summary>
        /// Video Action
        /// </summary>
        /// <returns>Alle Videos</returns>
        public ActionResult Video()
        {
            return View();
        }

        /// <summary>
        /// Video Action
        /// </summary>
        /// <param name="kategorie"></param>
        /// <returns>Alle Videos einer Kategorie</returns>
        public ActionResult Video(string kategorie)
        {
            return View();
        }

        /// <summary>
        /// Video Action
        /// </summary>
        /// <param name="anzahl"></param>
        /// <param name="seite"></param>
        /// <returns>Eine Anzahl an Videos einer bestimmten Seite</returns>
        public ActionResult Video(int anzahl, int seite)
        {
            return View();
        }

        /// <summary>
        /// Video Action
        /// </summary>
        /// <param name="kategorie"></param>
        /// <param name="anzahl"></param>
        /// <param name="seite"></param>
        /// <returns>Eine Anzahl Videos einer Kategorie einer bestimmten Seite</returns>
        public ActionResult Video(string kategorie, int anzahl, int seite)
        {
            return View();
        }

        /// <summary>
        /// Video Action
        /// </summary>
        /// <param name="kategorie"></param>
        /// <param name="schlagwort"></param>
        /// <returns>Alle Videos einer Kategorie die einem bestimmten Schlagwort entsprechen</returns>
        public ActionResult Video(string kategorie, string schlagwort)
        {
            return View();
        }

        /// <summary>
        /// Video Action
        /// </summary>
        /// <param name="kategorie"></param>
        /// <param name="schlagwort"></param>
        /// <param name="anzahl"></param>
        /// <param name="seite"></param>
        /// <returns>Eine Anzahl an Video einer Kategorie, die einem Schlagwort entsprechen, einer bestimmten Seite</returns>
        public ActionResult Video(string kategorie, string schlagwort, int anzahl, int seite)
        {
            return View();
        }

        #endregion

        /// <summary>
        /// Detail Action
        /// </summary>
        /// <param name="artID"></param>
        /// <returns>Die Detail-Ansicht eines Artikel oder Videos</returns>
        public ActionResult Details(string artID)
        {
            return View();
        }


    }
}
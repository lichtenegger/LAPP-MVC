using System;
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
            if (ModelState.IsValid && sindAnmeldeDatenRichtig(daten.Benuztername, daten.Passwort))
            {
                FormsAuthentication.SetAuthCookie(daten.Benuztername, true);
                Debug.WriteLine("AnmeldeDaten sind richtig; AuthCookie gesetzt.");
                return RedirectToAction("Willkommen");
            }
            return RedirectToRoute("/Error");
        }
    }
}
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
            return RedirectToAction("Produkte");
        }
        #endregion

        #region Kategorien
        /// <summary>
        /// Liefert alle Kategorien an die Kategorien-Teilansicht in der InhaltsNavigation.
        /// </summary>
        /// <param name="inhalt"></param>
        /// <returns></returns>
        //[HttpGet]
        [AllowAnonymous]
        [OutputCache(VaryByParam = "inhalt", Duration = 300)]
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
        [HttpGet]
        [AllowAnonymous]
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
        public ActionResult ProduktDetails(int produktID = 0)
        {
            if (produktID != 0)
            {
                ArtikelDetailModell modell = null;
                BL_BricoMarche.Artikel geladenerArtikel = Artikel.LadeArtikel(produktID);
                if (geladenerArtikel != null)
                {
                    List<BL_BricoMarche.Video> verlinkteVideos = geladenerArtikel.VerlinkteVideos.Take(3).ToList();
                    modell = new ArtikelDetailModell
                    {
                        ID = geladenerArtikel.ID,
                        Bezeichnung = geladenerArtikel.Bezeichnung,
                        Langbeschreibung = geladenerArtikel.Beschreibung,
                        Kategorie = geladenerArtikel.EineKategorie.Bezeichnung,
                        Preis = geladenerArtikel.Preis,
                        verlinkteVideos = new List<VideoModell>(),
                        Gemerkt = Artikel.WirdGemerkt(produktID, User.Identity.Name)
                    };
                    foreach (var video in verlinkteVideos)
                    {
                        modell.verlinkteVideos.Add(new VideoModell
                        {
                            ID = video.ID,
                            Bezeichnung = video.Bezeichnung
                        });
                    }
                    return View(modell);
                }
                TempData["Fehler"] = "Fehler beim Laden von Artikel " + produktID + " aus der Datenbank.";
                if (Request.UrlReferrer != null)
                {
                    return Redirect(Request.UrlReferrer.ToString());
                }
            }
            return RedirectToAction("Willkommen", "Inhalt", null);

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
        //[OutputCache(VaryByParam = "id", Duration = 300)]
        public ActionResult ProduktBild(int ProduktID = -1)
        {
            ActionResult geladenesBild = null;
            ActionResult bild = new FilePathResult(Url.Content("~/Content/images/default-produkt.png"), "image/*");
            if (ProduktID != -1)
            {
                MemoryStream stream = new MemoryStream(Artikel.LadeArtikelBild(ProduktID));
                geladenesBild = new FileStreamResult(stream, "image/*");

            }
            return geladenesBild != null ? geladenesBild : bild;
        }
        #endregion


        #region Produkt merken
        [HttpGet]
        [Authorize]
        public ActionResult ProduktMerken(int produktID = 0)
        {
            if (produktID < 1)
            {
                return RedirectToRoute("~/Error");
            }
            if (!Benutzer.MerkeArtikel(produktID, User.Identity.Name))
            {
                TempData["Fehler"] = "Video wurde nicht gemerkt!";
            }
            return RedirectToAction("ProduktDetails", "Inhalt", new { produktID = produktID });
        }
        #endregion

        #region Produkt vergessen
        [HttpGet]
        [Authorize]
        public ActionResult ProduktVergessen(int produktID = 0)
        {
            if (produktID < 1)
            {
                return RedirectToRoute("~/Error");
            }
            if (!Benutzer.VergissArtikel(produktID, User.Identity.Name))
            {
                TempData["Fehler"] = "Video wurde nicht vergessen!";
            }
            return RedirectToAction("ProduktDetails", "Inhalt", new { produktID = produktID });
        }
        #endregion

        #endregion


        #region -- Videos Action ---------------------------------------------------------

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
            ViewBag.Schlagwort = "";
            Debug.WriteLine("-- START: Videos - GET --------------------------------------------------------------- ");
            Debug.Indent();
            List<BL_BricoMarche.Video> geladeneVideos = kategorieID == -1 ? Video.LadeAlleVideos(seite, anzahl) : Video.LadeAlleVideos(kategorieID, seite, anzahl);
            if (geladeneVideos == null)
            {
                Debug.WriteLine("Fehler!");
                TempData["Fehler"] = "Fehler beim Laden der Videos aus der Datenbank.";
                return RedirectToAction("Videos");
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

        #region Videos : Suchbegriff : Seite : Anzahl
        /// <summary>
        /// Gibt eine Anzahl an Videos einer Seite die einem Suchbegriff entpsrechen an die Sicht weiter.
        /// </summary>
        /// <param name="suchbegriff"></param>
        /// <param name="seite"></param>
        /// <param name="anzahl"></param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public ActionResult VideosSuche(string suchbegriff = "", int seite = 1, int anzahl = 20)
        {
            if (suchbegriff == "")
            {
                return RedirectToAction("Videos");
            }
            Debug.WriteLine("-- START: Videos - GET --------------------------------------------------------------- ");
            Debug.Indent();
            List<BL_BricoMarche.Video> geladeneVideos = Video.LadeAlleVideos(suchbegriff, seite, anzahl);
            if (geladeneVideos == null)
            {
                TempData["Fehler"] = "Fehler beim Laden gesucher Videos aus der Datenbank.";
                return RedirectToAction("Videos");
            }
            Debug.WriteLine("Erfolg! " + geladeneVideos.Count + " Videos in Controller geladen.");
            View("Videos").ViewBag.Suchbegriff = suchbegriff;
            View("Videos").ViewBag.AnzahlVideos = Video.ZaehleAlleVideos(suchbegriff);
            View("Videos").ViewBag.Seite = 1;
            View("Videos").ViewBag.AnzahlProSeite = 20;
            List<VideoModell> modell = new List<VideoModell>();
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
            return View("Videos", modell);
        }
        #endregion

        #region VideoDetails
        [HttpGet]
        [AllowAnonymous]
        public ActionResult VideoDetails(int videoID = -1)
        {
            if (videoID == -1)
            {
                return RedirectToAction("Willkommen", "Inhalt", null);
            }
            VideoDetailModell modell = null;
            BL_BricoMarche.Video geladenesVideo = Video.LadeVideo(videoID);
            if (geladenesVideo == null)
            {
                TempData["Fehler"] = "Fehler beim Laden von Video " + videoID + "aus der Datenbank.";
                return RedirectToRoute("~/Error");
            }
            modell = new VideoDetailModell
            {
                ID = geladenesVideo.ID,
                Bezeichnung = geladenesVideo.Bezeichnung,
                Langbeschreibung = geladenesVideo.Beschreibung,
                Kategorie = geladenesVideo.EineKategorie.Bezeichnung,
                Pfad = geladenesVideo.Pfad,
                Schlagwoerter = new List<string>(),
                verlinkteProdukte = new List<ArtikelModell>(),
                Gemerkt = Video.WirdGemerkt(videoID, User.Identity.Name)
            };
            foreach (var schlagwort in geladenesVideo.VieleSchlagwoerter)
            {
                modell.Schlagwoerter.Add(schlagwort.Bezeichnung);
            }
            foreach (var produkt in geladenesVideo.VerlinkteArtikel)
            {
                modell.verlinkteProdukte.Add(new ArtikelModell
                {
                    ID = produkt.ID,
                    Bezeichnung = produkt.Bezeichnung
                });
            }
            return View(modell);
        }
        #endregion

        #region VideoBild
        /// <summary>
        /// Holt zu einem bestimmten Video das VideoBild aus der Datenbank.
        /// </summary>
        /// <param name="videoID"></param>
        /// <returns>Bild</returns>
        [HttpGet]
        [AllowAnonymous]
        [OutputCache(VaryByParam = "id", Duration = 300)]
        public ActionResult VideoBild(int videoID = -1)
        {
            ActionResult geladenesBild = null;
            ActionResult bild = new FilePathResult(Url.Content("~/Content/images/default-produkt.png"), "image/png");
            if (videoID != -1)
            {
                MemoryStream stream = new MemoryStream(Video.LadeVideoBild(videoID));
                geladenesBild = new FileStreamResult(stream, "image/png");

            }
            return geladenesBild != null ? geladenesBild : bild;
        }
        #endregion

        #region Video merken
        [HttpGet]
        [Authorize]
        public ActionResult VideoMerken(int videoID = 0)
        {
            if (videoID < 1)
            {
                return RedirectToRoute("~/Error");
            }
            if (!Benutzer.MerkeVideo(videoID, User.Identity.Name))
            {
                TempData["Fehler"] = "Video wurde nicht gemerkt!";
            }
            return RedirectToAction("VideoDetails", "Inhalt", new { videoID = videoID });
        }
        #endregion

        #region Video vergessen
        [HttpGet]
        [Authorize]
        public ActionResult VideoVergessen(int videoID = 0)
        {
            if (videoID < 1)
            {
                return RedirectToRoute("~/Error");
            }
            if (!Benutzer.VergissVideo(videoID, User.Identity.Name))
            {
                TempData["Fehler"] = "Video wurde nicht vergessen!";
            }
            return RedirectToAction("VideoDetails", "Inhalt", new { videoID = videoID });
        }
        #endregion

        #endregion

    }

    #region Wandler
    class Wandler
    {
        public static VideoDetailModell Wandle(BL_BricoMarche.Video video)
        {
            VideoDetailModell modell = null;
            if (video != null)
            {
                modell = new VideoDetailModell()
                {
                    ID = video.ID,
                    Bezeichnung = video.Bezeichnung,
                    Kategorie = video.EineKategorie.Bezeichnung,
                    Langbeschreibung = video.Beschreibung,
                    Pfad = video.Pfad
                };
            }

            return modell;
        }
    }
    #endregion
}
using BL_BricoMarche.DatenVerwaltung;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UI_BricoMarche.Models.BenutzerModelle;
using UI_BricoMarche.Models.InhaltModelle;

namespace UI_BricoMarche.Controllers
{
    public class MerklisteController : Controller
    {
        #region Merkliste
        [HttpGet]
        [Authorize]
        public ActionResult Willkommen()
        {
            Debug.WriteLine("-- START: MERKLISTE - GET --------------------------------------------------------------- ");
            Debug.Indent();

            MerklisteModell modell = new MerklisteModell();

            #region gemerkte Videos
            List<BL_BricoMarche.Video> gemerkteVideos = Video.LadeGemerkteVideos(User.Identity.Name);
            modell.Videos = new List<VideoModell>();
            if (gemerkteVideos != null && gemerkteVideos.Count() > 0)
            {
                foreach (var video in gemerkteVideos)
                {
                    modell.Videos.Add(new VideoModell()
                    {
                        ID = video.ID,
                        Bezeichnung = video.Bezeichnung
                    });
                }
            }
            #endregion

            #region gemerkte Artikel
            List<BL_BricoMarche.Artikel> gemerkteArtikel = Artikel.LadeGemerkteArtikel(User.Identity.Name);
            modell.Produkte = new List<ArtikelModell>();
            if (gemerkteArtikel != null && gemerkteArtikel.Count() > 0)
            {
                foreach (var artikel in gemerkteArtikel)
                {
                    modell.Produkte.Add(new ArtikelModell()
                    {
                        ID = artikel.ID,
                        Bezeichnung = artikel.Bezeichnung
                    });
                }
            }
            #endregion


            Debug.Unindent();
            Debug.WriteLine("-- Ende: Videos - GET --------------------------------------------------------------- ");
            return View(modell);
        }

        #endregion
    }
}
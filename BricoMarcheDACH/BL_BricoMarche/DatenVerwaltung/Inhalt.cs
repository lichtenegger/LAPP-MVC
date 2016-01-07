using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL_BricoMarche.DatenVerwaltung
{
    public class Kategorie
    {
        #region LadeAlleKategorien
        /// <summary>
        /// Holt alle Kategorien aus der Datenbank.
        /// </summary>
        /// <returns>Alle Kategorien</returns>
        public static List<BL_BricoMarche.Kategorie> LadeAlleKategorien()
        {
            Debug.WriteLine("-- START : LADE ALLE KATEGORIEN ------------------------");
            Debug.Indent();
            List<BL_BricoMarche.Kategorie> alleKategorien = null;
            try
            {
                using (var kontext = new BricoMarcheDBObjekte())
                {
                    alleKategorien = kontext.AlleKategorien.ToList();
                }
                if (alleKategorien != null)
                {
                    Debug.WriteLine("ERFOLG!");
                }
                else
                {
                    throw new Exception("Fehler! 0 Kategorien geladen.");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("FEHLER!\n " + ex.Message);
            }
            Debug.Unindent();
            Debug.WriteLine("-- Ende : LADE ALLE KATEGORIEN ------------------------");
            return alleKategorien;
        }
        #endregion
    }

    public class Artikel
    {
        #region LadeAlleArtikel
        /// <summary>
        /// Holt alle Artikel aus der Datenbank.
        /// </summary>
        /// <returns>geladene Artikel</returns>
        public static List<BL_BricoMarche.Artikel> LadeAlleArtikel()
        {
            List<BL_BricoMarche.Artikel> geladeneArtikel = null;
            Debug.WriteLine("-- START : LADE ARTIKEL -------------------------------------------------------");
            Debug.Indent();
            try
            {
                using (var kontext = new BricoMarcheDBObjekte())
                {
                    geladeneArtikel = kontext.AlleArtikel.Include("EineKategorie").Where(x => x.Aktiv).ToList();
                }
                if (geladeneArtikel == null)
                {
                    throw new Exception("Fehler! 0 Artikel geladen!");
                }
                Debug.WriteLine("ERFOLG!");
            }
            catch (Exception ex)
            {
                Debug.WriteLine("FEHLER! " + ex.Message);
            }
            Debug.Unindent();
            Debug.WriteLine("-- ENDE : LADE ARTIKEL -------------------------------------------------------");
            return geladeneArtikel;
        }
        #endregion

        #region LadeAlleArtikel : Seite : Anzahl
        /// <summary>
        /// Holt eine Anzahl Artikel auf einer bestimmten Seite aus der Datenbank.
        /// </summary>
        /// <param name="seite"></param>
        /// <param name="anzahl"></param>
        /// <returns></returns>
        public static List<BL_BricoMarche.Artikel> LadeAlleArtikel(int seite, int anzahl)
        {
            List<BL_BricoMarche.Artikel> geladeneArtikel = null;
            Debug.WriteLine("-- START : LADE ARTIKEL -------------------------------------------------------");
            Debug.Indent();
            try
            {
                using (var kontext = new BricoMarcheDBObjekte())
                {
                    geladeneArtikel = kontext.AlleArtikel.Include("EineKategorie").Where(x => x.Aktiv).OrderByDescending(x => x.ID).Skip((seite -1) * anzahl).Take(anzahl).ToList();
                }
                if (geladeneArtikel == null)
                {
                    throw new Exception("Fehler! 0 Artikel geladen!");
                }
                Debug.WriteLine("ERFOLG!");
            }
            catch (Exception ex)
            {
                Debug.WriteLine("FEHLER! " + ex.Message);
            }
            Debug.Unindent();
            Debug.WriteLine("-- ENDE : LADE ARTIKEL -------------------------------------------------------");
            return geladeneArtikel;
        }
        #endregion

        #region LadeAlleArtikel : Kategorie
        /// <summary>
        /// Holt alle Artikel aus einer bestimmten Kategoire aus der Datenbank.
        /// </summary>
        /// <param name="kategorie"></param>
        /// <returns>geladene Artikel</returns>
        public static List<BL_BricoMarche.Artikel> LadeAlleArtikel(int kategorieID)
        {
            List<BL_BricoMarche.Artikel> geladeneArtikel = null;
            Debug.WriteLine("-- START : LADE ARTIKEL -------------------------------------------------------");
            Debug.Indent();
            Debug.WriteLine("-- KATEGORIE : " + kategorieID);
            try
            {
                using (var kontext = new BricoMarcheDBObjekte())
                {
                    geladeneArtikel = kontext.AlleArtikel.Include("EineKategorie").Where(x => x.Aktiv).Where(x => x.Kategorie_ID == kategorieID).ToList();
                }
                if (geladeneArtikel == null)
                {
                    throw new Exception("Fehler! 0 Artikel geladen!");
                }
                Debug.WriteLine("ERFOLG!");
            }
            catch (Exception ex)
            {
                Debug.WriteLine("FEHLER! " + ex.Message);
            }


            Debug.Unindent();
            Debug.WriteLine("-- ENDE : LADE ARTIKEL -------------------------------------------------------");
            return geladeneArtikel;
        }
        #endregion

        #region LadeAlleArtikel : Kategorie : Seite : Anzahl
        /// <summary>
        /// Holt eine Anzahl an Artikel einer bestimmten Kategorie auf einer bestimmten Seite aus der Datenbank.
        /// </summary>
        /// <param name="kategorieID"></param>
        /// <param name="seite"></param>
        /// <param name="anzahl"></param>
        /// <returns></returns>
        public static List<BL_BricoMarche.Artikel> LadeAlleArtikel(int kategorieID, int seite, int anzahl)
        {
            List<BL_BricoMarche.Artikel> geladeneArtikel = null;
            Debug.WriteLine("-- START : LADE ARTIKEL -------------------------------------------------------");
            Debug.Indent();
            Debug.WriteLine("-- KATEGORIE : " + kategorieID);
            try
            {
                using (var kontext = new BricoMarcheDBObjekte())
                {
                    geladeneArtikel = kontext.AlleArtikel.Include("EineKategorie").Where(x => x.Aktiv).Where(x => x.Kategorie_ID == kategorieID).OrderByDescending(x => x.ID).Skip((seite -1) * anzahl).Take(anzahl).ToList();
                }
                if (geladeneArtikel == null)
                {
                    throw new Exception("Fehler! 0 Artikel geladen!");
                }
                Debug.WriteLine("ERFOLG!");
            }
            catch (Exception ex)
            {
                Debug.WriteLine("FEHLER! " + ex.Message);
            }


            Debug.Unindent();
            Debug.WriteLine("-- ENDE : LADE ARTIKEL -------------------------------------------------------");
            return geladeneArtikel;
        }
        #endregion

        #region LadeArtikel : ID
        /// <summary>
        /// Holt einen bestimmten Artikel aus der Datenbank
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static BL_BricoMarche.Artikel LadeArtikel(int ID)
        {
            BL_BricoMarche.Artikel geladenerArtikel = null;
            Debug.WriteLine("-- START : LADE ARTIKEL -------------------------------------------------------------");
            Debug.Indent();
            try
            {
                using (var kontext = new BricoMarcheDBObjekte())
                {
                    geladenerArtikel = kontext.AlleArtikel.Include("VerlinkteVideos").Include("EineKategorie").Where(x => x.Aktiv).Where(x => x.ID == ID).Single();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Fehler! \n" + ex.Message);
            }
            Debug.Unindent();
            Debug.WriteLine("-- ENDE : LADE ARTIKEL -------------------------------------------------------------");

            return geladenerArtikel;
        }
        #endregion

        #region LadeAlleArtikel : Suchbegriff
        /// <summary>
        /// Holt alle Artikel, dessen Bezeichnung oder Beschreibung den Suchbegriff enthält aus der Datenbank.
        /// </summary>
        /// <param name="suchbegriff"></param>
        /// <returns></returns>
        public static List<BL_BricoMarche.Artikel> LadeAlleArtikel(string suchbegriff)
        {
            List<BL_BricoMarche.Artikel> geladenerArtikel = null;
            Debug.WriteLine("-- START : LADE ARTIKEL -------------------------------------------------------------");
            Debug.Indent();
            try
            {
                using (var kontext = new BricoMarcheDBObjekte())
                {
                    geladenerArtikel = kontext.AlleArtikel.Include("EineKategorie").Where(x => x.Aktiv)
                                                          .Where(x => x.Bezeichnung.ToLower().Contains(suchbegriff.ToLower()) || 
                                                                 x.Beschreibung.ToLower().Contains(suchbegriff.ToLower())).ToList();
                    if (geladenerArtikel == null)
                    {
                        throw new Exception("Fehler! keine Artikel aus der Datenbank geladen");
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Fehler! \n" + ex.Message);
            }
            Debug.Unindent();
            Debug.WriteLine("-- ENDE : LADE ARTIKEL -------------------------------------------------------------");

            return geladenerArtikel;
        }
        #endregion

        #region LadeAlleArtikel : Suchbegriff : Seite : Anzahl
        /// <summary>
        /// Holt eine Anzahl an Artikel einer Seite, die einen bestimmten Suchbegriff enthalten, aus der Datenbank.
        /// </summary>
        /// <param name="suchbegriff"></param>
        /// <param name="seite"></param>
        /// <param name="anzahl"></param>
        /// <returns></returns>
        public static List<BL_BricoMarche.Artikel> LadeAlleArtikel(string suchbegriff, int seite, int anzahl)
        {
            List<BL_BricoMarche.Artikel> geladenerArtikel = null;
            Debug.WriteLine("-- START : LADE ARTIKEL -------------------------------------------------------------");
            Debug.Indent();
            try
            {
                using (var kontext = new BricoMarcheDBObjekte())
                {
                    geladenerArtikel = kontext.AlleArtikel.Include("EineKategorie").Where(x => x.Aktiv)
                                                          .Where(x => x.Bezeichnung.ToLower().Contains(suchbegriff.ToLower()) ||
                                                                 x.Beschreibung.ToLower().Contains(suchbegriff.ToLower()))
                                                          .OrderByDescending(x => x.ID).Skip((seite - 1) * anzahl).Take(anzahl).ToList();
                    if (geladenerArtikel == null)
                    {
                        throw new Exception("Fehler! keine Artikel aus der Datenbank geladen");
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Fehler! \n" + ex.Message);
            }
            Debug.Unindent();
            Debug.WriteLine("-- ENDE : LADE ARTIKEL -------------------------------------------------------------");

            return geladenerArtikel;
        }
        #endregion

        #region LadeArtikelBild : ID
        /// <summary>
        /// Holt das Bild eines bestimmten Artikels aus der Datenbank.
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static byte[] LadeArtikelBild(int ID)
        {
            byte[] geladenesBild = null;
            Debug.WriteLine("-- START : LADE ARTIKEL BILD -------------------------------------------------------------");
            Debug.Indent();
            try
            {
                using (var kontext = new BricoMarcheDBObjekte())
                {
                    geladenesBild = kontext.AlleArtikel.Where(x => x.Aktiv).Where(x => x.ID == ID).Single().Bild;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Fehler! \n" + ex.Message);
            }
            Debug.Unindent();
            Debug.WriteLine("-- ENDE : LADE ARTIKEL BILD -------------------------------------------------------------");


            return geladenesBild;
        }
        #endregion

        #region WirdGemerkt : ID : BenutzerName
        /// <summary>
        /// Fragt ab ob ein bestimmter Artikel auf der Merkliste eines bestimmten Benutzer steht.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="benutzerName"></param>
        /// <returns>true; false</returns>
        public static bool WirdGemerkt(int id, string benutzerName)
        {
            bool gemerkt = false;
            Debug.WriteLine("-- START : ARTIKEL WIRD GEMERKT? -------------------------------------------------------------");
            Debug.Indent();
            try
            {
                using (var kontext = new BricoMarcheDBObjekte())
                {
                    gemerkt = kontext.AlleBenutzer.Include("GemerkteArtikel").Where(x => x.Benutzername == benutzerName)
                                    .SingleOrDefault()
                                    .GemerkteArtikel.Contains(kontext.AlleArtikel.Where(x => x.ID == id)
                                    .SingleOrDefault());
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Fehler! \n" + ex.Message);
            }
            Debug.Unindent();
            Debug.WriteLine("-- ENDE : ARTIKEL WIRD GEMERKT? -------------------------------------------------------------");


            return gemerkt;
        }
        #endregion

        #region LadeGemerkteArtikel : benutzerName
        public static List<BL_BricoMarche.Artikel> LadeGemerkteArtikel(string benutzerName)
        {
            List<BL_BricoMarche.Artikel> gemerkteArtikel = null;
            Debug.WriteLine("-- START : LADE GEMEKRTE ARTIKEL -------------------------------------------------------------");
            Debug.Indent();
            try
            {
                using (var kontext = new BricoMarcheDBObjekte())
                {
                    gemerkteArtikel = kontext.AlleBenutzer.Include("GemerkteArtikel").Where(x => x.Benutzername == benutzerName).SingleOrDefault().GemerkteArtikel.ToList();

                    if (gemerkteArtikel == null)
                    {
                        throw new Exception("Fehler! keine Artikel aus der Datenbank geladen");
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Fehler! \n" + ex.Message);
            }
            Debug.Unindent();
            Debug.WriteLine("-- ENDE : LADE GEMEKRTE ARTIKEL -------------------------------------------------------------");
            return gemerkteArtikel;

        }
        #endregion

        #region ZaehleAlleArtikel
        /// <summary>
        /// Gibt die Anzahl aller aktiven Artikel in der Datenbank zurück.
        /// </summary>
        /// <returns></returns>
        public static int ZaehleAlleArtikel()
        {
            int anzahl = -1;
            Debug.WriteLine("-- START : ZAEHLE ALLE ARTIKEL ----------------------------------------");
            Debug.Indent();
            try
            {
                using (var kontext = new BricoMarcheDBObjekte())
                {
                    anzahl = kontext.AlleArtikel.Where(x => x.Aktiv).Count();
                    Debug.WriteLine("ERFOLG! ArtikelAnzahl ist" + anzahl);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("FEHLER!\n" + ex.Message);
            }

            Debug.Unindent();
            Debug.WriteLine("-- ENDE : ZAEHLE ALLE ARTIKEL ----------------------------------------");
            return anzahl;
        }
        #endregion

        #region ZaehleAlleArtikel : Kategorie
        /// <summary>
        /// Gibt die Anzahl aller aktiven Artikel einer bestimmten Kategorie in der Datenbank zurück.
        /// </summary>
        /// <param name="kategorieID"></param>
        /// <returns></returns>
        public static int ZaehleAlleArtikel(int kategorieID)
        {
            int anzahl = -1;
            Debug.WriteLine("-- START : ZAEHLE ALLE ARTIKEL ----------------------------------------");
            Debug.Indent();
            try
            {
                using (var kontext = new BricoMarcheDBObjekte())
                {
                    anzahl = kontext.AlleArtikel.Where(x => x.Aktiv).Where(x => x.Kategorie_ID == kategorieID).Count();
                    Debug.WriteLine("ERFOLG! ArtikelAnzahl ist" + anzahl);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("FEHLER!\n" + ex.Message);
            }

            Debug.Unindent();
            Debug.WriteLine("-- ENDE : ZAEHLE ALLE ARTIKEL ----------------------------------------");
            return anzahl;
        }
        #endregion

        #region ZaehleAlleArtikel : Suchbegriff
        /// <summary>
        /// Gibt die Anzahl der Artikel zu einem Suchbegriff aus der Datenbank zurück.
        /// </summary>
        /// <param name="suchbegriff"></param>
        /// <returns></returns>
        public static int ZaehleAlleArtikel(string suchbegriff)
        {
            int anzahl = -1;
            Debug.WriteLine("-- START : ZAEHLE ALLE ARTIKEL ----------------------------------------");
            Debug.Indent();
            try
            {
                using (var kontext = new BricoMarcheDBObjekte())
                {
                    anzahl = kontext.AlleArtikel.Include("EineKategorie").Where(x => x.Aktiv)
                                                          .Where(x => x.Bezeichnung.ToLower().Contains(suchbegriff.ToLower()) ||
                                                                 x.Beschreibung.ToLower().Contains(suchbegriff.ToLower())).Count();
                    Debug.WriteLine("ERFOLG! ArtikelAnzahl ist" + anzahl);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("FEHLER!\n" + ex.Message);
            }

            Debug.Unindent();
            Debug.WriteLine("-- ENDE : ZAEHLE ALLE ARTIKEL ----------------------------------------");
            return anzahl;
        }
        #endregion

        #region SpeichereArtikel
        public static int SpeichereArtikel()
        {
            int id = -1;


            return id;
        }

        #endregion

        #region SpeichereArtikel : ID
        public static bool SpeichereArtikel(int id, string bezeichnung, string beschreibung, decimal preis)
        {
            bool erfolgt = false;
            Debug.WriteLine("-- START: SPEICHERE ARTIKEL  ----------------------------------------------------");
            Debug.Indent();

            using (var kontext = new BricoMarcheDBObjekte())
            {
                try
                {
                    BL_BricoMarche.Artikel geladenerArtikel = kontext.AlleArtikel.Where(x => x.ID == id).SingleOrDefault();

                    geladenerArtikel.Bezeichnung = bezeichnung;
                    geladenerArtikel.Beschreibung = beschreibung;
                    geladenerArtikel.Preis = preis;

                    int anzahlBetroffeneZeilen = kontext.SaveChanges();
                    erfolgt = anzahlBetroffeneZeilen == 1;
                    Debug.WriteLine(anzahlBetroffeneZeilen + " Artikel gespeichert!");
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("FEHLER! \n" + ex.Message);
                    Debugger.Break();
                }
            }
            Debug.Unindent();
            Debug.WriteLine("-- ENDE: SPEICHERE ARTIKEL  -----------------------------------------------------");
            return erfolgt;
        }

        #endregion

    }

    public class Video {

        #region LadeAlleVideos
        /// <summary>
        /// Holt alle Videos aus der Datenbank.
        /// </summary>
        /// <returns>Liste geladener Videos</returns>
        public static List<BL_BricoMarche.Video> LadeAlleVideos()
        {
            List<BL_BricoMarche.Video> geladeneVideos = null;
            Debug.WriteLine("-- START : LADE  VIDEO -------------------------------------------------------");
            Debug.Indent();
            try
            {
                using (var kontext = new BricoMarcheDBObjekte())
                {
                    geladeneVideos = kontext.AlleVideos.Include("EineKategorie").Include("VieleSchlagwoerter").Where(x => x.Aktiv).ToList();
                }
                if (geladeneVideos == null)
                {
                    throw new Exception("Fehler! kein Video geladen!");
                }
                Debug.WriteLine("ERFOLG!");
            }
            catch (Exception ex)
            {
                Debug.WriteLine("FEHLER! " + ex.Message);
            }
            Debug.Unindent();
            Debug.WriteLine("-- ENDE : LADE VIDEO -------------------------------------------------------");
            return geladeneVideos;
        }
        #endregion

        #region LadeAlleVideos : Seite : Anzahl
        /// <summary>
        /// Holt eine Anzahl Videos auf einer bestimmten Seite aus der Datenbank.
        /// </summary>
        /// <param name="seite"></param>
        /// <param name="anzahl"></param>
        /// <returns></returns>
        public static List<BL_BricoMarche.Video> LadeAlleVideos(int seite, int anzahl)
        {
            List<BL_BricoMarche.Video> geladeneVideos = null;
            Debug.WriteLine("-- START : LADE VIDEO -------------------------------------------------------");
            Debug.Indent();
            try
            {
                using (var kontext = new BricoMarcheDBObjekte())
                {
                    geladeneVideos = kontext.AlleVideos.Include("EineKategorie").Include("VieleSchlagwoerter").Where(x => x.Aktiv).OrderByDescending(x => x.ID).Skip((seite - 1) * anzahl).Take(anzahl).ToList();
                }
                if (geladeneVideos == null)
                {
                    throw new Exception("Fehler! 0 Video geladen!");
                }
                Debug.WriteLine("ERFOLG!");
            }
            catch (Exception ex)
            {
                Debug.WriteLine("FEHLER! " + ex.Message);
            }
            Debug.Unindent();
            Debug.WriteLine("-- ENDE : LADE VIDEO -------------------------------------------------------");
            return geladeneVideos;
        }
        #endregion

        #region LadeAlleVideos : Kategorie
        /// <summary>
        /// Holt alle Videos aus einer bestimmten Kategorie aus der Datenbank.
        /// </summary>
        /// <param name="kategorie"></param>
        /// <returns>Liste geladener Videos</returns>
        public static List<BL_BricoMarche.Video> LadeAlleVideos(int kategorieID)
        {
            List<BL_BricoMarche.Video> geladeneVideos = null;
            Debug.WriteLine("-- START : LADE VIDEO -------------------------------------------------------");
            Debug.Indent();
            Debug.WriteLine("-- KATEGORIE : " + kategorieID);
            try
            {
                using (var kontext = new BricoMarcheDBObjekte())
                {
                    geladeneVideos = kontext.AlleVideos.Include("EineKategorie").Include("VieleSchlagwoerter").Where(x => x.Aktiv).Where(x => x.Kategorie_ID == kategorieID).ToList();
                }
                if (geladeneVideos == null)
                {
                    throw new Exception("Fehler! 0 Video geladen!");
                }
                Debug.WriteLine("ERFOLG!");
            }
            catch (Exception ex)
            {
                Debug.WriteLine("FEHLER! " + ex.Message);
            }


            Debug.Unindent();
            Debug.WriteLine("-- ENDE : LADE Video -------------------------------------------------------");
            return geladeneVideos;
        }
        #endregion

        #region LadeAlleVideos : Kategorie : Seite : Anzahl
        /// <summary>
        /// Holt eine Anzahl an Videos einer bestimmten Kategorie auf einer bestimmten Seite aus der Datenbank.
        /// </summary>
        /// <param name="kategorieID"></param>
        /// <param name="seite"></param>
        /// <param name="anzahl"></param>
        /// <returns>Liste geladener Videos</returns>
        public static List<BL_BricoMarche.Video> LadeAlleVideos(int kategorieID, int seite, int anzahl)
        {
            List<BL_BricoMarche.Video> geladeneVideos = null;
            Debug.WriteLine("-- START : LADE VIDEO -------------------------------------------------------");
            Debug.Indent();
            Debug.WriteLine("-- KATEGORIE : " + kategorieID);
            try
            {
                using (var kontext = new BricoMarcheDBObjekte())
                {
                    geladeneVideos = kontext.AlleVideos.Include("EineKategorie").Include("VieleSchlagwoerter").Where(x => x.Aktiv).Where(x => x.Kategorie_ID == kategorieID).OrderByDescending(x => x.ID).Skip((seite - 1) * anzahl).Take(anzahl).ToList();
                }
                if (geladeneVideos == null)
                {
                    throw new Exception("Fehler! 0 Video geladen!");
                }
                Debug.WriteLine("ERFOLG!");
            }
            catch (Exception ex)
            {
                Debug.WriteLine("FEHLER! " + ex.Message);
            }


            Debug.Unindent();
            Debug.WriteLine("-- ENDE : LADE VIDEO -------------------------------------------------------");
            return geladeneVideos;
        }
        #endregion

        #region LadeVideo : ID
        /// <summary>
        /// Holt ein bestimmtes Video aus der Datenbank
        /// </summary>
        /// <param name="ID"></param>
        /// <returns>Video</returns>
        public static BL_BricoMarche.Video LadeVideo(int ID)
        {
            BL_BricoMarche.Video geladeneVideos = null;
            Debug.WriteLine("-- START : LADE VIDEO -------------------------------------------------------------");
            Debug.Indent();
            try
            {
                using (var kontext = new BricoMarcheDBObjekte())
                {
                    geladeneVideos = kontext.AlleVideos.Include("VerlinkteArtikel").Include("EineKategorie").Where(x => x.Aktiv).Where(x => x.ID == ID).SingleOrDefault();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Fehler! \n" + ex.Message);
            }
            Debug.Unindent();
            Debug.WriteLine("-- ENDE : LADE VIDEO -------------------------------------------------------------");

            return geladeneVideos;
        }
        #endregion

        #region LadeAlleVideos : Schlagwort
        /// <summary>
        /// Holt alle Videos, dessen Bezeichnung oder Beschreibung den Schlagwort enthält aus der Datenbank.
        /// </summary>
        /// <param name="schlagwort"></param>
        /// <returns>Eine Liste von Videos</returns>
        public static List<BL_BricoMarche.Video> LadeAlleVideos(string schlagwort)
        {
            List<BL_BricoMarche.Video> geladeneVideos = null;
            Debug.WriteLine("-- START : LADE VIDEOS -------------------------------------------------------------");
            Debug.Indent();
            try
            {
                using (var kontext = new BricoMarcheDBObjekte())
                {
                    // Hole alle Videos, die dem gesuchtem Schlagwort zugeordnet sind aus DB.
                     geladeneVideos = kontext.AlleSchlagwoerter.Include("VieleVideos") // Alle Schlagwörter verknüpft mit Videos aus DB holen.
                                                            .Where(x => x.Bezeichnung.ToLower().Equals(schlagwort.ToLower())) // auf übergebenes Schlagwort reduzieren.
                                                            .SelectMany(x => x.VieleVideos).ToList(); // Videos schnappen, die mit gefundenem Schlagwort verknüpft sind.

                    if (geladeneVideos == null)
                    {
                        throw new Exception("Fehler! keine Video aus der Datenbank geladen");
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Fehler! \n" + ex.Message);
            }
            Debug.Unindent();
            Debug.WriteLine("-- ENDE : LADE VIDEOS -------------------------------------------------------------");

            return geladeneVideos;
        }
        #endregion

        #region LadeAlleVideos : Schlagwort : Seite : Anzahl
        /// <summary>
        /// Holt eine Anzahl an Videos einer Seite, die einen bestimmten Schlagwort enthalten, aus der Datenbank.
        /// </summary>
        /// <param name="schlagwort"></param>
        /// <param name="seite"></param>
        /// <param name="anzahl"></param>
        /// <returns>Liste geladener Videos</returns>
        public static List<BL_BricoMarche.Video> LadeAlleVideo(string schlagwort, int seite, int anzahl)
        {
            List<BL_BricoMarche.Video> geladeneVideos = null;
            Debug.WriteLine("-- START : LADE VIDEO -------------------------------------------------------------");
            Debug.Indent();
            try
            {
                using (var kontext = new BricoMarcheDBObjekte())
                {
                    geladeneVideos = kontext.AlleSchlagwoerter.Include("VieleVideos") // Alle Schlagwörter verknüpft mit Videos aus DB holen.
                                                              .Where(x => x.Bezeichnung.ToLower().Equals(schlagwort.ToLower())) // auf übergebenes Schlagwort reduzieren.
                                                              .SelectMany(x => x.VieleVideos).ToList() // Videos schnappen, die mit gefundenem Schlagwort verknüpft sind.
                                                              .OrderByDescending(x => x.ID).Skip((seite - 1) * anzahl).Take(anzahl).ToList(); // Sortieren & auf Auswahl reduzieren.
                    if (geladeneVideos == null)
                    {
                        throw new Exception("Fehler! kein Video aus der Datenbank geladen");
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Fehler! \n" + ex.Message);
            }
            Debug.Unindent();
            Debug.WriteLine("-- ENDE : LADE VIDEO -------------------------------------------------------------");

            return geladeneVideos;
        }
        #endregion

        #region LadeGemerkteVideos : benutzerName
        public static List<BL_BricoMarche.Video> LadeGemerkteVideos(string benutzerName)
        {
            List<BL_BricoMarche.Video> gemerkteVideos = null;
            Debug.WriteLine("-- START : LADE GEMEKRTE VIDEOS -------------------------------------------------------------");
            Debug.Indent();
            try
            {
                using (var kontext = new BricoMarcheDBObjekte())
                {
                    gemerkteVideos = kontext.AlleBenutzer.Include("GemerkteVideos").Where(x => x.Benutzername == benutzerName).SingleOrDefault().GemerkteVideos.ToList();

                    if (gemerkteVideos == null)
                    {
                        throw new Exception("Fehler! keine Video aus der Datenbank geladen");
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Fehler! \n" + ex.Message);
            }
            Debug.Unindent();
            Debug.WriteLine("-- ENDE : LADE GEMEKRTE VIDEOS -------------------------------------------------------------");
            return gemerkteVideos;

        }
        #endregion

        #region LadeVideoBild: ID
        /// <summary>
        /// Holt das Bild eines bestimmten Videos aus der Datenbank.
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static byte[] LadeVideoBild(int ID)
        {
            byte[] geladenesBild = null;
            Debug.WriteLine("-- START : LADE VIDEO BILD -------------------------------------------------------------");
            Debug.Indent();
            try
            {
                using (var kontext = new BricoMarcheDBObjekte())
                {
                    geladenesBild = kontext.AlleVideos.Where(x => x.Aktiv).Where(x => x.ID == ID).Single().Bild;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Fehler! \n" + ex.Message);
            }
            Debug.Unindent();
            Debug.WriteLine("-- ENDE : LADE VIDEO BILD -------------------------------------------------------------");


            return geladenesBild;
        }
        #endregion

        #region ZaehleAlleVideos
        /// <summary>
        /// Gibt die Anzahl aller aktiven Videos in der Datenbank zurück.
        /// </summary>
        /// <returns>Anzahl</returns>
        public static int ZaehleAlleVideos()
        {
            int anzahl = -1;
            Debug.WriteLine("-- START : ZAEHLE ALLE VIDEOS ----------------------------------------");
            Debug.Indent();
            try
            {
                using (var kontext = new BricoMarcheDBObjekte())
                {
                    anzahl = kontext.AlleVideos.Where(x => x.Aktiv).Count();
                    Debug.WriteLine("ERFOLG! VideoAnzahl ist" + anzahl);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("FEHLER!\n" + ex.Message);
            }

            Debug.Unindent();
            Debug.WriteLine("-- ENDE : ZAEHLE ALLE VIDEOS ----------------------------------------");
            return anzahl;
        }
        #endregion

        #region ZaehleAlleVideos : Kategorie
        /// <summary>
        /// Gibt die Anzahl aller aktiven Videos einer bestimmten Kategorie in der Datenbank zurück.
        /// </summary>
        /// <param name="kategorieID"></param>
        /// <returns>Anzahl</returns>
        public static int ZaehleAlleVideos(int kategorieID)
        {
            int anzahl = -1;
            Debug.WriteLine("-- START : ZAEHLE ALLE VIDEOS ----------------------------------------");
            Debug.Indent();
            try
            {
                using (var kontext = new BricoMarcheDBObjekte())
                {
                    anzahl = kontext.AlleVideos.Where(x => x.Aktiv).Where(x => x.Kategorie_ID == kategorieID).Count();
                    Debug.WriteLine("ERFOLG! VideoAnzahl ist" + anzahl);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("FEHLER!\n" + ex.Message);
            }

            Debug.Unindent();
            Debug.WriteLine("-- ENDE : ZAEHLE ALLE VIDEOS ----------------------------------------");
            return anzahl;
        }
        #endregion

        #region ZaehleAlleVideos : Schlagwort
        /// <summary>
        /// Gibt die Anzahl der Videos zu einem Suchbegriff aus der Datenbank zurück.
        /// </summary>
        /// <param name="schlagwort"></param>
        /// <returns>Anzahl</returns>
        public static int ZaehleAlleVideo(string schlagwort)
        {
            int anzahl = -1;
            Debug.WriteLine("-- START : ZAEHLE ALLE VIDEOS ----------------------------------------");
            Debug.Indent();
            try
            {
                using (var kontext = new BricoMarcheDBObjekte())
                {
                    anzahl = kontext.AlleVideos.Include("EineKategorie").Include("VieleSchlagwoerter").Where(x => x.Aktiv)
                                                          .Where(x => x.Bezeichnung.ToLower().Contains(schlagwort.ToLower()) ||
                                                                 x.Beschreibung.ToLower().Contains(schlagwort.ToLower())).Count();
                    Debug.WriteLine("ERFOLG! VideoAnzahl ist" + anzahl);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("FEHLER!\n" + ex.Message);
            }

            Debug.Unindent();
            Debug.WriteLine("-- ENDE : ZAEHLE ALLE VIDEOS ----------------------------------------");
            return anzahl;
        }
        #endregion

        #region WirdGemerkt : ID : BenutzerName
        /// <summary>
        /// Fragt ab ob ein bestimmtes Video auf der Merkliste eines bestimmten Benutzer steht.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="benutzerName"></param>
        /// <returns>true; false</returns>
        public static bool WirdGemerkt(int id, string benutzerName)
        {
            bool gemerkt = false;
            Debug.WriteLine("-- START : VIDEO WIRD GEMERKT? -------------------------------------------------------------");
            Debug.Indent();
            try
            {
                using (var kontext = new BricoMarcheDBObjekte())
                {
                    gemerkt = kontext.AlleBenutzer.Include("GemerkteVideos").Where(x => x.Benutzername == benutzerName)
                                    .SingleOrDefault()
                                    .GemerkteVideos.Contains(kontext.AlleVideos.Where(x => x.ID == id)
                                    .SingleOrDefault());
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Fehler! \n" + ex.Message);
            }
            Debug.Unindent();
            Debug.WriteLine("-- ENDE : VIDEO WIRD GEMERKT? -------------------------------------------------------------");


            return gemerkt;
        }
        #endregion
    }
}

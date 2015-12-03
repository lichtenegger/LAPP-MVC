using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL_BricoMarche.DatenVerwaltung
{
    public class Inhalt
    {
        #region LadeAlleArtikel
        /// <summary>
        /// Holt alle Artikel aus der Datenbank.
        /// </summary>
        /// <returns>geladene Artikel</returns>
        public static List<Artikel> LadeAlleArtikel()
        {
            List<Artikel> geladeneArtikel = null;
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
        public static List<Artikel> LadeAlleArtikel(int seite, int anzahl)
        {
            List<Artikel> geladeneArtikel = null;
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
        public static List<Artikel> LadeAlleArtikel(int kategorieID)
        {
            List<Artikel> geladeneArtikel = null;
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
        public static List<Artikel> LadeAlleArtikel(int kategorieID, int seite, int anzahl)
        {
            List<Artikel> geladeneArtikel = null;
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
        public static Artikel LadeArtikel(int ID)
        {
            Artikel geladenerArtikel = null;
            Debug.WriteLine("-- START : LADE ARTIKEL -------------------------------------------------------------");
            Debug.Indent();
            try
            {
                using (var kontext = new BricoMarcheDBObjekte())
                {
                    geladenerArtikel = kontext.AlleArtikel.Where(x => x.ID == ID).Single();
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

        #region LadeArtikelBild: ID
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
                    geladenesBild = kontext.AlleArtikel.Where(x => x.ID == ID).Single().Bild;
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

        #region LadeAlleKategorien
        /// <summary>
        /// Holt alle Kategorien aus der Datenbank.
        /// </summary>
        /// <returns>Alle Kategorien</returns>
        public static List<Kategorie> LadeAlleKategorien()
        {
            Debug.WriteLine("-- START : LADE ALLE KATEGORIEN ------------------------");
            Debug.Indent();
            List<Kategorie> alleKategorien = null;
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

        #region ZaehleAlleArtikel
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

    }
}

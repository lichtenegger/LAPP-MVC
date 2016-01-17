using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL_BricoMarche.DatenVerwaltung
{
    public static class Benutzer
    {
        #region LadeAlleBenutzer
        public static List<BL_BricoMarche.Benutzer> LadeAlleBenutzer()
        {
            List<BL_BricoMarche.Benutzer> geladeneBenutzer = null;
            Debug.WriteLine("-- START: LADE ALLE BENUTZER ----------------------------------------------------");
            Debug.Indent();

            using (var kontext = new BricoMarcheDBObjekte())
            {
                try
                {
                    geladeneBenutzer = kontext.AlleBenutzer.ToList();

                    Debug.WriteLine("Erfolg! " + geladeneBenutzer.Count() + " Benutzer geladen!");
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("FEHLER! \n" + ex.Message);
                    Debugger.Break();
                }
            }
            Debug.Unindent();
            Debug.WriteLine("-- ENDE: LADE ALLE BENUTZER  -----------------------------------------------------");
            return geladeneBenutzer;
        }
        #endregion

        #region SindAnmeldeDatenRichtig
        public static bool SindAnmeldeDatenRichtig(string benutzerName, string passwort)
        {
            return Sicherheit.IstPasswortRichtig(benutzerName, passwort);
        }
        #endregion SindAnmeldeDatenRichtig

        #region IstBenutzernameFrei
        public static bool IstBenutzernameFrei(string benutzerName)
        {
            bool istFrei = false;
            Debug.WriteLine("-- START: IST BENUTZERNAME FREI ---------------------------------------------------");
            Debug.Indent();
            using (var kontext = new BricoMarcheDBObjekte())
            {
                try
                {
                    istFrei = !kontext.AlleBenutzer.Select(x => x.Benutzername).ToList().Contains(benutzerName);
                    Debug.WriteLine("ERFOLG! Benutzername ist " + (istFrei ? "frei" : "vergeben"));
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("FEHLER! \n" + ex.Message);
                    Debugger.Break();
                }
            }
            Debug.Unindent();
            Debug.WriteLine("-- ENDE: IST BENUTZERNAME FREI  ----------------------------------------------------");


            return istFrei;
        }

        #endregion


        #region IstBenutzerAdministrator
        public static bool IstBenutzerAdministrator(string benutzerName)
        {
            bool istAdmin = false;
            Debug.WriteLine("-- START: IST BENUTZER ADMIN ---------------------------------------------------");
            Debug.Indent();
            using (var kontext = new BricoMarcheDBObjekte())
            {
                try
                {
                    istAdmin = kontext.AlleBenutzer.Include("EineGruppe").Where(x => x.Benutzername == benutzerName).SingleOrDefault().EineGruppe.Bezeichnung == "Administrator";
                    Debug.WriteLine("ERFOLG!");
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("FEHLER! \n" + ex.Message);
                    Debugger.Break();
                }
            }
            Debug.Unindent();
            Debug.WriteLine("-- ENDE: IST BENUTZER ADMIN  ----------------------------------------------------");


            return istAdmin;
        }

        #endregion

        #region RegistriereBenutzer
        /// <summary>
        /// Übergibt ein BenutzerObjekt an die Sicherheitsschicht zum Registrieren
        /// </summary>
        /// <param name="benutzerName"></param>
        /// <param name="passwort"></param>
        /// <param name="geburtsDatum"></param>
        /// <param name="vorname"></param>
        /// <param name="nachname"></param>
        /// <param name="adresse"></param>
        /// <param name="ortId"></param>
        /// <returns>true, false</returns>
        public static bool RegistriereBenutzer(string benutzerName, string passwort, DateTime geburtsDatum, string vorname, string nachname, string adresse, int ortId)
        {
            BL_BricoMarche.Benutzer neuerBenutzer = new BL_BricoMarche.Benutzer()
            {
                Benutzername = benutzerName,
                Geburtsdatum = geburtsDatum,
                Vorname = vorname,
                Nachname = nachname,
                Adresse = adresse,
                Ort_ID = ortId,
                Gruppe_ID = 1,
                Aktiv = true,
            };
            return Sicherheit.Registrierung(neuerBenutzer, passwort);
        }
        #endregion RegistriereBenutzer

        #region EditiereBenutzer
        public static bool EditiereBenutzer(string benutzerName, string altesPasswort, string neuesPasswort, DateTime geburtsDatum, string vorname, string nachname, string adresse, int ortId, bool aktiv)
        {
            BL_BricoMarche.Benutzer editierterBenutzer = new BL_BricoMarche.Benutzer() {
                Benutzername = benutzerName,
                Vorname = vorname,
                Nachname = nachname,
                Geburtsdatum = geburtsDatum,
                Adresse = adresse,
                Ort_ID = ortId,
                Aktiv = aktiv
            };
 
            return Sicherheit.Editieren(editierterBenutzer, altesPasswort, neuesPasswort);

        }
        #endregion

        #region LadeBenutzerProfil
        public static BL_BricoMarche.Benutzer LadeBenutzerProfil(string benutzerName)
        {
            return Sicherheit.HoleBenutzerDaten(benutzerName);
        }
        #endregion

        #region MerkeArtikel
        public static bool MerkeArtikel(int id, string benutzerName)
        {
            bool erfolgt = false;
            Debug.WriteLine("-- START: MEKRE ARTIKEL ----------------------------------------------------");
            Debug.Indent();

            using (var kontext = new BricoMarcheDBObjekte())
            {
                try
                {
                    BL_BricoMarche.Benutzer aktuellerBenutzer = kontext.AlleBenutzer.Where(x => x.Benutzername == benutzerName).SingleOrDefault();

                    aktuellerBenutzer.GemerkteArtikel.Add(kontext.AlleArtikel.Where(x => x.ID == id).SingleOrDefault());

                    int anzahlBetroffeneZeilen = kontext.SaveChanges();
                    erfolgt = anzahlBetroffeneZeilen == 1;
                    Debug.WriteLine(anzahlBetroffeneZeilen + " Artikel gemerkt!");
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("FEHLER! \n" + ex.Message);
                    Debugger.Break();
                }
            }
            Debug.Unindent();
            Debug.WriteLine("-- ENDE: MEKRE ARTIKEL -----------------------------------------------------");
            return erfolgt;

        }
        #endregion

        #region VergissArtikel
        public static bool VergissArtikel(int id, string benutzerName)
        {
            bool erfolgt = false;
            Debug.WriteLine("-- START: VERGISS ARTIKEL ----------------------------------------------------");
            Debug.Indent();

            using (var kontext = new BricoMarcheDBObjekte())
            {
                try
                {
                    BL_BricoMarche.Benutzer aktuellerBenutzer = kontext.AlleBenutzer.Where(x => x.Benutzername == benutzerName).SingleOrDefault();

                    aktuellerBenutzer.GemerkteArtikel.Remove(kontext.AlleArtikel.Where(x => x.ID == id).SingleOrDefault());

                    int anzahlBetroffeneZeilen = kontext.SaveChanges();
                    erfolgt = anzahlBetroffeneZeilen == 1;
                    Debug.WriteLine(anzahlBetroffeneZeilen + " Artikel vergessen!");
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("FEHLER! \n" + ex.Message);
                    Debugger.Break();
                }
            }
            Debug.Unindent();
            Debug.WriteLine("-- ENDE: VERGISS ARTIKEL  -----------------------------------------------------");
            return erfolgt;

        }
        #endregion

        #region MerkeVideo
        public static bool MerkeVideo(int id, string benutzerName)
        {
            bool erfolgt = false;
            Debug.WriteLine("-- START: MEKRE VIDEO ----------------------------------------------------");
            Debug.Indent();

            using (var kontext = new BricoMarcheDBObjekte())
            {
                try
                {
                    BL_BricoMarche.Benutzer aktuellerBenutzer = kontext.AlleBenutzer.Where(x => x.Benutzername == benutzerName).SingleOrDefault();

                    aktuellerBenutzer.GemerkteVideos.Add(kontext.AlleVideos.Where(x => x.ID == id).SingleOrDefault());

                    int anzahlBetroffeneZeilen = kontext.SaveChanges();
                    erfolgt = anzahlBetroffeneZeilen == 1;
                    Debug.WriteLine(anzahlBetroffeneZeilen + " Video gemerkt!");
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("FEHLER! \n" + ex.Message);
                    Debugger.Break();
                }
            }
            Debug.Unindent();
            Debug.WriteLine("-- ENDE: MEKRE VIDEO  -----------------------------------------------------");
            return erfolgt;

        }
        #endregion

        #region VergissVideo
        public static bool VergissVideo(int id, string benutzerName)
        {
            bool erfolgt = false;
            Debug.WriteLine("-- START: VERGISS VIDEO ----------------------------------------------------");
            Debug.Indent();

            using (var kontext = new BricoMarcheDBObjekte())
            {
                try
                {
                    BL_BricoMarche.Benutzer aktuellerBenutzer = kontext.AlleBenutzer.Where(x => x.Benutzername == benutzerName).SingleOrDefault();

                    aktuellerBenutzer.GemerkteVideos.Remove(kontext.AlleVideos.Where(x => x.ID == id).SingleOrDefault());

                    int anzahlBetroffeneZeilen = kontext.SaveChanges();
                    erfolgt = anzahlBetroffeneZeilen == 1;
                    Debug.WriteLine(anzahlBetroffeneZeilen + " Video vergessen!");
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("FEHLER! \n" + ex.Message);
                    Debugger.Break();
                }
            }
            Debug.Unindent();
            Debug.WriteLine("-- ENDE: VERGISS VIDEO  -----------------------------------------------------");
            return erfolgt;

        }
        #endregion

        #region Sicherheit
        #region summary
        /// <summary>
        /// Diese Klasse ist nur innerhalb der Benutzerklasse verwendbar & 
        /// soll von außen weder ersichtlich noch angreifbar sein.
        /// </summary>
        #endregion summary
        private class Sicherheit
        {
            #region Editieren
            /// <summary>
            /// Holt sich den zu ändernden Benutzer aus der Datenbank, verändert seine Daten & speichert diesen wieder in der DB.
            /// Wird ein neues Passwort übergeben, wird erst das alte mit der Datenbank abgeglichen bevor das neue als Paswort-Hash im Benutzer gespeichert wird.
            /// </summary>
            /// <param name="editierterBenutzer"></param>
            /// <param name="altesPasswort"></param>
            /// <param name="neuesPasswort"></param>
            /// <returns>true, false</returns>
            public static bool Editieren(BL_BricoMarche.Benutzer editierterBenutzer, string altesPasswort, string neuesPasswort)
            {
                bool erfolgt = false;
                Debug.WriteLine("-- START: EDITIEREN ----------------------------------------------------");
                Debug.Indent();

                using (var kontext = new BricoMarcheDBObjekte())
                {
                    try
                    {
                        int id = HoleBenutzerID(editierterBenutzer.Benutzername);
                        BL_BricoMarche.Benutzer aktuellerBenutzer = kontext.AlleBenutzer.Where(x => x.ID == id).Single();

                        if ((!string.IsNullOrEmpty(altesPasswort) && !string.IsNullOrEmpty(neuesPasswort)) && 
                            ErmittleHashWert(altesPasswort).SequenceEqual(aktuellerBenutzer.Passwort))
                        {
                            aktuellerBenutzer.Passwort = ErmittleHashWert(neuesPasswort);
                        }
                        aktuellerBenutzer.Vorname = editierterBenutzer.Vorname;
                        aktuellerBenutzer.Nachname = editierterBenutzer.Nachname;
                        aktuellerBenutzer.Adresse = editierterBenutzer.Adresse;
                        aktuellerBenutzer.Geburtsdatum = editierterBenutzer.Geburtsdatum;
                        aktuellerBenutzer.Ort_ID = editierterBenutzer.Ort_ID;
                        aktuellerBenutzer.Aktiv = editierterBenutzer.Aktiv;

                        int anzahlBetroffeneZeilen = kontext.SaveChanges();
                        erfolgt = anzahlBetroffeneZeilen == 1;
                        Debug.WriteLine(anzahlBetroffeneZeilen + " Benutzer gespeichert!");
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine("FEHLER! \n" + ex.Message);
                        Debugger.Break();
                    }
                }
                Debug.Unindent();
                Debug.WriteLine("-- ENDE: EDITIEREN -----------------------------------------------------");
                return erfolgt;
            }
            #endregion Editieren

            #region Registrierung
            /// <summary>
            /// Speichert neuen Benutzer mit passwort-hash in DB. Gibt retour ob erfolgt oder nicht.
            /// </summary>
            /// <param name="neuerBenutzer"></param>
            /// <param name="passwort"></param>
            /// <returns>true, false</returns>
            public static bool Registrierung(BL_BricoMarche.Benutzer neuerBenutzer, string passwort)
            {
                bool erfolgt = false;
                Debug.WriteLine("-- START: REGISTRIERUNG ----------------------------------------------------");
                Debug.Indent();
                try
                {
                    using (var kontext = new BricoMarcheDBObjekte())
                    {

                        neuerBenutzer.Passwort = ErmittleHashWert(passwort);

                        kontext.AlleBenutzer.Add(neuerBenutzer);
                        int anzahlBetroffeneZeilen = kontext.SaveChanges();

                        erfolgt = anzahlBetroffeneZeilen == 1;
                        Debug.WriteLine("ERFOLG! " + anzahlBetroffeneZeilen + " Benutzer gespeichert!");
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("FEHLER! \n" + ex.Message);
                    Debugger.Break();
                }
                Debug.Unindent();
                Debug.WriteLine("-- ENDE: REGISTRIERUNG -----------------------------------------------------");
                return erfolgt;
            }
            #endregion Registrierung

            #region IstPasswortRichtig
            /// <summary>
            /// Eingegebenes Passwort wird via Funktion ErmittleHashWert verfremdet &
            /// mit dem verfremdeten Passwort aus der Datenbank verglichen.
            /// </summary>
            /// <param name="benutzerID">ID des Benutzers</param>
            /// <param name="passwort">Vom Benutzer eingegebenes Passwort</param>
            /// <returns>true, false</returns>
            public static bool IstPasswortRichtig(int benutzerID, string passwort)
            {
                bool richtig = false;
                Debug.WriteLine("-- START: IST PASSWORT RICHTIG ---------------------------------------------------");
                Debug.Indent();
                using (var kontext = new BricoMarcheDBObjekte())
                {
                    try
                    {
                        byte[] eingegeben = ErmittleHashWert(passwort);
                        byte[] gespeichert = kontext.AlleBenutzer.Where(x => x.ID == benutzerID).Single().Passwort;

                        richtig = gespeichert.SequenceEqual(eingegeben);
                        Debug.WriteLine("ERFOLG!");
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine("FEHLER! \n" + ex.Message);
                        Debugger.Break();
                    }
                }
                Debug.Unindent();
                Debug.WriteLine("-- ENDE: IST PASSWORT RICHTIG ----------------------------------------------------");
                return richtig;
            }

            /// <summary>
            /// Ruft IstPaswortRichtig auf & übergibt benutzerID via HoleBenuterID
            /// </summary>
            /// <param name="benutzerName"></param>
            /// <param name="passwort"></param>
            /// <returns>true, false</returns>
            public static bool IstPasswortRichtig(string benutzerName, string passwort)
            {
                return IstPasswortRichtig(HoleBenutzerID(benutzerName), passwort);
            }
            #endregion IstPasswortRichtig

            #region HoleBenutzerDaten
            public static BL_BricoMarche.Benutzer HoleBenutzerDaten(string benutzerName)
            {
                BL_BricoMarche.Benutzer benutzer = null;
                Debug.WriteLine("-- START : HOLE BENUTZERDATEN ---------------------------");
                Debug.Indent();
                try
                {
                    using (var kontext = new BricoMarcheDBObjekte())
                    {
                        benutzer = kontext.AlleBenutzer.Where(x => x.Benutzername == benutzerName).Single();
                        if (benutzer != null)
                        {
                            Debug.WriteLine("Erfolg!");
                        }
                        else
                        {
                            throw new Exception("Fehler! 0 Benutzer geladen.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("FEHLER!\n" + ex.Message);
                }

                Debug.Unindent();
                Debug.WriteLine("-- ENDE : HOLE BENUTZERDATEN ---------------------------");
                return benutzer;
            }
            #endregion

            #region HoleBenutzerID
            /// <summary>
            /// Vergleicht den BenutzerNamen mit den BenutzerEinträgen in der Datenbank &
            /// gibt die dazugehörige BenutzerID zurück oder -1, wenn kein Benutzer gefunden wurde.
            /// </summary>
            /// <param name="benutzerName"></param>
            /// <returns>benutzerID, -1</returns>
            private static int HoleBenutzerID(string benutzerName)
            {
                int id = -1;
                Debug.WriteLine("-- START: HOLE BENUTZER ID ---------------------------------------------------");
                Debug.Indent();
                using (var kontext = new BricoMarcheDBObjekte())
                {
                    try
                    {
                        id = kontext.AlleBenutzer.Where(x => x.Benutzername == benutzerName).Single().ID;
                        Debug.WriteLine("ERFOLG!");
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine("FEHLER! \n" + ex.Message);
                        Debugger.Break();
                    }
                }
                Debug.Unindent();
                Debug.WriteLine("-- ENDE: HOLE BENUTZER ID ----------------------------------------------------");
                return id;
            }
            #endregion HoleBentzerID

            #region ErmittleHashWert
            /// <summary>
            /// Verfremdet einen String via SHA256 &
            /// gibt den HashWert zurück.
            /// </summary>
            /// <param name="text"></param>
            /// <returns>HashWert</returns>
            public static byte[] ErmittleHashWert(string text)
            {
                SHA256 algorithm = SHA256.Create();
                return algorithm.ComputeHash(UnicodeEncoding.UTF8.GetBytes(text));
            }
            #endregion ErmittleHashWert
        }
        #endregion

        #region PasswortReset
        public static bool PasswortReset(string benutzerName)
        {
            bool erfolgt = false;
            Debug.WriteLine("-- START: PASSWORT RESET ----------------------------------------------------");
            Debug.Indent();

            using (var kontext = new BricoMarcheDBObjekte())
            {
                try
                {
                    BL_BricoMarche.Benutzer geladenerBenutzer = kontext.AlleBenutzer.Where(x => x.Benutzername == benutzerName).SingleOrDefault();

                    geladenerBenutzer.Passwort = Sicherheit.ErmittleHashWert("123Passwort!");

                    int anzahlBetroffeneZeilen = kontext.SaveChanges();
                    erfolgt = anzahlBetroffeneZeilen == 1;
                    Debug.WriteLine(anzahlBetroffeneZeilen + " Video gemerkt!");
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("FEHLER! \n" + ex.Message);
                    Debugger.Break();
                }
            }
            Debug.Unindent();
            Debug.WriteLine("-- ENDE: PASSWORT RESET  -----------------------------------------------------");
            return erfolgt;
        }
        #endregion

        #region Orte
        public static class Orte
        {
            public static List<Ort> LadeAlleOrte()
            {
                List<Ort> orte = null;
                Debug.WriteLine("-- START: LADE ALLE ORTE ----------------------------------");
                Debug.Indent();

                using (var kontext = new BricoMarcheDBObjekte())
                {
                    try
                    {
                        orte = kontext.AlleOrte.Include("EinLand").OrderBy(x => x.PLZ).OrderBy(x => x.EinLand.Bezeichnung).ToList();
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine("FEHLER! \n" + ex.Message);
                        Debugger.Break();
                    }
                }

                Debug.Unindent();
                Debug.WriteLine("-- ENDE: LADE ALLE ORTE ----------------------------------");
                return orte;
            }
        }
        #endregion
    }
}

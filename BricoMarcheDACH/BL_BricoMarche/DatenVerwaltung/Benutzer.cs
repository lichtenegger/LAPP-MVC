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
        #region SindAnmeldeDatenRichtig
        public static bool SindAnmeldeDatenRichtig(string benutzerName, string passwort)
        {
            return Sicherheit.IstPasswortRichtig(benutzerName, passwort);
        }
        #endregion SindAnmeldeDatenRichtig

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
                Gruppe_ID = 1
            };
            return Sicherheit.Registrierung(neuerBenutzer, passwort);
        }
        #endregion RegistriereBenutzer

        public static bool EditiereBenutzer(string benutzerName, string altesPasswort, string neuesPasswort, DateTime geburtsDatum, string vorname, string nachname, string adresse, int ortId)
        {
            BL_BricoMarche.Benutzer editierterBenutzer = new BL_BricoMarche.Benutzer() {
                Benutzername = benutzerName,
                Vorname = vorname,
                Nachname = nachname,
                Geburtsdatum = geburtsDatum,
                Adresse = adresse,
                Ort_ID = ortId
            };
            return Sicherheit.Editieren(editierterBenutzer, altesPasswort, neuesPasswort);
        }

        public static BL_BricoMarche.Benutzer LadeBenutzerProfil(string benutzerName)
        {
            return Sicherheit.HoleBenutzerDaten(benutzerName);
        }

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
                ;
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
            private static byte[] ErmittleHashWert(string text)
            {
                SHA256 algorithm = SHA256.Create();
                return algorithm.ComputeHash(UnicodeEncoding.UTF8.GetBytes(text));
            }
            #endregion ErmittleHashWert
        }
        #endregion Sicherheit

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
    }
}

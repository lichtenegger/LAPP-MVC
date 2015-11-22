using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL_BricoMarche.DatenVerwaltung
{
    public class Benutzer
    {
        public static bool sindAnmeldeDatenRichtig(string benutzerName, string passwort)
        {
            return Sicherheit.IstPasswortRichtig(benutzerName, passwort);
        }

        #region Sicherheit
        /// <summary>
        /// Diese Klasse ist nur innerhalb der Benutzerklasse verwendbar & 
        /// soll von außen weder ersichtlich noch angreifbar sein.
        /// </summary>
        private class Sicherheit
        {
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
                        Debug.WriteLine("FEHLER! \n", ex.Message);
                    }
                }
                Debug.Unindent();
                Debug.WriteLine("-- ENDE: IST PASSWORT RICHTIG ----------------------------------------------------");
                Debugger.Break();
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
                        Debug.WriteLine("FEHLER! \n", ex.Message);
                    }
                }
                Debug.Unindent();
                Debug.WriteLine("-- ENDE: HOLE BENUTZER ID ----------------------------------------------------");
                Debugger.Break();
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
    }
}

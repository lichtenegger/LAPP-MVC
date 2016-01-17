using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static BL_BricoMarche.DatenVerwaltung.Benutzer;
using System.Diagnostics;

namespace UT_BricoMarche
{
    [TestClass]
    public class BenutzerTests
    {
        [TestMethod]
        public void BenutzerKannSichAnmelden()
        {
            Debug.WriteLine("TEST-EINGABE : richtige Benutzerdaten \n");
            Assert.IsTrue(SindAnmeldeDatenRichtig("test@mail.me", "test"));
            Debug.WriteLine("\nTEST-ERGEBNIS: Anmeldung ist true. \n\n");

            Debug.WriteLine("TEST-EINGABE : falsche Benutzerdaten \n");
            Assert.IsFalse(SindAnmeldeDatenRichtig("phalsh", "phalsh"));
            Debug.WriteLine("\nTEST-ERGEBNIS: Anmeldung ist false. \n");
        }

        [TestMethod]
        public void BenutzerKannSichRegistrieren()
        {
            Debug.WriteLine("TEST-EINGABE : richtige Benutzerdaten \n");
            Assert.IsTrue(RegistriereBenutzer("tester@regis.ter", "test", new DateTime(1917, 7, 1), "Testoro", "Testilli", "Am Testplatz 1", 1));
            Debug.WriteLine("\nTEST-ERGEBNIS: Registrieren ist true. \n\n");
        }

        [TestMethod]
        public void BenutzerKannSichEditieren()
        {
            Debug.WriteLine("TEST-EINGABE : neues Geburtsdatum, neue Adresse \n");
            Assert.IsTrue(EditiereBenutzer("tester@regis.ter", "", "", new DateTime(1971, 7, 1), "Testoro", "Testilli", "Beim Testplatz 1", 1, true));
            Debug.WriteLine("\nTEST-ERGEBNIS: Editieren ist true. \n\n");

            Debug.WriteLine("TEST-EINGABE : neues Passwort \n");
            Assert.IsTrue(EditiereBenutzer("tester@regis.ter", "test", "123test", new DateTime(1971, 7, 1), "Testoro", "Testilli", "Beim Testplatz 1", 1, true));
            Debug.WriteLine("\nTEST-ERGEBNIS: Editieren ist true. \n\n");

            Debug.WriteLine("TEST-EINGABE : Daten wieder zurücksetzen \n");
            Assert.IsTrue(EditiereBenutzer("tester@regis.ter", "123test", "test", new DateTime(1917, 7, 1), "Testoro", "Testilli", "Am Testplatz 1", 1, true));
            Debug.WriteLine("\nTEST-ERGEBNIS: Editieren ist true. \n\n");
        }

        [TestMethod]
        public void BenutzerProfilWirdGeladen()
        {
            Debug.WriteLine("TEST-EINGABE : richtiger BenutzerName \n");
            Assert.IsNotNull(LadeBenutzerProfil("test@mail.me"));
            Debug.WriteLine("\nTEST-ERGEBNIS: Benutzer geladen. \n\n");
        }
    }
}

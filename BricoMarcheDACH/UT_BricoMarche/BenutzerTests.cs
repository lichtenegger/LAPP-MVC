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
            Assert.IsTrue(sindAnmeldeDatenRichtig("test@mail.me", "test"));
            Debug.WriteLine("\nTEST-ERGEBNIS: Anmeldung ist true. \n\n");

            Debug.WriteLine("TEST-EINGABE : falsche Benutzerdaten \n");
            Assert.IsFalse(sindAnmeldeDatenRichtig("phalsh", "phalsh"));
            Debug.WriteLine("\nTEST-ERGEBNIS: Anmeldung ist false. \n");
        }
    }
}

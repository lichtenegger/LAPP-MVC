using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BL_BricoMarche.DatenVerwaltung;
using System.Diagnostics;

namespace UT_BricoMarche
{
    [TestClass]
    public class ZusatzTests
    {
        [TestMethod]
        public void OrteWerdenGeladen()
        {
            Assert.IsNotNull(Benutzer.Orte.LadeAlleOrte());
            Debug.WriteLine("\nTEST-ERGEBNIS: Orte geladen.");
        }
    }
}

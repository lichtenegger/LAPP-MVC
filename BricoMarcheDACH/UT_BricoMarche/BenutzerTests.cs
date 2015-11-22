using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static BL_BricoMarche.DatenVerwaltung.Benutzer;

namespace UT_BricoMarche
{
    [TestClass]
    public class BenutzerTests
    {


        [TestMethod]
        public void BenutzerKannSichAnmelden()
        {
            Assert.IsTrue(sindAnmeldeDatenRichtig("test@mail.me", "test"));
            Assert.IsFalse(sindAnmeldeDatenRichtig("phalsh", "phalsh"));
        }
    }
}

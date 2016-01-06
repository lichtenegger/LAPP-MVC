using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BL_BricoMarche.DatenVerwaltung;

namespace UT_BricoMarche
{
    [TestClass]
    public class InhaltTests
    {
        [TestMethod]
        public void KategorienWerdenGeladen()
        {
            Assert.IsNotNull(Kategorie.LadeAlleKategorien());
        }
    }
}

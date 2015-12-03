using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static BL_BricoMarche.DatenVerwaltung.Inhalt;

namespace UT_BricoMarche
{
    [TestClass]
    public class InhaltTests
    {
        [TestMethod]
        public void KategorienWerdenGeladen()
        {
            Assert.IsNotNull(LadeAlleKategorien());
        }
    }
}

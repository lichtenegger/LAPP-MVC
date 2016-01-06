using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using  BL_BricoMarche.DatenVerwaltung;

namespace UT_BricoMarche
{
    [TestClass]
    public class InhaltTests
    {
        [TestMethod]
        public void KategorienWerdenGeladen()
        {
            Assert.IsNotNull(Artikel.LadeAlleKategorien());
        }

        [TestMethod]
        public void VideosWerdenGeladen()
        {
            Assert.IsNotNull(Video.LadeAlleVideos());
        }

        [TestMethod]
        public void VideosWerdenGeladen_Schlagwort()
        {
            Assert.IsNotNull(Video.LadeAlleVideos("kokosnuss"));
        }

    }
}

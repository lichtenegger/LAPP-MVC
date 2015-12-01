using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL_BricoMarche.DatenVerwaltung
{
    public class Inhalt
    {
        public class InhaltKategorien
        {
            public static List<Kategorie> LadeAlleKategorien()
            {
                Debug.WriteLine("-- START : LADE ALLE KATEGORIEN ------------------------");
                Debug.Indent();
                List<Kategorie> alleKategorien = null;
                try
                {
                    using (var kontext = new BricoMarcheDBObjekte())
                    {
                        alleKategorien = kontext.AlleKategorien.ToList();
                    }
                    if (alleKategorien != null)
                    {
                        Debug.WriteLine("ERFOLG!");
                    }
                    else
                    {
                        throw new Exception("Fehler! 0 Kategorien geladen.");
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("FEHLER!\n " + ex.Message);
                }
                Debug.Unindent();
                Debug.WriteLine("-- Ende : LADE ALLE KATEGORIEN ------------------------");
                return alleKategorien;
            }
        }
    }
}

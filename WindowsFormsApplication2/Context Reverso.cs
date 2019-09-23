using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication2
{
    class Context_Reverso : BazniServer
    {
        public Context_Reverso(string kraticaOriginalnogJezika) : base(kraticaOriginalnogJezika)
        { }

        public string link;
        public string link1;
        public int n = 0, i = 0,kon = 0 ;
        public string result;
        public override string SlanjeZahtjeva(string tekstZaPrijevod, string jezik)
        {
            
            tekstZaPrijevod = tekstZaPrijevod.Trim();
            tekstZaPrijevod = tekstZaPrijevod.Replace("+", "");
            tekstZaPrijevod = Uri.EscapeDataString(tekstZaPrijevod);
            tekstZaPrijevod = tekstZaPrijevod.Replace("%20", "+");
            link1 = tekstZaPrijevod;
            
            for (i = 1; i < 10; i++)
            {
                kon = link1.IndexOf("+");
                link1 = link1.Substring(kon + 1);
                n = n + kon + 1;
            }
            link = "http://context.reverso.net/translation/" + originalniJezik + "-" + jezik + "/" + tekstZaPrijevod;
            WebClient client = new WebClient();
            var link2 = client.DownloadData(link);
            link1 = Encoding.UTF8.GetString(link2);
            return link1;
        }
        public override string Obrada_odgovora(string spremnik)
        {
            spremnik = link1;
            string rezac = "\">";
            string rezac2 = "search\">";
            i = 0; n = 2;
            if (link1.IndexOf("Adverb") != -1)
            {
                i = 1;
                int rezanje = link1.IndexOf("class=\"wide-container\">");
                link1 = link1.Substring(rezanje);
                rezanje = link1.IndexOf("class='translation'");
                link1 = link1.Substring(rezanje);
                rezanje = link1.IndexOf("on'");
                link1 = link1.Substring(rezanje);
                int prvi = link1.IndexOf(">") + i;
                int zadnji = link1.IndexOf("<");
                result = link1.Substring(prvi, zadnji - prvi);
            }

            else if ((originalniJezik == "english" || link1.IndexOf("split wide-container") != -1) /*&& jezik_prijevoda != "english"*/)
            {
                rezac2 = "lang"; i = 6; rezac = "="; n = 0;
            }
            if (link1.IndexOf("split wide-container") != -1 && result == null)
            {
                while (link1.IndexOf("split wide-container") != -1)
                {
                    int rezanje = link1.IndexOf("split wide-container");
                    link1 = link1.Substring(rezanje);
                    rezanje = link1.IndexOf(rezac2);
                    link1 = link1.Substring(rezanje + 2);
                    if (originalniJezik != "english")
                    {
                        rezanje = link1.IndexOf(rezac2);
                        link1 = link1.Substring(rezanje);
                    }
                    int prvi = link1.IndexOf(rezac) + i;
                    int zadnji = link1.IndexOf("</a>");
                    result = result + " " + link1.Substring(prvi + n, zadnji - prvi - n);
                }
            }
            else if (link1.IndexOf("title=\"Other\">") != -1 && result == null)
            {
                rezac = "</div>\">";
                i = 7;
                rezac2 = "examples with alignment)";
                while (link1.IndexOf("title=\"Other\">") != -1)
                {
                    int rezanje = link1.IndexOf("title=\"Other\">");
                    link1 = link1.Substring(rezanje);
                    rezanje = link1.IndexOf(rezac2);
                    link1 = link1.Substring(rezanje);
                    int prvi = link1.IndexOf(rezac) + i;
                    int zadnji = link1.IndexOf("</a>");
                    result = result + " " + link1.Substring(prvi + n, zadnji - prvi - n);
                    while (result.IndexOf(" ") == 0) { result = result.Substring(1); }
                }
            }
            else if (result == null)
            {
                i = 1;
                int rezanje = link1.IndexOf("class=\"wide-container\">");
                link1 = link1.Substring(rezanje);
                rezanje = link1.IndexOf("class='translation'");
                link1 = link1.Substring(rezanje);
                rezanje = link1.IndexOf("on'");
                link1 = link1.Substring(rezanje);
                int prvi = link1.IndexOf(">") + i;
                int zadnji = link1.IndexOf("<");
                result = link1.Substring(prvi, zadnji - prvi);
            }
            spremnik = result;
            return spremnik;
        }
    }
}


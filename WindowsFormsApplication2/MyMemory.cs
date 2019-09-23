using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WindowsFormsApplication2
{
    class MyMemory : BazniServer
    {
        public MyMemory(string kraticaOriginalnogJezika) : base(kraticaOriginalnogJezika)
        { }

        public string link;
        public string link1;
        public override string SlanjeZahtjeva(string tekstZaPrijevod, string jezik)
        {
            tekstZaPrijevod = Uri.EscapeDataString(tekstZaPrijevod);
            link = "https://api.mymemory.translated.net/get?q=" + tekstZaPrijevod + "&langpair=" + originalniJezik + "|" + jezik;
            WebClient client = new WebClient();
            var link2 = client.DownloadData(link);
            link1 = Encoding.UTF8.GetString(link2);
            return link1;
        }
        public override string ObradaOdgovora(string spremnik)
        {
            int rezanje = spremnik.IndexOf("ext");
            spremnik = spremnik.Substring(rezanje);
            rezanje = spremnik.IndexOf(":");
            spremnik = spremnik.Substring(rezanje);
            spremnik = spremnik.Substring(1);
            int zadnji = spremnik.IndexOf("match") - 4;
            spremnik = spremnik.Substring(1, zadnji);
            spremnik = String.Join(" ", spremnik.Split(' ').Reverse());
            if (spremnik.IndexOf("\\u") != -1)
            {              
                    zadnji = link.IndexOf("\\u");
                    link = link.Substring(zadnji);
                    link1 = link.Substring(0, 6);
                    link = link.Substring(6);
                    string stari = link1;
                    link1 = Regex.Replace(link1, @"\\u(?<Value>[a-fA-F0-9]{4})", m => {
                        return ((char)int.Parse(m.Groups["Value"].Value, NumberStyles.HexNumber)).ToString();
                    });
                    spremnik = spremnik.Replace(stari, link1);             
            }
            return spremnik;
        }
    }
}


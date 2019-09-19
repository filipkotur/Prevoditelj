using System;
using System.Text;
using System.Net;
using System.Collections.Specialized;
using Newtonsoft.Json;
using static WindowsFormsApplication2.Form1;

namespace WindowsFormsApplication2
{
     class Yandex : Bazni_server
    {
        public Yandex() : base()
        { }

        public string rezultat;
        public override string SlanjeZahtjeva(string tekstZaPrijevod, string jezik)
        {

            using (var wb = new WebClient())
            {
                var reqData = new NameValueCollection();
                reqData["text"] = tekstZaPrijevod; // text to translate
                reqData["lang"] = jezik; // target language
                reqData["key"] = "trnsl.1.1.20190727T115039Z.2a06d4a1411c87fc.399cbc233d5bdbba636feba9f4975b23ff145a9d";

                try
                {
                    var response = wb.UploadValues("https://translate.yandex.net/api/v1.5/tr.json/translate", "POST", reqData);
                    string responseInString = Encoding.UTF8.GetString(response);

                    var rootObject = JsonConvert.DeserializeObject<Translation>(responseInString);
                    rezultat = rootObject.text[0];
                    return rezultat;
                }
                catch (Exception ex)
                {
                    rezultat = ex.Message;
                    throw;
                }
            
            }
        }
        public override string Obrada_odgovora(string spremnik)
        {
            return spremnik;
        }

    }
}

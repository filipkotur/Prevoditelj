using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication2
{
    class Bing : BazniServer
    {
        public Bing() : base()
        { }

        public override string SlanjeZahtjeva(string tekstZaPrijevod, string jezik)
        {
            string strTranslatedText = null;
            try
            {
                TranslatorService.LanguageServiceClient client = new TranslatorService.LanguageServiceClient();
                strTranslatedText = client.Translate("6CE9C85A41571C050C379F60DA173D286384E0F2", tekstZaPrijevod, "", jezik);
                return strTranslatedText;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return "error";
            }
        }
        public override string ObradaOdgovora(string spremnik)
        {
            return spremnik;
        }
    }
}

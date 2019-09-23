using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static WindowsFormsApplication2.Form1;
namespace WindowsFormsApplication2
{
    abstract class BazniServer
    {
        protected BazniServer(string kraticaOriginalnogJezika)
        {
            originalniJezik = kraticaOriginalnogJezika;
        }
        protected BazniServer()
        {
        }

        readonly protected string originalniJezik;
        public abstract string SlanjeZahtjeva(string tekstZaPrijevod, string jezik);
        public abstract string ObradaOdgovora(string spremnik);
        public string ZahtjevIOdgovor(string tekstZaPrijevod,string jezik)
        {
            string spremnik = this.SlanjeZahtjeva(tekstZaPrijevod, jezik);
            spremnik = this.ObradaOdgovora(spremnik);
            return spremnik;
        }
    }
}

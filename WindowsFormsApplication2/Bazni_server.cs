﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static WindowsFormsApplication2.Form1;
namespace WindowsFormsApplication2
{
    abstract class Bazni_server
    {
        protected Bazni_server(string kraticaOriginalnogJezika)
        {
            originalniJezik = kraticaOriginalnogJezika;
        }
        protected Bazni_server()
        {
        }

        readonly protected string originalniJezik;
        public abstract string SlanjeZahtjeva(string tekstZaPrijevod, string jezik);
        public abstract string Obrada_odgovora(string spremnik);
        public string ZahtjevIOdgovor(string tekstZaPrijevod,string jezik)
        {
            string spremnik = this.SlanjeZahtjeva(tekstZaPrijevod, jezik);
            spremnik = this.Obrada_odgovora(spremnik);
            return spremnik;
        }
    }
}
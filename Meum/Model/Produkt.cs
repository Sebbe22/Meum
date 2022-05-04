using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Meum.Model
{
    public class Produkt
    {
        private int _produktId;
        private string _navn;
        private int _pris;
        private string _beskrivelse;

        public Produkt()
        {
        }

        public Produkt(int produktId, string navn, int pris, string beskrivelse)
        {
            _produktId = produktId;
            _navn = navn;
            _pris = pris;
            _beskrivelse = beskrivelse;
        }

        public int ProduktId
        {
            get => _produktId;
            set => _produktId = value;
        }

        public string Navn
        {
            get => _navn;
            set => _navn = value;
        }

        public int Pris
        {
            get => _pris;
            set => _pris = value;
        }

        public string Beskrivelse
        {
            get => _beskrivelse;
            set => _beskrivelse = value;
        }

        public override string ToString()
        {
            return $"{nameof(ProduktId)}: {ProduktId}, {nameof(Navn)}: {Navn}, {nameof(Pris)}: {Pris}, {nameof(Beskrivelse)}: {Beskrivelse}";
        }
    }
}

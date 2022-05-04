using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Meum.Model
{
    public class Enhed
    {
        private int _VareId;
        private string _Navn;
        private int _Pris;
        private int _Antal;

        public Enhed()
        {
        }

        public Enhed(int vareId, string navn, int pris, int antal)
        {
            _VareId = vareId;
            _Navn = navn;
            _Pris = pris;
            _Antal = antal;
        }

        public int VareId
        {
            get => _VareId;
            set => _VareId = value;
        }

        public string Navn
        {
            get => _Navn;
            set => _Navn = value;
        }

        public int Pris
        {
            get => _Pris;
            set => _Pris = value;
        }

        public int Antal
        {
            get => _Antal;
            set => _Antal = value;
        }

        public override string ToString()
        {
            return $"{nameof(VareId)}: {VareId}, {nameof(Navn)}: {Navn}, {nameof(Pris)}: {Pris}, {nameof(Antal)}: {Antal}";
        }
    }
}

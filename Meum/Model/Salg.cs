using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Meum.Model
{
    public class Salg
    {
        private int _id;
        private int _produktId;
        private DateTime _dato;
        private int _antal;

        public Salg()
        {

        }

        public Salg(int id, int produktId, DateTime dato, int antal)
        {
            _id = id;
            _produktId = produktId;
            _dato = dato;
            _antal = antal;
        }
        public int Id
        {
            get => _id;
            set => _id = value;
        }

        public int ProduktId
        {
            get => _produktId;
            set => _produktId = value;
        }

        public DateTime Dato
        {
            get => _dato;
            set => _dato = value;
        }

        public int Antal
        {
            get => _antal;
            set => _antal = value;
        }

        public override string ToString()
        {
            return $"{nameof(ProduktId)}: {ProduktId}, {nameof(Dato)}: {Dato}, {nameof(Antal)}: {Antal},{nameof(Id)}: {Id}";
        }
    }
}

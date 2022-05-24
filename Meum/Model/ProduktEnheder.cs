using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Meum.Model
{
   
        public class ProduktEnheder
        {
            private int _produktId;
            private int _enhedId;
            private int _antal;

            public ProduktEnheder(int produktId, int enhedId, int antal)
            {
                _produktId = produktId;
                _enhedId = enhedId;
                _antal = antal;
            }

            public ProduktEnheder()
            {

            }

            public int ProduktId
            {
                get => _produktId;
                set => _produktId = value;
            }

            public int EnhedId
            {
                get => _enhedId;
                set => _enhedId = value;
            }

            public int Antal
            {
                get => _antal;
                set => _antal = value;
            }

            public override string ToString()
            {
                return $"{nameof(ProduktId)}: {ProduktId}, {nameof(EnhedId)}: {EnhedId}, {nameof(Antal)}: {Antal}";
            }

        }
    }

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Meum.Model
{
    public class Adresse
    {
        private int _AdresseID;
        private string _Vejnavn;
        private string _HusNr;
        private string _Etage;
        private int _ByID;


        public Adresse()
        {

        }
        public Adresse(int adresseId, string vejnavn, string husNr, string etage, int byId)
        {
            _AdresseID = adresseId;
            _Vejnavn = vejnavn;
            _HusNr = husNr;
            _Etage = etage;
            _ByID = byId;
        }

        public int AdresseId
        {
            get => _AdresseID;
            set => _AdresseID = value;
        }
        [Required]
        [Range(1000, 9999, ErrorMessage = "Postnummer skal være mellem 1000-9999")]
        
        public int ById
        {
            get => _ByID;
            set => _ByID = value;
        }

        public string Vejnavn
        {
            get => _Vejnavn;
            set => _Vejnavn = value;
        }

        public string HusNr
        {
            get => _HusNr;
            set => _HusNr = value;
        }

        public string Etage
        {
            get => _Etage;
            set => _Etage = value;
        }


        public override string ToString()
        {
            return $"{nameof(AdresseId)}: {AdresseId}, {nameof(ById)}: {ById}, {nameof(Vejnavn)}: {Vejnavn}, {nameof(HusNr)}: {HusNr}, {nameof(Etage)}: {Etage}";
        }
    }
}

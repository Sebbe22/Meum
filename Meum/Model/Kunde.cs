using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Meum.Model
{
    public class Kunde
    {
        private int _idKunde;
        private string _fornavn;
        private string _efternavn;
        private string _email;
        private string _tlfNummer;
        private int _adresseId;
        private bool _newlySub;
        private DateTime _subStart;

        public Kunde()
        {
        }

        public Kunde(int idKunde, string fornavn, string efternavn, string email, string tlfNummer, int adresseId, bool newlySub, DateTime subStart)
        {
            _idKunde = idKunde;
            _fornavn = fornavn;
            _efternavn = efternavn;
            _email = email;
            _tlfNummer = tlfNummer;
            _adresseId = adresseId;
            _newlySub = newlySub;
            _subStart = subStart;
        }

        public int IdKunde
        {
            get => _idKunde;
            set => _idKunde = value;
        }

        public string Fornavn
        {
            get => _fornavn;
            set => _fornavn = value;
        }

        public string Efternavn
        {
            get => _efternavn;
            set => _efternavn = value;
        }

        public string Email
        {
            get => _email;
            set => _email = value;
        }

        public string TlfNummer
        {
            get => _tlfNummer;
            set => _tlfNummer = value;
        }

        public int AdresseID
        {
            get => _adresseId;
            set => _adresseId = value;
        }

        public bool NewlySub
        {
            get => _newlySub;
            set => _newlySub = value;
        }

        public DateTime SubStart
        {
            get => _subStart;
            set => _subStart = value;
        }

        public override string ToString()
        {
            return $"{nameof(IdKunde)}: {IdKunde},{nameof(Fornavn)}: {Fornavn}, {nameof(Efternavn)}: {Efternavn}, {nameof(Email)}: {Email},{nameof(TlfNummer)}: {TlfNummer},{nameof(AdresseID)}: {AdresseID},{nameof(NewlySub)}: {NewlySub},{nameof(SubStart)}: {SubStart}";
        }
    }
}

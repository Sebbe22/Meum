using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        private int _abonType;
        private bool _afsend;

        public Kunde()
        {
        }

        public Kunde(int idKunde, string fornavn, string efternavn, int abontype, string email, string tlfNummer, int adresseId, bool newlySub, DateTime subStart, bool afsend)
        {
            IdKunde = idKunde;
            Fornavn = fornavn;
            Efternavn = efternavn;
            Email = email;
            TlfNummer = tlfNummer;
            AdresseID = adresseId;
            NewlySub = newlySub;
            SubStart = subStart;
            AbonType = abontype;
            Afsend = afsend;
        }

        [Range(1, 3, ErrorMessage = "Abonnomentstype skal være mellem 1-3")]
        public int AbonType
        {
            get => _abonType;
            set => _abonType = value;
        }

        public int IdKunde
        {
            get => _idKunde;
            set => _idKunde = value;
        }

        public string Fornavn
        {
            get => _fornavn;
            set
            {
                CheckNavn(value);
                _fornavn = value;
            }
        }

        public string Efternavn
        {
            get => _efternavn;
            set
            {
                CheckNavn(value);
                _efternavn = value;
            }
        }

        public string Email
        {
            get => _email;
            set
            {
                CheckEmail(value);
                _email = value;
            }
        }

        public string TlfNummer
        {
            get => _tlfNummer;
            set
            {
                CheckTlf(value);
                _tlfNummer = value;
            }
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

        public bool Afsend
        {
            get => _afsend;
            set => _afsend = value;
        }

        public override string ToString()
        {
            return $"{nameof(IdKunde)}: {IdKunde},{nameof(Fornavn)}: {Fornavn}, {nameof(Efternavn)}: {AbonType},{nameof(AbonType)}: {Efternavn}, {nameof(Email)}: {Email},{nameof(TlfNummer)}: {TlfNummer},{nameof(AdresseID)}: {AdresseID},{nameof(NewlySub)}: {NewlySub},{nameof(SubStart)}: {SubStart}, {nameof(Afsend)}: {Afsend}";
        }

        private void CheckTlf(string Tlf)
        {
            if (Tlf.Length != 8)
            {
                throw new ArgumentException("Tlf nummer skal være 8 karakterer langt!");
            }

            if (string.IsNullOrEmpty(Tlf))
            {
                throw new ArgumentNullException("der skal være et tlf nummer");
            }
        }

        private void CheckNavn(string navn)
        {
            if (navn.Length < 2)
            {
                throw new ArgumentException("et navn skal være 2 bokstaver eller mere");
            }

            if (string.IsNullOrEmpty(navn))
            {
                throw new ArgumentNullException("der skal være et navn");
            }
        }

        private void CheckEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                throw new ArgumentNullException("der skal være en email");
            }

            if (!email.Contains("@"))
            {
                throw new ArgumentException("en email skal indholde @");
            }


        }
    }
}

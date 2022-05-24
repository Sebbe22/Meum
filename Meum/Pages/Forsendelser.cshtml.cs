using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Meum.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Meum.Pages
{
    [Authorize]
    public class ForsendelserModel : PageModel
    {
        private IKundeKatalog _kundeData;

        private ProduktEnhederKatalog _forsendelsesData;
        private SalgKatalog _salgData;


        public ForsendelserModel(IKundeKatalog data1, ProduktEnhederKatalog data2, SalgKatalog data3)
        {
            _forsendelsesData = data2;
            _kundeData = data1;
            _salgData = data3;

        }

        public SalgKatalog SalgData
        {
            get => _salgData;
            set => _salgData = (SalgKatalog) value;
        }


        public ProduktEnhederKatalog ForsendelsesData
        {
            get => _forsendelsesData;
            set => _forsendelsesData = (ProduktEnhederKatalog)value;

        }

        public IKundeKatalog KundeData
        {
            get => _kundeData;
            set => _kundeData = (IKundeKatalog)value;
        }

        public List<Kunde> forsendelser1 = new List<Kunde>();
        public List<Kunde> forsendelser2 = new List<Kunde>();
        public List<Kunde> forsendelser3 = new List<Kunde>();
        public List<Kunde> forsendelser4 = new List<Kunde>();
        public List<Kunde> forsendelser5 = new List<Kunde>();
        public List<Kunde> forsendelser6 = new List<Kunde>();
        public List<Kunde> forsendelser7 = new List<Kunde>();

        public void OnGet()
        {

            forsendelser1 = _kundeData.Forsendelser(0, _kundeData.GetAllKunder());
            forsendelser2 = _kundeData.Forsendelser(1, _kundeData.GetAllKunder());
            forsendelser3 = _kundeData.Forsendelser(2, _kundeData.GetAllKunder());
            forsendelser4 = _kundeData.Forsendelser(3, _kundeData.GetAllKunder());
            forsendelser5 = _kundeData.Forsendelser(4, _kundeData.GetAllKunder());
            forsendelser6 = _kundeData.Forsendelser(5, _kundeData.GetAllKunder());
            forsendelser7 = _kundeData.Forsendelser(6, _kundeData.GetAllKunder());
        }

        public IActionResult OnPost(string TlfNummer, bool NewlySub)
        {
            Kunde k = _kundeData.GetPersonByPhoneNo(TlfNummer);
            _salgData.SalgPerMåned(NewlySub);
            
            if (k.Afsend == false)
            {
                k.Afsend = true;
                _kundeData.UpdateKunde(TlfNummer,k);
            }

            if (NewlySub == false)
            {

                _forsendelsesData.UpdateAntal(2, 4);
            }

            if (NewlySub == true)
            {
                _kundeData.UpdateNewlySub(TlfNummer, false);
                _forsendelsesData.UpdateAntal(2, 2);
                _forsendelsesData.UpdateAntal(4, 1);
            }

            return RedirectToPage("Forsendelser");
        }
    }
}

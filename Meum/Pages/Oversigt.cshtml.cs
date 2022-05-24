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
    public class OversigtModel : PageModel
    {
        private SalgKatalog _salgData;
        private IKundeKatalog _kundeData;
        private EnhedKatalog _enhedData;
        
        public int subCount;
        public DateTime skraberTomDato;
        
        public OversigtModel(SalgKatalog data, IKundeKatalog data1, EnhedKatalog data2)
        {
            _salgData = data;
            _kundeData = data1;
            _enhedData = data2;
        }
        public EnhedKatalog EnhedData
        {
            get => _enhedData;
            set => _enhedData = (EnhedKatalog)value;
        }
        public IKundeKatalog KundeData
        {
            get => _kundeData;
            set => _kundeData = (IKundeKatalog)value;
        }
        public SalgKatalog SalgData
        {
            get => _salgData;
            set => _salgData = (SalgKatalog)value;
        }

        public List<Kunde> kundeList = new List<Kunde>();
        public List<Salg> salgList = new List<Salg>();
        public List<Salg> salgListStarterkit = new List<Salg>();
        public double skraberSolgt;
        public double nyeAbb;
        public double mindsteværdiRefill;
        public double mindsteværdiStarterKits;
        public int lagerAntalHåndtag;
        public int lagerAntalSkraber;
        public string lagerstatusSkraber;
        public string lagerstatusHåndtag;
        public int[] SkraberPlotY;
        public string[] SkraberPlotX;
        public int[] SkraberPloty;
        public string[] SkraberPlotx;
        public int AntalForsendelseridag;
        public IActionResult OnGet()
        {
            //Ændrer afsendt til false 2 dage efter pakken er blevet sendt så knappen dukker op næste gang en pakke skal sendes til kunden
            _kundeData.ReturnToFalse(kundeList);
            //Får lagerstatus til at køre
            salgList = _salgData.GetAllSalg();
            salgList = _salgData.FilterById(salgList, 3);
            skraberSolgt = _salgData.skraberSolgt(salgList);
            kundeList = _kundeData.GetAllKunder();
            nyeAbb = _salgData.NyeAbonnenter(kundeList);
            mindsteværdiRefill = _salgData.mindsteSkraber(nyeAbb, skraberSolgt);
            mindsteværdiStarterKits = _salgData.mindsteVaerdiHåndtag(nyeAbb);
            lagerAntalSkraber = (_enhedData.GetEnhedByName("Skrabehoved")).Antal;
            lagerAntalHåndtag = (_enhedData.GetEnhedByName("Håndtag")).Antal;
            lagerstatusHåndtag = _salgData.LagerStatus(lagerAntalHåndtag, mindsteværdiStarterKits);
            lagerstatusSkraber = _salgData.LagerStatus(lagerAntalSkraber, mindsteværdiRefill);

            //Plot for Refills
            SkraberPlotY = _salgData.PlotData(salgList);
            SkraberPlotX = _salgData.PlotDataXakse();
            //Plot for StarterKits
            salgListStarterkit = _salgData.GetAllSalg();
            salgListStarterkit = _salgData.FilterById(salgListStarterkit, 2);
            SkraberPloty = _salgData.PlotData(salgListStarterkit);
            SkraberPlotx = _salgData.PlotDataXakse();

            //Den øverste del af siden
            AntalForsendelseridag = _kundeData.Forsendelser(0, kundeList).Count();
            subCount = _kundeData.SubCountKunde(kundeList);

            skraberTomDato = _kundeData.LagerTomSkrabehovede(_enhedData.GetEnhedById(2).Antal, _kundeData.GetAllKunder());
            
            return Page();
        }
    }
}

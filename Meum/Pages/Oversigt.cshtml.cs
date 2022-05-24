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
        public double mindstev�rdiRefill;
        public double mindstev�rdiStarterKits;
        public int lagerAntalH�ndtag;
        public int lagerAntalSkraber;
        public string lagerstatusSkraber;
        public string lagerstatusH�ndtag;
        public int[] SkraberPlotY;
        public string[] SkraberPlotX;
        public int[] SkraberPloty;
        public string[] SkraberPlotx;
        public int AntalForsendelseridag;
        public IActionResult OnGet()
        {
            //�ndrer afsendt til false 2 dage efter pakken er blevet sendt s� knappen dukker op n�ste gang en pakke skal sendes til kunden
            _kundeData.ReturnToFalse(kundeList);
            //F�r lagerstatus til at k�re
            salgList = _salgData.GetAllSalg();
            salgList = _salgData.FilterById(salgList, 3);
            skraberSolgt = _salgData.skraberSolgt(salgList);
            kundeList = _kundeData.GetAllKunder();
            nyeAbb = _salgData.NyeAbonnenter(kundeList);
            mindstev�rdiRefill = _salgData.mindsteSkraber(nyeAbb, skraberSolgt);
            mindstev�rdiStarterKits = _salgData.mindsteVaerdiH�ndtag(nyeAbb);
            lagerAntalSkraber = (_enhedData.GetEnhedByName("Skrabehoved")).Antal;
            lagerAntalH�ndtag = (_enhedData.GetEnhedByName("H�ndtag")).Antal;
            lagerstatusH�ndtag = _salgData.LagerStatus(lagerAntalH�ndtag, mindstev�rdiStarterKits);
            lagerstatusSkraber = _salgData.LagerStatus(lagerAntalSkraber, mindstev�rdiRefill);

            //Plot for Refills
            SkraberPlotY = _salgData.PlotData(salgList);
            SkraberPlotX = _salgData.PlotDataXakse();
            //Plot for StarterKits
            salgListStarterkit = _salgData.GetAllSalg();
            salgListStarterkit = _salgData.FilterById(salgListStarterkit, 2);
            SkraberPloty = _salgData.PlotData(salgListStarterkit);
            SkraberPlotx = _salgData.PlotDataXakse();

            //Den �verste del af siden
            AntalForsendelseridag = _kundeData.Forsendelser(0, kundeList).Count();
            subCount = _kundeData.SubCountKunde(kundeList);

            skraberTomDato = _kundeData.LagerTomSkrabehovede(_enhedData.GetEnhedById(2).Antal, _kundeData.GetAllKunder());
            
            return Page();
        }
    }
}

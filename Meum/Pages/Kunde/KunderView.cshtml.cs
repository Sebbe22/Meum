using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Meum.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Meum.Pages
{
    [Authorize]
    public class KunderViewModel : PageModel
    {
        private IKundeKatalog _kundeData;

        public KunderViewModel(IKundeKatalog data)
        {
            _kundeData = data;
        }

        public IKundeKatalog KundeData
        {
            get => _kundeData;
            set => _kundeData = (IKundeKatalog)value;
        }

        public List<Kunde> KundeList = new List<Kunde>();

        public IActionResult OnGet()
        {
            KundeList = _kundeData.GetAllKunder();
            return Page();
        }
    }
}
    
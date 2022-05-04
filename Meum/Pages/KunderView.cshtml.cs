using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Meum.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Meum.Pages
{
    public class KunderViewModel : PageModel
    {
        private KundeDatabase _kundeData;

        public KunderViewModel(KundeDatabase data)
        {
            _kundeData = data;
        }

        public KundeDatabase KundeData
        {
            get => _kundeData;
            set => _kundeData = (KundeDatabase)value;
        }

        public List<Kunde> KundeList = new List<Kunde>();

        public IActionResult OnGet()
        {
            KundeList = _kundeData.GetAllKunder();
            return Page();
        }
    }
}
    
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Meum.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Meum.Pages
{
    public class ProdukterModel : PageModel
    {
        private ProduktDatabase _produktData;

        public ProdukterModel(ProduktDatabase data)
        {
            _produktData = data;
        }

        public ProduktDatabase ProduktData
        {
            get => _produktData;
            set => _produktData = (ProduktDatabase)value;
        }

        public List<Produkt> ProduktList = new List<Produkt>();

        public IActionResult OnGet()
        {
            ProduktList = _produktData.GetAllProdukter();
            return Page();
        }

    }
}

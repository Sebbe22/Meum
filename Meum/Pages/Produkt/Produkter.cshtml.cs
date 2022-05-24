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
    public class ProdukterModel : PageModel
    {
        private ProduktKatalog _produktData;

        public ProdukterModel(ProduktKatalog data)
        {
            _produktData = data;
        }

        public ProduktKatalog ProduktData
        {
            get => _produktData;
            set => _produktData = (ProduktKatalog)value;
        }

        public List<Produkt> ProduktList = new List<Produkt>();

        public IActionResult OnGet()
        {
            ProduktList = _produktData.GetAllProdukter();
            return Page();
        }

    }
}

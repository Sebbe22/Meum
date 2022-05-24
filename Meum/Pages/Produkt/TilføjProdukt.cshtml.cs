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
    public class TilføjProduktModel : PageModel
    {
        private ProduktKatalog _produkt;

        [BindProperty]
        public Produkt Produkt { get; set; }

        public TilføjProduktModel(ProduktKatalog data)
        {
            _produkt = data;
        }
        public IActionResult OnGet()
        {
            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            _produkt.AddProdukt(Produkt);
            return RedirectToPage("TilføjProduktEnheder", new { Id = _produkt.GetProduktByNavn(Produkt.Navn).ProduktId });
        }
    }
}

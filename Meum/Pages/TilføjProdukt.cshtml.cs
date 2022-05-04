using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Meum.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Meum.Pages
{
    public class TilføjProduktModel : PageModel
    {
        private ProduktDatabase _produkt;

        [BindProperty]
        public Produkt Produkt { get; set; }

        public TilføjProduktModel(ProduktDatabase data)
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
            return RedirectToPage("Lager");
        }
    }
}

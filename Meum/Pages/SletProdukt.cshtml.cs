using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Meum.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Meum.Pages
{
    public class SletProduktModel : PageModel
    {
        private ProduktDatabase _produktData;


        [BindProperty]
        public Produkt produkt { get; set; }

        public SletProduktModel(ProduktDatabase data)
        {
            _produktData = data;
        }

        public IActionResult OnGet(int id)
        {

            produkt = _produktData.GetProduktById(id);

            return Page();
        }

        public IActionResult OnPost(int id)
        {
            _produktData.DeleteProduktById(id);

            return RedirectToPage("Produkter");
        }
    }
}

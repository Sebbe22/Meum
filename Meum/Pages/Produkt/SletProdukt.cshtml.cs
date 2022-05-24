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
    public class SletProduktModel : PageModel
    {
        private ProduktKatalog _produktData;
        private ProduktEnhederKatalog _produktEnhedData;


        [BindProperty]
        public Produkt produkt { get; set; }


        public SletProduktModel(ProduktKatalog data,ProduktEnhederKatalog data2)
        {
            _produktData = data;
            _produktEnhedData = data2;
        }

        public IActionResult OnGet(int id)
        {

            produkt = _produktData.GetProduktById(id);

            return Page();
        }

        public IActionResult OnPost(int id)
        {
            _produktEnhedData.DeleteProduktEnhederByProduktId(id);
            _produktData.DeleteProduktById(id);
            

            return RedirectToPage("Produkter");
        }
    }
}

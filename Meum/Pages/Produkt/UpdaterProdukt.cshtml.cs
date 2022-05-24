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
    public class UpdaterProduktModel : PageModel
    {
        

        private ProduktKatalog _produktData;

        [BindProperty]
        public Produkt produkt { get; set; }


        public UpdaterProduktModel(ProduktKatalog data)
        {
            _produktData = data;
        }
        public void OnGet(int produktId)
        {

            produkt = _produktData.GetProduktById(produktId);

        }

        public IActionResult OnPost(int produktID)
        {
            _produktData.UpdateProdukt(produktID, produkt);
            return RedirectToPage("Produkter");
        }
    }
}

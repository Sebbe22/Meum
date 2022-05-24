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
    public class TilføjKundeAdresseModel : PageModel
    {
        private AdresseKatalog _adresse;

        [BindProperty]
        public Adresse Adresse { get; set; }

        public TilføjKundeAdresseModel(AdresseKatalog data)
        {
            _adresse = data;
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

            _adresse.AddAdresse(Adresse);

            return RedirectToPage("TilføjKunde", new { Id = _adresse.GetAdresseByVejnav(Adresse.Vejnavn).AdresseId });
        }
    }
}

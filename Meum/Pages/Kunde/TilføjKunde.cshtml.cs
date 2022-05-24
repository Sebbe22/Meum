using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Meum.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Meum.Pages
{
    [Authorize]
    public class TilføjKundeModel : PageModel
    {
        private IKundeKatalog _kunde;

        [BindProperty] public Kunde Kunde { get; set; }

        public TilføjKundeModel(IKundeKatalog data)
        {
            _kunde = data;
        }

        
        public IActionResult OnGet(int Id)
        {
            Kunde = new Kunde();
            _kunde.SetAdresseID(Id, Kunde);
            Kunde.SubStart = DateTime.Now;
            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _kunde.AddPerson(Kunde);
            return RedirectToPage("KunderView");
        }
    }
}

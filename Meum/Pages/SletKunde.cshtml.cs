using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Meum.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Meum.Pages
{
    public class SletKundeModel : PageModel
    {
        private KundeDatabase _kundeData;


        [BindProperty]
        public Kunde kunde { get; set; }

        public SletKundeModel(KundeDatabase data)
        {
            _kundeData = data;
        }

        public IActionResult OnGet(string Tlf)
        {
            
           kunde = _kundeData.GetPersonByPhoneNo(Tlf);

            return Page();
        }

        public IActionResult OnPost(string Tlf)
        {
            _kundeData.DeletePersonByPhoneNo(Tlf);

            return RedirectToPage("KunderView");
        }
    }
}

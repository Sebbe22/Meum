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
    public class SletKundeModel : PageModel
    {
        private IKundeKatalog _kundeData;

        public SletKundeModel(IKundeKatalog data)
        {
            _kundeData = data;
        }

        public IActionResult OnGet(string Tlf)
        {


            return Page();
        }

        public IActionResult OnPost(string Tlf)
        {
            _kundeData.DeletePersonByPhoneNo(Tlf);

            return RedirectToPage("KunderView");
        }
    }
}

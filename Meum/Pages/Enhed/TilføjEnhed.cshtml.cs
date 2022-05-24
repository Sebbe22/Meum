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
    public class TilføjEnhedModel : PageModel
    {
        private EnhedKatalog _enhed;

        [BindProperty]
        public Enhed Enhed { get; set; }
        
        public TilføjEnhedModel(EnhedKatalog data)
        {
            _enhed = data;
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
            _enhed.AddEnhed(Enhed);
            return RedirectToPage("Lager");
        }
    }
}

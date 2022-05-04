using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Meum.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Meum.Pages
{
    public class SletVareModel : PageModel
    {
        private EnhedDatabase _vareData;


        [BindProperty]
        public Enhed Vare { get; set; }

        public SletVareModel(EnhedDatabase data)
        {
            _vareData = data;
        }

        public IActionResult OnGet(int ID)
        {

            Vare = _vareData.GetEnhedById(ID);

            return Page();
        }

        public IActionResult OnPost(int ID)
        {
            _vareData.DeleteEnhedById(ID);

            return RedirectToPage("Lager");
        }
    }
}

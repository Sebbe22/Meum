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
    public class SletVareModel : PageModel
    {
        private EnhedKatalog _vareData;
        private ProduktEnhederKatalog _produktEnhederData;


        [BindProperty]
        public Enhed Vare { get; set; }

        public SletVareModel(EnhedKatalog data, ProduktEnhederKatalog data1)
        {
            _vareData = data;
            _produktEnhederData = data1;
        }

        public IActionResult OnGet(int ID)
        {

            Vare = _vareData.GetEnhedById(ID);

            return Page();
        }

        public IActionResult OnPost(int ID)
        {
            _produktEnhederData.DeleteProduktEnhederByVareId(ID);
            _vareData.DeleteEnhedById(ID);

            return RedirectToPage("Lager");
        }
    }
}

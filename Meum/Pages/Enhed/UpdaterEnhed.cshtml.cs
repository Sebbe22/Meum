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
    public class UpdaterEnhedModel : PageModel
    {
        private EnhedKatalog _enhedData;
        [BindProperty]
        public Enhed enhed { get; set; }

        public UpdaterEnhedModel(EnhedKatalog enhedData)
        {
            _enhedData = enhedData;
        }
        public void OnGet(int id)
        {

            enhed = _enhedData.GetEnhedById(id);

        }

        public IActionResult OnPost(int id)
        {

            _enhedData.UpdateEnhed(id, enhed);
            return RedirectToPage("Lager");
        }

    }
}

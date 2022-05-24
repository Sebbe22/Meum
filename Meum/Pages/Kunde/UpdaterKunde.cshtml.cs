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
    public class UpdaterKundeModel : PageModel
    {
        private IKundeKatalog _kundeData;

        [BindProperty]
        public Kunde kunde { get; set; }

        public UpdaterKundeModel(IKundeKatalog data)
        {
            _kundeData = data;
        }
        public void OnGet(string Tlf)
        {

            kunde = _kundeData.GetPersonByPhoneNo(Tlf);

        }

        public IActionResult OnPost(string Tlf)
        {
            _kundeData.UpdateKunde(Tlf, kunde);
            return RedirectToPage("KunderView");
        }
    }
}

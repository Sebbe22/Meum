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
    public class LagerModel : PageModel
    {

        private EnhedKatalog _enhedData;

        public LagerModel(EnhedKatalog data)
        {
            _enhedData = data;
        }

        public EnhedKatalog EnhedData
        {
            get => _enhedData;
            set => _enhedData = (EnhedKatalog)value;
        }

        public List<Enhed> EnhedList = new List<Enhed>();

        public IActionResult OnGet()
        {
            EnhedList = _enhedData.GetAllEnheder();
            return Page();
        }
    }
}

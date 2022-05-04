using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Meum.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Meum.Pages
{
    public class LagerModel : PageModel
    {

        private EnhedDatabase _enhedData;

        public LagerModel(EnhedDatabase data)
        {
            _enhedData = data;
        }

        public EnhedDatabase EnhedData
        {
            get => _enhedData;
            set => _enhedData = (EnhedDatabase)value;
        }

        public List<Enhed> EnhedList = new List<Enhed>();

        public IActionResult OnGet()
        {
            EnhedList = _enhedData.GetAllEnheder();
            return Page();
        }
    }
}

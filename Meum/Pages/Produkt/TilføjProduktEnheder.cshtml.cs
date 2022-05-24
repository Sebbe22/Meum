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
    public class TilføjProduktEnhederModel : PageModel
    {
        private EnhedKatalog _enhed;
        private ProduktEnhederKatalog _produktEnheder;


        [BindProperty]
        public Enhed Enhed { get; set; }
        [BindProperty]
        public ProduktEnheder pe { get; set; }


        [BindProperty]
        public int antal { get; set; }
        [BindProperty]
        public string enhed { get; set; }
        public int enhedId { get; set; }
        public List<Enhed> Enheder { get; set; }



        public TilføjProduktEnhederModel(EnhedKatalog data, ProduktEnhederKatalog data1)
        {
            _enhed = data;
            _produktEnheder = data1;
        }
        public IActionResult OnGet()
        {
            Enheder = _enhed.GetAllEnheder();

            return Page();
        }

        public IActionResult OnPost(int Id)
        {
            Enheder = _enhed.GetAllEnheder();
            Enhed = _enhed.GetEnhedByName(enhed.Trim());
            enhedId = Enhed.VareId;
            pe = new ProduktEnheder(Id, enhedId, antal);
            _produktEnheder.AddProduktEnhed(pe);



            return Page();
        }
    }
}

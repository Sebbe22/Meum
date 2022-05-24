using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Meum.Catalog;
using Meum.Model;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Meum.Pages
{
    public class LoginModel : PageModel
    {
        public static User LoggedInUser { get; set; } = null;
        private readonly ILoginCatalog _userService;



        public LoginModel(ILoginCatalog userService)
        {
            _userService = userService;
        }



        [BindProperty]
        public String UserName { get; set; }

        [BindProperty]
        [DataType(DataType.Password)]
        public String Password { get; set; }

        public String Message { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            User user = new User(UserName, Password);

            if (_userService.Contains(user))
            {
                LoggedInUser = user;

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, UserName),
                    
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity));
                return RedirectToPage("Oversigt");
            }

            Message = "Invalid attempt";
            return Page();

        }
    }
}

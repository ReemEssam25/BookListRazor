using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BookListRazor.Model;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookListRazor.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly ApplicationDBContext _db;

        public LoginModel(ApplicationDBContext db)
        {
            _db = db;
        }

        [BindProperty]
        public new User User{ get; set; }
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = _db.User.FirstOrDefault(o => o.UserName == User.UserName && o.Password == User.Password);
            var c = _db.Credentials.FirstOrDefault(o => o.UserName == User.UserName && o.Password == User.Password);

            if (user != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, c.UserName),
                    new Claim(ClaimTypes.Email, c.mail)
                };

                //Nice try
                var identity = new ClaimsIdentity(claims, "MyCookieAuth");
                ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync("MyCookieAuth", claimsPrincipal);

                return RedirectToPage("/Index");
            }

            return Page();
        }
    }
}

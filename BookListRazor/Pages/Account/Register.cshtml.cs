using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BookListRazor.Model;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BookListRazor.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly ApplicationDBContext _db;

        public RegisterModel(ApplicationDBContext db)
        {
            _db = db;
        }

        [BindProperty]
        public Credential Credential { get; set; }
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            var c = _db.Credentials.FirstOrDefault(o => o.UserName == Credential.UserName);

   
            if (ModelState.IsValid && c== null)
            {

                User user =  new Model.User();
                user.UserName = Credential.UserName;
                user.Password = Credential.Password;

                await _db.Credentials.AddAsync(Credential);
                await _db.User.AddAsync(user);
                await _db.SaveChangesAsync();

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, Credential.UserName),
                    new Claim(ClaimTypes.Email, Credential.mail)
                };

                var identity = new ClaimsIdentity(claims, "MyCookieAuth");
                ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync("MyCookieAuth", claimsPrincipal);

                return RedirectToPage("/Index");
            }
            else
            {
                return Page();
            }
        }
    }
}

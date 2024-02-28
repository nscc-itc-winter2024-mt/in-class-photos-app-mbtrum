using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Photos.Data;
using Photos.Models;
using System.Security.Claims;

namespace Photos.Pages.Users
{
    public class LoginModel : PageModel
    {
        private readonly PhotosContext _context;

        [BindProperty]
        public string UserName { get; set; } = string.Empty;

        [BindProperty]
        public string Password { get; set; } = string.Empty;

        public LoginModel (PhotosContext context)
        {
            _context = context;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {            
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Find the input username in the database
            User? userDb = await _context.User.SingleOrDefaultAsync(m => m.UserName == UserName);

            if (userDb == null)
            {
                // Invalid username
                return Page();
            }

            // Verify the password entered
            bool validPassword = BCrypt.Net.BCrypt.Verify(Password, userDb.Password);

            if(validPassword)
            {
                //
                // Initialize user session
                //

                // Create Claims, 1 custom one
                List<Claim> claims = new List<Claim>
                { 
                    new Claim(ClaimTypes.Name, userDb.UserId.ToString()),
                    new Claim(ClaimTypes.Role, "Member"),
                    new Claim("FullName", userDb.FirstName + " " + userDb.LastName)
                };

                // Create Claims Identity
                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                // Sign in user
                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    new AuthenticationProperties());


                return RedirectToPage("/PhotoAdmin/Index");
            }
            else
            {
                // Invalid password
                return Page();
            }            
        }
    }
}

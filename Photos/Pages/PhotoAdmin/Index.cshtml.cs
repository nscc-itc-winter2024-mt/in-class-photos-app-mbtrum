using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Photos.Data;
using Photos.Models;

namespace Photos.Pages.PhotoAdmin
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly Photos.Data.PhotosContext _context;

        public IndexModel(Photos.Data.PhotosContext context)
        {
            _context = context;
        }

        public IList<Photo> Photos { get;set; } = default!;

        public async Task OnGetAsync()
        {
            // Get the user id from the "name" claims
            int userId = Int32.Parse(User.Identity.Name);

            // Add an Include() to join the Photo to Category when executing the SQL statement.
            Photos = await _context.Photo.Where(m => m.User.UserId == userId).Include("User").Include("Category").ToListAsync();           
        }
    }
}

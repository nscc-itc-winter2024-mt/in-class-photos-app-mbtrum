using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Photos.Data;
using Photos.Models;

namespace Photos.Pages.PhotoAdmin
{
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
            // Add an Include() to join the Photo to Category when executing the SQL statement.
            Photos = await _context.Photo.Include("Category").ToListAsync();           
        }
    }
}

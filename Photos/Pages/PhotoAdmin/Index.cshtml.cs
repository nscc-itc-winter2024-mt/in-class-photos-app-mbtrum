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

        public IList<Photo> Photo { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Photo = await _context.Photo.ToListAsync();
        }
    }
}

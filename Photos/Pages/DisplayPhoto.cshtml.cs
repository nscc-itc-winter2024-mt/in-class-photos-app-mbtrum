using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Photos.Data;
using Photos.Models;


namespace Photos.Pages
{
    public class DisplayPhotoModel : PageModel
    {
        private readonly Photos.Data.PhotosContext _context;

        public Photo Photo { get; set; } = default!;

        // Constructor
        public DisplayPhotoModel(PhotosContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var photo = await _context.Photo.FirstOrDefaultAsync(m => m.PhotoId == id);

            if (photo == null)
            {
                return NotFound();
            }
            else
            {
                Photo = photo;
            }
            return Page();
        }
    }
}

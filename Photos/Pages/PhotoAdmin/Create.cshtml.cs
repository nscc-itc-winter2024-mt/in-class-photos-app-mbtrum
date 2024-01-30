using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Photos.Data;
using Photos.Models;

namespace Photos.Pages.PhotoAdmin
{
    public class CreateModel : PageModel
    {
        private readonly Photos.Data.PhotosContext _context;

        [BindProperty]
        public Photo Photo { get; set; } = default!;

        public CreateModel(Photos.Data.PhotosContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }      

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Set the Publish Date for the photo
            Photo.PublishDate = DateTime.Now;

            // Update the .net context
            _context.Photo.Add(Photo);

            // Sync .net context with database (execute insert command)
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}

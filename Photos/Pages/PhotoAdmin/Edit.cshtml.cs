using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Photos.Data;
using Photos.Models;

namespace Photos.Pages.PhotoAdmin
{
    [Authorize]
    public class EditModel : PageModel
    {
        private readonly Photos.Data.PhotosContext _context;

        [BindProperty]
        public Photo Photo { get; set; } = default!;

        // Category select options
        public List<SelectListItem> CategoryOptions { get; set; } = new List<SelectListItem>();

        public EditModel(PhotosContext context)
        {
            _context = context;

            //
            // Populate the category select options
            //

            // get all the categories in table
            List<Category> categories = _context.Category.ToList();

            foreach (var category in categories)
            {
                CategoryOptions.Add(new SelectListItem
                {
                    Text = category.Title,
                    Value = category.CategoryId.ToString()
                });
            }
        }        

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var photo =  await _context.Photo.FirstOrDefaultAsync(m => m.PhotoId == id);
            if (photo == null)
            {
                return NotFound();
            }
            Photo = photo;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            // Set the category for the Photo object based on user's selection
            Category selectCategory = _context.Category.Single(m => m.CategoryId == Photo.Category.CategoryId);
            Photo.Category = selectCategory;

            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Photo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PhotoExists(Photo.PhotoId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool PhotoExists(int id)
        {
            return _context.Photo.Any(e => e.PhotoId == id);
        }
    }
}

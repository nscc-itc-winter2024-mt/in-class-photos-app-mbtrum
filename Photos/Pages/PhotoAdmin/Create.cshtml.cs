using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Hosting;
using Photos.Data;
using Photos.Models;

namespace Photos.Pages.PhotoAdmin
{
    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly PhotosContext _context;
        private readonly IHostEnvironment _environment;

        [BindProperty]
        public Photo Photo { get; set; } = default!;

        [BindProperty]
        [DisplayName("Upload Photo")]
        public IFormFile FileUpload { get; set; }

        // Category select options
        public List<SelectListItem> CategoryOptions { get; set; } = new List<SelectListItem>();

        public CreateModel(PhotosContext context, IHostEnvironment environment)
        {
            _context = context;
            _environment = environment;

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
                    Value= category.CategoryId.ToString()
                });
            }
        }

        public IActionResult OnGet()
        {
            return Page();
        }    
        
        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            // Set the category for the Photo object based on user's selection
            Category selectCategory = _context.Category.Single(m => m.CategoryId == Photo.Category.CategoryId);
            Photo.Category = selectCategory;

            // Set the Publish Date for the photo
            Photo.PublishDate = DateTime.Now;

            if (!ModelState.IsValid)
            {
                return Page();
            }

            //
            // Upload file to server
            //

            // Make a unique filename
            string filename = DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss-fff_") +  FileUpload.FileName;

            // Update Photo object to include the photo filename
            Photo.FileName = filename;

            // Save the file
            string projectRootPath = _environment.ContentRootPath;
            string fileSavePath = Path.Combine(projectRootPath, "wwwroot", "uploads", filename);

            // We use a "using" to ensure the filestream is disposed of when we're done with it
            using (FileStream fileStream = new FileStream(fileSavePath, FileMode.Create))
            {
                FileUpload.CopyTo(fileStream);
            }

            // Update the .net context
            _context.Photo.Add(Photo);

            // Sync .net context with database (execute insert command)
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}

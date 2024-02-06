using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Photos.Data;
using Photos.Models;

namespace Photos.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly PhotosContext _context;

        public IList<Photo> Photos { get; set; } = default!;

        public IndexModel(ILogger<IndexModel> logger, PhotosContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task OnGetAsync()
        {
            Photos = await _context.Photo.ToListAsync();
        }
    }
}

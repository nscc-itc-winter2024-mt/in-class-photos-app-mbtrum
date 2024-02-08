using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Photos.Models;

namespace Photos.Data
{
    public class PhotosContext : DbContext
    {
        public PhotosContext (DbContextOptions<PhotosContext> options)
            : base(options)
        {
        }

        public DbSet<Photos.Models.Photo> Photo { get; set; } = default!;
        public DbSet<Photos.Models.Category> Category { get; set; } = default!;
    }
}

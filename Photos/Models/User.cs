using Microsoft.EntityFrameworkCore;

namespace Photos.Models
{
    [Index(nameof(UserName), IsUnique=true)]
    public class User
    {        
        public int UserId { get; set; }
        public string UserName { get; set; } = string.Empty;    
        public string Password { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public List<Photo> Photos { get; set; } = new();
    }
}

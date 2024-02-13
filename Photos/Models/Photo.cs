using System.Data;

namespace Photos.Models
{
    public class Photo
    {
        public int PhotoId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string FileName { get; set; } = string.Empty;
        public DateTime PublishDate { get; set; }
        public Category Category { get; set; } = new();
    }
}

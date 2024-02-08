namespace Photos.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string Title { get; set; } = string.Empty;
        public List<Photo> Photos { get; set; } = default!;
    }
}

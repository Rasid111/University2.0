namespace UniversityWebapi.Models
{
    public class New
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public required string Content { get; set; }
        public string? PictureUrl { get; set; }
    }
}

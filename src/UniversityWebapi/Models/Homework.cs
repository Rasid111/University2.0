namespace UniversityWebapi.Models
{
    public class Homework
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string? PictureUrl { get; set; }
        public DateTime StartDate { get; set; } = DateTime.Now;
        public DateTime EndDate { get; set; } = DateTime.Now.AddDays(7);

        public int TeachingAssignmentId { get; set; }
        public TeachingAssignment? TeachingAssignment { get; set; }
    }
}

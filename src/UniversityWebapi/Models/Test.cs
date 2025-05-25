namespace UniversityWebapi.Models
{
    public class Test
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        
        public int TeachingAssignmentId { get; set; }
        public TeachingAssignment TeachingAssignment { get; set; } = null!;
        public List<Question> Questions { get; set; } = [];
    }
}

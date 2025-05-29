namespace UniversityWebapi.Models
{
    public class StudentProfile
    {
        public int Id { get; set; }

        public required string UserId { get; set; }
        public User User { get; set; } = null!;
        public int GroupId { get; set; }
        public Group Group { get; set; } = null!;
    }
}

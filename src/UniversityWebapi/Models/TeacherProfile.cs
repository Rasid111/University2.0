namespace UniversityWebapi.Models
{
    public class TeacherProfile
    {
        public int Id { get; set; }

        public required string UserId { get; set; }
        public User User { get; set; } = null!;
        public int FacultyId { get; set; }
        public Faculty Faculty { get; set; } = null!;
        public int DegreeId { get; set; }
        public Degree Degree { get; set; } = null!;
    }
}

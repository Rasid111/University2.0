namespace UniversityWebapi.Models
{
    public class TeacherProfile
    {
        public int Id { get; set; }

        public int FacultyId { get; set; }
        public Faculty? Faculty { get; set; }
        public int DegreeId { get; set; }
        public Degree Degree { get; set; } = null!;
    }
}

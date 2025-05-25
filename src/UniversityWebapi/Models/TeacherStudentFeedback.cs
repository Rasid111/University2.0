namespace UniversityWebapi.Models
{
    public class TeacherStudentFeedback
    {
        public int Id { get; set; }
        public required string Feedback { get; set; }

        public int TeacherProfileId { get; set; }
        public TeacherProfile TeacherProfile { get; set; } = null!;
        public int StudentProfileId { get; set; }
        public StudentProfile StudentProfile { get; set; } = null!;
    }
}

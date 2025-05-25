namespace UniversityWebapi.Models
{
    public class StudentTeacherFeedback
    {
        public int Id { get; set; }
        public required string Feedback { get; set; }

        public int StudentProfileId { get; set; }
        public StudentProfile StudentProfile { get; set; } = null!;
        public int TeacherProfileId { get; set; }
        public TeacherProfile TeacherProfile { get; set; } = null!;
    }
}

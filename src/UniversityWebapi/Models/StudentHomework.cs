namespace UniversityWebapi.Models
{
    public class StudentHomework
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? HomeworkUrl { get; set; }
        public HomeworkStatus HomeworkStatus { get; set; } = HomeworkStatus.NotSubmitted;
        public int Score { get; set; }

        public int StudentProfileId { get; set; }
        public StudentProfile StudentProfile { get; set; } = null!;
        public int HomeworkId { get; set; }
        public Homework Homework { get; set; } = null!;
    }
}

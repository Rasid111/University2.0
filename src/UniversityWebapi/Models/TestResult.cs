namespace UniversityWebapi.Models
{
    public class TestResult
    {
        public int Id { get; set; }
        public int Score { get; set; }

        public int StudentProfileId { get; set; }
        public StudentProfile StudentProfile { get; set; } = null!;
        public int TestId { get; set; }
        public Test Test { get; set; } = null!;
        public List<StudentAnswer> StudentAnswers { get; set; } = [];
    }
}

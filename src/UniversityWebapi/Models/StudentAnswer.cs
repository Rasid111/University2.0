using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace UniversityWebapi.Models
{
    public class StudentAnswer
    {
        public int Id { get; set; }
        public string? Answer { get; set; }

        public int TestResultId { get; set; }
        public TestResult TestResult { get; set; } = null!;
        public int QuestionId { get; set; }
        public Question Question { get; set; } = null!;
    }
}

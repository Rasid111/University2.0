using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace UniversityWebapi.Models
{
    public class Question
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public required string CorrectAnswerTitle { get; set; }
        public string? PictureUrl { get; set; }

        public int TestId { get; set; }
        public Test Test { get; set; } = null!;
        public List<QuestionAnswer> Answers { get; set; } = [];
    }
}

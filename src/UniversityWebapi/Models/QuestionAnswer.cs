namespace UniversityWebapi.Models
{
    public class QuestionAnswer
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public string? PictureUrl { get; set; }

        public int TestQuestionId { get; set; }
        public Question TestQuestion { get; set; } = null!;
    }
}
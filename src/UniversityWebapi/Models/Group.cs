namespace UniversityWebapi.Models
{
    public class Group
    {
        public int Id { get; set; }
        public required string Name { get; set; }

        public int MajorId { get; set; }
        public Major Major { get; set; } = null!;
        public List<StudentProfile> Students { get; set; } = [];
    }
}

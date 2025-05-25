namespace UniversityWebapi.Models
{
    public class Major
    {
        public int Id { get; set; }
        public required string Name { get; set; }

        public int FacultyId { get; set; }
        public Faculty Faculty { get; set; } = null!;
    }
}

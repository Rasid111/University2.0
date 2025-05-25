namespace UniversityWebapi.Models
{
    public class Faculty
    {
        public int Id { get; set; }
        public required string Name { get; set; }

        public List<Major> Majors { get; set; } = [];
    }
}
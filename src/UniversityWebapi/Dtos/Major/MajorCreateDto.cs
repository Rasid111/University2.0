namespace UniversityWebapi.Dtos.Major
{
    public class MajorCreateDto
    {
        public required string Name { get; set; }
        public int FacultyId { get; set; }
    }
}

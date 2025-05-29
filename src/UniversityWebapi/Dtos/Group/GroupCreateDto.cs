namespace UniversityWebapi.Dtos.Group
{
    public class GroupCreateDto
    {
        public required string Name { get; set; }
        public int MajorId { get; set; }
    }
}
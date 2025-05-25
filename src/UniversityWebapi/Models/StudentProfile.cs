namespace UniversityWebapi.Models
{
    public class StudentProfile
    {
        public int Id { get; set; }

        public int GroupId { get; set; }
        public Group? Group { get; set; }
    }
}

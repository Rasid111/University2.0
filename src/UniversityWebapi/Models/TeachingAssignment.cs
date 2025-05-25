using System.Security.Cryptography.X509Certificates;

namespace UniversityWebapi.Models
{
    public class TeachingAssignment
    {
        public int Id { get; set; }

        public int TeacherProfileId { get; set; }
        public TeacherProfile TeacherProfiel { get; set; } = null!;
        public int GroupId { get; set; }
        public Group Group { get; set; } = null!;
        public int SubjectId { get; set; }
        public Subject Subject { get; set; } = null!;
        public List<ScheduleElement> ScheduleElements { get; set; } = [];
    }
}

using Microsoft.AspNetCore.Identity;

namespace UniversityWebapi.Models
{
    public class User : IdentityUser
    {
        public required string Name { get; set; }
        public required string Surname { get; set; }
        public string? ProfilePictureUrl { get; set; }

        public int? StudentProfileId { get; set; }
        public StudentProfile? StudentProfile { get; set; }
        public int? TeacherProfileId { get; set; }
        public TeacherProfile? TeacherProfile { get; set; }
    }
}

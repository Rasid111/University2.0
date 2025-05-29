namespace UniversityWebapi.Dtos.User
{
    public class UserUpdateDto
    {
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? ProfilePictureUrl { get; set; }
        public required string Password { get; set; }
        public string? NewPassword { get; set; }
    }
}

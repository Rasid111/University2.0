namespace UniversityWebapi.Models
{
    public class Grade
    {
        public int Id { get; set; }
        public int Value { get; set; }
        public string? Note { get; set; }

        public int StudentProfileId { get; set; }
        public StudentProfile StudentProfile { get; set; } = null!;
        public int ScheduleElementId { get; set; }
        public ScheduleElement ScheduleElement { get; set; } = null!;
    }
}

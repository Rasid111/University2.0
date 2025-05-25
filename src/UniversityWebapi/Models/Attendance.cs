namespace UniversityWebapi.Models
{
    public class Attendance
    {
        public int Id { get; set; }
        public PresenceStatus PresenceStatus { get; set; } = PresenceStatus.Present;
        public string? Note { get; set; }
        public DateTime Date { get; set; }

        public int StudentProfileId { get; set; }
        public StudentProfile StudentProfile { get; set; } = null!;
        public int ScheduleElementId { get; set; }
        public ScheduleElement ScheduleElement { get; set; } = null!;
    }
}
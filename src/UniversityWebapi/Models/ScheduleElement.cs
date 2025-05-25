using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace UniversityWebapi.Models
{
    public class ScheduleElement
    {
        public int Id { get; set; }
        public int Classroom { get; set; }
        public TimeOnly StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
        public WeekType WeekType { get; set; }
        
        public int TeachingAssignmentId { get; set; }
        public TeachingAssignment TeachingAssignment { get; set; } = null!;
    }
}

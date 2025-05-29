using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UniversityWebapi.Models;

namespace UniversityWebapi.Database
{
    public class UniversityDbContext(DbContextOptions options) : IdentityDbContext<User, IdentityRole, string>(options)
    {
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<StudentProfile> StudentProfiles { get; set; }
        public DbSet<TeacherProfile> TeacherProfiles { get; set; }
        public DbSet<Attendance> Attendances { get; set; }
        public DbSet<StudentHomework> StudentHomework { get; set; }
        public DbSet<Homework> Homeworks { get; set; }
        public DbSet<Faculty> Faculties { get; set; }
        public DbSet<Major> Majors { get; set; }
        public DbSet<Degree> Degrees { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Grade> Grades { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Test> Tests { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<QuestionAnswer> QuestionAnswer { get; set; }
        public DbSet<TestResult> TestResults { get; set; }
        public DbSet<StudentAnswer> StudentAnswers { get; set; }
        public DbSet<New> News { get; set; }
        public DbSet<TeachingAssignment> TeachingAssignments { get; set; }
        public DbSet<ScheduleElement> ScheduleElements { get; set; }
        public DbSet<StudentTeacherFeedback> StudentTeacherFeedbacks { get; set; }
        public DbSet<TeacherStudentFeedback> TeacherStudentFeedbacks { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder
                .Entity<RefreshToken>()
                .HasKey(rt => rt.Token);
            builder
                .Entity<User>()
                .HasOne(u => u.StudentProfile)
                .WithOne(sp => sp.User)
                .HasForeignKey<StudentProfile>(sp => sp.UserId);
            builder
                .Entity<User>()
                .HasOne(u => u.TeacherProfile)
                .WithOne(tp => tp.User)
                .HasForeignKey<TeacherProfile>(tp => tp.UserId);
        }
    }
}

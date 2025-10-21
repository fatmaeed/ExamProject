using ExamProject.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ExamProject.Infrastructure.Data {

    public class ExamDbContext : IdentityDbContext<ApplicationUser, IdentityRole<int>, int> {
        public virtual DbSet<ExamEntity> Exams { get; set; }
        public virtual DbSet<UserExamEntity> UserExams { get; set; }
        public virtual DbSet<QuestionEntity> Questions { get; set; }
        public virtual DbSet<UserExamQuestionEntity> UserExamQuestions { get; set; }

        public ExamDbContext(DbContextOptions<ExamDbContext> options) : base(options) {
        }

        protected override void OnModelCreating(ModelBuilder builder) {
            base.OnModelCreating(builder);

            builder.Entity<UserExamQuestionEntity>()
                    .HasOne(uq => uq.User)
                    .WithMany()
                    .HasForeignKey(uq => uq.Id)
                    .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<UserExamQuestionEntity>()
                .HasOne(uq => uq.Question)
                .WithMany()
                .HasForeignKey(uq => uq.QuestionId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<UserExamQuestionEntity>()
                .HasOne(uq => uq.Exam)
                .WithMany() // أو .WithMany(e => e.UserExamQuestions)
                .HasForeignKey(uq => uq.ExamId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.Entity<UserExamEntity>().Property(x => x.IsCompleted).HasDefaultValue(false);
            builder.Entity<ExamEntity>()
                .Property(x => x.EndTime)
                .HasComputedColumnSql("DATEADD(SECOND, DATEDIFF(SECOND, 0, [Duration]), [StartTime])", stored: true);
        }
    }
}
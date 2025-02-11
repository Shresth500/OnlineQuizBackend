using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OnlineQuizBackend.Models.Domain;
using System.Reflection.Emit;

namespace OnlineQuizBackend.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Quizzes> Quizzes { get; set; }
        public DbSet<Questions> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }  
        public DbSet<UserQuizAttendee> UserQuizAttendee { get; set; }
        public DbSet<Options> Options { get; set; }
        public DbSet<UserAnswer> UserAnswer { get; set; }
        public DbSet<UserAnswersList> userAnswersLists { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);


            var readerRoleId = "a71a55d6-99d7-4123-b4e0-1218ecb90e3e";
            var writerRoleId = "c309fa92-2123-47be-b397-a1c77adb502c";
            var adminRoleId = "b78d4f92-56c8-4f33-8b59-9a1d7b23f15a";
            var userRoleId = "d73b4f98-62d9-4g38-9c60-8b2e6a12e14b";

            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Id = readerRoleId,
                    ConcurrencyStamp = readerRoleId,
                    Name = "Reader",
                    NormalizedName = "Reader".ToUpper()
                },
                new IdentityRole
                {
                    Id = writerRoleId,
                    ConcurrencyStamp = writerRoleId,
                    Name = "Writer",
                    NormalizedName = "Writer".ToUpper()
                },
                new IdentityRole
                {
                    Id = adminRoleId,
                    ConcurrencyStamp = adminRoleId,
                    Name = "Admin",
                    NormalizedName = "Admin".ToUpper()
                },
                new IdentityRole
                {
                    Id = userRoleId,
                    ConcurrencyStamp = userRoleId,
                    Name = "User",
                    NormalizedName = "User".ToUpper()
                }
            };

            builder.Entity<IdentityRole>().HasData(roles);

            builder.Entity<UserQuizAttendee>().HasKey(a => new { a.ApplicationUserId, a.QuizzesId });

            builder.Entity<UserAnswer>().HasKey(a => new { a.ApplicationUserId,a.QuizzesId, a.QuestionsId });

            builder.Entity<UserAnswersList>().HasKey(a => new { a.ApplicationUserId, a.QuizzesId,a.QuestionsId});



            builder.Entity<UserQuizAttendee>()
                .HasOne(uq => uq.ApplicationUser)
                .WithMany(u => u.QuizAttended)
                .HasForeignKey(uq => uq.ApplicationUserId);

            builder.Entity<UserQuizAttendee>()
                .HasOne(uq => uq.Quizzes)
                .WithMany(q => q.QuizAttended)
                .HasForeignKey(uq => uq.QuizzesId);

            builder.Entity<UserAnswer>()
                    .HasOne(ua => ua.Questions)
                    .WithMany(q => q.UserAnswers)
                    .HasForeignKey(ua => ua.QuestionsId);


            // One-to-Many: Quiz -> Questions
            builder.Entity<Questions>()
                .HasOne(q => q.Quizzes)
                .WithMany(quiz => quiz.QuizQuestions)
                .HasForeignKey(q => q.QuizzesId);

            // One-to-Many: Question -> Answers
            builder.Entity<Answer>()
                .HasOne(a => a.Questions)
                .WithOne(q => q.Answers)
                .HasForeignKey<Questions>(a => a.QuestionId);

            // One-to-Many: UserQuizAttempt -> UserAnswer
            builder.Entity<UserAnswer>()
                .HasOne(ua => ua.Attempt)
                .WithMany(uq => uq.UserAnswers)
                .HasForeignKey(ua => new { ua.ApplicationUserId, ua.QuizzesId });

            // One-to-Many: Question -> UserAnswer
            builder.Entity<UserAnswer>()
                .HasOne(ua => ua.Questions)
                .WithMany(q => q.UserAnswers)
                .HasForeignKey(ua => ua.QuestionsId);

            builder.Entity<Options>()
                .HasOne(ua => ua.Question)
                .WithMany(q => q.Options)
                .HasForeignKey(ua => ua.QuestionId);



        }

    }
}

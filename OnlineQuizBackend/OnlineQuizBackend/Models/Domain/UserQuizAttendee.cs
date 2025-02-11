using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace OnlineQuizBackend.Models.Domain
{
    public class UserQuizAttendee
    {
        public int? Score { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public List<UserAnswer>? UserAnswers { get; set; }
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public int QuizzesId { get; set; }
        public Quizzes Quizzes { get; set; }
    }
}

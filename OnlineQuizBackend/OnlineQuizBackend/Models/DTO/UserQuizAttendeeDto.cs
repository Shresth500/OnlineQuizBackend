using OnlineQuizBackend.Models.Domain;

namespace OnlineQuizBackend.Models.DTO
{
    public class UserQuizAttendeeDto
    {
        public int AttemptId { get; set; }
        public int? Score { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public List<UserAnswerDto>? UserAnswers { get; set; }


        public string ApplicationUserId { get; set; }
        public int QuizzesId { get; set; }
    }
}

using OnlineQuizBackend.Models.Domain;

namespace OnlineQuizBackend.Models.DTO
{
    public class UserAnswerDto
    {
        public int? UserAnswerId { get; set; }
        public bool? isCorrect { get; set; }

        // Composite Foreign Key
        //public string ApplicationUserId { get; set; }
        public int QuizzesId { get; set; }
        public string QuizzText { get; set; }
        public int QuestionsId { get; set; }
        public string QuestionsText { get; set; }
        public int AnswerId { get; set; }
        public string AnswerText { get; set; }
    }
}

using OnlineQuizBackend.Models.Domain;

namespace OnlineQuizBackend.Models.DTO
{
    public class QuestionDto
    {
        public string QuestionText { get; set; }
        public string Type { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public int QuizzesId { get; set; }
        //public Quizzes Quizzes { get; set; }

        public string Answers { get; set; }

        public List<string>? Options { get; set; }
        public List<userAnswer>? UserAnswers { get; set; }
    }

    public class userAnswer
    {
        public string user { get; set; }
        public string answer { get; set; }
    }
}

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace OnlineQuizBackend.Models.Domain
{
    public class Questions
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int QuestionId { get; set; }
        public string QuestionText {  get; set; }
        public string Type { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public int QuizzesId { get; set; }
        public Quizzes Quizzes { get; set; }

        public Answer Answers { get; set; }

        public List<Options>? Options { get; set; }
        public List<UserAnswer> UserAnswers { get; set; }

    }
}

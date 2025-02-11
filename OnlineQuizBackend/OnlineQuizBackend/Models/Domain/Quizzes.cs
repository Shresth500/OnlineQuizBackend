using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineQuizBackend.Models.Domain
{
    public class Quizzes
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int QuizId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public List<UserQuizAttendee>? QuizAttended { get; set; }
        public List<Questions>? QuizQuestions { get; set; }
    }
}

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace OnlineQuizBackend.Models.Domain
{
    public class Answer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int QuestionsId { get; set; }
        public Questions Questions { get; set; }
        public int AnswerId { get; set; }
        public string AnswerText { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}

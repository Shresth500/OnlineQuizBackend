using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineQuizBackend.Models.Domain
{
    public class UserAnswersList
    {
        //[Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //public int UserAnswerListId { get; set; }
        public string ApplicationUserId { get; set; }
        public int QuizzesId { get; set; }
        public int QuestionsId { get; set; }
        public UserAnswer UserAnswer { get; set; }
        public string UserAnswerTexts { get; set; }
    }
}

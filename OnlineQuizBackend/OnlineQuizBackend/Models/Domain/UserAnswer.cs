using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace OnlineQuizBackend.Models.Domain
{
    public class UserAnswer
    {
        public bool isCorrect { get; set; }
        // Composite Foreign Key
        public string ApplicationUserId { get; set; }
        public int QuizzesId { get; set; }
        public int QuestionsId { get; set; }
        public UserQuizAttendee Attempt { get; set; }
        public Questions Questions { get; set; }
        public string UserAnswerText { get; set; }
        //public List<UserAnswersList>? userAnswersLists {  get; set; } 

    }
}

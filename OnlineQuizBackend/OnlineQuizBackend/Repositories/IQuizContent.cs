using OnlineQuizBackend.Controllers;
using OnlineQuizBackend.Models.Domain;
using OnlineQuizBackend.Models.DTO;
using System.Security.Cryptography.X509Certificates;

namespace OnlineQuizBackend.Repositories
{
    public interface IQuizContent
    {
        Task<List<ApplicationUser>> GetParticpants(string? name);
        Task<ApplicationUser> GetParticpant(string? email);
        Task<List<Quizzes>> GetQuizzes(int? id);
        Task<List<Quizzes>> PostQuizz(string Title, string Description);
        Task<Questions> PostQuestionAnswer(int quizId, QuestionDto QuizQuestion);
        Task<Quizzes> UpdateQuizzTitleOrDescription(QuizzResponse quizzResponse, int Id);
        //Task<UserAnswer> PostAnswerForAQuestion(int quizzId, int questionId,string UserId, List<string> Answer);
        Task<UserAnswer> TakeQuiz(int quizzId, int questionId, string userEmail, GetAnswer getAnswerFromUser);
    }
}

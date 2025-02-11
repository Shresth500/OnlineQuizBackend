using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using OnlineQuizBackend.Controllers;
using OnlineQuizBackend.Data;
using OnlineQuizBackend.Models.Domain;
using OnlineQuizBackend.Models.DTO;
using OnlineQuizBackend.Repositories;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Xml.Linq;

namespace OnlineQuizBackend.Repositories
{
    public class SqlQuizzWorkshop : IQuizContent
    {
        private ApplicationDbContext _db;

        public SqlQuizzWorkshop(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<ApplicationUser> GetParticpant(string email)
        {
            var data = await _db.Users.Include(a => a.QuizAttended).ThenInclude(b => b.UserAnswers).Where(a => a.UserName == email).FirstOrDefaultAsync();
            return data;
        }
        public async Task<List<ApplicationUser>> GetParticpants(string? name)
        {
            var data = _db.Users.Include(a => a.QuizAttended).ThenInclude(b => b.UserAnswers).AsQueryable();
            if(name != null)
            {
                data = _db.Users.Include(a => a.QuizAttended).ThenInclude(b => b.UserAnswers).Where(a => a.UserName == name).AsQueryable();
            }
            var users = await data.ToListAsync();
            return users;
        }
        public async Task<List<Quizzes>> GetQuizzes(int? id)
        {
            var quizzes = _db.Quizzes.Include(x => x.QuizAttended).ThenInclude(y => y.UserAnswers).Include(a => a.QuizQuestions).ThenInclude(b => b.Answers).Include(c => c.QuizQuestions).ThenInclude(d => d.Options).AsQueryable();
            if(id != null)
            {
                quizzes = _db.Quizzes.Include(x => x.QuizAttended).ThenInclude(y => y.UserAnswers).Include(a => a.QuizQuestions).ThenInclude(b => b.Answers).Include(c => c.QuizQuestions).ThenInclude(d => d.Options).Where(x => x.QuizId == id).AsQueryable();
            }
            var data = await quizzes.ToListAsync();
            return data;
        }
        public async Task<List<Quizzes>> PostQuizz(string Title,string Description)
        {
            var quizz = new Quizzes
            {
                Title = Title,
                Description = Description
            };
            await _db.AddAsync(quizz);
            await _db.SaveChangesAsync();
            return await _db.Quizzes.Where(x => x.Title == Title).ToListAsync();
        }
        public async Task<Questions> PostQuestionAnswer(int quizId, QuestionDto QuizQuestion)
        {
            var checkQuiz = await _db.Quizzes.Where(x => x.QuizId == quizId).FirstOrDefaultAsync();
            if (checkQuiz == null) {
                throw new InvalidOperationException($"Quiz with Id {quizId} is not there");
            }

            var data = new Questions
            {
                Type = QuizQuestion.Type,
                QuestionText = QuizQuestion.QuestionText,
                QuizzesId = quizId,
                Answers = new Answer { AnswerText = QuizQuestion.Answers },
                Options = QuizQuestion.Options.Select(opt => new Models.Domain.Options { OptionText = opt }).ToList()
            };
            await _db.AddAsync(data);
            await _db.SaveChangesAsync();
            return data;
        }
        public async Task<Quizzes> UpdateQuizzTitleOrDescription(QuizzResponse quizzResponse, int Id)
        {
            var query = await _db.Quizzes.Where(m => m.QuizId == Id).FirstOrDefaultAsync();
            if (query == null)
            {
                throw new InvalidOperationException($"Quizz Id {Id} is not valid");
            }
            query.Description = quizzResponse.Description;
            query.Title = quizzResponse.Title;
            await _db.SaveChangesAsync();
            var quizzData = await _db.Quizzes.Include(m => m.QuizQuestions).ThenInclude(n => n.Answers).Include(x => x.QuizQuestions).ThenInclude(y => y.Options).Where(m => m.QuizId == Id).FirstOrDefaultAsync();
            return quizzData;
        }
        public async Task<UserAnswer> TakeQuiz(int quizzId,int questionId,string userEmail,GetAnswer getAnswerFromUser)
        {
            var userId = await _db.Users.Where(x => x.Email == userEmail).FirstOrDefaultAsync();
            var checkquiz = await _db.Quizzes.Where(x => x.QuizId == quizzId).FirstOrDefaultAsync();
            if(checkquiz  == null)
            {
                throw new Exception("Quiz Not Found");
            }
            var checkQuestion = await _db.Questions.Include(y => y.Answers).Where(x => x.QuizzesId == quizzId && x.QuestionId == questionId).FirstOrDefaultAsync();
            if(checkQuestion == null)
            {
                throw new Exception("Question Not Found");
            }
            bool isCorrect = (checkQuestion.Answers.AnswerText == getAnswerFromUser.Answer);
            var getAttempt = await _db.UserQuizAttendee.Where(x => x.QuizzesId ==  quizzId && x.ApplicationUserId == userId.Id).Include(x => x.UserAnswers).FirstOrDefaultAsync();
            if (getAttempt == null) {
                getAttempt = new UserQuizAttendee
                {
                    ApplicationUserId = userId.Id,
                    QuizzesId = quizzId,
                    Score = isCorrect ? 1 : 0,
                    UserAnswers = new List<UserAnswer>()
                };
                await _db.UserQuizAttendee.AddAsync(getAttempt);
            }
            else{
                getAttempt.Score += isCorrect ? 1 : 0;
            }
            var getUserAnswer = new UserAnswer
            {
                ApplicationUserId = userId.Id,
                QuizzesId = quizzId,
                QuestionsId = questionId,
                UserAnswerText = getAnswerFromUser.Answer
            };
            getAttempt.UserAnswers.Add(getUserAnswer);
            //await _db.AddAsync(getAttempt);
            //await _db.AddAsync(getAttempt);
            await _db.SaveChangesAsync();
            return getUserAnswer;
        }
    }
}

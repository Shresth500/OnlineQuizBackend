using OnlineQuizBackend.Controllers;
using OnlineQuizBackend.Models.Domain;
using System;
using System.Collections.Generic;

namespace OnlineQuizBackend.Models.DTO
{
    public class QuizzesDto
    {
        public int QuizId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public List<string>? QuizAttended { get; set; }
        public List<QuestionDto> questionAnswers { get; set; }
    }
}

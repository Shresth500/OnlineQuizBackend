using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineQuizBackend.Data;
using OnlineQuizBackend.Models.Domain;
using OnlineQuizBackend.Models.DTO;
using OnlineQuizBackend.Repositories;
using System.Security.Claims;

namespace OnlineQuizBackend.Controllers
{
    public class QuizzResponse
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
    }

    public class QuestionAnswer
    {
        public string QuestionText { get; set; }
        public string Answer { get; set; }
        public string Type {  get; set; }
        public List<string> UserAnswers { get; set; }
        public List<string>? Options { get; set; }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class QuizController:ControllerBase
    {
        public IQuizContent _repo;
        private readonly UserManager<ApplicationUser> _userManager;
        private IMapper _mapper;
        public QuizController(UserManager<ApplicationUser> userManager,IQuizContent _repo, IMapper _mapper)
        {
            this._repo = _repo;
            this._userManager = userManager;
            this._mapper = _mapper;
        }

        [HttpGet]
        [Route("Quizz")]
        [Authorize]
        public async Task<IActionResult> Quizzes()
        {
            var data = await _repo.GetQuizzes(null);
            var quizzDetails = _mapper.Map<List<QuizzesDto>>(data);
            return Ok(quizzDetails);
        }

        [HttpGet]
        [Route("Quizz/{id:int}")]
        [Authorize]
        public async Task<IActionResult> Quizz(int? id)
        {
            var data = await _repo.GetQuizzes(id);
            var quizzDetails = _mapper.Map<List<QuizzesDto>>(data);
            return Ok(quizzDetails);
        }

        [HttpPost]
        [Route("Quizz")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Quizz([FromBody] QuizzResponse quizzResponse)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // 400 response (Errors will be there)
            }

            var email = User?.FindFirst(ClaimTypes.Email)?.Value;

            if (string.IsNullOrEmpty(email))
            {
                return Unauthorized("Email not found in the claims.");
            }
            var data = await _repo.PostQuizz(quizzResponse.Title, quizzResponse.Description);
            var quizz = _mapper.Map<List<QuizzesDto>>(data);
            return Ok(quizz);
        }

        [HttpPost]
        [Route("Quizz/{Id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Quizz([FromBody] QuestionDto QuizQuestion, [FromRoute] int Id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var email = User?.FindFirst(ClaimTypes.Email)?.Value;

            if (string.IsNullOrEmpty(email))
            {
                return Unauthorized("Email not found");
            }
            var data = await _repo.PostQuestionAnswer(Id, QuizQuestion);
            var questionDetails = _mapper.Map<QuestionDto>(data);
            return Ok(questionDetails);
        }

        [HttpPatch]
        [Route("Quizz/{Id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> QuizzUpdate([FromBody] QuizzResponse Quiz, [FromRoute] int Id)
        {
            var data = await _repo.UpdateQuizzTitleOrDescription(Quiz, Id);
            var quizz = _mapper.Map<QuizzesDto>(data);
            return Ok(quizz);
        }


    }
}

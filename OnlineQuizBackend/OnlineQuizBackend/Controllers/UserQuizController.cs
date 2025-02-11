using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineQuizBackend.Models.Domain;
using OnlineQuizBackend.Models.DTO;
using OnlineQuizBackend.Repositories;
using System.Security.Claims;

namespace OnlineQuizBackend.Controllers
{
    public class GetAnswer
    {
        public string Answer { get; set; }
    }
    [Route("api/[controller]")]
    [ApiController]
    public class UserQuizController : ControllerBase
    {
        public IQuizContent _repo;
        private readonly UserManager<ApplicationUser> _userManager;
        public IMapper _mapper;

        public UserQuizController(IQuizContent repo, UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            this._repo = repo;
            this._userManager = userManager;
            this._mapper = mapper;
        }


        [HttpGet("debug-claims")]
        public IActionResult DebugClaims()
        {
            var claims = User.Claims.Select(c => new { c.Type, c.Value }).ToList();
            return Ok(claims);
        }


        [HttpPost]
        [Route("QuizAttempted/{quizzId:int}/{questionId:int}")]
        [Authorize]
        public async Task<IActionResult> PostQuizAnswer([FromRoute]int quizzId,[FromRoute]int questionId, [FromBody] GetAnswer answerFromUser)
        {
            if(answerFromUser == null)
            {
                return BadRequest("Answer cannot be null");
            }
            var userEmail = User?.FindFirst(ClaimTypes.Email)?.Value;
            if (userEmail == null)
            {
                throw new Exception("Not Found");
            }
            var data = await _repo.TakeQuiz(quizzId, questionId,userEmail,answerFromUser);
            var quizzAnswer = _mapper.Map<UserAnswerDto>(data);
            return Ok(quizzAnswer);
        }

    }
}

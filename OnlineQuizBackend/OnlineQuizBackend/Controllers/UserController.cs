using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineQuizBackend.Models.Domain;
using OnlineQuizBackend.Models.DTO;
using OnlineQuizBackend.Repositories;

namespace OnlineQuizBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public IQuizContent _repo;
        private readonly UserManager<ApplicationUser> _userManager;
        private IMapper _mapper;

        public UserController(UserManager<ApplicationUser> _userManager, IQuizContent _repo,IMapper _mapper)
        {
            this._userManager = _userManager;
            this._repo = _repo;
            this._mapper = _mapper;
        }

        [HttpGet]
        [Route("Users")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Users(string? username)
        {
            var users = await _repo.GetParticpants(username);
            var usersDto = _mapper.Map<List<UsersDto>>(users);
            return Ok(usersDto);
        }
    }
}

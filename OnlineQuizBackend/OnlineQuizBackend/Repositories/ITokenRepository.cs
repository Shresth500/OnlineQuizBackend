using Microsoft.AspNetCore.Identity;
using OnlineQuizBackend.Models.Domain;

namespace OnlineQuizBackend.Repositories
{
    public interface ITokenRepository
    {
        string CreateJWTToken(ApplicationUser user, List<string> roles);
    }
}

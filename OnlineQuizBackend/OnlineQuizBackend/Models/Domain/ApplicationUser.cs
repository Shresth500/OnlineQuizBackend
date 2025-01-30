using Microsoft.AspNetCore.Identity;

namespace OnlineQuizBackend.Models.Domain
{
    public class ApplicationUser :IdentityUser
    {
        public List<UserQuizAttendee>? QuizAttended { get; set; }
    }
}

namespace OnlineQuizBackend.Models.DTO
{
    public class UsersDto
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public List<UserQuizAttendeeDto>? Attendee { get; set; }
    }
}

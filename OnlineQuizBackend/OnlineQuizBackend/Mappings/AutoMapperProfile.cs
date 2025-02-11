using AutoMapper;
using OnlineQuizBackend.Controllers;
using OnlineQuizBackend.Models.Domain;
using OnlineQuizBackend.Models.DTO;

namespace OnlineQuizBackend.Mappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Questions, QuestionDto>().ReverseMap();

            CreateMap<Answer, string>().ConvertUsing(a => a.AnswerText);

            CreateMap<string, Answer>().ConvertUsing(text => new Answer { AnswerText = text });

            CreateMap<Options, string>().ConvertUsing(a => a.OptionText);

            CreateMap<string, Options>().ConvertUsing(text => new Options { OptionText = text });

            CreateMap<Quizzes, QuizzesDto>()
                .ForMember(dest => dest.QuizAttended, opt => opt.MapFrom(src => src.QuizAttended.Select(a => a.ApplicationUserId)))
                .ForMember(dest => dest.questionAnswers, opt => opt.MapFrom(src => src.QuizQuestions));

            CreateMap<Questions, QuestionDto>()
                .ForMember(dest => dest.Answers, opt => opt.MapFrom(src => src.Answers.AnswerText))
                .ForMember(dest => dest.QuestionText, opt => opt.MapFrom(src => src.QuestionText))
                .ForMember(dest => dest.Options, opt => opt.MapFrom(src => src.Options.Select(o => o.OptionText)))
                .ForMember(dest => dest.UserAnswers, opt => opt.MapFrom(src => src.UserAnswers.Select(ua => new userAnswer
                    {
                        user = ua.ApplicationUserId,
                        answer = ua.UserAnswerText
                })));
            CreateMap<ApplicationUser, UsersDto>()
                .ForMember(dest => dest.Attendee, opt => opt.MapFrom(src => src.QuizAttended));

            CreateMap<UserQuizAttendee, UserQuizAttendeeDto>()
                .ForMember(dest => dest.UserAnswers, opt => opt.MapFrom(src => src.UserAnswers));

            CreateMap<UserAnswer, UserAnswerDto>()
                .ForMember(dest => dest.QuestionsText, opt => opt.MapFrom(src => src.Questions.QuestionText));
            CreateMap<UserAnswer, UserAnswerDto>()
                .ForMember(dest => dest.QuizzText, opt => opt.MapFrom(src => src.Attempt.Quizzes.Title));

            CreateMap<UserAnswer, UserAnswerDto>()
                .ForMember(dest => dest.AnswerText, opt => opt.MapFrom(src => src.UserAnswerText));

        }
    }
}

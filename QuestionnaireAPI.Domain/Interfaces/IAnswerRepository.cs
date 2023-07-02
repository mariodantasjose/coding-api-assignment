using AutoMapper;
using QuestionnaireAPI.Domain.DTO;

namespace QuestionnaireAPI.Persistence.Interfaces
{
    public interface IAnswerRepository
    {
        Task<bool> SubmitAnswer(IMapper mapper, PostAnswer answer);

        Task<bool> SubmitAnswers(IMapper mapper, List<PostAnswer> answers);
    }
}

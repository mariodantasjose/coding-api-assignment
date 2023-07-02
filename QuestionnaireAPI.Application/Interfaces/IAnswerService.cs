
using AutoMapper;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using QuestionnaireAPI.Domain.DTO;

namespace QuestionnaireAPI.Application.Interfaces
{
    public interface IAnswerService
    {
        Task<ServiceResponse<bool>> SubmitAnswers(IMapper mapper, List<PostAnswer> answers);
    }
}

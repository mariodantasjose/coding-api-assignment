using QuestionnaireAPI.Domain.DataClasses;
using QuestionnaireAPI.Domain.DTO;

namespace QuestionnaireAPI.Application.Interfaces
{
    public interface IQuestionnaireFormService
    {
        Task<ServiceResponse<List<Question>>> LoadPaginatedQuestionnaire(PagingParameters pagingParameters);
    }
}

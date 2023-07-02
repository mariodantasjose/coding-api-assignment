using QuestionnaireAPI.Domain.DataClasses;
using QuestionnaireAPI.Domain.DTO;
using QuestionnaireAPI.Domain.Enums;

namespace QuestionnaireAPI.Persistence.Interfaces
{
    public interface IQuestionnaireFormRepository
    {
        Task<Questionnaire> LoadQuestionnaire();
        Task<List<Question>> LoadPaginatedPage(PagingParameters pagingParameters);
    }
}

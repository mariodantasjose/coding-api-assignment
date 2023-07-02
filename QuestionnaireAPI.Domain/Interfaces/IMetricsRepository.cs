
using QuestionnaireAPI.Domain.Entities;

namespace QuestionnaireAPI.Persistence.Interfaces
{
    public interface IMetricsRepository
    {
        Task<List<SubmittedAnswer>> LoadMetrics(int questionnaireId);
    }
}

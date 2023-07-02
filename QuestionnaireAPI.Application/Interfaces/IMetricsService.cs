using Microsoft.AspNetCore.Mvc.ModelBinding;
using QuestionnaireAPI.Domain.DTO;

namespace QuestionnaireAPI.Application.Interfaces
{
    public interface IMetricsService
    {
        Task<ServiceResponse<List<MetricsDTO>>> CalculateMetrics(ModelStateDictionary modelState);
    }
}

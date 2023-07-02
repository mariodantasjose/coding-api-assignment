using Microsoft.EntityFrameworkCore;
using QuestionnaireAPI.Domain.Entities;
using QuestionnaireAPI.Domain.Enums;
using QuestionnaireAPI.Persistence.Context;
using QuestionnaireAPI.Persistence.Interfaces;

namespace QuestionnaireAPI.Persistence.Repository
{
    public class MetricsRepository : IMetricsRepository
    {
        public WebAPIContext _context;
        public MetricsRepository(WebAPIContext context)
        {
            _context = context;
        }

        public async Task<List<SubmittedAnswer>> LoadMetrics(int questionnaireId)
        {
            try
            {
                if (_context.SubmittedAnswers == null)
                {
                    throw new ArgumentNullException("Load Metrics", "Entity set 'WebAPIContext.SubmittedAnswers' is null.");
                }

                var answers = await _context.SubmittedAnswers
                    .Where(x => x.QuestionnaireId == questionnaireId
                        && x.AnswerType == AnswerType.Choice && x.Value != null).ToListAsync();

                return answers;
            }
            catch(Exception ex)
            {
                throw new Exception($"An error occurred while loading metrics: {ex}");
            }
        }
    }
}

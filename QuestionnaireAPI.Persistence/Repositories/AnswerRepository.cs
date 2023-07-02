using AutoMapper;
using QuestionnaireAPI.Domain.DTO;
using QuestionnaireAPI.Domain.Entities;
using QuestionnaireAPI.Persistence.Context;
using QuestionnaireAPI.Persistence.Interfaces;

namespace QuestionnaireAPI.Persistence.Repository
{
    public class AnswerRepository : IAnswerRepository
    {
        public WebAPIContext _context;
        public AnswerRepository(WebAPIContext context) 
        {
            _context = context;
        }

        public async Task<bool> SubmitAnswer(IMapper mapper, PostAnswer answer)
        {
            if (_context.SubmittedAnswers == null)
            {
                throw new ArgumentNullException("Submitted Answers", "Entity set 'WebAPIContext.SubmittedAnswers' is null.");
            }
            try
            {
                var item = mapper.Map<SubmittedAnswer>(answer);
                await _context.SubmittedAnswers.AddAsync(item);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"An exception occurred at the Repository level: {ex}");
            }

            return true;
        }

        public async Task<bool> SubmitAnswers(IMapper mapper, List<PostAnswer> answers)
        {
            if (_context.SubmittedAnswers == null)
            {
                throw new ArgumentNullException("Submitted Answers: Entity set 'WebAPIContext.SubmittedAnswers' is null.", nameof(_context.SubmittedAnswers));
            }

            if(answers.Count == 0)
            {
                throw new ArgumentException("Input parameter 'answers' cannot be empty.", nameof(answers));
            }
            try
            {
                var items = mapper.Map<ICollection<PostAnswer>, ICollection<SubmittedAnswer>>(answers);
                await _context.SubmittedAnswers.AddRangeAsync(items);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"An exception occurred at the Repository level: {ex}");
            }

            return true;
        }
    }
}

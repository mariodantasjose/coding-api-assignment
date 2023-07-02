using AutoMapper;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;
using QuestionnaireAPI.Application.Interfaces;
using QuestionnaireAPI.Domain.DTO;
using QuestionnaireAPI.Persistence.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace QuestionnaireAPI.Application.Services
{
    public class AnswerService : IAnswerService
    {
        private readonly IAnswerRepository AnswerRepository;
        private readonly ILogger<AnswerService> Logger;

        public AnswerService(IAnswerRepository answerRepository, ILogger<AnswerService> logger)
        {
            AnswerRepository = answerRepository;
            Logger = logger;
        }

        public async Task<ServiceResponse<bool>> SubmitAnswers(IMapper mapper, List<PostAnswer> answers)
        {
            try
            {
                var result = await AnswerRepository.SubmitAnswers(mapper, answers);

                return ServiceResponse<bool>.SuccessResult(true, "The answers were submitted successfully.");
            }
            catch (ArgumentNullException ex)
            {
                Logger.LogError($"{ex.Message}: {ex}");
                return ServiceResponse<bool>.ErrorResult($"A required entity set is null.");
            }
            catch (ArgumentException ex)
            {
                Logger.LogError($"{ex.Message}: {ex}");
                return ServiceResponse<bool>.ErrorResult($"{ex.Message}.");
            }
            catch (Exception ex)
            {
                Logger.LogError($"An error occurred while submitting the answer: {ex}");
                return ServiceResponse<bool>.ErrorResult($"An error occurred while submitting the answer.");
            }
        }
    }
}

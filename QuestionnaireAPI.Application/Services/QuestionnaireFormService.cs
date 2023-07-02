using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using QuestionnaireAPI.Application.Interfaces;
using QuestionnaireAPI.Domain.DataClasses;
using QuestionnaireAPI.Domain.DTO;
using QuestionnaireAPI.Domain.Enums;
using QuestionnaireAPI.Persistence.Interfaces;

namespace QuestionnaireAPI.Application.Services
{
    public class QuestionnaireFormService : IQuestionnaireFormService
    {
        private readonly IQuestionnaireFormRepository QuestionnaireFormRepository;
        private readonly ILogger<QuestionnaireFormService> Logger;

        public QuestionnaireFormService(IQuestionnaireFormRepository questionnaireFormRepository, ILogger<QuestionnaireFormService> logger)
        {
            QuestionnaireFormRepository = questionnaireFormRepository;
            Logger = logger;
        }

        public async Task<ServiceResponse<List<Question>>> LoadPaginatedQuestionnaire(PagingParameters pagingParameters)
        {
            try
            {
                if (pagingParameters.PageNumber <= 0 || pagingParameters.PageSize <= 0)
                {
                    throw new ArgumentException("Invalid input values for paging");
                }

                var page = await QuestionnaireFormRepository.LoadPaginatedPage(pagingParameters);

                return ServiceResponse<List<Question>>.SuccessResult(page, "The page was loaded successfully.");
            }
            catch (ArgumentException ex)
            {
                Logger.LogError($"{ex}");
                return ServiceResponse<List<Question>>.ErrorResult($"Invalid paging parameters were passed. Both page size and page number must be greater than 0");
            }
            catch (FileNotFoundException ex)
            {
                Logger.LogError($"{ex}");
                return ServiceResponse<List<Question>>.ErrorResult($"The questionnaire form could not be found.");
            }
            catch (JsonReaderException ex)
            {
                Logger.LogError($"{ex}");
                return ServiceResponse<List<Question>>.ErrorResult($"An error was occurred that prevented loading the questionnaire template.");
            }
            catch (JsonSerializationException ex)
            {
                Logger.LogError($"{ex}");
                return ServiceResponse<List<Question>>.ErrorResult($"An error was occurred that prevented sending the questionnaire template.");
            }
            catch (Exception ex)
            {
                Logger.LogError($"An error occurred while getting the questionnaire form: {ex}");
                return ServiceResponse<List<Question>>.ErrorResult("Internal server error");
            }
        }
    }
}

using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using QuestionnaireAPI.Application.Interfaces;
using QuestionnaireAPI.Domain.DTO;
using QuestionnaireAPI.Domain.Enums;
using QuestionnaireAPI.Persistence.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace QuestionnaireAPI.Application.Services
{
    public class MetricsService : IMetricsService
    {
        private readonly IMetricsRepository MetricsRepository;
        private readonly IQuestionnaireFormRepository QuestionnaireRepository;
        private readonly ILogger<MetricsService> Logger;

        public MetricsService(IMetricsRepository metricsRepository, IQuestionnaireFormRepository questionnaireRepository, ILogger<MetricsService> logger)
        {
            MetricsRepository = metricsRepository;
            QuestionnaireRepository = questionnaireRepository;
            Logger = logger;
        }

        public async Task<ServiceResponse<List<MetricsDTO>>> CalculateMetrics(ModelStateDictionary modelState)
        {
            try
            {
                if (!modelState.IsValid)
                {
                    var errorMessages = string.Join(" \n", modelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage));

                    modelState.AddModelError("Validation error", errorMessages);
                    throw new ValidationException(errorMessages);
                }

                var questionnaireForm = await QuestionnaireRepository.LoadQuestionnaire();
                var answers = await MetricsRepository.LoadMetrics(questionnaireForm.Id);

                if (answers is null || !answers.Any() || questionnaireForm is null)
                    return ServiceResponse<List<MetricsDTO>>.NotFoundResult();

                var questions = questionnaireForm.QuestionnaireItems
                    .SelectMany(subject => subject.QuestionnaireItems)
                .ToList();

                var result = new List<MetricsDTO>();
                foreach(var department in Enum.GetValues(typeof(Department)).Cast<Department>().Where(x => x != Department.None).ToList())
                {
                    result.AddRange(questions.Select(question => new MetricsDTO
                    {
                        Department = department,
                        QuestionId = question.Id,
                        Text = question.Texts,
                        TotalAnswers = answers
                        .Where(x => x.QuestionnaireId == questionnaireForm.Id && x.SubjectId == question.SubjectType && x.QuestionId == question.Id
                            && x.AnswerType == AnswerType.Choice && x.Department == department)
                        .Where(x => x.Value.HasValue)
                        .Select(x => x.Value)
                        .Count(),
                        Min = answers
                        .Where(x => x.QuestionnaireId == questionnaireForm.Id && x.SubjectId == question.SubjectType && x.QuestionId == question.Id
                            && x.AnswerType == AnswerType.Choice && x.Department == department)
                        .Where(x => x.Value.HasValue)
                        .Select(x => x.Value)
                        .DefaultIfEmpty()
                        .Min(),
                        Max = answers
                        .Where(x => x.QuestionnaireId == questionnaireForm.Id && x.SubjectId == question.SubjectType && x.QuestionId == question.Id
                            && x.AnswerType == AnswerType.Choice && x.Department == department)
                        .Where(x => x.Value.HasValue)
                        .Select(x => x.Value)
                        .DefaultIfEmpty()
                        .Max(),
                        Average = answers
                        .Where(x => x.QuestionnaireId == questionnaireForm.Id && x.SubjectId == question.SubjectType && x.QuestionId == question.Id
                            && x.AnswerType == AnswerType.Choice && x.Department == department)
                        .Where(x => x.Value.HasValue)
                        .Select(x => x.Value)
                        .DefaultIfEmpty()
                        .Average()
                    }).ToList());
                }
                

                return ServiceResponse<List<MetricsDTO>>.SuccessResult(result, "The metrics were calculated successfully.");
            }
            catch (FileNotFoundException ex)
            {
                Logger.LogError($"An issue has occurred while loading the questionnaire form: {ex}");
                return ServiceResponse<List<MetricsDTO>>.ErrorResult($"The questionnaire form could not be found.");
            }
            catch (JsonReaderException ex)
            {
                Logger.LogError($"An issue has occurred while loading the questionnaire form: {ex}");
                return ServiceResponse<List<MetricsDTO>>.ErrorResult($"An error occurred that prevented loading the questionnaire template.");
            }
            catch (JsonSerializationException ex)
            {
                Logger.LogError($"An issue has occurred while loading the questionnaire form: {ex}");
                return ServiceResponse<List<MetricsDTO>>.ErrorResult($"An error occurred that prevented sending the questionnaire template.");
            }
            catch (ValidationException ex)
            {
                Logger.LogError($"Entity set 'WebAPIContext.SubmittedAnswers' is invalid: {ex}");
                return ServiceResponse<List<MetricsDTO>>.ErrorResult($"A required entity set is invalid.");
            }
            catch (ArgumentNullException ex)
            {
                Logger.LogError($"An argument was null: {ex}");
                return ServiceResponse<List<MetricsDTO>>.ErrorResult($"An error occurred that prevented the metrics calculation.");
            }
            catch (Exception ex)
            {
                Logger.LogError($"An issue has occurred while loading the metrics: {ex}");
                return ServiceResponse<List<MetricsDTO>>.ErrorResult($"An error occurred that prevented the metrics calculation.");
            }
        }
    }
}

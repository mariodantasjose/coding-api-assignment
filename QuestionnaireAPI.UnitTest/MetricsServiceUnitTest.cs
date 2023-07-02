using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using QuestionnaireAPI.Application.Services;
using QuestionnaireAPI.Domain.DataClasses;
using QuestionnaireAPI.Domain.Entities;
using QuestionnaireAPI.Domain.Enums;
using QuestionnaireAPI.Persistence.Context;
using QuestionnaireAPI.Persistence.Interfaces;
using QuestionnaireAPI.Persistence.Repository;
using System.ComponentModel.DataAnnotations;
using WorkQuestionnaire.Domain.Repository;

namespace QuestionnaireAPI.UnitTest
{
    public class MetricsServiceUnitTest
    {
        private const int _QuestionnaireId = 1000;

        private static WebAPIContext SeedContext()
        {
            var options = new DbContextOptionsBuilder<WebAPIContext>()
                          .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;

            var context = new WebAPIContext(options);

            List<User> users = new()
            {
                new User { Id = Guid.NewGuid(), Name = "Patrick Doe", Email = "patrickdoe@company.com", Department = Department.Development },
                new User { Id = Guid.NewGuid(), Name = "Don Vito Corleone", Email = "donvitocorleone@company.com", Department = Department.Development },
                new User { Id = Guid.NewGuid(), Name = "Carla Doe", Email = "carladoe@company.com", Department = Department.Development },
                new User { Id = Guid.NewGuid(), Name = "George Scarface", Email = "georgescarface@company.com", Department = Department.HumanResources },
                new User { Id = Guid.NewGuid(), Name = "Elizabeth Doe", Email = "lizdoe@company.com", Department = Department.HumanResources },
                new User { Id = Guid.NewGuid(), Name = "William Doe", Email = "williamdoe@company.com", Department = Department.HumanResources }
            };

            context.Users.AddRange(users);

            Dictionary<User, List<int>> answers = new()
            {
                { users[0], new List<int>() { 2, 5, 4 } },
                { users[1], new List<int>() { 3, 3, 2 } },
                { users[2], new List<int>() { 5, 5, 5 } },
                { users[3], new List<int>() { 1, 2, 2 } },
                { users[4], new List<int>() { 3, 3, 3 } },
                { users[5], new List<int>() { 5, 5, 5 } }
            };


            var submittedAnswers = new List<SubmittedAnswer>();

            foreach (var user in answers)
            {
                for (int j = 0; j < user.Value.Count; j++)
                {
                    submittedAnswers.Add(new SubmittedAnswer
                    {
                        User = user.Key,
                        AnswerType = AnswerType.Choice,
                        SubjectId = 2605515,
                        Department = user.Key.Department,
                        QuestionnaireId = _QuestionnaireId,
                        QuestionId = j + 1,
                        Value = user.Value[j],
                        Text = string.Empty
                    });
                }
            }

            context.SubmittedAnswers.AddRange(submittedAnswers);
            context.SaveChanges();

            return context;
        }

        [Fact]
        public void CalculateMetrics_ReturnsSuccessfully()
        {
            // Arrange
            var modelState = new ModelStateDictionary();
            Mock<ILogger<MetricsService>> logger = new();
            var context = SeedContext();
            var metricsRepository = new Mock<IMetricsRepository>();
            var questionnaireRepository = new Mock<IQuestionnaireFormRepository>();
            var questionnaireForm = questionnaireRepository.Setup(repo => repo.LoadQuestionnaire()).ReturnsAsync(
                new Questionnaire()
                {
                    Id = _QuestionnaireId,
                    QuestionnaireItems = new List<Subject>()
                    {
                        new Subject() {
                            Id = 1,
                            OrderNumber = 0,
                            Texts = new TextItem()
                            {
                                English = "This is my first question",
                                Dutch = "This is my first question"
                            },
                            ItemType = ItemType.Answer,
                            QuestionnaireItems = new List<Question>()
                            {
                                new Question()
                                {
                                    Id = 1,
                                    OrderNumber = 0,
                                    ItemType = ItemType.Question,
                                    Texts = new TextItem()
                                    {
                                        English = "First question?",
                                        Dutch = "First question?"
                                    },
                                    SubjectType = 2605515,
                                    AnswerCategoryType = AnswerCategoryType.MultipleChoice,
                                    QuestionnaireItems = new List<Answer>()
                                },
                                new Question()
                                {
                                    Id = 2,
                                    OrderNumber = 1,
                                    ItemType = ItemType.Question,
                                    Texts = new TextItem()
                                    {
                                        English = "Second question?",
                                        Dutch = "Second question?"
                                    },
                                    SubjectType = 2605515,
                                    AnswerCategoryType = AnswerCategoryType.MultipleChoice,
                                    QuestionnaireItems = new List<Answer>()
                                },
                                new Question()
                                {
                                    Id = 3,
                                    OrderNumber = 2,
                                    ItemType = ItemType.Question,
                                    Texts = new TextItem()
                                    {
                                        English = "Third question?",
                                        Dutch = "Third question?"
                                    },
                                    SubjectType = 2605515,
                                    AnswerCategoryType = AnswerCategoryType.MultipleChoice,
                                    QuestionnaireItems = new List<Answer>()
                                },
                            }
                        }
                    }
                });
            metricsRepository.Setup(repo => repo.LoadMetrics(_QuestionnaireId)).ReturnsAsync(context.SubmittedAnswers.ToList());
            var service = new MetricsService(metricsRepository.Object, questionnaireRepository.Object, logger.Object);

            // Act
            var calculations = service.CalculateMetrics(modelState).Result.Data;

            var submittedQuestionOneDev = context.SubmittedAnswers.Where(x => x.QuestionnaireId == _QuestionnaireId && x.QuestionId == 1 && x.Department == Department.Development && x.Value != null).Select(x => x.Value);
            var calculationsQuestionOneDev = calculations?.Where(x => x.QuestionId == 1 && x.Department == Department.Development);

            var submittedQuestionTwoDev = context.SubmittedAnswers.Where(x => x.QuestionnaireId == _QuestionnaireId && x.QuestionId == 2 && x.Department == Department.Development && x.Value != null).Select(x => x.Value);
            var calculationsQuestionTwoDev = calculations?.Where(x => x.QuestionId == 2 && x.Department == Department.Development);

            var submittedQuestionThreeDev = context.SubmittedAnswers.Where(x => x.QuestionnaireId == _QuestionnaireId && x.QuestionId == 3 && x.Department == Department.Development && x.Value != null).Select(x => x.Value);
            var calculationsQuestionThreeDev = calculations?.Where(x => x.QuestionId == 3 && x.Department == Department.Development);

            //Assert
            Assert.Equal(submittedQuestionOneDev.Count(), calculationsQuestionOneDev?.Select(x => x.TotalAnswers).FirstOrDefault());
            Assert.Equal(submittedQuestionOneDev.Min(), calculationsQuestionOneDev?.Select(x => x.Min).FirstOrDefault());
            Assert.Equal(submittedQuestionOneDev.Max(), calculationsQuestionOneDev?.Select(x => x.Max).FirstOrDefault());
            Assert.Equal(submittedQuestionOneDev.Average(), calculationsQuestionOneDev?.Select(x => x.Average).FirstOrDefault());

            Assert.Equal(submittedQuestionTwoDev.Count(), calculationsQuestionTwoDev?.Select(x => x.TotalAnswers).FirstOrDefault());
            Assert.Equal(submittedQuestionTwoDev.Min(), calculationsQuestionTwoDev?.Select(x => x.Min).FirstOrDefault());
            Assert.Equal(submittedQuestionTwoDev.Max(), calculationsQuestionTwoDev?.Select(x => x.Max).FirstOrDefault());
            Assert.Equal(submittedQuestionTwoDev.Average(), calculationsQuestionTwoDev?.Select(x => x.Average).FirstOrDefault());

            Assert.Equal(submittedQuestionThreeDev.Count(), calculationsQuestionThreeDev?.Select(x => x.TotalAnswers).FirstOrDefault());
            Assert.Equal(submittedQuestionThreeDev.Min(), calculationsQuestionThreeDev?.Select(x => x.Min).FirstOrDefault());
            Assert.Equal(submittedQuestionThreeDev.Max(), calculationsQuestionThreeDev?.Select(x => x.Max).FirstOrDefault());
            Assert.Equal(submittedQuestionThreeDev.Average(), calculationsQuestionThreeDev?.Select(x => x.Average).FirstOrDefault());
        }

        [Fact]
        public void CalculateMetrics_ThrowsValidationExceptionWhenThereAreModelErrors()
        {
            // Arrange
            Mock<ILogger<MetricsService>> logger = new();
            var context = SeedContext();
            var repository = new Mock<MetricsRepository>(context);
            var questionnaireFormRepository = new Mock<QuestionnaireFormRepository>();
            var modelState = new ModelStateDictionary();
            modelState.AddModelError("Id", "Error message");

            // Act
            var service = new MetricsService(repository.Object, questionnaireFormRepository.Object, logger.Object);

            //Assert
            var exception = Assert.ThrowsAsync<ValidationException>(() => service.CalculateMetrics(modelState));
        }
    }
}
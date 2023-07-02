using Microsoft.Extensions.Logging;
using Moq;
using QuestionnaireAPI.Application.Services;
using QuestionnaireAPI.Domain.DataClasses;
using QuestionnaireAPI.Domain.DTO;
using QuestionnaireAPI.Domain.Enums;
using QuestionnaireAPI.Persistence.Interfaces;
using WorkQuestionnaire.Domain.Repository;

namespace QuestionnaireAPI.UnitTest
{
    public class QuestionnaireFormServiceUnitTest
    {
        private static List<Question> GenerateQuestionnaire()
        {
            return new List<Question>()
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
            };
        }

        private static List<Question> GenerateQuestionnairePageResponse()
        {
            return new List<Question>()
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
                }
            };
        }

        [Fact]
        public async void LoadPaginatedQuestionnaire_ReturnsSuccessfully()
        {
            // Arrange
            var generatedQuestionnaire = GenerateQuestionnaire();
            var generatedResponse = GenerateQuestionnairePageResponse();
            var pagingParameters = new PagingParameters()
            {
                PageNumber = 1,
                PageSize = 2
            };
            Mock<ILogger<QuestionnaireFormService>> logger = new();
            var repository = new Mock<IQuestionnaireFormRepository>();
            repository.Setup(repo => repo.LoadPaginatedPage(pagingParameters).Result).Returns(generatedResponse);
            var service = new QuestionnaireFormService(repository.Object, logger.Object);

            // Act
            var result = await service.LoadPaginatedQuestionnaire(pagingParameters);
            var page = result.Data?.Select(x => x.Id).ToList();
            var questionnairePages = generatedQuestionnaire
                .Skip((pagingParameters.PageNumber - 1) * pagingParameters.PageSize)
                .Take(pagingParameters.PageSize).Select(x => x.Id).ToList();

            //Assert
            Assert.True(result.Success);
            Assert.Equal(questionnairePages, page);
        }

        [Fact]
        public void LoadPageQuestionnaire_ThrowsArgumentExceptionWithInvalidPagingParameters()
        {
            // Arrange
            Mock<ILogger<QuestionnaireFormService>> logger = new();
            var repository = new Mock<QuestionnaireFormRepository>();

            // Act
            var service = new QuestionnaireFormService(repository.Object, logger.Object);

            //Assert
            var exception = Assert.ThrowsAsync<ArgumentException>(async () => await service.LoadPaginatedQuestionnaire(new PagingParameters() { PageNumber = 0, PageSize = 0 }));
        }
    }
}

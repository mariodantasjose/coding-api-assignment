using Newtonsoft.Json;
using QuestionnaireAPI.Domain.Enums;

namespace QuestionnaireAPI.Domain.DataClasses
{
    public class Question : QuestionnaireItem
    {
        [JsonProperty("questionId")]
        public int Id { get; set; }
        [JsonProperty("subjectId")]
        public int SubjectType { get; set; }
        public AnswerCategoryType AnswerCategoryType { get; set; }
        public virtual ICollection<Answer> QuestionnaireItems { get; set; }

        public Question()
        {
            Texts = new();
            QuestionnaireItems = new List<Answer>();
        }

        public Question(int id, int subjectType, AnswerCategoryType answerCategoryType, int orderNumber, TextItem texts, ItemType itemType, ICollection<Answer> questionnaireItems)
        {
            Id = id;
            SubjectType = subjectType;
            AnswerCategoryType = answerCategoryType;
            OrderNumber = orderNumber;
            Texts = texts;
            ItemType = itemType;
            QuestionnaireItems = questionnaireItems;
        }
    }
}

using Newtonsoft.Json;
using QuestionnaireAPI.Domain.Enums;

namespace QuestionnaireAPI.Domain.DataClasses
{
    public class Answer : QuestionnaireItem
    {
        [JsonPropertyAttribute("answerId")]
        public int? Id { get; set; }
        public int QuestionId { get; set; }
        public AnswerType AnswerType { get; set; }
        public virtual ICollection<QuestionnaireItem> QuestionnaireItems { get; set; }

        public Answer() 
        {
            Texts = new();
            QuestionnaireItems = new List<QuestionnaireItem>();
        }

        public Answer(int? id, int questionId, AnswerType answerType, int orderNumber, TextItem texts, ItemType itemType, ICollection<QuestionnaireItem> questionnaireItems)
        {
            Id = id;
            QuestionId = questionId;
            AnswerType = answerType;
            OrderNumber = orderNumber;
            Texts = texts;
            ItemType = itemType;
            QuestionnaireItems = questionnaireItems;
        }
    }
}

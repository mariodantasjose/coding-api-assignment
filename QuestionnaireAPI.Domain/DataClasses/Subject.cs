using Newtonsoft.Json;
using QuestionnaireAPI.Domain.Enums;

namespace QuestionnaireAPI.Domain.DataClasses
{
    public class Subject
    {
        [JsonProperty("subjectId")]
        public int Id { get; set; }
        public int OrderNumber { get; set; }
        public virtual TextItem Texts { get; set; }
        public ItemType ItemType { get; set; }
        public virtual ICollection<Question> QuestionnaireItems { get; set; }

        public Subject()
        {
            QuestionnaireItems = new List<Question>();
            Texts = new();
        }

        public Subject(int id, int orderNumber, TextItem texts, ItemType itemType, ICollection<Question> questionnaireItems)
        {
            Id = id;
            OrderNumber = orderNumber;
            Texts = texts;
            ItemType = itemType;
            QuestionnaireItems = questionnaireItems;
        }
    }
}

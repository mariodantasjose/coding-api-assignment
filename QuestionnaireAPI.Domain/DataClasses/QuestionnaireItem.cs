using QuestionnaireAPI.Domain.Enums;

namespace QuestionnaireAPI.Domain.DataClasses
{
    public class QuestionnaireItem
    {
        public int OrderNumber { get; set; }
        public virtual TextItem Texts { get; set; }
        public ItemType ItemType { get; set; }
        ICollection<QuestionnaireItem> Items { get; set; }

        public QuestionnaireItem() 
        {
            Texts = new();
            Items = new List<QuestionnaireItem>();
        }

        public QuestionnaireItem(int orderNumber, TextItem texts, ItemType itemType, List<QuestionnaireItem> questionnaireItems)
        {
            OrderNumber = orderNumber;
            Texts = texts;
            ItemType = itemType;
            Items = questionnaireItems;
        }
    }
}

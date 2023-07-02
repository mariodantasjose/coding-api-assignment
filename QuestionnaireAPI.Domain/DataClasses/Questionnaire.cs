using Newtonsoft.Json;

namespace QuestionnaireAPI.Domain.DataClasses
{
    public class Questionnaire
    {
        [JsonProperty("questionnaireId")]
        public int Id { get; set; }
        public virtual ICollection<Subject> QuestionnaireItems { get; set; }

        public Questionnaire()
        {
            QuestionnaireItems = new List<Subject>();
        }

        public Questionnaire(ICollection<Subject> items)
        {
            QuestionnaireItems = items;
        }
    }
}

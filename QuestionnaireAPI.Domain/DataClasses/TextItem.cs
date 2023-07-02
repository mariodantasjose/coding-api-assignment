using Newtonsoft.Json;

namespace QuestionnaireAPI.Domain.DataClasses
{
    public class TextItem
    {
        [JsonProperty("nl-NL")]
        public string Dutch { get; set; }
        [JsonProperty("en-US")]
        public string English { get; set; }

        public TextItem()
        {
            Dutch = string.Empty;
            English = string.Empty;
        }

        public TextItem(string nl_NL, string en_US)
        {
            Dutch = nl_NL;
            English = en_US;
        }
    }
}

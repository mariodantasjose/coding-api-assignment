using QuestionnaireAPI.Domain.DataClasses;
using QuestionnaireAPI.Domain.Enums;

namespace QuestionnaireAPI.Domain.DTO
{
    public class MetricsDTO
    {
        public Department Department { get; set; }
        public int QuestionId { get; set; }
        public TextItem Text { get; set; }
        public int TotalAnswers { get; set; }
        public double? Average { get; set; }
        public int? Min { get; set; }
        public int? Max { get; set; }

        public MetricsDTO()
        {
            Text = new TextItem();
        }

        public MetricsDTO(Department department, int questionId, TextItem text, int totalAnswers, double average, int min, int max)
        {
            Department = department;
            QuestionId = questionId;
            Text = text;
            TotalAnswers = totalAnswers;
            Average = average;
            Min = min;
            Max = max;
        }
    }
}

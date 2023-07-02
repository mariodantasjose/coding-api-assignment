using QuestionnaireAPI.Domain.Enums;

namespace QuestionnaireAPI.Domain.DTO
{
    public class PostAnswer
    {
        public Guid UserId { get; set; }
        public Department Department { get; set; }
        public int? QuestionnaireId { get; set; }
        public int? SubjectId { get; set; }
        public int? QuestionId { get; set; }
        public AnswerType AnswerType { get; set; }
        public int Value { get; set; }
        public string Text { get; set; }

        public PostAnswer()
        {
            Text = string.Empty;
        }

        public PostAnswer(Guid userId, Department department, int? questionnaireId, int? subjectId, int? questionId, AnswerType answerType, int value, string text)
        {
            UserId = userId;
            Department = department;
            QuestionnaireId = questionnaireId;
            SubjectId = subjectId;
            QuestionId = questionId;
            AnswerType = answerType;
            Value = value;
            Text = text;
        }
    }
}

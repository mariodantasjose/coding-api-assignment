using Microsoft.EntityFrameworkCore;
using QuestionnaireAPI.Domain.Enums;

namespace QuestionnaireAPI.Domain.Entities
{
    [PrimaryKey(nameof(UserId), nameof(QuestionnaireId), nameof(SubjectId), nameof(QuestionId))]
    [Index(nameof(Department), nameof(QuestionnaireId), nameof(SubjectId), nameof(QuestionId), Name = "IX_SubmittedAnswer_Department_FK_QuestionnaireId_FK_SubjectId_FK_QuestionId")]
    public class SubmittedAnswer
    {
        public User User { get; set; }
        public Guid UserId { get; set; }
        public int QuestionnaireId { get; set; }
        public int SubjectId { get; set; }
        public int QuestionId { get; set; }
        public AnswerType AnswerType { get; set; }
        public Department Department { get; set; }
        public int? Value { get; set; }
        public string Text { get; set; }

        public SubmittedAnswer()
        {
            User = new User();
            Text = string.Empty;
        }

        public SubmittedAnswer(User user, int questionnaireId, int subjectId, int questionId, AnswerType answerType, Department department, int? value, string text)
        {
            User = user;
            QuestionnaireId = questionnaireId;
            SubjectId = subjectId;
            QuestionId = questionId;
            AnswerType = answerType;
            Department = department;
            Value = value;
            Text = text;
        }
    }
}

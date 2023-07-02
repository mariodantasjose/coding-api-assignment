using QuestionnaireAPI.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuestionnaireAPI.Domain.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public Department Department { get; set; }

        public ICollection<SubmittedAnswer> SubmittedAnswers { get; set; }


        public User()
        {
            Name = string.Empty;
            Email = string.Empty;
            SubmittedAnswers = new List<SubmittedAnswer>();
        }
        public User(string name, string email, Department department, ICollection<SubmittedAnswer> submittedAnswers)
        {
            Name = name;
            Email = email;
            Department = department;
            SubmittedAnswers = submittedAnswers;

        }
    }
}

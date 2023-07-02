using AutoMapper;
using QuestionnaireAPI.Domain.DTO;
using QuestionnaireAPI.Domain.Entities;
using QuestionnaireAPI.Persistence.Context;

namespace QuestionnaireAPI.Persistence.Mappers
{
    public class UserIdToUserResolver : IValueResolver<PostAnswer, SubmittedAnswer, User>
    {
        private readonly WebAPIContext _context;

        public UserIdToUserResolver(WebAPIContext dbContext)
        {
            _context = dbContext;
        }

        public User Resolve(PostAnswer source, SubmittedAnswer destination, User destinationMember, ResolutionContext context)
        {
            Guid userId = source.UserId;

            User user = _context.Users.FirstOrDefault(u => u.Id == userId) ?? new User();

            return user;
        }
    }
}

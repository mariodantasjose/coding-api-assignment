using AutoMapper;
using QuestionnaireAPI.Domain.DTO;
using QuestionnaireAPI.Domain.Entities;

namespace QuestionnaireAPI.Persistence.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<PostAnswer, SubmittedAnswer>()
                .ForMember(x => x.User, opt =>
                {
                    opt.MapFrom<UserIdToUserResolver>();
                });
        }
    }
}

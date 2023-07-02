using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using QuestionnaireAPI.Domain.Entities;
using QuestionnaireAPI.Domain.Enums;
using QuestionnaireAPI.Persistence.Context;

namespace QuestionnaireAPI.Persistence.DBSeeds
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable(nameof(WebAPIContext.Users));
            builder.HasData(
                new User { Id = Guid.NewGuid(), Name = "John Doe", Email = "johndoe@company.com", Department = Department.Marketing },
                new User { Id = Guid.NewGuid(), Name = "Marie Doe", Email = "mariedoe@company.com", Department = Department.Sales },
                new User { Id = Guid.NewGuid(), Name = "Patrick Doe", Email = "patrickdoe@company.com", Department = Department.Development },
                new User { Id = Guid.NewGuid(), Name = "Elizabeth Doe", Email = "lizdoe@company.com", Department = Department.HumanResources },
                new User { Id = Guid.NewGuid(), Name = "William Doe", Email = "williamdoe@company.com", Department = Department.Reception },

                new User { Id = Guid.NewGuid(), Name = "Francesco Corleone", Email = "francescocorleone@company.com", Department = Department.Marketing },
                new User { Id = Guid.NewGuid(), Name = "Sophie Corleone", Email = "sophiecorleone@company.com", Department = Department.Sales },
                new User { Id = Guid.NewGuid(), Name = "Don Vito Corleone", Email = "donvitocorleone@company.com", Department = Department.Development },
                new User { Id = Guid.NewGuid(), Name = "George Scarface", Email = "georgescarface@company.com", Department = Department.HumanResources },
                new User { Id = Guid.NewGuid(), Name = "Patrick Corleone", Email = "patrickcorleone@company.com", Department = Department.Reception }
            );
        }
    }
}

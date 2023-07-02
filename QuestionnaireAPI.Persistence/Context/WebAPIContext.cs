using Microsoft.EntityFrameworkCore;
using QuestionnaireAPI.Domain.Entities;
using QuestionnaireAPI.Persistence.DBSeeds;

namespace QuestionnaireAPI.Persistence.Context
{
    public class WebAPIContext : DbContext
    {
        public WebAPIContext(DbContextOptions<WebAPIContext> options)
        : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SubmittedAnswer>()
                .HasOne(b => b.User)
                .WithMany(a => a.SubmittedAnswers)
                .HasForeignKey(b => b.UserId);

            modelBuilder.ApplyConfiguration(new UserConfiguration());
        }

        public DbSet<User> Users { get; set; } = null!;
        public DbSet<SubmittedAnswer> SubmittedAnswers { get; set; } = null!;
    }
}

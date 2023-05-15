using SignatureEmailParser.EFCore.Entities;
using Microsoft.EntityFrameworkCore;

namespace SignatureEmailParser.EFCore
{
    public class ApplicationContext : DbContext
    {
        public DbSet<TempalateAutoRespondStatistic> TempalateAutoRespondStatistics { get; set; }
        public DbSet<StripeIdentity> StripeIdentities { get; set; }
        public DbSet<SocialMediaMapping> SocialMediaMappings { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<City> City { get; set; }
        public DbSet<Industry> Industries { get; set; }
        public DbSet<Regions> Regions { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Log> Logs { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            Database.SetCommandTimeout(150000);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SocialMediaMapping>()
                .HasAlternateKey(socialMediaMapping => socialMediaMapping.LinkedIn);

            base.OnModelCreating(modelBuilder);
        }
    }
}

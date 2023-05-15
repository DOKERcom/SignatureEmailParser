using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace SignatureEmailParser.EFCore
{
    public class ApplicationContextFactory : IDesignTimeDbContextFactory<ApplicationContext>
    {
        const string sectionKey = "Values";
        const string connectionStringKey = "LeadHootzFunctionConnectionString";
        const string localSettingFile = "local.settings.json";
        public ApplicationContext CreateDbContext(string[] args)
        {
            // Database.SetCommandTimeout(150000);

            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(localSettingFile, optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();

            string connectionString = "";// config.GetSection(sectionKey)[connectionStringKey];

            var optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();
            optionsBuilder.UseSqlServer(connectionString);
            return new ApplicationContext(optionsBuilder.Options);
        }
    }
}

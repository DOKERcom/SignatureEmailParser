using SignatureEmailParser.EFCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Data;
using System.Data.SqlClient;

namespace SignatureEmailParser.BusinessLogic.Configurations
{
    public static class DatabaseContextConfiguration
    {
        public static void AddDatabaseContextConfiguration(this IServiceCollection services)
        {
            services.AddSingleton<ConnectionStringInjector>();
            using (var serviceProvider = services.BuildServiceProvider())
            {
                ConnectionStringInjector connectionStringInjector = serviceProvider.GetService<ConnectionStringInjector>();
                services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(connectionStringInjector.GetConnectionString()));
                services.AddTransient<IDbConnection>((sp) => new SqlConnection(connectionStringInjector.GetConnectionString()));
            }
        }
    }
}

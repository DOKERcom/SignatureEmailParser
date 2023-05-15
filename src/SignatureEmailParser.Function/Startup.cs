using SignatureEmailParser.BusinessLogic.Configurations;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using SignatureEmailParser.Interfaces;
using SignatureEmailParser.Extractors.Interfaces;
using SignatureEmailParser.Extractors.Implementation;
using SignatureEmailParser.Extractors;
using SignatureEmailParser.Factories;
using SignatureEmailParser.Handlers;
using SignatureEmailParser.ModelExtractors;
using SignatureEmailParser.Services;

[assembly: FunctionsStartup(typeof(SignatureEmailParser.Function.Startup))]

namespace SignatureEmailParser.Function
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            var configuration = builder.GetContext().Configuration;

            builder.Services.AddHttpClient();

            builder.Services.AddDependencyConfiguration();

            builder.Services.AddDatabaseContextConfiguration();

            builder.Services.AddStripeConfiguration();

            builder.Services.AddExtractors(configuration);

            builder.Services.AddFactories(configuration);

            builder.Services.AddHandlers(configuration);

            builder.Services.AddModelExtractors(configuration);

            builder.Services.AddServices(configuration);
        }
    }
}

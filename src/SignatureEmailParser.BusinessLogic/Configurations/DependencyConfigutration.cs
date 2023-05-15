using SignatureEmailParser.BusinessLogic.Handlers;
using SignatureEmailParser.BusinessLogic.Handlers.Interfaces;
using SignatureEmailParser.BusinessLogic.Helpers;
using SignatureEmailParser.BusinessLogic.Helpers.Interfaces;
using SignatureEmailParser.BusinessLogic.Services;
using SignatureEmailParser.BusinessLogic.Services.Interfaces;
using SignatureEmailParser.EFCore.Respository;
using SignatureEmailParser.EFCore.Respository.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Stripe;
using SignatureEmailParser.Interfaces;

namespace SignatureEmailParser.BusinessLogic.Configurations
{
    public static class DependencyConfiguration
    {
        public static void AddDependencyConfiguration(this IServiceCollection services)
        {
            services.AddTransient<ITemplateRespondsDailyStatisticRepository, TemplateRespondsDailyStatisticRepository>();
            services.AddTransient<IStripeIdentityRepository, StripeIdentityRepository>();
            services.AddTransient<ISocialMediaMappingRepository, SocialMediaMappingRepository>();
            services.AddTransient<ICompanyRepository, CompanyRepository>();
            services.AddTransient<ICityRepository, CityRepository>();
            services.AddTransient<IRegionRepository, RegionRepository>();
            services.AddTransient<IPositionRepository, PositionRepository>();
            services.AddTransient<IIndustryRepository, IndustryRepository>();
            services.AddTransient<ILogRepository, LogRepository>();

            services.AddTransient<SubscriptionService>();
            services.AddTransient<CustomerService>();

            services.AddTransient<IAuthenticationService, AuthenticationService>();
            services.AddTransient<IApplicationConfigurationsService, ApplicationConfigurationsService>();
            services.AddTransient<ILicenseService, LicenseService>();
            services.AddTransient<ISocialMediaMappingService, SocialMediaMappingService>();
            services.AddTransient<ICityService, CityService>();
            services.AddTransient<IRegionService, RegionService>();
            services.AddTransient<ICompanyService, CompanyService>();
            services.AddTransient<IHttpSenderHelper, HttpSenderHelper>();
            services.AddTransient<IEncryptionHelper, EncryptionHelper>();
            services.AddTransient<IZenDeskHelper, ZenDeskHelper>();
            services.AddTransient<IQueryParserHelper, QueryParserHelper>();
            services.AddTransient<ILogHelper, LogHelper>();

            services.AddTransient<IStripeEventsHandler, StripeEventsHandler>();

            services.AddTransient<ISignatureParser, SignatureParser>();
        }
    }
}
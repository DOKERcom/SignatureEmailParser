using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SignatureEmailParser.Extractors.Implementation;
using SignatureEmailParser.Extractors.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace SignatureEmailParser.Extractors
{
    public static class ServiceCollectionExtensions
    {

        public static IServiceCollection AddExtractors(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IExtractor, Extractor>();

            services.AddTransient<IEmailExtractor, EmailExtractor>();

            services.AddTransient<IFacebookExtractor, FacebookExtractor>();

            services.AddTransient<IFullNameExtractor, FullNameExtractor>();

            services.AddTransient<IInstagramExtractor, InstagramExtractor>();

            services.AddTransient<ILinkedInExtractor, LinkedInExtractor>();

            services.AddTransient<ILocationExtractor, LocationExtractor>();

            services.AddTransient<IMobileExtractor, MobileExtractor>();

            services.AddTransient<IPositionExtractor, PositionExtractor>();

            services.AddTransient<IPostCodeExtractor, PostCodeExtractor>();
            
            services.AddTransient<ITwitterExtractor, TwitterExtractor>();

            services.AddTransient<IWebSiteExtractor, WebSiteExtractor>();

            return services;
        }

    }
}

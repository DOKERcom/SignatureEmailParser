using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SignatureEmailParser.ModelExtractors.Implementation;
using SignatureEmailParser.ModelExtractors.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace SignatureEmailParser.ModelExtractors
{
    public static class ServiceCollectionExtensions
    {

        public static IServiceCollection AddModelExtractors(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddTransient<IEmailModelExtractor, EmailModelExtractor>();

            services.AddTransient<IFacebookModelExtractor, FacebookModelExtractor>();

            services.AddTransient<IFullNameModelExtractor, FullNameModelExtractor>();

            services.AddTransient<IInstagramModelExtractor, InstagramModelExtractor>();

            services.AddTransient<ILinkedInModelExtractor, LinkedInModelExtractor>();

            services.AddTransient<ILocationModelExtractor, LocationModelExtractor>();

            services.AddTransient<IMobileModelExtractor, MobileModelExtractor>();

            services.AddTransient<IPositionModelExtractor, PositionModelExtractor>();

            services.AddTransient<IPostCodeModelExtractor, PostCodeModelExtractor>();

            services.AddTransient<ITwitterModelExtractor, TwitterModelExtractor>();

            services.AddTransient<IWebSiteModelExtractor, WebSiteModelExtractor>();


            return services;
        }


    }
}

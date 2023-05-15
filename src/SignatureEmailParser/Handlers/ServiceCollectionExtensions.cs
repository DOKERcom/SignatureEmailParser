using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SignatureEmailParser.Handlers.Implementation;
using SignatureEmailParser.Handlers.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace SignatureEmailParser.Handlers
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddHandlers(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddTransient<IDataFilterHandler, DataFilterHandler>();

            services.AddTransient<IExtractDataHandler, ExtractDataHandler>();

            services.AddTransient<IExtractModelHandler, ExtractModelHandler>();

            services.AddTransient<IJsonDataHandler, JsonDataHandler>();


            return services;
        }

    }
}

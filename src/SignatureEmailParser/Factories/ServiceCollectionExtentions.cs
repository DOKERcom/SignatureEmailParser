using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SignatureEmailParser.Factories.Implementation;
using SignatureEmailParser.Factories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace SignatureEmailParser.Factories
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddFactories(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IDictionaryToListModelFactory, DictionaryToListModelFactory>();

            services.AddTransient<IJsonToListModelFactory, JsonToListModelFactory>();

            services.AddTransient<IListModelToJsonFactory, ListModelToJsonFactory>();

            return services;
        }

    }
}

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SignatureEmailParser.Services.Implementation;
using SignatureEmailParser.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace SignatureEmailParser.Services
{
    public static class ServiceCollectionExtensions
    {

        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddTransient<IEmailParseModelFillService, EmailParseModelFillService>();

            services.AddTransient<ILocationModelService, LocationModelService>();

            return services;
        }

    }
}

using SignatureEmailParser.BusinessLogic.Constants;
using Microsoft.Extensions.DependencyInjection;
using Stripe;
using System;

namespace SignatureEmailParser.BusinessLogic.Configurations
{
    public static class StripeInitConfiguration
    {
        public static void AddStripeConfiguration(this IServiceCollection services)
        {
            StripeConfiguration.ApiKey = Environment.GetEnvironmentVariable(SettingConstant.ENVIRONMENT_KEY_STRIPE_SECRET, EnvironmentVariableTarget.Process);
        }
    }
}

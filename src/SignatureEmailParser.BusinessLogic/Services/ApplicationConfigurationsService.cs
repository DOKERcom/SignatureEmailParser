using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SignatureEmailParser.BusinessLogic.Extensions;
using SignatureEmailParser.BusinessLogic.Helpers.Interfaces;
using SignatureEmailParser.BusinessLogic.Services.Interfaces;
using SignatureEmailParser.EFCore.Respository.Interfaces;
using SignatureEmailParser.Models.ConfigurationModels;
using SignatureEmailParser.Models.Enums;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace SignatureEmailParser.BusinessLogic.Services
{
    public class ApplicationConfigurationsService : IApplicationConfigurationsService
    {
        private readonly IConfiguration _configuration;
        private readonly IEncryptionHelper _encryptionHelper;
        private readonly ISocialMediaMappingRepository _socialMediaMappingRepository;
        private static List<string> inferredSalary;

        public ApplicationConfigurationsService(IConfiguration configuration,
            IEncryptionHelper encryptionHelper,
            ISocialMediaMappingRepository socialMediaMappingRepository)
        {
            _configuration = configuration;
            _encryptionHelper = encryptionHelper;
            _socialMediaMappingRepository = socialMediaMappingRepository;
        }

        public string GetEncryptedConfiguration(ConfigurationType configurationType)
        {
            var configuration = GetConfigurationFromSettings(configurationType);

            string configurationJson = JsonConvert.SerializeObject(configuration);

            var encryptedConfig = _encryptionHelper.Encrypt(configurationJson);

            return encryptedConfig;
        }

        public List<string> GetInferredSalaryFilterValues()
        {
            return inferredSalary;
        }

        public async Task UpdateInferredSalaryFiltersAsync()
        {
            inferredSalary = await _socialMediaMappingRepository.GetInferredSalaryFiltersAsync();
        }
        
        private object GetConfigurationFromSettings(ConfigurationType configurationType)
        {
            if (configurationType == ConfigurationType.DBC)
            {
                var dbcConfigurations = _configuration.GetSection(configurationType.GetDescription()).Get<DbcConfigurations>();

                return dbcConfigurations;
            }

            if (configurationType == ConfigurationType.Proxy)
            {
                var proxyConfigurations = _configuration.GetSection(configurationType.GetDescription()).Get<ProxyConfiguration>();

                return proxyConfigurations;
            }

            if (configurationType == ConfigurationType.SendGrid)
            {
                var sendGridConfigurations = _configuration.GetSection(configurationType.GetDescription()).Get<SendGridConfigurations>();

                return sendGridConfigurations;
            }

            if (configurationType == ConfigurationType.Updater)
            {
                var updaterConfigurations = _configuration.GetSection(configurationType.GetDescription()).Get<UpdaterConfigurations>();

                return updaterConfigurations;
            }

            if (configurationType == ConfigurationType.Zendesk)
            {
                var zendeskConfigurations = _configuration.GetSection(configurationType.GetDescription()).Get<ZendeskConfigurations>();

                return zendeskConfigurations;
            }

            return default;
        }
    }
}

using System.Collections.Generic;
using System.Threading.Tasks;
using SignatureEmailParser.Models.Enums;

namespace SignatureEmailParser.BusinessLogic.Services.Interfaces
{
    public interface IApplicationConfigurationsService
    {
        string GetEncryptedConfiguration(ConfigurationType configurationType);

        List<string> GetInferredSalaryFilterValues();

        Task UpdateInferredSalaryFiltersAsync();
    }
}

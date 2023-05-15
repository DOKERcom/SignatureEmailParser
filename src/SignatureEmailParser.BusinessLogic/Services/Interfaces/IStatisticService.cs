using System.Threading.Tasks;

namespace SignatureEmailParser.BusinessLogic.Services.Interfaces
{
    public interface IStatisticService
    {
        Task StoreTemplateAutoRespondsDailyData(string requestBody);
        Task SendDataToApi();
    }
}

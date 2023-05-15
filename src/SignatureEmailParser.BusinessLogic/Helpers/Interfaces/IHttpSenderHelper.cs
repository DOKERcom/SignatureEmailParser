using System.Threading.Tasks;

namespace SignatureEmailParser.BusinessLogic.Helpers.Interfaces
{
    public interface IHttpSenderHelper
    {
        Task<bool> SendPostRequest(string url, string stringContent);
    }
}

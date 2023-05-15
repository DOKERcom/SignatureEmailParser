using System.Threading.Tasks;

namespace SignatureEmailParser.BusinessLogic.Handlers.Interfaces
{
    public interface IStripeEventsHandler
    {
        Task HandleAsync(string requestBody);
    }
}

using SignatureEmailParser.BusinessLogic.Handlers.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using System.IO;
using System.Threading.Tasks;

namespace SignatureEmailParser.HttpTriggerFunctions.License.Post
{
    public class HttpTriggerHandleStripeWebhookEvent
    {
        private readonly IStripeEventsHandler _stripeEventsHandler;

        public HttpTriggerHandleStripeWebhookEvent(IStripeEventsHandler stripeEventsHandler)
        {
            _stripeEventsHandler = stripeEventsHandler;
        }

        [FunctionName("HttpTriggerHandleStripeWebhookEvent")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req)
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();

            await _stripeEventsHandler.HandleAsync(requestBody);

            return new OkObjectResult(true);
        }
    }
}

using SignatureEmailParser.BusinessLogic.Helpers.Interfaces;
using SignatureEmailParser.BusinessLogic.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using System.IO;
using System.Threading.Tasks;

namespace SignatureEmailParser.HttpTriggerFunctions.License.Post
{
    public class HttpTriggerUpdateProfilesLicenseFunction
    {
        private readonly ILicenseService _licenseService;
        private readonly ILogHelper _logHelper;

        public HttpTriggerUpdateProfilesLicenseFunction(ILicenseService licenseService, ILogHelper logHelper)
        {
            _licenseService = licenseService;
            _logHelper = logHelper;
        }

        [FunctionName("PostHttpTriggerUpdateProfilesLicenseFunction")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req)
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();

            await _licenseService.UpdateProfilesLicenseAsync(requestBody);

            await _logHelper.LogSuccessRequest(req.Headers, nameof(HttpTriggerUpdateProfilesLicenseFunction));

            return new OkObjectResult(true);
        }
    }
}

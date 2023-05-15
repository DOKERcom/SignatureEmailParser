using CloudFlare.Client;
using SignatureEmailParser.BusinessLogic.Constants;
using SignatureEmailParser.BusinessLogic.Extensions;
using SignatureEmailParser.BusinessLogic.Helpers;
using SignatureEmailParser.BusinessLogic.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SignatureEmailParser.HttpTriggerFunctions.License.Get
{
    public class HttpTriggerCheckLicenseIsValid
    {
        private readonly IAuthenticationService _authenticationService;
     
        public HttpTriggerCheckLicenseIsValid(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [FunctionName("HttpTriggerCheckLicenseIsValid")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req)
        {
            // we don't want requests coming from anything other than our api
            //if (req.Host.HasValue ? req.Host.Value.Contains(CloudflareConstant.LEADHOOTZDOMAIN, StringComparison.OrdinalIgnoreCase) : false)
            //{
            //    var ip = req.HttpContext.GetRemoteIPAddress(true);

            //    if (!string.IsNullOrEmpty(ip.ToString()))
            //        await CloudflareHelper.BlockIp(ip.ToString(), CloudflareConstant.ZONE);
            //}
            //else
            //{
            //    // this is where we will block any ip addresses that didn't come through cloudflare.  There might
            //    // be a bit way to do this using proxies but I haven't found a way around that yet.
            //    await CloudflareHelper.RemoveIpBlocks(CloudflareConstant.ZONE);
            //}

            var responseModel = await _authenticationService.HasPermissionsAsync(req.Query, nameof(HttpTriggerCheckLicenseIsValid));




            // might need to update this to provide
            // the actual license issue, we have the 
            // value inside the responseModel
            return new OkObjectResult(responseModel.Success);
        }
    }
}

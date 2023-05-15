using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using SignatureEmailParser.BusinessLogic.Services.Interfaces;
using SignatureEmailParser.EFCore.Entities;
using SignatureEmailParser.Models.Enums;
using SignatureEmailParser.Models.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Newtonsoft.Json;
using SignatureEmailParser.Interfaces;
using SignatureEmailParser.Models;

namespace SignatureEmailParser.HttpTriggerFunctions.Parser
{
    public class HttpTriggerGetParser
    {
        private readonly ISocialMediaMappingService _socialMediaMappingService;
        private readonly IAuthenticationService _authenticationService;
        private readonly ISignatureParser _signatureParser;

        public HttpTriggerGetParser(ISocialMediaMappingService socialMediaMappingService, IAuthenticationService authenticationService, ISignatureParser signatureParser)
        {
            _socialMediaMappingService = socialMediaMappingService;
            _authenticationService = authenticationService;
            _signatureParser = signatureParser;
        }
        
        [FunctionName("HttpTriggerGetParser")]
        public async Task<IActionResult> RunAsync([HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req)
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();

            ResponseModel responseModel = new ResponseModel();
            //ResponseModel responseModel = await _authenticationService.HasPermissionsAsync(req.Headers, nameof(HttpTriggerGetParser));

            //if (!responseModel.Success)
            //    return new ObjectResult(responseModel);

            //if (string.IsNullOrWhiteSpace(requestBody))
            //{
            //    return new BadRequestResult();
            //}
            
            EmailParseModel profiles = await _signatureParser.Parse(requestBody);
            responseModel.JsonValue = JsonConvert.SerializeObject(profiles);
            responseModel.Success = true;
            responseModel.ResponseType = ResponseType.Success;
            
            return new ObjectResult(responseModel);
        }
    }
}
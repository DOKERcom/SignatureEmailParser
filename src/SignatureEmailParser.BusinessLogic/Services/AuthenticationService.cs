using SignatureEmailParser.BusinessLogic.Constants;
using SignatureEmailParser.BusinessLogic.Helpers.Interfaces;
using SignatureEmailParser.BusinessLogic.Services.Interfaces;
using SignatureEmailParser.EFCore.Entities;
using SignatureEmailParser.EFCore.Enums;
using SignatureEmailParser.EFCore.Respository.Interfaces;
using SignatureEmailParser.Models.Enums;
using SignatureEmailParser.Models.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignatureEmailParser.BusinessLogic.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IStripeIdentityRepository _stripeIdentityRepository;
        private readonly ILogHelper _logHelper;

        public AuthenticationService(IStripeIdentityRepository stripeIdentityRepository, ILogHelper logHelper)
        {
            _stripeIdentityRepository = stripeIdentityRepository;
            _logHelper = logHelper;
        }

        public async Task<ResponseModel> HasPermissionsAsync(IHeaderDictionary headers, string source, bool ignoreRequestLimits = false)
        {
            ResponseModel responseModel = new ResponseModel();

            headers.TryGetValue("licenseId", out StringValues licenseValues);

            string licenseId = licenseValues.FirstOrDefault();

            if (string.IsNullOrWhiteSpace(licenseId))
            {
                await _logHelper.LogErrorRequest(headers, source, SettingConstant.REQUEST_INVALID_HEADERS_MESSAGE);

                responseModel.Success = false;
                responseModel.ResponseType = Models.Enums.ResponseType.InvalidHeaders;
                return responseModel;
            }

            StripeIdentity stripeIdentity = await _stripeIdentityRepository.GetByLicensingId(licenseId);

            if (!(stripeIdentity is null) && stripeIdentity.StripeStatusType == StripeStatusType.OK)
            {
                if (!ignoreRequestLimits)
                {
                    // get all requests for this user
                    long count = await _logHelper.GetRequestsCountByDate(DateTime.UtcNow.Date, stripeIdentity.LicenseId, source);

                    if (count > 0)
                    {
                        List<RequestLimitModel> requestLimits = Requests.RequestLimitsList.GetRequestLimits();
                        var requestType = requestLimits.Where(i => i.RequestType.ToString() == source).FirstOrDefault();

                        if (requestType != null ? requestType.AllowedRequestsPerDay <= count : false)
                        {
                            responseModel.Success = false;
                            responseModel.ResponseType = ResponseType.RateLimitReached;
                            responseModel.DailyLimitCount = count;
                            return responseModel;
                        }
                    }

                    headers.Add("email", stripeIdentity.Email);

                    await _logHelper.LogSuccessRequest(headers, source);
                    responseModel.DailyLimitCount = count;
                }

                responseModel.Success = true;
                responseModel.ResponseType = ResponseType.Success;
                
                return responseModel;
            }

            await _logHelper.LogErrorRequest(headers, source, SettingConstant.REQUEST_INVALID_HEADERS_MESSAGE);

            responseModel.Success = false;
            responseModel.ResponseType = Models.Enums.ResponseType.InvalidHeaders;
            return responseModel;
        }

        public async Task<ResponseModel> HasPermissionsAsync(IQueryCollection queryCollection, string source, bool ignoreRequestLimits = false)
        {
            ResponseModel responseModel = new ResponseModel();

            string licenseId = queryCollection[SettingConstant.LICENSE_ID_URL_KEY];

            if (string.IsNullOrWhiteSpace(licenseId))
            {
                await _logHelper.LogErrorRequest(queryCollection, source, SettingConstant.REQUEST_INVALID_HEADERS_MESSAGE);
                responseModel.Success = false;
                responseModel.ResponseType = Models.Enums.ResponseType.InvalidHeaders;
                return responseModel;

            }

            StripeIdentity stripeIdentity = await _stripeIdentityRepository.GetByLicensingId(licenseId);

            if (!(stripeIdentity is null) && stripeIdentity.StripeStatusType == StripeStatusType.OK)
            {
                await _logHelper.LogServerRequest(queryCollection, source, SettingConstant.REQUEST_SUCCESS_HEADERS_MESSAGE);

                responseModel.Success = true;
                responseModel.ResponseType = Models.Enums.ResponseType.Success;
                return responseModel;
            }

            await _logHelper.LogErrorRequest(queryCollection, source, SettingConstant.REQUEST_INVALID_HEADERS_MESSAGE);
            responseModel.Success = false;
            responseModel.ResponseType = Models.Enums.ResponseType.InvalidHeaders;
            return responseModel;
        }
    }
}

using SignatureEmailParser.Models.Enums;
using SignatureEmailParser.Models.Requests;
using System;
using System.Collections.Generic;
using System.Text;

namespace SignatureEmailParser.BusinessLogic.Requests
{
    public static class RequestLimitsList
    {
        /// <summary>
        /// This should really come from a db
        /// </summary>
        /// <returns></returns>
        public static List<RequestLimitModel> GetRequestLimits()
        {
            List<RequestLimitModel> requestLimits = new List<RequestLimitModel>();
            requestLimits.Add(new RequestLimitModel() { RequestType = RequestType.HttpTriggerGetProfilesWithMissingInfo, AllowedRequestsPerDay = 20 });
            requestLimits.Add(new RequestLimitModel() { RequestType = RequestType.HttpTriggerGetSocialMediaMappingsByFilter, AllowedRequestsPerDay = 20 });
            requestLimits.Add(new RequestLimitModel() { RequestType = RequestType.HttpTriggerImportProfilesFromLinkedInApp, AllowedRequestsPerDay = 20 });
            requestLimits.Add(new RequestLimitModel() { RequestType = RequestType.HttpTriggerSearchSocialMediaMapping, AllowedRequestsPerDay = 20 });
            requestLimits.Add(new RequestLimitModel() { RequestType = RequestType.HttpTriggerStoreSocialMediaProfilesFunction, AllowedRequestsPerDay = 20 });
            requestLimits.Add(new RequestLimitModel() { RequestType = RequestType.HttpTriggerUpdateProfilesFromSocialMediaFunction, AllowedRequestsPerDay = 20 });

            return requestLimits;
        }

    }
}

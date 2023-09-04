using System;
using System.Collections.Generic;
using System.Text;

namespace SignatureEmailParser
{
    public class Constants
    {
        public const string AZURE_FUNCTION_DOMAIN = "https://api.YOURDOMAIN.com/api/";
        public const string AZURE_GET_ALL_NEW_CITIES_BY_LATEST_ID_URL = "HttpTriggerGetAllNewCitiesByLatestId";
        public const string AZURE_GET_ALL_NEW_REGIONS_BY_LATEST_ID_URL = "HttpTriggerGetAllNewRegionsByLatestId";

        public const string LICENSE_HEADER_NAME = "licenseId";
        public const string AZURE_X_FUNCTION_KEY = "x-functions-key";
        public const string AZURE_X_FUNCTION_KEY_VALUE = "oMKKe9qDgNTV4OwBDrlzLM8no09IopqoA8wS8acCPv6lvA77GQJdgw==";

        public const string TMP_LICENSE = "sub_JtQ2NAeWt8v1VW";
    }
}

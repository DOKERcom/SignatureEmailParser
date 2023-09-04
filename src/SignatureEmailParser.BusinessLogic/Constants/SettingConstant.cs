namespace SignatureEmailParser.BusinessLogic.Constants
{
    public class SettingConstant
    {
        public const string ENVIRONMENT_KEY_POST_AUTORESPONSE_STATISTIC_API_URL = "YOURDOMAINPostAutoResponseStatisticApiUrl";

        public const string ENVIRONMENT_KEY_DB_CONNECTION_STRING = "YOURDOMAINFunctionConnectionString";
        public const string ENVIRONMENT_KEY_STRIPE_SECRET = "StripeSecretKey";
        public const string ENVIRONMENT_TEST_KEY_STRIPE_SECRET = "StripeSecretTestKey";
       
        public const string ENVIRONMENT_KEY_CHILKAT_UNLOCK_BUNDLE = "ChilkatUnlockBundleCode";

        public const string ENCRYPTION_KEY = "secretEncyptionKey";
        public const string ENCRYPTION_SALT = "stringForSalt";
        public const int ENCRYPTION_VECTOR_COUNT_BYTES = 16;
        public const int ENCRYPTION_KEY_COUNT_BYTES = 32;

        public const string PROFILE_PATH = "https://www.linkedin.com/in/";
        public const string PROFILE_URL_KEY = "profileUrl";
        public const string LICENSE_ID_URL_KEY = "licenseId";
        public const string EMAIL_URL_KEY = "email";

//#if DEBUG
//        public const string DB_CONNECTION_STRING = "";
//#else
        public const string DB_CONNECTION_STRING = "Data Source=yourdomain.database.windows.net;Database=YOURDOMAIN.Function;User Id=azure;Password=;MultipleActiveResultSets=true"; 
//#endif

        public const string REQUEST_REQUEST_SUCCESS_MESSAGE = "Data successfully added.";
        public const string REQUEST_BAD_REQUEST_ERROR_MESSAGE = "Data is empty.";

        public const string REQUEST_SEND_STATISTIC_TO_API_SUCCESS_MESSAGE = "Statistic successfully sended";
        public const string REQUEST_SEND_STATISTIC_TO_API_ERROR_MESSAGE = "Statistic didn't sended";

        public const string REQUEST_INVALID_HEADERS_MESSAGE = "Headers is invalid";
        public const string REQUEST_SUCCESS_HEADERS_MESSAGE = "Request was valid";
    }
}

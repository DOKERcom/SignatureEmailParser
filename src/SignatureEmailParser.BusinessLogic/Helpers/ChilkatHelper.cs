//using SignatureEmailParser.BusinessLogic.Constants;
//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace SignatureEmailParser.BusinessLogic.Helpers
//{
//    public class ChilkatHelper
//    {
//        private void ChilkatInit()
//        {
//            Chilkat.Global global = new Chilkat.Global();
//            string bundleUnlockCode = Environment.GetEnvironmentVariable(SettingConstant.ENVIRONMENT_KEY_CHILKAT_UNLOCK_BUNDLE, EnvironmentVariableTarget.Process);
//            if (string.IsNullOrWhiteSpace(bundleUnlockCode))
//            {
//                return;
//            }
//            _isChilkatUnlocked = global.UnlockBundle(bundleUnlockCode);

//            string clientId = Environment.GetEnvironmentVariable(SettingConstant.ENVIRONMENT_KEY_SNOVIO_CLIENT_ID, EnvironmentVariableTarget.Process);
//            string clientSecret = Environment.GetEnvironmentVariable(SettingConstant.ENVIRONMENT_KEY_SNOVIO_CLIENT_SECRET, EnvironmentVariableTarget.Process);
//            if (!_isChilkatUnlocked || string.IsNullOrWhiteSpace(clientId) || string.IsNullOrWhiteSpace(clientSecret))
//            {
//                return;
//            }
//            Chilkat.Rest rest = new Chilkat.Rest();

//            bool bTls = true;
//            int port = 443;
//            bool bAutoReconnect = true;
//            bool success = rest.Connect(SnovIOConstant.URL_SERVICE_API, port, bTls, bAutoReconnect);
//            if (success != true)
//            {
//                return;
//            }
//            //  Provide query params.
//            rest.AddQueryParam("grant_type", "client_credentials");
//            rest.AddQueryParam("client_id", clientId);
//            rest.AddQueryParam("client_secret", clientSecret);

//            string authToken = rest.FullRequestFormUrlEncoded("POST", SnovIOConstant.URL_AUTH);
//            if (rest.LastMethodSuccess != true)
//            {
//                return;
//            }

//            Chilkat.JsonObject json = new Chilkat.JsonObject();
//            bool successLoad = json.Load(authToken);
//            if (!successLoad)
//            {
//                return;
//            }

//            _authToken = json.StringOf("access_token");

//            if (string.IsNullOrWhiteSpace(_authToken))
//            {
//                return;
//            }
//            _chilkatHttp = new Chilkat.Http();
//            _chilkatHttp.AuthToken = _authToken;
//        }
//    }
//}

using SignatureEmailParser.BusinessLogic.Constants;
using System;

namespace SignatureEmailParser.BusinessLogic.Configurations
{
    public class ConnectionStringInjector
    {
        public string GetConnectionString()
        {
            string connectionStringEnvironment = Environment.GetEnvironmentVariable(SettingConstant.ENVIRONMENT_KEY_DB_CONNECTION_STRING, EnvironmentVariableTarget.Process);
            return !string.IsNullOrWhiteSpace(connectionStringEnvironment) ? connectionStringEnvironment : SettingConstant.DB_CONNECTION_STRING;
        }
    }
}

using System.ComponentModel;

namespace SignatureEmailParser.Models.Enums
{
    public enum ConfigurationType
    {
        None = 0,
        
        [Description("DbcConfigurations")]
        DBC = 1,
        
        [Description("ProxyConfiguration")]
        Proxy = 2,
        
        [Description("SendGridConfigurations")]
        SendGrid = 3,
        
        [Description("UpdaterConfigurations")]
        Updater = 4,

        [Description("ZendeskConfigurations")]
        Zendesk = 5
    }
}

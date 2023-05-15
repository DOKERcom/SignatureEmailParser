using System;
using System.Collections.Generic;
using System.Text;

namespace SignatureEmailParser.Models.SocialMediaMappingModels.Nymeria
{
    public class EmailsFromUrlNymeriaResponseModel
    {
        public string request_limit { get; set; }
        public string requests_used { get; set; }
        public string status { get; set; }

        public DataEmailsFromUrlNymeriaModel Data { get; set; }
    }
    public class DataEmailsFromUrlNymeriaModel
    {
        public List<EmailsDataEmailsFromUrlNymeriaModel> Emails { get; set; }
        public DataEmailsFromUrlNymeriaModel()
        {
            Emails = new List<EmailsDataEmailsFromUrlNymeriaModel>();
        }
    }

    public class Profile
    {
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string occupation { get; set; }
        public string industry { get; set; }
        public string location { get; set; }
        public string company { get; set; }
    }

    public class EmailsDataEmailsFromUrlNymeriaModel
    {
        public string Email { get; set; }

        public string Type { get; set; }

        public Profile Profile { get; set; }
    }
}

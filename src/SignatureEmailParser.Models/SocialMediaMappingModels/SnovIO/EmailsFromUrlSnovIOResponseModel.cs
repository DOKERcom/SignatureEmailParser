using System;
using System.Collections.Generic;

namespace SignatureEmailParser.Models.SocialMediaMappingModels.SnovIO
{
    public class EmailsFromUrlSnovIOResponseModel
    {
        public bool Success { get; set; }
        public DataEmailsFromUrlSnovIOModel Data { get; set; }
    }
    public class DataEmailsFromUrlSnovIOModel
    {
        public string Id { get; set; }  
        public string Name { get; set; } 
        public string FirstName { get; set; }  
        public string LastName { get; set; }  
        public string SourcePage { get; set; } 
        public string Source { get; set; }  
        public string Industry { get; set; }
        public string Country { get; set; }
        public string Locality { get; set; }
        public string[] Skills { get; set; }
        public List<CurrentJobsDataEmailsFromUrlSnovIOModel> CurrentJob { get; set; }
        public List<PreviousJobsDataEmailsFromUrlSnovIOModel> PreviousJob { get; set; }
        public List<SocialDataEmailsFromUrlSnovIOModel> Social { get; set; }
        public List<EmailsDataEmailsFromUrlSnovIOModel> Emails { get; set; }
        public DataEmailsFromUrlSnovIOModel()
        {
            Social = new List<SocialDataEmailsFromUrlSnovIOModel>();
            Emails = new List<EmailsDataEmailsFromUrlSnovIOModel>();
            CurrentJob = new List<CurrentJobsDataEmailsFromUrlSnovIOModel>();
            PreviousJob = new List<PreviousJobsDataEmailsFromUrlSnovIOModel>();
            
        }

    }
    public class CurrentJobsDataEmailsFromUrlSnovIOModel
    {
        public string CompanyName { get; set; }
        public string Position { get; set; }
        public string SocialLink { get; set; }
        public string Site { get; set; }
        public string Locality { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string Street2 { get; set; }
        public string Postal { get; set; }
        public string Founded { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Size { get; set; }
        public string Industry { get; set; }
        public string CompanyType { get; set; }
        public string Country { get; set; }
    }
    public class PreviousJobsDataEmailsFromUrlSnovIOModel : CurrentJobsDataEmailsFromUrlSnovIOModel
    {
    }
    public class SocialDataEmailsFromUrlSnovIOModel
    {
        public string Link { get; set; }
        public string Type { get; set; }
    }
    public class EmailsDataEmailsFromUrlSnovIOModel
    {
        public string Email { get; set; }
        public string Status { get; set; }
    }
}

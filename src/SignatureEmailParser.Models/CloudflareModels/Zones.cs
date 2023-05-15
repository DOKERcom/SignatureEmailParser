using System;
using System.Collections.Generic;
using System.Text;

namespace SignatureEmailParser.Models.CloudflareModels
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class Configuration
    {
        public string target { get; set; }
        public string value { get; set; }
    }

    public class Scope
    {
        public string id { get; set; }
        public string email { get; set; }
        public string type { get; set; }
    }

    public class Result
    {
        public string id { get; set; }
        public bool paused { get; set; }
        public DateTime modified_on { get; set; }
        public List<string> allowed_modes { get; set; }
        public string mode { get; set; }
        public string notes { get; set; }
        public Configuration configuration { get; set; }
        public Scope scope { get; set; }
        public DateTime created_on { get; set; }
    }

    public class ResultInfo
    {
        public int page { get; set; }
        public int per_page { get; set; }
        public int count { get; set; }
        public int total_count { get; set; }
        public int total_pages { get; set; }
    }

    public class Zones
    {
        public List<Result> result { get; set; }
        public bool success { get; set; }
        public List<object> errors { get; set; }
        public List<object> messages { get; set; }
        public ResultInfo result_info { get; set; }
    }


}

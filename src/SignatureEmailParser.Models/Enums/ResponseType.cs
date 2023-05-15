using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace SignatureEmailParser.Models.Enums
{
    public enum ResponseType : int
    {
        [Description("NA")]
        NA = 0,

        [Description("Success")]
        Success = 1,

        [Description("Rate limit reached")]
        RateLimitReached = 2,

        [Description("Headers are invalid")]
        InvalidHeaders = 3,

        [Description("Global Database Update Unsuccessful")]
        GlobalUpdateUnsuccessful = 4,

        [Description("Fail")]
        Fail = 5
    }
}

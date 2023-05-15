using SignatureEmailParser.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace SignatureEmailParser.Models.Requests
{
    public class RequestLimitModel
    {
        public RequestType RequestType { get; set; }

        public int AllowedRequestsPerDay { get; set; }
    }
}

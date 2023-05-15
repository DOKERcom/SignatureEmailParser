using SignatureEmailParser.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace SignatureEmailParser.Models.Requests
{
    public class ResponseModel
    {
        public bool Success { get; set; }

        public ResponseType ResponseType { get; set; }

        public string JsonValue { get; set; }

        public long DailyLimitCount { get; set; }
    }
}

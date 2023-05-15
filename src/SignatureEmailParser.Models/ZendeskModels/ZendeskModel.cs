using System;
using System.Collections.Generic;
using System.Text;

namespace SignatureEmailParser.Models.ZendeskModels
{
    public class ZendeskModel
    {
        public string TicketSub { get; set; }
        public string TicketBody { get; set; }
        public string TokenFile { get; set; }
        public string Priority { get; set; }
    }
}

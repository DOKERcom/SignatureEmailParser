using System;
using System.Collections.Generic;
using System.Text;

namespace SignatureEmailParser.BusinessLogic.Helpers
{
    public interface IZenDeskHelper
    {
        public bool CreateNewTicket(string ticketSub, string ticketBody, string tokenFile, string priority);

        public bool ShouldCreateNewTicket(string ticketSubject);
    }
}

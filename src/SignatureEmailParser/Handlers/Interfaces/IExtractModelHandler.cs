using SignatureEmailParser.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SignatureEmailParser.Handlers.Interfaces
{
    public interface IExtractModelHandler
    {

        public Task<EmailParseModel> ExtractData(string data);

    }
}

using SignatureEmailParser.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SignatureEmailParser.Interfaces
{
    public interface ISignatureParser
    {
        public Task<EmailParseModel> Parse(string data);
    }
}

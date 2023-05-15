using SignatureEmailParser.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SignatureEmailParser.Extractors.Interfaces
{
    public interface IInstagramExtractor
    {

        public Task<ExtractWordModels> ExtractInstagrams(string data);

    }
}

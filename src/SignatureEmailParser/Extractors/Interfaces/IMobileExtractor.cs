using SignatureEmailParser.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SignatureEmailParser.Extractors.Interfaces
{
    public interface IMobileExtractor
    {

        public Task<ExtractWordModels> ExtractMobiles(string data);

    }
}

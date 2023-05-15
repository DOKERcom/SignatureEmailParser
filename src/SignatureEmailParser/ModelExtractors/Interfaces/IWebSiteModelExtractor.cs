using SignatureEmailParser.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SignatureEmailParser.ModelExtractors.Interfaces
{
    public interface IWebSiteModelExtractor
    {

        public Task<ExtractWordModels> ExtractPostCodes(string data);

    }
}

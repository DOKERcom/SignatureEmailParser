using SignatureEmailParser.Extractors.Implementation;
using SignatureEmailParser.Extractors.Interfaces;
using SignatureEmailParser.ModelExtractors.Interfaces;
using SignatureEmailParser.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SignatureEmailParser.ModelExtractors.Implementation
{
    public class WebSiteModelExtractor : IWebSiteModelExtractor
    {

        IWebSiteExtractor _webSiteExtractor;

        public WebSiteModelExtractor(IWebSiteExtractor webSiteExtractor)
        {
            _webSiteExtractor = webSiteExtractor;
        }

        public async Task<ExtractWordModels> ExtractPostCodes(string data)
        {
            return await _webSiteExtractor.ExtractWebSites(data);
        }

    }
}

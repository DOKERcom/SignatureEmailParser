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
    public class TwitterModelExtractor : ITwitterModelExtractor
    {

        ITwitterExtractor _twitterExtractor;

        public TwitterModelExtractor(ITwitterExtractor twitterExtractor)
        {
            _twitterExtractor = twitterExtractor;
        }

        public async Task<ExtractWordModels> ExtractPostCodes(string data)
        {
            return await _twitterExtractor.ExtractTwitters(data);
        }

    }
}

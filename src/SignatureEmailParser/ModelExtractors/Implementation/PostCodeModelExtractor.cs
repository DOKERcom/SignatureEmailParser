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
    public class PostCodeModelExtractor : IPostCodeModelExtractor
    {

        IPostCodeExtractor _postCodeExtractor;

        public PostCodeModelExtractor(IPostCodeExtractor postCodeExtractor)
        {
            _postCodeExtractor = postCodeExtractor;
        }

        public async Task<ExtractWordModels> ExtractPostCodes(string data)
        {
            return await _postCodeExtractor.ExtractPostCodes(data);
        }

    }
}

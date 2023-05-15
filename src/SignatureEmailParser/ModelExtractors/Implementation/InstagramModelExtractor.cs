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
    public class InstagramModelExtractor : IInstagramModelExtractor
    {

        IInstagramExtractor _instagramExtractor;

        public InstagramModelExtractor(IInstagramExtractor instagramExtractor)
        {
            _instagramExtractor = instagramExtractor;
        }

        public async Task<ExtractWordModels> ExtractInstagrams(string data)
        {
            return await _instagramExtractor.ExtractInstagrams(data);
        }
    }
}

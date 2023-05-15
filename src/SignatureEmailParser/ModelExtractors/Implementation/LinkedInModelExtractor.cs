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
    public class LinkedInModelExtractor : ILinkedInModelExtractor
    {

        ILinkedInExtractor _linkedInExtractor;

        public LinkedInModelExtractor(ILinkedInExtractor linkedInExtractor)
        {
            _linkedInExtractor = linkedInExtractor;
        }

        public async Task<ExtractWordModels> ExtractLinkedIns(string data)
        {
            return await _linkedInExtractor.ExtractLinkedIns(data);
        }
    }
}

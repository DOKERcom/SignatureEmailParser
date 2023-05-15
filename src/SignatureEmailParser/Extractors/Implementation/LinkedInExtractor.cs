using SignatureEmailParser.Extractors.Interfaces;
using SignatureEmailParser.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SignatureEmailParser.Extractors.Implementation
{
    public class LinkedInExtractor : Extractor, ILinkedInExtractor
    {
        private readonly List<Regex> regexPatterns = new List<Regex>
        {
            new Regex("(http(s)?:\\/\\/)?([\\w]+\\.)?linkedin\\.com\\/(pub|in|profile)\\/.*?(\\s|(.*?\\/\\s$){1,10})"),

        };

        public async Task<ExtractWordModels> ExtractLinkedIns(string data)
        {
            return await ExtractData(data, regexPatterns);
        }
    }
}

using SignatureEmailParser.Extractors.Interfaces;
using SignatureEmailParser.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SignatureEmailParser.Extractors.Implementation
{
    public class TwitterExtractor : Extractor, ITwitterExtractor
    {
        private readonly List<Regex> regexPatterns = new List<Regex>
        {
            new Regex("(?:https?:\\/\\/)?(?:www\\.)?twitter\\.com\\/(?:(?:\\w)*#!\\/)?(?:pages\\/)?(?:[\\w\\-]*\\/)*([\\w\\-]*)"),

        };

        public async Task<ExtractWordModels> ExtractTwitters(string data)
        {
            return await ExtractData(data, regexPatterns);
        }
    }
}

using SignatureEmailParser.Extractors.Interfaces;
using SignatureEmailParser.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SignatureEmailParser.Extractors.Implementation
{
    public class InstagramExtractor : Extractor, IInstagramExtractor
    {
        private readonly List<Regex> regexPatterns = new List<Regex>
        {
            new Regex("(?:(?:http|https):\\/\\/)?(?:www.)?(?:instagram.com|instagr.am|instagr.com)\\/(\\w+)"),

        };

        public async Task<ExtractWordModels> ExtractInstagrams(string data)
        {
            return await ExtractData(data, regexPatterns);
        }
    }
}

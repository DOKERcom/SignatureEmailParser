using SignatureEmailParser.Extractors.Interfaces;
using SignatureEmailParser.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SignatureEmailParser.Extractors.Implementation
{
    public class EmailExtractor : Extractor, IEmailExtractor
    {
        private readonly List<Regex> regexPatterns = new List<Regex>
        {
            new Regex("[-\\w.]+@([A-z0-9][-A-z0-9]+\\.)+[A-z]{2,4}"),

        };

        public async Task<ExtractWordModels> ExtractEmails(string data)
        {
            return await ExtractData(data, regexPatterns);
        }
    }
}

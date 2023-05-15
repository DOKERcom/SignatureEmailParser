using SignatureEmailParser.Extractors.Interfaces;
using SignatureEmailParser.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SignatureEmailParser.Extractors.Implementation
{
    public class MobileExtractor : Extractor, IMobileExtractor
    {
        private readonly List<Regex> regexPatterns = new List<Regex>
        {
            new Regex("\\s(\\+)?((\\d{2,3}) ?\\d|\\d)(([ --)(]*?\\d)|( ?(\\d{2,3}) ?)){7,12}\\d\\s"),

        };

        public async Task<ExtractWordModels> ExtractMobiles(string data)
        {
            return await ExtractData(data, regexPatterns);
        }
    }
}

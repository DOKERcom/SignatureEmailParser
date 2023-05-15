using SignatureEmailParser.Extractors.Interfaces;
using SignatureEmailParser.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SignatureEmailParser.Extractors.Implementation
{
    public class FacebookExtractor : Extractor, IFacebookExtractor
    {

        private readonly List<Regex> regexPatterns = new List<Regex>
        {
            new Regex("(?:http:\\/\\/)?(?:www\\.)?facebook\\.com\\/(?:(?:\\w)*#!\\/)?(?:pages\\/)?(?:[\\w\\-]*\\/)*([\\w\\-]*)"),

        };

        public async Task<ExtractWordModels> ExtractFacebooks(string data)
        {
            return await ExtractData(data, regexPatterns);
        }

    }
}

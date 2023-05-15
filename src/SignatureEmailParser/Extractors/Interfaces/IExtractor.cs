using SignatureEmailParser.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SignatureEmailParser.Extractors.Interfaces
{
    public interface IExtractor
    {

        public Task<ExtractWordModels> ExtractData(string data, List<Regex> regexPatterns);

        public Task<ExtractWordModels> ExtractDataByWords(string data, List<Regex> regexPatterns, bool replaceFoundData = false);

        public string SetGapBetweenWords(string name);

    }
}

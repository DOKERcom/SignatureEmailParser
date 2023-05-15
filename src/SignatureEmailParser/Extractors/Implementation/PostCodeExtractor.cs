using SignatureEmailParser.Extractors.Interfaces;
using SignatureEmailParser.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SignatureEmailParser.Extractors.Implementation
{
    public class PostCodeExtractor : Extractor, IPostCodeExtractor
    {
        private readonly List<Regex> regexPatterns = new List<Regex>
        {
            new Regex("([ --)(][Gg][Ii][Rr] 0[Aa]{2}[ --)(])|([ --)(](([A-Za-z][0-9]{1,2})|(([A-Za-z][A-Ha-hJ-Yj-y][0-9]{1,2})|(([A-Za-z][0-9][A-Za-z])|([A-Za-z][A-Ha-hJ-Yj-y][0-9][A-Za-z]?))))\\s?[0-9][A-Za-z]{2}[ --)(])"),


        };

        public async Task<ExtractWordModels> ExtractPostCodes(string data)
        {
            var result = await ExtractData(data, regexPatterns);

            Regex regex = new Regex("\\W");

            foreach (var extractData in result.ExtractDatas)
            {
                extractData.Word = regex.Replace(extractData.Word, "");
            }

            return result;
        }
    }
}

using SignatureEmailParser.Extractors.Interfaces;
using SignatureEmailParser.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SignatureEmailParser.Extractors.Implementation
{
    public class WebSiteExtractor : Extractor, IWebSiteExtractor
    {
        private readonly List<Regex> regexPatterns = new List<Regex>
        {
            new Regex("(https?:\\/\\/)?([\\w-]{1,32}\\.[\\w-]{1,32})[^\\s@]*"),

        };

        private readonly Regex notSiteFilter = new Regex(".*?(\\.png|\\.jpg|\\.bmp|\\.gif|\\.svg|\\.js|\\.css)");

        public async Task<ExtractWordModels> ExtractWebSites(string data)
        {
            ExtractWordModel extractData = new ExtractWordModel();

            ExtractWordModels newExtractData = new ExtractWordModels();

            ExtractWordModels extractDatas = await ExtractData(data, regexPatterns);

            newExtractData.OutUpdateData = extractDatas.OutUpdateData;

            foreach (var extract in extractDatas.ExtractDatas)
            {
                if (!notSiteFilter.IsMatch(extract.Word))
                {
                    newExtractData.ExtractDatas.Add(extractData.CreateExtractData(extract.Word, extract.FirstCharPos, extract.LastCharPos, extract.WordLength));

                    newExtractData.OutUpdateData = extractDatas.OutUpdateData;
                }
            }

            return newExtractData;
        }
    }
}

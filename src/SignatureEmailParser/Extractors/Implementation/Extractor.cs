using SignatureEmailParser.Extractors.Interfaces;
using SignatureEmailParser.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SignatureEmailParser.Extractors.Implementation
{
    public class Extractor : IExtractor
    {
        public virtual async Task<ExtractWordModels> ExtractData(string data, List<Regex> regexPatterns)
        {
            ExtractWordModel extractData = new ExtractWordModel();

            ExtractWordModels extractDatas = new ExtractWordModels();

            foreach (Regex regex in regexPatterns)
            {
                foreach (Match item in regex.Matches(data))
                {
                    data = data.Remove(item.Index, item.Value.Length);

                    int iterator = 0;

                    while (iterator < item.Value.Length)
                    {
                        data = data.Insert(item.Index, "*");
                        iterator++;
                    }

                    extractDatas.ExtractDatas.Add(extractData.CreateExtractData(item.Value.Trim(), item.Index, item.Index + item.Value.Length, item.Value.Length));

                    extractDatas.OutUpdateData = data;
                }
            }

            if (extractDatas.OutUpdateData == null)
                extractDatas.OutUpdateData = data;

            return extractDatas;
        }

        public virtual async Task<ExtractWordModels> ExtractDataByWords(string data, List<Regex> regexPatterns, bool replaceFoundData = false)
        {
            ExtractWordModel extractData = new ExtractWordModel();

            ExtractWordModels extractDatas = new ExtractWordModels();

            foreach (Regex regex in regexPatterns)
            {
                foreach (Match item in regex.Matches(data))
                {
                    string currentValue = Regex.Match(item.Value, "\\W(.*)\\W").Groups[1].Value;

                    if (replaceFoundData)
                    {
                        data = data.Remove(item.Index + 1, currentValue.Length);

                        int iterator = 0;

                        while (iterator < currentValue.Length)
                        {
                            data = data.Insert(item.Index + 1, "*");
                            iterator++;
                        }
                    }

                    extractDatas.ExtractDatas.Add(extractData.CreateExtractData(currentValue, item.Index + 1, item.Index + 1 + currentValue.Length, currentValue.Length));

                    extractDatas.OutUpdateData = data;
                }
            }

            if (extractDatas.OutUpdateData == null)
                extractDatas.OutUpdateData = data;

            return extractDatas;
        }

        public string SetGapBetweenWords(string name)
        {
            Regex regex = new Regex("[A-Z][a-z]*");

            if (regex.Matches(name).Count > 1)
            {
                string updname = null;

                foreach (Match match in regex.Matches(name))
                {
                    if (!string.IsNullOrEmpty(updname))
                        updname += " " + match.Value;
                    else
                        updname = match.Value;
                }

                return updname.Trim();
            }
            return name;
        }
    }
}

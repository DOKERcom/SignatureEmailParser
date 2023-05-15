using SignatureEmailParser.Extractors.Interfaces;
using SignatureEmailParser.Handlers;
using SignatureEmailParser.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SignatureEmailParser.Extractors.Implementation
{
    public class PositionExtractor : Extractor, IPositionExtractor
    {

        public async Task<ExtractWordModels> ExtractPositions(string data, List<PositionModel> _positionModels)
        {
            List<Regex> regexes = new List<Regex>();

            foreach (var model in _positionModels)
            {
                regexes.Add(new Regex("[\\s|,]" + SetGapBetweenWords(model.PositionName) + "[\\s|,]"));
            }

            return await ExtractDataByWords(data, regexes);
        }

    }
}

using SignatureEmailParser.Extractors.Implementation;
using SignatureEmailParser.Extractors.Interfaces;
using SignatureEmailParser.ModelExtractors.Interfaces;
using SignatureEmailParser.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SignatureEmailParser.ModelExtractors.Implementation
{
    public class PositionModelExtractor : IPositionModelExtractor
    {

        private List<PositionModel> _positionModels;

        private IPositionExtractor _positionExtractor;

        public PositionModelExtractor(List<PositionModel> positionModels, IPositionExtractor positionExtractor)
        {
            _positionModels = positionModels;

            _positionExtractor = positionExtractor = new PositionExtractor();
        }

        public async Task<ExtractWordModels> ExtractPositions(string data)
        {
            return await _positionExtractor.ExtractPositions(data, _positionModels);
        }
    }
}

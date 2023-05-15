using SignatureEmailParser.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SignatureEmailParser.Extractors.Interfaces
{
    public interface IPositionExtractor
    {

        public Task<ExtractWordModels> ExtractPositions(string data, List<PositionModel> _positionModels);

    }
}

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
    public class MobileModelExtractor : IMobileModelExtractor
    {

        IMobileExtractor _mobileExtractor;

        public MobileModelExtractor(IMobileExtractor mobileExtractor)
        {
            _mobileExtractor = mobileExtractor;
        }

        public async Task<ExtractWordModels> ExtractMobiles(string data)
        {
            return await _mobileExtractor.ExtractMobiles(data);
        }

    }
}

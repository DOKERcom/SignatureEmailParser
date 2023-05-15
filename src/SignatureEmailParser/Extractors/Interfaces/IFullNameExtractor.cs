using SignatureEmailParser.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SignatureEmailParser.Extractors.Interfaces
{
    public interface IFullNameExtractor
    {

        public Task<ExtractWordModels> ExtractNames(string data, List<string> names = null);

        public Task<ExtractWordModels> ExtractSurnamesByNames(string data, ExtractWordModels names, List<string> surnames = null);

        public Task<ExtractWordModels> ExtractNearestSurnames(string data, ExtractWordModels names);

        public Task<ExtractWordModels> ExtractNearestLastNames(string data, ExtractWordModels names);

    }
}

using SignatureEmailParser.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SignatureEmailParser.Extractors.Interfaces
{
    public interface ILocationExtractor
    {

        public ExtractWordModels ExtractLocations();

        public Task<ExtractWordModels> ExtractCountries(string data, List<CountryModel> countryModels = null);

        public Task<ExtractWordModels> ExtractRegions(string data, List<RegionModel> regionModels = null);

        public Task<ExtractWordModels> ExtractCities(string data, List<CityModel> cityModels = null);

    }
}

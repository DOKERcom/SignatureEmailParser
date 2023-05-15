using Newtonsoft.Json;
using SignatureEmailParser.Factories.Interfaces;
using SignatureEmailParser.Models;
using System.Collections.Generic;

namespace SignatureEmailParser.Factories.Implementation
{
    public class ListModelToJsonFactory : IListModelToJsonFactory
    {
        public string CountryModelsToJson(List<CountryModel> countries)
        {
            return JsonConvert.SerializeObject(countries);
        }

        public string PositionModelsToJson(List<PositionModel> positions)
        {
            return JsonConvert.SerializeObject(positions);
        }

        public string RegionModelsToJson(List<RegionModel> regions)
        {
            return JsonConvert.SerializeObject(regions);
        }

        public string CityModelsToJson(List<CityModel> cities)
        {
            return JsonConvert.SerializeObject(cities);
        }
    }
}

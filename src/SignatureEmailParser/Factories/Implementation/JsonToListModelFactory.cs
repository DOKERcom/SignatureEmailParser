using Newtonsoft.Json;
using SignatureEmailParser.Factories.Interfaces;
using SignatureEmailParser.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SignatureEmailParser.Factories.Implementation
{
    public class JsonToListModelFactory : IJsonToListModelFactory
    {
        public List<CountryModel> JsonToCountryModels(string countries)
        {
            return JsonConvert.DeserializeObject<List<CountryModel>>(countries);
        }

        public List<PositionModel> JsonToPositionModels(string positions)
        {
            return JsonConvert.DeserializeObject<List<PositionModel>>(positions);
        }

        public List<RegionModel> JsonToRegionModels(string regions)
        {
            return JsonConvert.DeserializeObject<List<RegionModel>>(regions);
        }

        public List<CityModel> JsonToCityModels(string cities)
        {
            return JsonConvert.DeserializeObject<List<CityModel>>(cities);
        }

        public List<CityModel> JsonToCityModels(List<string> cities)
        {
            List<CityModel> citiesList = new List<CityModel>();

            foreach (string city in cities)
            {
                citiesList.AddRange(JsonConvert.DeserializeObject<List<CityModel>>(city));
            }

            return citiesList;
        }

        public List<FirstNameModel> JsonToFirstNames(string firstNames)
        {
            return JsonConvert.DeserializeObject<List<FirstNameModel>>(firstNames);
        }

        public List<string> JsonToLastNames(string lastNames)
        {
            return JsonConvert.DeserializeObject<List<string>>(lastNames);
        }
    }
}

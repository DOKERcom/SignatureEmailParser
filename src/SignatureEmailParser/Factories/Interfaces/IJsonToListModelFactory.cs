using SignatureEmailParser.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SignatureEmailParser.Factories.Interfaces
{
    public interface IJsonToListModelFactory
    {

        public List<CountryModel> JsonToCountryModels(string countries);

        public List<PositionModel> JsonToPositionModels(string positions);

        public List<RegionModel> JsonToRegionModels(string regions);

        public List<CityModel> JsonToCityModels(string cities);

        public List<CityModel> JsonToCityModels(List<string> cities);

        public List<FirstNameModel> JsonToFirstNames(string firstNames);

        public List<string> JsonToLastNames(string lastNames);

    }
}

using SignatureEmailParser.Factories.Implementation;
using SignatureEmailParser.Factories.Interfaces;
using SignatureEmailParser.Handlers.Interfaces;
using SignatureEmailParser.Helpers;
using SignatureEmailParser.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SignatureEmailParser.Handlers.Implementation
{
    public class JsonDataHandler : IJsonDataHandler
    {
        private IJsonToListModelFactory _jsonToListModel;
        
        private readonly string _basepath;

        public JsonDataHandler()
        {
            _jsonToListModel = new JsonToListModelFactory();
            _basepath = "JsonData/";
        }

        public List<CountryModel> GetCountriesFromJsonData()
        {
            return _jsonToListModel.JsonToCountryModels(FileHelper.ReadAllFromFile(_basepath, "Countries.json"));
        }

        public List<RegionModel> GetRegionsFromJsonData()
        {
            return _jsonToListModel.JsonToRegionModels(FileHelper.ReadAllFromFile(_basepath, "Regions.json"));
        }

        public List<CityModel> GetCitiesFromJsonData()
        {
            return _jsonToListModel.JsonToCityModels(FileHelper.ReadAllFromFile(_basepath, "Cities.json"));
        }

        public List<PositionModel> GetPositionsFromJsonData()
        {
            return _jsonToListModel.JsonToPositionModels(FileHelper.ReadAllFromFile(_basepath, "Positions.json"));
        }

        public List<FirstNameModel> GetFirstNamesFromJsonData()
        {
            return _jsonToListModel.JsonToFirstNames(FileHelper.ReadAllFromFile(_basepath, "FirstNames.json"));
        }

        public List<string> GetLastNamesFromJsonData()
        {
            return _jsonToListModel.JsonToLastNames(FileHelper.ReadAllFromFile(_basepath, "LastNames.json"));
        }
    }
}

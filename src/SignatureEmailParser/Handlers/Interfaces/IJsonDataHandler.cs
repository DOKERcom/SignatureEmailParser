using SignatureEmailParser.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SignatureEmailParser.Handlers.Interfaces
{
    public interface IJsonDataHandler
    {

        public List<CountryModel> GetCountriesFromJsonData();

        public List<RegionModel> GetRegionsFromJsonData();

        public List<CityModel> GetCitiesFromJsonData();

        public List<PositionModel> GetPositionsFromJsonData();

        public List<FirstNameModel> GetFirstNamesFromJsonData();

        public List<string> GetLastNamesFromJsonData();

    }
}

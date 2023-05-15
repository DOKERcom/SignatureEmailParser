using SignatureEmailParser.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SignatureEmailParser.Factories.Interfaces
{
    public interface IListModelToJsonFactory
    {

        public string CountryModelsToJson(List<CountryModel> countries);

        public string PositionModelsToJson(List<PositionModel> positions);

        public string RegionModelsToJson(List<RegionModel> regions);

        public string CityModelsToJson(List<CityModel> cities);

    }
}

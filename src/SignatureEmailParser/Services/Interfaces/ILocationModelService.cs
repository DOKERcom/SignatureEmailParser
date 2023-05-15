using SignatureEmailParser.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SignatureEmailParser.Services.Interfaces
{
    public interface ILocationModelService
    {

        public List<LocationModel> ConnectLocationModel(List<CountryModel> countryModels, List<CityModel> cityModels, List<RegionModel> regionModels, List<CountryModel> allCountryModels);

    }
}

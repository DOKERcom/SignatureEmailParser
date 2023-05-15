using SignatureEmailParser.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SignatureEmailParser.ModelExtractors.Interfaces
{
    public interface ILocationModelExtractor
    {

        public Task<ExtractWordModels> ExtractCountries(string data, List<CountryModel> countryModels = null);

        public Task<ExtractWordModels> ExtractRegions(string data, List<RegionModel> regionModels = null);

        public Task<ExtractWordModels> ExtractCities(string data, List<CityModel> cityModels = null);

        public Task<ExtractWordModels> ExtractCitiesDependCountry(string data, ExtractWordModel country);

        public Task<ExtractWordModels> ExtractRegionsDependCountry(string data, ExtractWordModel country);

        public Task<ExtractWordModels> ExtractCountriesDependRegion(string data, ExtractWordModel region);

        public Task<ExtractWordModels> ExtractCountriesDependCity(string data, ExtractWordModel city);

        public List<CountryModel> GetCountriesByNames(ExtractWordModels countries);

        public List<CityModel> GetCitiesByNames(ExtractWordModels cities);

        public List<RegionModel> GetRegionsByNames(ExtractWordModels regions);

        public CountryModel GetCountryByName(string countryName);

        public CityModel GetCityByName(string cityName);

        public RegionModel GetRegionByName(string regionName);

    }
}

using SignatureEmailParser.Extractors.Implementation;
using SignatureEmailParser.Extractors.Interfaces;
using SignatureEmailParser.ModelExtractors.Interfaces;
using SignatureEmailParser.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignatureEmailParser.ModelExtractors.Implementation
{
    public class LocationModelExtractor : ILocationModelExtractor
    {
        private List<CountryModel> _countryModels;

        private List<RegionModel> _regionModels;

        private List<CityModel> _cityModels;

        private ILocationExtractor _locationExtractor;

        public LocationModelExtractor(List<CountryModel> countryModels, List<RegionModel> regionModels, List<CityModel> cityModels)
        {
            if (_countryModels == null)
                _countryModels = countryModels;

            if (_regionModels == null)
                _regionModels = regionModels;

            if (_cityModels == null)
                _cityModels = cityModels;

            if (_locationExtractor == null)
                _locationExtractor = new LocationExtractor(_countryModels, _regionModels, _cityModels);
        }


        public async Task<ExtractWordModels> ExtractCountries(string data, List<CountryModel> countryModels = null)
        {
            return await _locationExtractor.ExtractCountries(data, countryModels);
        }

        public async Task<ExtractWordModels> ExtractRegions(string data, List<RegionModel> regionModels = null)
        {
            return await _locationExtractor.ExtractRegions(data, regionModels);
        }

        public async Task<ExtractWordModels> ExtractCities(string data, List<CityModel> cityModels = null)
        {
            return await _locationExtractor.ExtractCities(data, cityModels);
        }

        public async Task<ExtractWordModels> ExtractCitiesDependCountry(string data, ExtractWordModel country)
        {
            CountryModel countryModel = GetCountryByName(country.Word);

            if (countryModel != null)
            {
                return await _locationExtractor.ExtractCities(data, _cityModels.Where(c => c.CountryId == countryModel.Id).ToList());
            }

            return null;
        }

        public async Task<ExtractWordModels> ExtractRegionsDependCountry(string data, ExtractWordModel country)
        {
            CountryModel countryModel = GetCountryByName(country.Word);

            if (countryModel != null)
            {
                return await _locationExtractor.ExtractRegions(data, _regionModels.Where(c => c.CountryId == countryModel.Id).ToList());
            }

            return null;
        }

        public async Task<ExtractWordModels> ExtractCountriesDependRegion(string data, ExtractWordModel region)
        {
            RegionModel regionModel = GetRegionByName(region.Word);

            if (regionModel != null)
            {
                return await _locationExtractor.ExtractCountries(data, _countryModels.Where(c => c.Id == regionModel.CountryId).ToList());
            }

            return null;
        }

        public async Task<ExtractWordModels> ExtractCountriesDependCity(string data, ExtractWordModel city)
        {
            CityModel cityModel = GetCityByName(city.Word);

            if (cityModel != null)
            {
                return await _locationExtractor.ExtractCountries(data, _countryModels.Where(c => c.Id == cityModel.CountryId).ToList());
            }

            return null;
        }

        public List<CountryModel> GetCountriesByNames(ExtractWordModels countries)
        {
            List<CountryModel> countryModels = new List<CountryModel>();

            foreach (var country in countries.ExtractDatas)
            {
                var model = GetCountryByName(country.Word);

                if (model != null)
                    countryModels.Add(model);
            }

            return countryModels;
        }

        public List<CityModel> GetCitiesByNames(ExtractWordModels cities)
        {
            List<CityModel> cityModels = new List<CityModel>();

            foreach (var city in cities.ExtractDatas)
            {
                var model = GetCityByName(city.Word);

                if (model != null)
                    cityModels.Add(model);
            }

            return cityModels;
        }

        public List<RegionModel> GetRegionsByNames(ExtractWordModels regions)
        {
            List<RegionModel> regionModels = new List<RegionModel>();

            foreach (var region in regions.ExtractDatas)
            {
                var model = GetRegionByName(region.Word);

                if (model != null)
                    regionModels.Add(model);
            }

            return regionModels;
        }

        public CountryModel GetCountryByName(string countryName)
        {
            return _countryModels.Where(c => c.CountryName == countryName).FirstOrDefault();
        }

        public CityModel GetCityByName(string cityName)
        {
            return _cityModels.Where(c => c.CityName == cityName).FirstOrDefault();
        }

        public RegionModel GetRegionByName(string regionName)
        {
            return _regionModels.Where(c => c.RegionName == regionName).FirstOrDefault();
        }
    }
}

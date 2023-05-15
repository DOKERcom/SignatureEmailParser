using SignatureEmailParser.Extractors.Interfaces;
using SignatureEmailParser.Handlers;
using SignatureEmailParser.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SignatureEmailParser.Extractors.Implementation
{
    public class LocationExtractor : Extractor, ILocationExtractor
    {
        private List<CountryModel> _countryModels;

        private List<RegionModel> _regionModels;

        private List<CityModel> _cityModels;

        public LocationExtractor(
            List<CountryModel> countryModels,
            List<RegionModel> regionModels,
            List<CityModel> cityModels)
        {
            if (_countryModels == null)
                _countryModels = countryModels;

            if (_regionModels == null)
                _regionModels = regionModels;

            if (_cityModels == null)
                _cityModels = cityModels;
        }

        public ExtractWordModels ExtractLocations()
        {
            return null;
        }

        public async Task<ExtractWordModels> ExtractCountries(string data, List<CountryModel> countryModels = null)
        {
            List<Regex> regexes = new List<Regex>();

            if (countryModels == null)
                countryModels = _countryModels;

            foreach (var model in countryModels)
            {
                regexes.Add(new Regex("[\\s|,]" + model.CountryName + "[\\s|,]"));
            }

            return await ExtractDataByWords(data, regexes);
        }

        public async Task<ExtractWordModels> ExtractRegions(string data, List<RegionModel> regionModels = null)
        {
            List<Regex> regexes = new List<Regex>();

            if (regionModels == null)
                regionModels = _regionModels;

            foreach (var model in regionModels)
            {
                regexes.Add(new Regex("[\\s|,]" + model.RegionName + "[\\s|,]"));
            }

            return await ExtractDataByWords(data, regexes);
        }

        public async Task<ExtractWordModels> ExtractCities(string data, List<CityModel> cityModels = null)
        {
            List<Regex> regexes = new List<Regex>();

            if (cityModels == null)
                cityModels = _cityModels;

            foreach (var model in cityModels)
            {
                if (model.CityName.Split('/', ',', '&').Length > 1)
                {
                    foreach (string subname in model.CityName.Split('/', ',', '&'))
                    {
                        if (subname.Length > 5)
                            regexes.Add(new Regex("[\\s|,]" + subname.Trim() + "[\\s|,]"));
                    }
                }
                else
                {
                    if (model.CityName.Length > 3)
                        regexes.Add(new Regex("[\\s|,]" + model.CityName + "[\\s|,]"));
                }
            }

            return await ExtractDataByWords(data, regexes);
        }


    }
}

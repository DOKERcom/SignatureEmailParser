using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SignatureEmailParser.Extractors;
using SignatureEmailParser.Factories;
using SignatureEmailParser.Handlers.Implementation;
using SignatureEmailParser.Handlers.Interfaces;
using SignatureEmailParser.Helpers;
using SignatureEmailParser.Interfaces;
using SignatureEmailParser.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SignatureEmailParser
{
    public class SignatureParser : ISignatureParser
    {

        JsonDataHandler jsonDataHandler = new JsonDataHandler();

        private List<CountryModel> _countryModels;

        private List<RegionModel> _regionModels;

        private List<CityModel> _cityModels;

        private List<PositionModel> _positionModels;

        private List<FirstNameModel> _firstNames;

        private List<string> _lastNames;

        private IExtractModelHandler _extractModelHandler;

        public async Task<EmailParseModel> Parse(string data)
        {

            if (_countryModels == null)
                _countryModels = jsonDataHandler.GetCountriesFromJsonData();

            if (_regionModels == null)
                _regionModels = jsonDataHandler.GetRegionsFromJsonData();

            if (_cityModels == null)
                _cityModels = jsonDataHandler.GetCitiesFromJsonData();

            if (_positionModels == null)
                _positionModels = jsonDataHandler.GetPositionsFromJsonData();

            if (_firstNames == null)
                _firstNames = jsonDataHandler.GetFirstNamesFromJsonData();

            if (_lastNames == null)
                _lastNames = jsonDataHandler.GetLastNamesFromJsonData();

            if (_extractModelHandler == null)
                _extractModelHandler = new ExtractModelHandler(_countryModels, _regionModels, _cityModels, _positionModels, _firstNames, _lastNames);

            return await _extractModelHandler.ExtractData(data);
        }
    }
}

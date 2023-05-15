using SignatureEmailParser.Models;
using System;
using System.Collections.Generic;
using System.Text;
using SignatureEmailParser.Factories;
using System.Linq;
using System.Text.RegularExpressions;
using SignatureEmailParser.Services;
using System.Threading.Tasks;
using SignatureEmailParser.ModelExtractors.Implementation;
using SignatureEmailParser.Extractors.Implementation;
using SignatureEmailParser.Handlers.Interfaces;
using SignatureEmailParser.ModelExtractors.Interfaces;
using SignatureEmailParser.Extensions;

namespace SignatureEmailParser.Handlers.Implementation
{
    public class ExtractDataHandler : IExtractDataHandler
    {
        private List<CountryModel> _countryModels;

        private List<RegionModel> _regionModels;

        private List<CityModel> _cityModels;

        private List<PositionModel> _positionModels;

        private List<FirstNameModel> _firstNames;

        private List<string> _lastNames;

        PositionExtractor _positionExtractor;

        private ILocationModelExtractor _locationModelExtractor;

        private IDataFilterHandler _dataFilterHandler;

        private IFullNameModelExtractor _fullNameExtractor;

        private ExtractParallelHandler _extractParallelHandler;
        private string globalData { get; set; }


        public ExtractDataHandler(
            List<CountryModel> countryModels, 
            List<RegionModel> regionModels, 
            List<CityModel> cityModels, 
            List<PositionModel> positionModels, 
            List<FirstNameModel> firstNames,
            List<string> lastNames)
        {
            if (_countryModels == null)
                _countryModels = countryModels;

            if (_regionModels == null)
                _regionModels = regionModels;

            if (_cityModels == null)
                _cityModels = cityModels;

            if (_positionModels == null)
                _positionModels = positionModels;

            if (_locationModelExtractor == null)
                _locationModelExtractor = new LocationModelExtractor(_countryModels, _regionModels, _cityModels);

            if (_firstNames == null)
                _firstNames = firstNames;

            if (_lastNames == null)
                _lastNames = lastNames;

            if(_fullNameExtractor == null)
                _fullNameExtractor = new FullNameModelExtractor(_firstNames, _lastNames);

            if (_dataFilterHandler == null)
                _dataFilterHandler = new DataFilterHandler();

            if(_positionExtractor == null)
                _positionExtractor = new PositionExtractor();

            if (_extractParallelHandler == null)
            {
                _extractParallelHandler = new ExtractParallelHandler(this);
            }
        }

        public async Task<ExtractDataModel> ExtractData(string data)
        {
            globalData = data;

            ExtractDataModel extractDataModel = new ExtractDataModel();

            extractDataModel.WorkEmails = await ExtractEmails(globalData);
            extractDataModel.LinkedIns = await ExtractLinkedIns(globalData);
            extractDataModel.Twitters = await ExtractTwitters(globalData);
            extractDataModel.Instagrams = await ExtractInstagrams(globalData);
            extractDataModel.Facebooks = await ExtractFacebooks(globalData);
            extractDataModel.Mobiles = await ExtractMobiles(globalData);
            extractDataModel.WebSites = await ExtractWebSites(globalData);
            extractDataModel.PostCodes = await ExtractPostCodes(globalData);
            extractDataModel.Positions = _extractParallelHandler.GetParallelExtractPositions(globalData, _positionModels);
            extractDataModel.Countries = await ExtractCountries(globalData);
            extractDataModel.Cities = _extractParallelHandler.GetParallelExtractCities(globalData, _cityModels);
            extractDataModel.Regions = _extractParallelHandler.GetParallelExtractRegions(globalData, _regionModels);
            extractDataModel.Names = _dataFilterHandler.ClearDuplicatesByWord(_extractParallelHandler.GetParallelExtractNames(globalData, _firstNames));
            extractDataModel.Surnames = _extractParallelHandler.GetParallelExtractSurnamesByNames(globalData, extractDataModel.Names);

            if (extractDataModel.Surnames.ExtractDatas.Count < 1)
                extractDataModel.Surnames = await ExtractNearestSurnames(globalData, extractDataModel.Names);

            extractDataModel.GlobalData = globalData;

            return extractDataModel;
        }

        


        private ExtractWordModels UpdateGlobalData(ExtractWordModels models)
        {
            globalData = models.OutUpdateData;

            return models;
        }

        public async Task<ExtractWordModels> ExtractNames(string data, List<string> firstNameModels)
        {
            return UpdateGlobalData(await _fullNameExtractor.ExtractNames(data, firstNameModels));
        }

        public async Task<ExtractWordModels> ExtractSurnamesByNames(string data, ExtractWordModels names)
        {
            return UpdateGlobalData(await _fullNameExtractor.ExtractSurnamesByNames(data, names));
        }

        private async Task<ExtractWordModels> ExtractNearestSurnames(string data, ExtractWordModels names)
        {
            return UpdateGlobalData(await _fullNameExtractor.ExtractNearestSurnames(data, names));
        }

        private async Task<ExtractWordModels> ExtractLinkedIns(string data)
        {
            LinkedInExtractor linkedInExtractor = new LinkedInExtractor();

            return UpdateGlobalData(await linkedInExtractor.ExtractLinkedIns(data));
        }

        private async Task<ExtractWordModels> ExtractEmails(string data)
        {
            EmailExtractor emailExtractor = new EmailExtractor();

            return UpdateGlobalData(await emailExtractor.ExtractEmails(data));
        }

        private async Task<ExtractWordModels> ExtractMobiles(string data)
        {
            MobileExtractor mobileExtractor = new MobileExtractor();

            return UpdateGlobalData(await mobileExtractor.ExtractMobiles(data));
        }

        private async Task<ExtractWordModels> ExtractTwitters(string data)
        {
            TwitterExtractor twitterExtractor = new TwitterExtractor();

            return UpdateGlobalData(await twitterExtractor.ExtractTwitters(data));
        }

        private async Task<ExtractWordModels> ExtractFacebooks(string data)
        {
            FacebookExtractor facebookExtractor = new FacebookExtractor();

            return UpdateGlobalData(await facebookExtractor.ExtractFacebooks(data));
        }

        private async Task<ExtractWordModels> ExtractInstagrams(string data)
        {
            InstagramExtractor instagramExtractor = new InstagramExtractor();

            return UpdateGlobalData(await instagramExtractor.ExtractInstagrams(data));
        }

        private async Task<ExtractWordModels> ExtractWebSites(string data)
        {
            WebSiteExtractor webSiteExtractor = new WebSiteExtractor();

            return UpdateGlobalData(await webSiteExtractor.ExtractWebSites(data));
        }

        private async Task<ExtractWordModels> ExtractCountries(string data)
        {
            return UpdateGlobalData(await _locationModelExtractor.ExtractCountries(data));
        }

        private async Task<ExtractWordModels> ExtractCountriesDependCity(string data, ExtractWordModel city)
        {
            var extractedDatas = await _locationModelExtractor.ExtractCountriesDependCity(data, city);

            if (extractedDatas != null)
                return UpdateGlobalData(extractedDatas);

            return extractedDatas;
        }

        private async Task<ExtractWordModels> ExtractCountriesDependRegion(string data, ExtractWordModel region)
        {
            var extractedDatas = await _locationModelExtractor.ExtractCountriesDependRegion(data, region);

            if (extractedDatas != null)
                return UpdateGlobalData(extractedDatas);

            return extractedDatas;
        }

        private async Task<ExtractWordModels> ExtractCitiesDependCountry(string data, ExtractWordModel country)
        {
            var extractedDatas = await _locationModelExtractor.ExtractCitiesDependCountry(data, country);

            if (extractedDatas != null)
                return UpdateGlobalData(extractedDatas);

            return extractedDatas;
        }

        private async Task<ExtractWordModels> ExtractRegionsDependCountry(string data, ExtractWordModel country)
        {
            var extractedDatas = await _locationModelExtractor.ExtractRegionsDependCountry(data, country);

            if (extractedDatas != null)
                return UpdateGlobalData(extractedDatas);

            return extractedDatas;
        }

        public async Task<ExtractWordModels> ExtractRegions(string data, List<RegionModel> regionModels)
        {
            return UpdateGlobalData(await _locationModelExtractor.ExtractRegions(data, regionModels));
        }

        public async Task<ExtractWordModels> ExtractCities(string data, List<CityModel> cityModels)
        {
            return UpdateGlobalData(await _locationModelExtractor.ExtractCities(data, cityModels));
        }

        private async Task<ExtractWordModels> ExtractPostCodes(string data)
        {
            PostCodeExtractor postCodeExtractor = new PostCodeExtractor();

            return UpdateGlobalData(await postCodeExtractor.ExtractPostCodes(data));
        }

        public async Task<ExtractWordModels> ExtractPositions(string data, List<PositionModel> positionModels)
        {

            return UpdateGlobalData(await _positionExtractor.ExtractPositions(data, positionModels));

        }
    }
}

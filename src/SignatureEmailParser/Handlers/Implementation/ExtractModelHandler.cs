using SignatureEmailParser.Handlers.Interfaces;
using SignatureEmailParser.ModelExtractors.Implementation;
using SignatureEmailParser.ModelExtractors.Interfaces;
using SignatureEmailParser.Models;
using SignatureEmailParser.Services.Implementation;
using SignatureEmailParser.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SignatureEmailParser.Handlers.Implementation
{


    public class ExtractModelHandler : IExtractModelHandler
    {
        private List<CountryModel> _countryModels;

        private List<RegionModel> _regionModels;

        private List<CityModel> _cityModels;

        private List<PositionModel> _positionModels;

        private List<FirstNameModel> _firstNames;

        private List<string> _lastNames;

        private ILocationModelExtractor _locationModelExtractor;

        private IExtractDataHandler _extractDataHandler;

        private ILocationModelService _locationModelService;

        private IDataFilterHandler _dataFilterHandler;

        private IEmailParseModelFillService _emailParseModelFillService;

        private EmailModifierService _emailModifierService;
        private string globalData { get; set; }

        public ExtractModelHandler(
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

            if (_extractDataHandler == null)
                _extractDataHandler = new ExtractDataHandler(_countryModels, _regionModels, _cityModels, _positionModels, _firstNames, _lastNames);

            if (_locationModelService == null)
                _locationModelService = new LocationModelService();

            if (_dataFilterHandler == null)
                _dataFilterHandler = new DataFilterHandler();

            if (_emailParseModelFillService == null)
                _emailParseModelFillService = new EmailParseModelFillService();

            if (_emailModifierService == null)
                _emailModifierService = new EmailModifierService();
        }


        public async Task<EmailParseModel> ExtractData(string data)
        {

            List<string> datas = _emailModifierService.SplitEmailToBlocks(data);

            if (datas.Count > 1)
                data = datas[0];

            EmailParseModel emailParseModel = new EmailParseModel();

            emailParseModel = await ExtractDataToEmailParseModel(data);
 
            return emailParseModel;
        }

        private List<EmailParseModel> GetParallel(List<string> datas)
        {
            List<EmailParseModel> emailParseModels = new List<EmailParseModel>();

            Parallel.ForEach(datas, data =>
            {

                emailParseModels.Add(ExtractDataToEmailParseModel(data).Result);

            });

            return emailParseModels;
        }

        private async Task<EmailParseModel> ExtractDataToEmailParseModel(string data)
        {
            ExtractDataModel extractDataModel = await _extractDataHandler.ExtractData(data);

            int middleNumber = GetMiddleNumber(UniteExtractWordModels(extractDataModel));

            globalData = extractDataModel.GlobalData;

            EmailParseModel emailParseModel = new EmailParseModel();

            var locationModel = await GetLocationModel(extractDataModel.Countries, extractDataModel.Cities, extractDataModel.Regions);

            var nameModel = await GetNameModel(extractDataModel.Names, extractDataModel.Surnames);

            var positionWord = _dataFilterHandler.GetLongestWord(extractDataModel.Positions).Word;

            emailParseModel = _emailParseModelFillService.FillName(emailParseModel, nameModel);

            emailParseModel = _emailParseModelFillService.FillLocation(emailParseModel, locationModel);

            emailParseModel = _emailParseModelFillService.FillPosition(emailParseModel, GetPositionModelByName(positionWord, _positionModels), positionWord);

            emailParseModel = _emailParseModelFillService.FillEmails(emailParseModel, SortWordModelsToMiddleNumber(extractDataModel.WorkEmails, middleNumber));

            emailParseModel = _emailParseModelFillService.FillFacebook(emailParseModel, SortWordModelsToMiddleNumber(extractDataModel.Facebooks, middleNumber));

            emailParseModel = _emailParseModelFillService.FillPostCode(emailParseModel, SortWordModelsToMiddleNumber(extractDataModel.PostCodes, middleNumber));

            emailParseModel = _emailParseModelFillService.FillInstagram(emailParseModel, SortWordModelsToMiddleNumber(extractDataModel.Instagrams, middleNumber));

            emailParseModel = _emailParseModelFillService.FillLinkedIn(emailParseModel, SortWordModelsToMiddleNumber(extractDataModel.LinkedIns, middleNumber));

            emailParseModel = _emailParseModelFillService.FillMobile(emailParseModel, SortWordModelsToMiddleNumber(extractDataModel.Mobiles, middleNumber));

            emailParseModel = _emailParseModelFillService.FillWebSite(emailParseModel, SortWordModelsToMiddleNumber(extractDataModel.WebSites, middleNumber));

            emailParseModel = _emailParseModelFillService.FillTwitter(emailParseModel, SortWordModelsToMiddleNumber(extractDataModel.Twitters, middleNumber));

            //if (extractDataModel.Addresses.ExtractDatas.Count > 0)
            //    emailParseModel.Address = extractDataModel.Addresses.ExtractDatas[0].Word;

            //if (extractDataModel.Telephones.ExtractDatas.Count > 0)
            //    emailParseModel.Telephone = extractDataModel.Telephones.ExtractDatas[0].Word;

            emailParseModel = CalculatePercence(emailParseModel);

            return emailParseModel;
        }


        private EmailParseModel CalculatePercence(EmailParseModel emailParseModel)
        {
            int partsAvailable = 0;

            int partsFound = 0;

            if (!string.IsNullOrEmpty(emailParseModel.Name)) { partsAvailable++; partsFound++; } else partsAvailable++;

            if (!string.IsNullOrEmpty(emailParseModel.Surname)) { partsAvailable++; partsFound++; } else partsAvailable++;

            if (!string.IsNullOrEmpty(emailParseModel.WorkEmail)) { partsAvailable++; partsFound++; } else partsAvailable++;

            if (!string.IsNullOrEmpty(emailParseModel.PersonalEmail)) { partsAvailable++; partsFound++; } else partsAvailable++;

            if (!string.IsNullOrEmpty(emailParseModel.Mobile)) { partsAvailable++; partsFound++; } else partsAvailable++;

            if (!string.IsNullOrEmpty(emailParseModel.Telephone)) { partsAvailable++; partsFound++; } else partsAvailable++;

            if (!string.IsNullOrEmpty(emailParseModel.LinkedIn)) { partsAvailable++; partsFound++; } else partsAvailable++;

            if (!string.IsNullOrEmpty(emailParseModel.Facebook)) { partsAvailable++; partsFound++; } else partsAvailable++;

            if (!string.IsNullOrEmpty(emailParseModel.Twitter)) { partsAvailable++; partsFound++; } else partsAvailable++;

            if (!string.IsNullOrEmpty(emailParseModel.Instagram)) { partsAvailable++; partsFound++; } else partsAvailable++;

            if (!string.IsNullOrEmpty(emailParseModel.Website)) { partsAvailable++; partsFound++; } else partsAvailable++;

            if (!string.IsNullOrEmpty(emailParseModel.Address)) { partsAvailable++; partsFound++; } else partsAvailable++;

            if (!string.IsNullOrEmpty(emailParseModel.Position)) { partsAvailable++; partsFound++; } else partsAvailable++;

            if (emailParseModel.PositionId != 0) { partsAvailable++; partsFound++; } else partsAvailable++;

            if (!string.IsNullOrEmpty(emailParseModel.Country)) { partsAvailable++; partsFound++; } else partsAvailable++;

            if (emailParseModel.CountryId != 0) { partsAvailable++; partsFound++; } else partsAvailable++;

            if (!string.IsNullOrEmpty(emailParseModel.Region)) { partsAvailable++; partsFound++; } else partsAvailable++;

            if (emailParseModel.RegionId != 0) { partsAvailable++; partsFound++; } else partsAvailable++;

            if (!string.IsNullOrEmpty(emailParseModel.City)) { partsAvailable++; partsFound++; } else partsAvailable++;

            if (emailParseModel.CityId != 0) { partsAvailable++; partsFound++; } else partsAvailable++;

            if (!string.IsNullOrEmpty(emailParseModel.Postcode)) { partsAvailable++; partsFound++; } else partsAvailable++;

            emailParseModel.PartsAvailable = partsAvailable;

            emailParseModel.PartsFound = partsFound;

            emailParseModel.PartsFoundPercent = 100 / emailParseModel.PartsAvailable * emailParseModel.PartsFound;

            return emailParseModel;
        }


        private List<ExtractWordModels> UniteExtractWordModels(ExtractDataModel extractDataModel)
        {
            List<ExtractWordModels> extractWordModels = new List<ExtractWordModels>();

            extractWordModels.Add(extractDataModel.LinkedIns);

            extractWordModels.Add(extractDataModel.Facebooks);

            extractWordModels.Add(extractDataModel.Instagrams);

            extractWordModels.Add(extractDataModel.PostCodes);

            extractWordModels.Add(extractDataModel.WorkEmails);

            extractWordModels.Add(extractDataModel.WebSites);

            extractWordModels.Add(extractDataModel.Mobiles);

            return extractWordModels;
        }
        

        private PositionModel GetPositionModelByName(string position, List<PositionModel> positionModels)
        {
            if (!string.IsNullOrEmpty(position))
              return positionModels.Where(p => p.PositionName == position).FirstOrDefault();

            return null;
        }

        private ExtractWordModels SortWordModelsToMiddleNumber(ExtractWordModels models, int middleNumber)
        {
            if (models != null && middleNumber > 0)
                models = _dataFilterHandler.SortWordModelsByMiddleNumber(models, middleNumber);

            return models;
        }

        private int GetMiddleNumber(List<ExtractWordModels> models)
        {
            int middleNumber = 0;

            if (models != null)
                middleNumber = _dataFilterHandler.GetMiddleNumber(models);

            return middleNumber;
        }

        private async Task<FullNameModel> GetNameModel(ExtractWordModels names, ExtractWordModels surnames)
        {
            var fullNameModels = await ExtarctFullNamesModels(globalData, names, surnames);

            FullNameModel fullNameModel = await TryGetFullNameModel(fullNameModels);

            return fullNameModel;
        }

        private async Task<LocationModel> GetLocationModel(ExtractWordModels countries, ExtractWordModels _cities, ExtractWordModels _regions)
        {

            ExtractWordModels cities = new ExtractWordModels();

            ExtractWordModels regions = new ExtractWordModels();

            if (countries != null && countries.ExtractDatas.Count > 0) 
            {
                foreach (var country in countries.ExtractDatas)
                {
                    foreach (var city in _cities.ExtractDatas.Where(c => c.Word != country.Word))
                        cities.ExtractDatas.Add(city);

                    foreach (var region in _regions.ExtractDatas.Where(c => c.Word != country.Word))
                        regions.ExtractDatas.Add(region);
                }
            }
            else
            {
                cities = _cities;
                regions = _regions;
            }
            

            List<LocationModel> locationModels = await CreateLocationModels(
                _dataFilterHandler.ClearDuplicatesByWord(countries),
                _dataFilterHandler.ClearDuplicatesByWord(cities),
                _dataFilterHandler.ClearDuplicatesByWord(regions));

            LocationModel locationModel = await TryGetFullLocationModel(locationModels);

            return locationModel;
        }

        private async Task<List<FullNameModel>> ExtarctFullNamesModels(string data, ExtractWordModels names, ExtractWordModels surnames)
        {

            FullNameModelExtractor fullNameExtractor = new FullNameModelExtractor(_firstNames, _lastNames);

            var nameModels = await fullNameExtractor.GetFullNameModels(data, names, surnames);

            return nameModels;
        }

        private async Task<FullNameModel> TryGetFullNameModel(List<FullNameModel> fullNameModels)
        {

            foreach (var fullNameModel in fullNameModels)
            {
                if (fullNameModel.Name != null && fullNameModel.Surname != null)
                {
                    return fullNameModel;
                }
            }

            return null;
        }

        private async Task<List<LocationModel>> CreateLocationModels(ExtractWordModels countries, ExtractWordModels cities, ExtractWordModels regions)
        {
            List<CountryModel> countryModels = _locationModelExtractor.GetCountriesByNames(countries);

            List<CityModel> cityModels = _locationModelExtractor.GetCitiesByNames(cities);

            List<RegionModel> regionModels = _locationModelExtractor.GetRegionsByNames(regions);

            return _locationModelService.ConnectLocationModel(countryModels, cityModels, regionModels, _countryModels);
        }


        private async Task<LocationModel> TryGetFullLocationModel(List<LocationModel> locationModels)
        {

            foreach (var locationModel in locationModels)
            {
                if (locationModel.Country != null && (locationModel.City != null || locationModel.Region != null))
                {
                    return locationModel;
                }
            }

            return null;
        }

        private CountryModel GetCountryByCityRegion(string city, string region)
        {
            var cityModel = _locationModelExtractor.GetCityByName(city);

            var regionModel = _locationModelExtractor.GetRegionByName(region);

            if (cityModel.CountryId == regionModel.CountryId)
                return _countryModels.Where(c => c.Id == cityModel.CountryId).FirstOrDefault();

            return _countryModels.Where(c => c.Id == regionModel.CountryId).FirstOrDefault();
        }

        private ExtractWordModels ClearDublicates(ExtractWordModels models)
        {
            ExtractWordModels noDublictesExtracted = new ExtractWordModels();

            foreach (var model in models.ExtractDatas)
            {
                if (noDublictesExtracted.ExtractDatas.Where(m =>
                m.Word == model.Word &&
                m.FirstCharPos == model.FirstCharPos &&
                m.LastCharPos == model.LastCharPos).Count() == 0)
                {
                    noDublictesExtracted.ExtractDatas.Add(model);
                }
            }

            return noDublictesExtracted;
        }
    }


}

using SignatureEmailParser.Models;
using SignatureEmailParser.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SignatureEmailParser.Services.Implementation
{
    public class LocationModelService : ILocationModelService
    {
        public List<LocationModel> ConnectLocationModel(List<CountryModel> countryModels, List<CityModel> cityModels, List<RegionModel> regionModels, List<CountryModel> allCountryModels)
        {
            List<LocationModel> locationModels = new List<LocationModel>();

            if (countryModels != null && countryModels.Count > 0)
            {
                foreach (var countryModel in countryModels)
                {
                    LocationModel locationModel = new LocationModel();

                    locationModel.Country = countryModel;

                    if (cityModels != null && cityModels.Count > 0)
                    {
                        if (cityModels.Where(c => c.CountryId == countryModel.Id).FirstOrDefault() != null)
                        {
                            locationModel.City = cityModels.Where(c => c.CountryId == countryModel.Id).FirstOrDefault();

                            cityModels.Remove(cityModels.Where(c => c.CountryId == countryModel.Id).FirstOrDefault());

                            if (regionModels != null && regionModels.Count > 0)
                            {
                                if (regionModels.Where(c => c.CountryId == countryModel.Id && c.Id == locationModel.City.RegionId).FirstOrDefault() != null)
                                {
                                    locationModel.Region = regionModels.Where(c => c.CountryId == countryModel.Id && c.Id == locationModel.City.RegionId).FirstOrDefault();

                                    regionModels.Remove(regionModels.Where(c => c.CountryId == countryModel.Id && c.Id == locationModel.City.RegionId).FirstOrDefault());
                                }
                            }
                        }
                    }
                    else if (regionModels != null && regionModels.Count > 0)
                    {
                        if (regionModels.Where(c => c.CountryId == countryModel.Id).FirstOrDefault() != null)
                        {
                            locationModel.Region = regionModels.Where(c => c.CountryId == countryModel.Id).FirstOrDefault();

                            regionModels.Remove(regionModels.Where(c => c.CountryId == countryModel.Id).FirstOrDefault());
                        }
                    }

                    if (locationModel.Country != null && (locationModel.City != null || locationModel.Region != null))
                        locationModels.Add(locationModel);

                }
            }
            else
            {
                if (cityModels != null && cityModels.Count > 0)
                {
                    foreach (var cityModel in cityModels)
                    {

                        LocationModel locationModel = new LocationModel();

                        locationModel.City = cityModel;

                        if (regionModels.Where(c => c.Id == cityModel.RegionId).FirstOrDefault() != null)
                        {
                            locationModel.Region = regionModels.Where(c => c.Id == cityModel.RegionId).FirstOrDefault();

                            regionModels.Remove(regionModels.Where(c => c.Id == cityModel.RegionId).FirstOrDefault());

                            if (allCountryModels.Where(c => c.Id == cityModel.CountryId && c.Id == locationModel.Region.CountryId).FirstOrDefault() != null)
                            {
                                locationModel.Country = allCountryModels.Where(c => c.Id == cityModel.CountryId && c.Id == locationModel.Region.CountryId).FirstOrDefault();
                            }
                        }

                        if (locationModel.Country != null && (locationModel.City != null || locationModel.Region != null))
                            locationModels.Add(locationModel);
                    }
                }
            }

            if (locationModels.Count == 0)
            {
                if (regionModels != null && regionModels.Count > 0)
                {
                    foreach (var regionModel in regionModels)
                    {
                        LocationModel locationModel = new LocationModel();

                        locationModel.Region = regionModel;

                        if (cityModels.Where(c => c.CountryId == regionModel.CountryId).FirstOrDefault() != null)
                        {
                            locationModel.City = cityModels.Where(c => c.CountryId == regionModel.CountryId).FirstOrDefault();

                            if (allCountryModels.Where(c => c.Id == locationModel.Region.CountryId && locationModel.City.CountryId == c.Id).FirstOrDefault() != null)
                            {
                                locationModel.Country = allCountryModels.Where(c => c.Id == locationModel.Region.CountryId && locationModel.City.CountryId == c.Id).FirstOrDefault();

                                if (locationModel.Country != null && (locationModel.City != null || locationModel.Region != null))
                                    locationModels.Add(locationModel);
                            }
                        }
                    }
                }
            }

            return locationModels;
        }
    }
}

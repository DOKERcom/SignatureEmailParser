using SignatureEmailParser.BusinessLogic.Constants;
using SignatureEmailParser.BusinessLogic.Helpers;
using SignatureEmailParser.BusinessLogic.Helpers.Interfaces;
using SignatureEmailParser.BusinessLogic.Services.Interfaces;
using SignatureEmailParser.EFCore.Entities;
using SignatureEmailParser.EFCore.Enums;
using SignatureEmailParser.EFCore.Respository.Interfaces;
using SignatureEmailParser.Models.EqualityComparers;
using SignatureEmailParser.Models.SocialMediaMappingModels;
using SignatureEmailParser.Models.SocialMediaMappingModels.Nymeria;
using SignatureEmailParser.Models.SocialMediaMappingModels.SnovIO;
using SignatureEmailParser.Models.SocialMediaMappingModels.VoilaNorbert;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SignatureEmailParser.BusinessLogic.Extensions;
using SignatureEmailParser.Models.Enums;
using System.Collections.Concurrent;

namespace SignatureEmailParser.BusinessLogic.Services
{
    public class SocialMediaMappingService : ISocialMediaMappingService
    {

        private readonly ICityRepository _cityRepository;
        
        private readonly ISocialMediaMappingRepository _socialMediaMappingRepository;
        private readonly ICompanyRepository _companyRepository;
        private readonly IPositionRepository _positionRepository;
        private readonly IIndustryRepository _industryRepository;

        public SocialMediaMappingService(ISocialMediaMappingRepository socialMediaMappingRepository,
                                         ICompanyRepository companyRepository, IPositionRepository positionRepository, IIndustryRepository industryRepository,
                                         ICityRepository cityRepository)
        {
            _socialMediaMappingRepository = socialMediaMappingRepository;
            _companyRepository = companyRepository;
            _positionRepository = positionRepository;
            _industryRepository = industryRepository;
            _cityRepository = cityRepository;
        }

        public async Task<ProfileFromAppModel> GetUpdatedProfileSocialMediaByUrl(string url)
        {
            ProfileFromAppModel response = new ProfileFromAppModel();
            response.ProfileUrl = url;

            string[] items = { url };

            List<SocialMediaMapping> socialMedia = await _socialMediaMappingRepository.GetByLinkedInUrl(items);
            if (socialMedia is null)
            {
                return response;
            }

            response.Email = socialMedia.FirstOrDefault().Email;
            response.Locality = socialMedia.FirstOrDefault().Locality;
            response.Company = socialMedia.FirstOrDefault().Company.Name;
            response.FirstName = socialMedia.FirstOrDefault().Name;
            response.LastName = socialMedia.FirstOrDefault().Surname;
            response.PositionTitle = socialMedia.FirstOrDefault().Position;
            response.Twitter = socialMedia.FirstOrDefault().Twitter;

            return response;

        }
        public async Task<List<ProfileFromAppModel>> GetUpdatedProfilesFromSocialMedia(string requestBody)
        {
            List<ProfileFromAppModel> profileFromAppModels = JsonConvert.DeserializeObject<List<ProfileFromAppModel>>(requestBody);
            if (profileFromAppModels is null || profileFromAppModels.Count is (int)default)
            {
                return profileFromAppModels;
            }

            string[] urlsFromApp = profileFromAppModels
                .Where(x => !string.IsNullOrWhiteSpace(x.ProfileUrl.Trim('/')))
                .Select(x => $"{x.ProfileUrl.Trim('/')}")
                .ToArray();
            List<SocialMediaMapping> socialMedia = await _socialMediaMappingRepository.GetByLinkedInIds(urlsFromApp);
            List<ProfileFromAppModel> response = GetUpdatedProfilesFromApp(profileFromAppModels, socialMedia);

            return response;
        }

        public async Task<bool> UpdateSocialMediaMappingStatus(string requestBody)
        {
            if (string.IsNullOrEmpty(requestBody))
                return false;

            List<SocialMediaMapping> socialMediaMappings = null;
            SocialMediaMapping socialMediaMapping = null;

            string[] ids = new[] { requestBody.Split('/').LastOrDefault() };

            if (requestBody.Contains("https://twitter.com"))
            {
                socialMediaMappings = await _socialMediaMappingRepository.GetByTwitterIds(ids);
                socialMediaMapping = socialMediaMappings.FirstOrDefault();
                socialMediaMapping.TwitterStatus = StatusType.Disabled;
            }
            else if (requestBody.Contains("https://facebook.com"))
            {
                socialMediaMappings = await _socialMediaMappingRepository.GetByFacebookIds(ids);
                socialMediaMapping = socialMediaMappings.FirstOrDefault();
                socialMediaMapping.FacebookStatus = StatusType.Disabled;
            }
            else if (requestBody.Contains("https://linkedin.com") || requestBody.Contains("https://www.linkedin.com"))
            {
                socialMediaMappings = await _socialMediaMappingRepository.GetByLinkedInIds(ids);
                socialMediaMapping = socialMediaMappings.FirstOrDefault();
                socialMediaMapping.LinkedInStatus = StatusType.Disabled;
            }
            else if (requestBody.Substring(0, 1) == "+")
            {
                socialMediaMappings = await _socialMediaMappingRepository.GetByMobile(new string[] { requestBody });
                socialMediaMapping = socialMediaMappings.FirstOrDefault();
                socialMediaMapping.MobileStatus = (int)StatusType.Disabled;
            }

            if (socialMediaMappings != null)
            {
                return await _socialMediaMappingRepository.Update(socialMediaMapping);
            }

            return false;
        }

        /// <summary>
        /// This function just get emails from third parties, will become
        /// redundant once the LinkedIn data is migrated
        /// </summary>
        /// <param name="requestBody"></param>
        /// <returns></returns>
        public async Task<List<ProfileFromAppModel>> GetProfilesWithMissingInfo(string requestBody)
        {
            List<ProfileFromAppModel> profileFromAppModelsWithNeededCriteria = new List<ProfileFromAppModel>();

            // for now, lets just get emails
            List<ProfileFromAppModel> profileFromAppModels = JsonConvert.DeserializeObject<List<ProfileFromAppModel>>(requestBody);

            if (profileFromAppModels != null && profileFromAppModels.Count > 0)
            {
                profileFromAppModels = ProfileHelper.ProfileCleaner(profileFromAppModels);

                PlatformType platformType = profileFromAppModels.FirstOrDefault().PlatformType;

                List<SocialMediaMapping> socialMediaMappings = null;

                switch (platformType)
                {
                    case PlatformType.LinkedIn:
                        socialMediaMappings = await _socialMediaMappingRepository.GetByLinkedInIds(profileFromAppModels.Select(i => i.ProfileUrl).ToArray());
                        break;
                    case PlatformType.Twitter:
                        socialMediaMappings = await _socialMediaMappingRepository.GetByTwitterIds(profileFromAppModels.Select(i => i.ProfileUrl).ToArray());
                        break;
                    case PlatformType.Facebook:
                        socialMediaMappings = await _socialMediaMappingRepository.GetByFacebookIds(profileFromAppModels.Select(i => i.ProfileUrl).ToArray());
                        break;
                }
            
                //List<SocialMediaMapping> socialMediaMappingsWithEmails = socialMediaMappings.Where(i => !string.IsNullOrWhiteSpace(i.Email) || !string.IsNullOrEmpty(i.WorkEmail)).ToList();

                foreach (SocialMediaMapping socialMediaMappingWithEmail in socialMediaMappings)
                {
                    profileFromAppModelsWithNeededCriteria.Add(new ProfileFromAppModel()
                    {
                        Email = socialMediaMappingWithEmail.Email,
                        FirstName = socialMediaMappingWithEmail.Name,
                        LastName = socialMediaMappingWithEmail.Surname,
                        Phone = socialMediaMappingWithEmail.Phone,
                        Mobile = socialMediaMappingWithEmail.Mobile,
                        WorkEmail = socialMediaMappingWithEmail.WorkEmail,
                        ProfileUrl = socialMediaMappingWithEmail.LinkedIn,
                        Twitter = socialMediaMappingWithEmail.Twitter,
                        Facebook = socialMediaMappingWithEmail.FacebookId,
                        PositionTitle = socialMediaMappingWithEmail.Position,
                        Company = socialMediaMappingWithEmail.Company != null ? socialMediaMappingWithEmail.Name : null,
                        LastPost = socialMediaMappingWithEmail.LastPost,
                        LastPostDate = socialMediaMappingWithEmail.LastPostDate,
                        Experience = socialMediaMappingWithEmail.Experience,
                        Postcode = socialMediaMappingWithEmail.Postcode,
                        SharedConnections = socialMediaMappingWithEmail.SharedConnections,
                        PositionId = socialMediaMappingWithEmail.PositionId.HasValue ? socialMediaMappingWithEmail.PositionId.Value : 0,
                        CityId = socialMediaMappingWithEmail.CityId.HasValue ? socialMediaMappingWithEmail.CityId.Value : 0,
                        CountryId = socialMediaMappingWithEmail.CountryId.HasValue ? socialMediaMappingWithEmail.CountryId.Value : 0    
                    }); 
                    //profileFromAppModels.Remove(profileFromAppModels.FirstOrDefault(i => i.ProfileUrl == socialMediaMappingWithEmail.LinkedIn));
                }

                // new, these are profiles we don't have emails for, lets check
                // if they're in the db in which we try and get the email and then add it to the db
                //List<City> city = await _cityRepository.FindByNameAsync(ProfileItemStatus.NOT_AVAILABLE);

                //foreach (ProfileFromAppModel profileFromAppModel in profileFromAppModels)
                //{
                    //SocialMediaMapping socialMediaMapping = await SearchVendorsForEmail(profileFromAppModel.ProfileUrl, DataSourceType.NA);

                    //if(socialMediaMapping != null ? !string.IsNullOrEmpty(socialMediaMapping.Email) : false)
                    //{ 
                        //SocialMediaMapping exists = socialMediaMappings.FirstOrDefault(i => i.LinkedIn.Equals(profileFromAppModel.ProfileUrl, StringComparison.OrdinalIgnoreCase));

                        //if (exists != null)
                        //{
                        //    // it exists in the db, but since it doesn't have an email let add it
                        //    exists.Email = socialMediaMapping.Email;
                        //    await _socialMediaMappingRepository.CreateOrUpdate(exists);
                        //}
                        //else
                        //{
                            // this is for the ui which only needs the email, nymeria doesn't add any other value
                            // profileFromAppModelsWithNeededCriteria.Add(ConvertSocialMediaMappingToProfileFromAppModel(socialMediaMapping));

                            // this is for the db
                            // we will need to add the data from our own scrape along with the data
                            // from nymeria, this is so that it can still be searched on
                            //profileFromAppModel.CityId = profileFromAppModel.CityId != 0 ? profileFromAppModel.CityId : city.FirstOrDefault().Id;

                            //socialMediaMapping = await ConvertFromAppModelToProfileSocialMediaMapping(profileFromAppModel);
                            //await _socialMediaMappingRepository.CreateOrUpdate(socialMediaMapping);
                        //}
                    //}
                //}
            }

            return profileFromAppModelsWithNeededCriteria;
        }

        public async Task<(bool, string)> ImportProfilesFromLinkedInApp(string requestBody)
        {
            bool result = default;
            string response = null;

            // for now, lets just say emails
            List<ProfileFromAppModel> profileFromAppModels = JsonConvert.DeserializeObject<List<ProfileFromAppModel>>(requestBody);

            if (profileFromAppModels != null && profileFromAppModels.Count > 0)
            {
                profileFromAppModels = ProfileHelper.ProfileCleaner(profileFromAppModels);

                foreach (ProfileFromAppModel profileFromAppModel in profileFromAppModels)
                {
                    StringBuilder stringBuilder = new StringBuilder();

                    // we need to use every identiable value to ensure we can get a match
                    var socialMediaMapping = await _socialMediaMappingRepository.GetByAnyIdentifiableIds(profileFromAppModel);

                    if (socialMediaMapping != null)
                    {
                        // we only want to update the data if we can be sure the data coming in is as accurate or better
                        // than what we already have

                        if (!string.IsNullOrEmpty(profileFromAppModel.PositionTitle) && profileFromAppModel.PositionId > 0)
                        {
                            socialMediaMapping.Position = profileFromAppModel.PositionTitle.Replace("&amp;", "&");
                            socialMediaMapping.PositionId = profileFromAppModel.PositionId;
                        }
                        else if (profileFromAppModel.PositionId > 0)
                        {
                            socialMediaMapping.PositionId = profileFromAppModel.PositionId;
                        }
                        else if (!string.IsNullOrEmpty(profileFromAppModel.PositionTitle))
                        {
                            // lets see if we can find a position based on the name
                            List<Position> positions = await _positionRepository.FindByNameAsync(profileFromAppModel.PositionTitle, true);

                            if (positions.Count == default)
                            {
                                // lets try the experience
                                if (!string.IsNullOrEmpty(profileFromAppModel.Experience))
                                {
                                    List<string> expParts = profileFromAppModel.Experience.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList();

                                    foreach (string expPart in expParts)
                                    {
                                        positions = await _positionRepository.FindByNameAsync(expPart.Trim(), true);

                                        if (positions.Count != default)
                                        {
                                            stringBuilder.Append("Position, ");
                                            socialMediaMapping.Position = positions.FirstOrDefault().Name;
                                            socialMediaMapping.PositionId = positions.FirstOrDefault().Id;

                                            positions.FirstOrDefault().Count++;
                                            break;
                                        }
                                    }
                                }
                            }
                        }

                        if (!string.IsNullOrEmpty(profileFromAppModel.Phone))
                            if (!profileFromAppModel.Phone.Equals(socialMediaMapping.Phone))
                            {
                                socialMediaMapping.Phone = profileFromAppModel.Phone;
                                socialMediaMapping.PhoneStatus = null;
                                stringBuilder.Append("Phone, ");
                            }

                        if (!string.IsNullOrEmpty(profileFromAppModel.Mobile))
                            if (!profileFromAppModel.Mobile.Equals(socialMediaMapping.Mobile))
                            {
                                socialMediaMapping.Mobile = profileFromAppModel.Mobile;
                                socialMediaMapping.MobileStatus = null;
                                stringBuilder.Append("Mobile, ");
                            }

                        if (!string.IsNullOrEmpty(profileFromAppModel.Email))
                            if (!profileFromAppModel.Email.Equals(socialMediaMapping.Email))
                            {
                                socialMediaMapping.Email = profileFromAppModel.Email;
                                socialMediaMapping.EmailStatus = null;
                                stringBuilder.Append("Email, ");
                            }

                        if (!string.IsNullOrEmpty(profileFromAppModel.WorkEmail))
                            if (!profileFromAppModel.WorkEmail.Equals(socialMediaMapping.WorkEmail))
                            {
                                socialMediaMapping.WorkEmail = profileFromAppModel.WorkEmail;
                                socialMediaMapping.WorkEmailStatus = null;
                                stringBuilder.Append("Work Email, ");
                            }

                        if (!string.IsNullOrEmpty(profileFromAppModel.Experience))
                        {
                            socialMediaMapping.Experience = profileFromAppModel.Experience.Replace("|", ",");
                            stringBuilder.Append("Experience, ");
                        }

                        if (!string.IsNullOrEmpty(profileFromAppModel.LastPost))
                        {
                            stringBuilder.Append("Last Post, ");
                            socialMediaMapping.LastPost = profileFromAppModel.LastPost;
                            DateTime lastPostDate = profileFromAppModel.LastPostDate.HasValue ? profileFromAppModel.LastPostDate.Value : DateTime.MinValue;
                            if (lastPostDate != DateTime.MinValue)
                                socialMediaMapping.LastPostDate = profileFromAppModel.LastPostDate.Value;
                        }

                        if (socialMediaMapping.SharedConnections != profileFromAppModel.SharedConnections)
                        {
                            stringBuilder.Append("Shared Connections, ");
                            socialMediaMapping.SharedConnections = profileFromAppModel.SharedConnections;
                        }

                        socialMediaMapping.DataSourceType = profileFromAppModel.DataSourceType;

                        result = await _socialMediaMappingRepository.Update(socialMediaMapping);

                        if (result) {
                            response = "Succesfully updated " + profileFromAppModel.ProfileUrl + " with new " + stringBuilder.ToString();
                        }
                        else {
                            response = "Error trying to update " + profileFromAppModel.ProfileUrl;
                        }
                    }
                    else
                    {
                        SocialMediaMapping socialMediaMappingNew = new SocialMediaMapping();

                        if (!string.IsNullOrEmpty(profileFromAppModel.City) && profileFromAppModel.CityId > 0)
                        {
                            socialMediaMappingNew.Position = profileFromAppModel.City.Replace("&amp;", "&");
                            socialMediaMappingNew.PositionId = profileFromAppModel.CityId;
                        }
                        else if (profileFromAppModel.CityId > 0)
                        {
                            socialMediaMappingNew.CityId = profileFromAppModel.CityId;
                        }
                        else if (!string.IsNullOrEmpty(profileFromAppModel.City))
                        {
                            // lets see if we can find a position based on the name
                            List<City> cities = await _cityRepository.FindByNameAsync(profileFromAppModel.City);

                            if (cities.Count() > 0)
                            {
                                socialMediaMappingNew.CityId = cities.FirstOrDefault().Id;
                            }
                        }

                        Company company = await _companyRepository.GetByName(profileFromAppModel.Company);
                        if (company != null)
                            socialMediaMappingNew.CompanyId = company.Id;

                        socialMediaMappingNew.CountryId = profileFromAppModel.CountryId;
                        socialMediaMappingNew.DataSourceType = profileFromAppModel.DataSourceType;
                        socialMediaMappingNew.Email = profileFromAppModel.Email;
                        socialMediaMappingNew.Experience = profileFromAppModel.Experience;
                        socialMediaMappingNew.FacebookId = profileFromAppModel.Facebook;
                        socialMediaMappingNew.Name = profileFromAppModel.FirstName;
                        socialMediaMappingNew.IndustryId = profileFromAppModel.IndustryId;
                        socialMediaMappingNew.Surname = profileFromAppModel.LastName;
                        socialMediaMappingNew.LastPost = profileFromAppModel.LastPost;
                        socialMediaMappingNew.LastPostDate = profileFromAppModel.LastPostDate;
                        socialMediaMappingNew.Mobile = profileFromAppModel.Mobile;
                        socialMediaMappingNew.Phone = profileFromAppModel.Phone;
                        socialMediaMappingNew.PositionId = profileFromAppModel.PositionId;
                        socialMediaMappingNew.Postcode = profileFromAppModel.Postcode;
                        socialMediaMappingNew.LinkedIn = profileFromAppModel.ProfileUrl;
                        socialMediaMappingNew.SharedConnections = profileFromAppModel.SharedConnections;
                        socialMediaMappingNew.Street = profileFromAppModel.Street;
                        socialMediaMappingNew.Title = profileFromAppModel.Title;
                        socialMediaMappingNew.Twitter = profileFromAppModel.Twitter;
                        socialMediaMappingNew.WhenUpdated = profileFromAppModel.WhenUpdated;
                        socialMediaMappingNew.CreatedAt = DateTime.Now;

                        result =  await _socialMediaMappingRepository.Create(socialMediaMappingNew);

                        if (result)
                        {
                            response = "Succesfully added new profile " + profileFromAppModel.ProfileUrl;
                        }
                        else
                        {
                            response = "Error trying to add new profile " + profileFromAppModel.ProfileUrl;
                        }
                    }
                }
            }

            return (result, response);
        }
  
        private async Task<List<SocialMediaMapping>> GetSocialMediaForCreateFromLinkedInApp(List<ProfileFromAppModel> profileFromAppModels, List<SocialMediaMapping> socialMediaForUpdate)
        {
            List<SocialMediaMapping> response = new List<SocialMediaMapping>();

            foreach (ProfileFromAppModel profileFromAppModel in profileFromAppModels)
            {
                SocialMediaMapping existSocialMedia = socialMediaForUpdate?.FirstOrDefault(x => x.LinkedIn == $"{profileFromAppModel.ProfileUrl.Trim('/')}");
                if (!(existSocialMedia is null))
                    continue;

                if (string.IsNullOrEmpty(profileFromAppModel.Company) || profileFromAppModel.Company == ProfileItemStatus.DEFAULT_COMPANY_INVITE)
                    profileFromAppModel.Company = ProfileItemStatus.NOT_AVAILABLE;

                if (string.IsNullOrEmpty(profileFromAppModel.PositionTitle) || profileFromAppModel.PositionTitle == ProfileItemStatus.DEFAULT_POSITION_INVITE)
                    profileFromAppModel.PositionTitle = ProfileItemStatus.NOT_AVAILABLE;

                List<Company> existingCompanies = await _companyRepository.GetByNames(new string[] { profileFromAppModel.Company });

                List<Company> companies = await GetCompaniesByNames(new string[] { profileFromAppModel.Company }, existingCompanies);

                Company company = companies.FirstOrDefault(x => x.Name == profileFromAppModel.Company);

                SocialMediaMapping socialMediaMappingFromLinkedInApp = new SocialMediaMapping()
                {
                    Title = profileFromAppModel.Title,
                    Name = profileFromAppModel.FirstName,
                    Surname = profileFromAppModel.LastName,
                    Street = default,
                    //City = default,
                    //Country = default,
                    Email = profileFromAppModel?.Email,
                    //Facebook = default,
                    Twitter = profileFromAppModel.Twitter,
                    //Industry = default,
                    LinkedIn = $"{profileFromAppModel.ProfileUrl.TrimEnd('/')}",
                    Locality = profileFromAppModel?.Locality,
                    PastCompany = profileFromAppModel.Company,
                    Position = profileFromAppModel.PositionTitle,
                    Postcode = default,
                    DataSourceType = DataSourceType.LinkedInApp,
                    Domain = profileFromAppModel.Website,
                    WhenUpdated = DateTime.UtcNow,
                    CompanyId = company != null ? company.Id : default,
                };
                response.Add(socialMediaMappingFromLinkedInApp);

            }

            return response;
        }
        private async Task<List<SocialMediaMapping>> GetSocialMediaForUpdateFromLinkedInApp(List<ProfileFromAppModel> profileFromApp)
        {
            List<SocialMediaMapping> socialMediaItems = await _socialMediaMappingRepository.GetByLinkedInIds(profileFromApp.Select(i => i.ProfileUrl.TrimEnd('/')).ToArray());

            foreach (var socialMediaItem in socialMediaItems)
            {
                if (socialMediaItem.LinkedIn.Contains("gary-thatcher"))
                {
                }

                var exists = profileFromApp.FirstOrDefault(i => i.ProfileUrl.TrimEnd('/') == socialMediaItem.LinkedIn);

                if (exists != null)
                {
                    Company company = socialMediaItem.Company;
                    socialMediaItem.CompanyId = company.Id;

                    socialMediaItem.Title = exists.Title;
                    socialMediaItem.Name = exists.FirstName;
                    socialMediaItem.Surname = exists.LastName;
                    socialMediaItem.Email = exists.Email;
                    //socialMediaItem.Industry = exists.Industry;
                    socialMediaItem.ProfileLanguage = exists.ProfileLanguage;
                    socialMediaItem.Locality = exists.Locality;
                    socialMediaItem.Street = exists.Street;
                    //socialMediaItem.City = exists.City;
                    //socialMediaItem.Country = exists.Country;
                    socialMediaItem.Position = exists.PositionTitle;
                    socialMediaItem.Twitter = exists.Twitter;
                    socialMediaItem.Domain = exists.Website;

                    socialMediaItem.DataSourceType = DataSourceType.LinkedInApp;
                    socialMediaItem.WhenUpdated = DateTime.UtcNow;
                }
            }

            return socialMediaItems;
        }

        private ProfileFromAppModel ConvertSocialMediaMappingToProfileFromAppModel(SocialMediaMapping socialMediaMapping)
        {
            ProfileFromAppModel profileFromAppModel = new ProfileFromAppModel();

            profileFromAppModel.ProfileUrl = socialMediaMapping.LinkedIn;
            profileFromAppModel.Email = socialMediaMapping.Email;
            profileFromAppModel.Locality = socialMediaMapping.Locality;
            profileFromAppModel.Company = socialMediaMapping.Company != null ? socialMediaMapping.Company.Name : null;
            profileFromAppModel.FirstName = socialMediaMapping.Name;
            profileFromAppModel.LastName = socialMediaMapping.Surname;
            profileFromAppModel.PositionTitle = socialMediaMapping.Position;
            profileFromAppModel.Twitter = socialMediaMapping.Twitter;
            profileFromAppModel.DataSourceType = socialMediaMapping.DataSourceType;
            profileFromAppModel.WhenUpdated = socialMediaMapping.WhenUpdated;

            profileFromAppModel.CountryId = socialMediaMapping.CountryId.HasValue ? socialMediaMapping.CountryId.Value : 0;
            profileFromAppModel.IndustryId = socialMediaMapping.IndustryId.HasValue ? socialMediaMapping.IndustryId.Value : 0;
            profileFromAppModel.CompanyId = socialMediaMapping.CompanyId;
        
            return profileFromAppModel;
        }

        private async Task<SocialMediaMapping> ConvertFromAppModelToProfileSocialMediaMapping(ProfileFromAppModel profileFromAppModel)
        {
            List<Company> existingCompanies = await _companyRepository.GetByNames(new string[] { profileFromAppModel.Company });

            List<Company> companies = await GetCompaniesByNames(new string[] { profileFromAppModel.Company }, existingCompanies);

            Company company = companies.FirstOrDefault(x => x.Name.Equals(profileFromAppModel.Company, StringComparison.OrdinalIgnoreCase));

            SocialMediaMapping socialMediaMappingFromLinkedInApp = new SocialMediaMapping()
            {
                Title = profileFromAppModel.Title,
                Name = profileFromAppModel.FirstName,
                Surname = profileFromAppModel.LastName,
                Street = default,
                //City = default,
                CityId = profileFromAppModel.CityId,
                //Country = default,
                CountryId = profileFromAppModel.CountryId,
                Email = profileFromAppModel?.Email,
                //Facebook = default,
                Twitter = profileFromAppModel.Twitter,
                //Industry = default,
                IndustryId = profileFromAppModel.IndustryId,
                LinkedIn = $"{profileFromAppModel.ProfileUrl.TrimEnd('/')}",
                Locality = profileFromAppModel?.Locality,
                PastCompany = profileFromAppModel.Company,
                Position = profileFromAppModel.PositionTitle,
                Postcode = default,
                DataSourceType = DataSourceType.LinkedInApp,
                Domain = profileFromAppModel.Website,
                WhenUpdated = DateTime.UtcNow,
                ProfileLanguage = profileFromAppModel.ProfileLanguage,
                Phone = profileFromAppModel.Phone,
                CompanyId = company != null ? company.Id : default,
            };

            return socialMediaMappingFromLinkedInApp;



            //SocialMediaMapping socialMediaMapping = new SocialMediaMapping();

            //socialMediaMapping.LinkedIn = profileFromAppModel.ProfileUrl;
            //socialMediaMapping.Email = profileFromAppModel.Email;
            //socialMediaMapping.Locality = profileFromAppModel.Locality;
            //socialMediaMapping.Company = new Company() { Name = profileFromAppModel.Company }; 
            //socialMediaMapping.Name = profileFromAppModel.FirstName;
            //socialMediaMapping.Surname = profileFromAppModel.LastName;
            //socialMediaMapping.Position = profileFromAppModel.PositionTitle;
            //socialMediaMapping.Twitter = profileFromAppModel.Twitter;

            //return socialMediaMapping;
        }
        private List<ProfileFromAppModel> GetUpdatedProfilesFromApp(List<ProfileFromAppModel> profileFromAppModels, List<SocialMediaMapping> socialMediaForUpdate, List<SocialMediaMapping> socialMediaForCreate = null)
        {
            List<ProfileFromAppModel> response = profileFromAppModels
                .Select(profile =>
                {
                    SocialMediaMapping socialMediaUpdate = socialMediaForUpdate.FirstOrDefault(x => x.LinkedIn.Contains($"{profile.ProfileUrl.Trim('/')}"));
                    SocialMediaMapping socialMediaCreate = socialMediaForCreate?.FirstOrDefault(x => x.LinkedIn.Contains($"{profile.ProfileUrl.Trim('/')}"));
                    if (socialMediaUpdate is null && socialMediaCreate is null)
                    {
                        return profile;
                    }
                    profile.Email = socialMediaUpdate is null ? socialMediaCreate.Email : socialMediaUpdate.Email;
                    profile.Locality = socialMediaUpdate is null ? socialMediaCreate.Locality : socialMediaUpdate.Locality;
                    profile.Company = socialMediaUpdate is null ? socialMediaCreate.Company.Name : socialMediaUpdate.Company.Name;
                    profile.FirstName = socialMediaUpdate is null ? socialMediaCreate.Name : socialMediaUpdate.Name;
                    profile.LastName = socialMediaUpdate is null ? socialMediaCreate.Surname : socialMediaUpdate.Surname;
                    profile.PositionTitle = socialMediaUpdate is null ? socialMediaCreate.Position : socialMediaUpdate.Position;
                    profile.Twitter = socialMediaUpdate is null ? socialMediaCreate.Twitter : socialMediaUpdate.Twitter;

                    return profile;
                })
                .ToList();

            return response;
        }
        private async Task<List<Company>> GetCompaniesByNames(string[] companyNames, List<Company> existingCompanies)
        {
            List<Company> createCompanies = new List<Company>();
            List<Company> response = new List<Company>();

            var uniqueNames = companyNames.Distinct(new CompanyNameEqualityComparer());

            foreach (string companyName in uniqueNames)
            {
                Company existingCompany = existingCompanies?.FirstOrDefault(x => x.Name.Equals(companyName.TrimStart().TrimEnd(), StringComparison.OrdinalIgnoreCase));

                if (!(existingCompany is null))
                {
                    continue;
                }

                Company company = new Company()
                {
                    City = existingCompany is null ? existingCompany?.City : default,
                    Country = existingCompany is null ? existingCompany?.Country : default,
                    HeadCount = existingCompany is null ? existingCompany?.HeadCount : default,
                    Domain = existingCompany is null ? existingCompany?.Domain : default,
                    AnnualTurnover = default, // No field at api response;
                    Street = existingCompany is null ? existingCompany?.Street : default,
                    Email = default, // No field at api response;
                    IPAddress = default, // No field at api response;
                    Locality = existingCompany is null ? existingCompany?.Locality : default,
                    Name = companyName,
                    Postcode = existingCompany is null ? existingCompany?.Postcode : default,
                    WhenUpdated = DateTime.UtcNow
                };

                createCompanies.Add(company);
            }

            if (existingCompanies.Count is (int)default)
            {
                return await _companyRepository.CreateRangeWihtIds(createCompanies);
            }

            await _companyRepository.UpdateRange(existingCompanies);

            response = await _companyRepository.CreateRangeWihtIds(createCompanies);
            response.AddRange(existingCompanies);

            return response;
        }
        private async Task<List<Company>> GetCompaniesByNames(string[] companyNames, List<CurrentJobsDataEmailsFromUrlSnovIOModel> currentJobs, List<PreviousJobsDataEmailsFromUrlSnovIOModel> previousJobs, List<Company> existingCompanies)
        {
            List<Company> createCompanies = new List<Company>();
            List<Company> response = new List<Company>();

            var uniqueNames = companyNames.Distinct(new CompanyNameEqualityComparer());

            foreach (string companyName in uniqueNames)
            {
                CurrentJobsDataEmailsFromUrlSnovIOModel currentJob = currentJobs.FirstOrDefault(x => x.CompanyName == companyName);
                PreviousJobsDataEmailsFromUrlSnovIOModel previousJob = previousJobs.FirstOrDefault(x => x.CompanyName == companyName);
                Company existingCompany = existingCompanies?.FirstOrDefault(x => x.Name == companyName);

                if (currentJob?.CompanyName is null && previousJob?.CompanyName is null || !(existingCompany is null))
                {
                    continue;
                }

                Company company = new Company()
                {
                    City = previousJob is null ? currentJob?.City : previousJob?.City,
                    Country = previousJob is null ? currentJob?.Country : previousJob?.Country,
                    HeadCount = previousJob is null ? currentJob?.Size : previousJob?.Size,
                    Domain = previousJob is null ? currentJob?.Site : previousJob?.Site,
                    AnnualTurnover = default, // No field at api response;
                    Street = previousJob is null ? currentJob?.Street : previousJob?.Street,
                    Email = default, // No field at api response;
                    IPAddress = default, // No field at api response;
                    Locality = previousJob is null ? currentJob?.Locality : previousJob?.Locality,
                    Name = previousJob is null ? currentJob?.CompanyName : previousJob?.CompanyName,
                    Postcode = previousJob is null ? currentJob?.Postal : previousJob?.Postal,
                    WhenUpdated = DateTime.UtcNow
                };

                createCompanies.Add(company);
            }

            if (existingCompanies.Count is (int)default)
            {
                response = await _companyRepository.CreateRangeWihtIds(createCompanies);

                return response;
            }

            foreach (var company in existingCompanies)
            {
                CurrentJobsDataEmailsFromUrlSnovIOModel currentJob = currentJobs.FirstOrDefault(x => x.CompanyName == company.Name);
                PreviousJobsDataEmailsFromUrlSnovIOModel previousJob = previousJobs.FirstOrDefault(x => x.CompanyName == company.Name);

                if (currentJob?.CompanyName is null && previousJob?.CompanyName is null)
                {
                    continue;
                }

                company.City = previousJob is null ? currentJob?.City : previousJob?.City;
                company.Country = previousJob is null ? currentJob?.Country : previousJob?.Country;
                company.HeadCount = previousJob is null ? currentJob?.Size : previousJob?.Size;
                company.Domain = previousJob is null ? currentJob?.Site : previousJob?.Site;
                company.AnnualTurnover = default; // No field at api response;
                company.Street = previousJob is null ? currentJob?.Street : previousJob?.Street;
                company.Email = default; // No field at api response;
                company.IPAddress = default; // No field at api response;
                company.Locality = previousJob is null ? currentJob?.Locality : previousJob?.Locality;
                company.Name = previousJob is null ? currentJob?.CompanyName : previousJob?.CompanyName;
                company.Postcode = previousJob is null ? currentJob?.Postal : previousJob?.Postal;
                company.WhenUpdated = DateTime.UtcNow;

            }

            await _companyRepository.UpdateRange(existingCompanies);

            response = await _companyRepository.CreateRangeWihtIds(createCompanies);
            response.AddRange(existingCompanies);

            return response;
        }

        public async Task<List<SocialMediaMapping>> FreeTextSearch(string requestBody)
        {
            FreeTextSearchRequestModel searchModel = JsonConvert.DeserializeObject<FreeTextSearchRequestModel>(requestBody);

            if (searchModel is null || string.IsNullOrWhiteSpace(searchModel.Values))
            {
                return new List<SocialMediaMapping>();
            }

            // logging request here


            string query = GeneratExplicitSearchQuery(searchModel);
             
            List<SocialMediaMapping> result = await _socialMediaMappingRepository.ExecuteQuery(query);

            return result;
        }

        public async Task<List<SocialMediaMapping>> GetByFilterAsync(string requestBody)
        { 
            var request = JsonConvert.DeserializeObject<GetSocialMediaMappingsByFilterRequestModel>(requestBody);

            string query = GenerateLikeBasedFilterQuery(request);

            List<SocialMediaMapping> profiles = await _socialMediaMappingRepository.ExecuteQuery(query);

            foreach (SocialMediaMapping profile in profiles)
            {
                var item = Industries.GetIndustries().FirstOrDefault(i => i.Key == profile.IndustryId);
                profile.Industries = new Industry() { Id = item.Key, Name = item.Value }; 
            }

            return profiles;
        }

        private string GetFreeTextSql(KeyValuePair<SocialMediaMappingsColumns, string> query)
        {
            StringBuilder stringBuilder = new StringBuilder();

            // need to ensure pairs with white space are wrapped in quotes
            List<string> words = query.Value.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries)
                                                    .Select(i => i.Trim())
                                                    .Where(i => !string.IsNullOrWhiteSpace(i)).ToList();

            bool firstBracketUsed = false;
            bool isWordStarted = false;

            foreach (string word in words)
            {
                if (word.Equals("OR"))
                {
                    stringBuilder.Append("%' OR ");
                    isWordStarted = false;
                }
                else if (word.Equals("AND"))
                {
                    stringBuilder.Append("%' AND ");
                    isWordStarted = false;

                }
                else if (word.Equals("%'NOT"))
                {
                    isWordStarted = false;
                    stringBuilder.Append(" NOT IN ");

                }
                else
                {
                    if (!isWordStarted)
                    {
                        if (!firstBracketUsed)
                        {
                            firstBracketUsed = true;
                            stringBuilder.Append(word.Replace(word, "FREETEXT (" + query.Key + ", '" + word).Replace("\"", ""));
                        }
                        else
                        {
                            stringBuilder.Append(word.Replace(word, "FREETEXT " + query.Key + ", '" + word + "')").Replace("\"", ""));
                        }

                        isWordStarted = true;
                    }
                    else
                    {
                        stringBuilder.Append(" " + word.Replace("\"", ""));
                    }
                }
            }

            if (firstBracketUsed)
            {
                stringBuilder.Append("')");
            }
            else
            {
                stringBuilder.Append("'");
            }

            return stringBuilder.ToString();
        }

        private string GetLikeSql(KeyValuePair<SocialMediaMappingsColumns, string> query)
        {
            StringBuilder stringBuilder = new StringBuilder();

            // need to ensure pairs with white space are wrapped in quotes
            List<string> words = query.Value.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries)
                                                    .Select(i => i.Trim())
                                                    .Where(i => !string.IsNullOrWhiteSpace(i)).ToList();

            bool firstBracketUsed = false;
            bool isWordStarted = false;

            foreach (string word in words)
            {
                if (word.Equals("OR"))
                {
                    stringBuilder.Append("%' OR ");
                    isWordStarted = false;
                }
                else if (word.Equals("AND"))
                {
                    stringBuilder.Append("%' AND ");
                    isWordStarted = false;

                }
                else if (word.Equals("%'NOT"))
                {
                    isWordStarted = false;
                    stringBuilder.Append(" NOT IN ");

                }
                else
                {
                    if (!isWordStarted)
                    {
                        if (!firstBracketUsed)
                        {
                            firstBracketUsed = true;
                            stringBuilder.Append(word.Replace(word, "(" + query.Key + " LIKE '%" + word).Replace("\"", ""));
                        }
                        else
                        {
                            stringBuilder.Append(word.Replace(word,  query.Key + " LIKE '%" + word).Replace("\"", ""));
                        }

                        isWordStarted = true;
                    }
                    else
                    {
                        stringBuilder.Append(" " + word.Replace("\"", ""));
                    }
                }
            }

            if (firstBracketUsed)
            {
                stringBuilder.Append("%')");
            }
            else
            {
                stringBuilder.Append("%'");
            }

            return stringBuilder.ToString();
        }

        private (string, string) TempTable(List<string> items,  int identifier, string dataType, string joinType, string joinTo)
        {
            var declareClause = new StringBuilder();
            declareClause.Append("DECLARE @TTable" + identifier + " TABLE (Tid" + identifier + " " + dataType + ") ");
            declareClause.Append("INSERT INTO @TTable" + identifier + " (Tid" + identifier + ") ");
            declareClause.Append("SELECT * FROM (");
            declareClause.Append("VALUES ");

            foreach (var item in items)
            {
                declareClause.Append("('" + item + "'),");
            }

            declareClause.Remove(declareClause.Length - 1, 1);
            declareClause.Append(") AS whatever" + identifier + "(Tid" + identifier + ")");

            return (declareClause.ToString(), joinType + " JOIN @TTable"+ identifier + " tt" + identifier + " ON s." + joinTo + " = tt" + identifier + ".Tid" + identifier);
        }

        /// <summary>
        /// This was the query you used to create the FT Search
        /// 
        /// create fulltext index on SocialMediaMappings(Experience)
        /// key index PK_SocialMediaMappings on ftCatalog
        /// 
        /// Note this was someone elses, notice they have change_tracking auto, you may need to add this
        /// create fulltext index on full_text(contents)
        //  key index PK_full_text on dd_full_text with change_tracking auto
        /// 
        /// https://azure.microsoft.com/en-gb/blog/full-text-search-is-now-available-for-preview-in-azure-sql-database/
        /// https://www.sqlshack.com/hands-full-text-search-sql-server/
        /// 
        /// This queries yoru FT index
        /// SELECT top 100 * FROM sys.dm_fts_index_keywords( DB_ID('LeadHootz.Function_2'), OBJECT_ID('SocialMediaMappings'))
        /// 
        /// This might be useful if you ever need to modify the FT column
        /// https://stackoverflow.com/questions/834841/how-do-i-disable-a-full-text-search-on-a-column-in-sql-server
        /// 
        /// This provides the status of the FT search
        /// http://www.andyfrench.info/2009/09/enabling-or-disabling-full-text.html
        /// 
        /// This was a good link that contained the same problems and concerns that I had
        /// https://www.mssqlforums.com/threads/how-long-takes-a-full-text-population.39944/
        /// 
        /// This is the catalog count
        /// SELECT FULLTEXTCATALOGPROPERTY('ftCatalog','ItemCount') -- 110765500
        /// 
        /// This will give a percentage
        /// https://www.mssqltips.com/sqlservertip/1681/gathering-status-and-detail-information-for-sql-server-full-text-catalogs/
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private string GenerateLikeBasedFilterQuery(GetSocialMediaMappingsByFilterRequestModel request)
        {
            //(string declare, string join) = TempTable(new List<string>() { "len-lamerton-34a084152'", "sharon-stevens-18691bb" }, 1, "nvarchar(200)", "LEFT", "LinkedIn");
            //(string declare2, string join2) = TempTable(new List<string>() { "len-lamerton-34a084152'", "sharon-stevens-18691bb" }, 2, "nvarchar(200)", "LEFT", "LinkedIn");

            var declareClause = new StringBuilder();
            var whereClause = new StringBuilder();

            StringBuilder query = new StringBuilder();

            if (!(request.ProfilesToExclude is null) && request.ProfilesToExclude.Any())
            {
                declareClause.Append("DECLARE @TTable TABLE (Tid nvarchar(200))");
                declareClause.Append("INSERT INTO @TTable (Tid) ");
                declareClause.Append("SELECT * FROM (");
                declareClause.Append("VALUES ");

                foreach (var profile in request.ProfilesToExclude)
                {
                    declareClause.Append("('" + profile + "'),");
                }

                declareClause.Remove(declareClause.Length - 1, 1);
                declareClause.Append(") AS whatever(Tid)");

                query = new StringBuilder();
                query.Append(declareClause.ToString());
                
                string headCount = null;
                if (request.ColumnValueSearchDictionary.TryGetValue(SocialMediaMappingsColumns.HeadCountType, out headCount))
                {
                    if (headCount != null)
                    {
                        query.Append(SqlConstants.FILTER_SEARCH_QUERY_TEMPLATE_TO_EXCLUDE_PROFILES_WITH_HEADCOUNT);
                    }
                    else
                    {
                        query.Append(SqlConstants.FILTER_SEARCH_QUERY_TEMPLATE_TO_EXCLUDE_PROFILES);
                    }
                }

                whereClause.Append(" tt.Tid is null AND ");
            }
            else
            {
                string headCount = null;
                if (request.ColumnValueSearchDictionary.TryGetValue(SocialMediaMappingsColumns.HeadCountType, out headCount))
                {
                    if (headCount != null)
                    {
                        //query.Append(SqlConstants.FILTER_SEARCH_QUERY_TEMPLATE_TO_EXCLUDE_PROFILES_WITH_HEADCOUNT);
                        query = new StringBuilder(SqlConstants.FILTER_SEARCH_QUERY_TEMPLATE_WITH_HEADCOUNT);
                    }
                    else
                    {
                        //query.Append(SqlConstants.FILTER_SEARCH_QUERY_TEMPLATE_TO_EXCLUDE_PROFILES);
                        query = new StringBuilder(SqlConstants.FILTER_SEARCH_QUERY_TEMPLATE);
                    }
                }
            }

            foreach (KeyValuePair<SocialMediaMappingsColumns,string> columnValuePair in request.ColumnValueSearchDictionary)
            {
                if (string.IsNullOrWhiteSpace(columnValuePair.Value))
                {
                    string removed;
                    request.ColumnValueSearchDictionary.TryRemove(columnValuePair.Key, out removed);
                    continue;
                }
                else
                {
                    if (columnValuePair.Value == SqlConstants.UNSPECIFIED)
                        continue;

                    string queryLine = null;

                    if (columnValuePair.Key == SocialMediaMappingsColumns.Summary)
                    {
                        queryLine = GetLikeSql(columnValuePair);

                        //var splitList = columnValuePair.Value.Split(' ', StringSplitOptions.RemoveEmptyEntries).ToList();

                        //if (splitList.Count > 1)
                        //{
                        //    queryLine = "CONTAINS (" + SocialMediaMappingsColumns.Summary + ", '\"" + columnValuePair.Value + "\"')";
                        //}
                        //else
                        //{
                        //    queryLine = "CONTAINS (" + SocialMediaMappingsColumns.Summary + ", '" + columnValuePair.Value + "')";
                        //}
                        //queryLine = GetFreeTextSql(columnValuePair);
                    }

                    //if (columnValuePair.Key == SocialMediaMappingsColumns.Position)
                    //{
                    //    queryLine = GetLikeSql(columnValuePair);
                    //}
                    
                    if(!string.IsNullOrEmpty(queryLine))
                        request.ColumnValueSearchDictionary[columnValuePair.Key] = queryLine.TrimStart().TrimEnd();                    
                }
            }

            foreach (var columnValuePair in request.ColumnValueSearchDictionary)
            {
                if (columnValuePair.Key == SocialMediaMappingsColumns.HeadCountType)
                {
                    var items = columnValuePair.Value.Split(',').ToList();

                    if (items.Count == 1)
                    {
                        if (columnValuePair.Value != null)
                        {
                            whereClause.AppendFormat(" $PARTITION.part_func_headcount(c.headcountid)" + " " + SqlConstants.FILTER_WHERE_CLAUSE_SINGLE_EQUALS_ID_TEMPLATE, columnValuePair.Value
                                .Replace("\"", string.Empty)
                                .Replace("'", string.Empty)
                                .TrimStart()
                                .TrimEnd());
                        }
                    }
                }

                // check for id based filters
                if (columnValuePair.Key == SocialMediaMappingsColumns.CountryId)
                {
                    var items = columnValuePair.Value.Split(',').ToList();

                    if (items.Count == 1)
                    {
                        // $PARTITION.part_func_country(s.CountryId) = 238 
                        whereClause.AppendFormat("$PARTITION.part_func_country(s.CountryId)" + " " + SqlConstants.FILTER_WHERE_CLAUSE_SINGLE_EQUALS_ID_TEMPLATE, columnValuePair.Value
                            .Replace("\"", string.Empty)
                            .Replace("'", string.Empty)
                            .TrimStart()
                            .TrimEnd());
                    }
                    else
                    {
                        whereClause.AppendFormat("$PARTITION.part_func_country(s.CountryId)" + " " + SqlConstants.FILTER_WHERE_CLAUSE_MULTIPLE_EQUALS_ID_TEMPLATE, columnValuePair.Value
                            .Replace("\"", string.Empty)
                            .Replace("'", string.Empty)
                            .TrimStart()
                            .TrimEnd());
                    }
                }   
                else if (columnValuePair.Key == SocialMediaMappingsColumns.CityId ||
                    columnValuePair.Key == SocialMediaMappingsColumns.IndustryId ||
                    columnValuePair.Key == SocialMediaMappingsColumns.RegionId ||
                    columnValuePair.Key == SocialMediaMappingsColumns.PositionId ||
                    columnValuePair.Key == SocialMediaMappingsColumns.CompanyId)
                {
                    if (!string.IsNullOrEmpty(columnValuePair.Value))
                    {
                        var items = columnValuePair.Value.Split(',').ToList();

                        if (items.Count == 1)
                        {
                            whereClause.AppendFormat("s." + columnValuePair.Key.ToString() + " " + SqlConstants.FILTER_WHERE_CLAUSE_SINGLE_EQUALS_ID_TEMPLATE, columnValuePair.Value
                                .Replace("\"", string.Empty)
                                .Replace("'", string.Empty)
                                .TrimStart()
                                .TrimEnd());
                        }
                        else
                        {
                            whereClause.AppendFormat("s." + columnValuePair.Key.ToString() + " " + SqlConstants.FILTER_WHERE_CLAUSE_MULTIPLE_EQUALS_ID_TEMPLATE, columnValuePair.Value
                                .Replace("\"", string.Empty)
                                .Replace("'", string.Empty)
                                .TrimStart()
                                .TrimEnd());
                        }
                    }
                }
                else if (columnValuePair.Key == SocialMediaMappingsColumns.Summary)
                {
                    whereClause.AppendFormat(columnValuePair.Value + " AND ");
                }
                else if (columnValuePair.Key == SocialMediaMappingsColumns.Postcode)
                {
                    if (columnValuePair.Value.Contains("*"))
                    {
                        whereClause.AppendFormat("s." + SocialMediaMappingsColumns.Postcode + " LIKE '" + columnValuePair.Value.Replace("*", "%") + "' AND ");
                    }
                    else
                    {
                        whereClause.AppendFormat("s." + SocialMediaMappingsColumns.Postcode + " = '"  + columnValuePair.Value + "' AND "); 
                    }
                }
            }

            var ageRangeClause = new StringBuilder();

            if (request.AgeStart.HasValue)
            {
                if (whereClause.Length > default(int))
                {
                    ageRangeClause.Append($" (s.BirthYear <= {DateTime.UtcNow.Year - request.AgeStart} ");
                }

                if (whereClause.Length == default)
                {
                    ageRangeClause.Append($" AND (s.BirthYear <= {DateTime.UtcNow.Year - request.AgeStart} ");
                }
            }

            if (request.AgeStart.HasValue && request.AgeEnd.HasValue)
            {
                ageRangeClause.Append($" AND s.BirthYear >= {DateTime.UtcNow.Year - request.AgeEnd} ");
            }

            if (request.AgeStart.HasValue)
            {
                ageRangeClause.Append($" ) AND ");
            }

            whereClause.Append(ageRangeClause);

            var experienceRange = new StringBuilder();

            if (request.ExperienceStart.HasValue)
            {
                if (whereClause.Length > default(int))
                {
                    experienceRange.Append($" (s.{SocialMediaMappingsColumns.InferredYearsExperience} > {request.ExperienceStart.Value} ");
                }

                if (whereClause.Length == default)
                {
                    experienceRange.Append($" AND (s.{SocialMediaMappingsColumns.InferredYearsExperience} > {request.ExperienceStart.Value} ");
                }
            }

            if (request.ExperienceStart.HasValue && request.ExperienceEnd.HasValue)
            {
                experienceRange.Append($" AND s.{SocialMediaMappingsColumns.InferredYearsExperience} < {request.ExperienceEnd.Value}");
            }

            if (request.ExperienceStart.HasValue)
            {
                experienceRange.Append(" ) AND ");
            }

            whereClause.Append(experienceRange);

            var salaryRangeClause = new StringBuilder();

            if (request.SalaryStart.HasValue)
            {
                if (whereClause.Length > default(int))
                {
                    salaryRangeClause.Append($" (s.{SocialMediaMappingsColumns.InferredSalary} > {request.SalaryStart.Value} ");
                }

                if (whereClause.Length == default)
                {
                    salaryRangeClause.Append($" AND (s.{SocialMediaMappingsColumns.InferredSalary} > {request.SalaryStart.Value} ");
                }
            }

            if (request.SalaryStart.HasValue && request.SalaryEnd.HasValue)
            {
                salaryRangeClause.Append($" AND s.{SocialMediaMappingsColumns.InferredSalary} < {request.SalaryEnd.Value}");
            }

            if (request.SalaryStart.HasValue)
            {
                salaryRangeClause.Append(" ) AND ");
            }

            whereClause.Append(salaryRangeClause);

            if (request.EmailWorkOrPersonalOptionsType == EmailWorkOrPersonalOptionsType.PublicOrPrivate)
            {
                whereClause.AppendFormat("(s." + SocialMediaMappingsColumns.Email + " IS NOT NULL OR " + SocialMediaMappingsColumns.WorkEmail + " IS NOT NULL) AND");
            }
            else if (request.EmailWorkOrPersonalOptionsType == EmailWorkOrPersonalOptionsType.Private)
            {
                whereClause.AppendFormat(SqlConstants.FILTER_WHERE_CLAUSE_NOT_NULL_TEMPLATE,
                    SocialMediaMappingsColumns.Email.ToString());
            }
            else if (request.EmailWorkOrPersonalOptionsType == EmailWorkOrPersonalOptionsType.Public)
            {
                whereClause.AppendFormat(SqlConstants.FILTER_WHERE_CLAUSE_NOT_NULL_TEMPLATE,
                    SocialMediaMappingsColumns.WorkEmail.ToString());
            }

            if (request.PhoneOptionsType == PhoneOptionsType.Mobile)
            {
                whereClause.AppendFormat("(s." + SocialMediaMappingsColumns.Mobile + " IS NOT NULL) AND");
            }

            if (request.IsFacebook || request.IsTwitter)
            {
                if (request.IsFacebook)
                {
                    whereClause.AppendFormat(SqlConstants.FILTER_WHERE_CLAUSE_NOT_NULL_TEMPLATE,
                        SocialMediaMappingsColumns.FacebookUsername.ToString());
                }
                else
                {
                    whereClause.AppendFormat(SqlConstants.FILTER_WHERE_CLAUSE_NOT_NULL_TEMPLATE,
                        SocialMediaMappingsColumns.Twitter.ToString());
                }
            }

            //if (request.IsPhone)
            //{
            //    //TODO: add phone number column to database and handle filter of it here
            //}

            if (whereClause.Length > default(int))
            {
                whereClause.Insert(0, "WHERE ");
                whereClause.Remove(whereClause.ToString().TrimEnd().Length - 3, 3);
            }

            string result = string.Format(query.ToString(),
                   whereClause,
                   request.Skip,
                   request.Take,
                   request.IsFacebook ? "s.FacebookUsername" : request.IsTwitter ? "s.Twitter" : "s.LinkedIn");

            return result;
        }

        private string GenerateFilterQuery(GetSocialMediaMappingsByFilterRequestModel request)
        {
             var query = new StringBuilder(SqlConstants.FILTER_SEARCH_QUERY_TEMPLATE);

            var whereClause = new StringBuilder();
 
            foreach (var columnValuePair in request.ColumnValueSearchDictionary)
            { 
                if (string.IsNullOrWhiteSpace(columnValuePair.Value))
                {
                    string removed;
                    request.ColumnValueSearchDictionary.TryRemove(columnValuePair.Key, out removed);
                    continue;
                }
                else
                {
                    if (columnValuePair.Value == SqlConstants.UNSPECIFIED)
                        continue;

                    // need to ensure pairs with white space are wrapped in quotes
                    List<string> words = columnValuePair.Value.Split(new string[] { "OR", "AND", "NOT" }, StringSplitOptions.RemoveEmptyEntries)
                                                            .Select(i => i.Trim())
                                                            .Where(i => !string.IsNullOrWhiteSpace(i)).ToList();

                    string updated = columnValuePair.Value;

                    bool isChanged = false;
                    foreach (string word in words)
                    {
                        isChanged = true;

                        if (word.Contains(" ") && (!(word.Substring(0, 1) == "\"") && !(word.Contains("(\""))))
                        {
                            updated = updated.Replace(word, "\"" + word + "\" ");
                        }
                    }

                    if (isChanged)
                    {
                        request.ColumnValueSearchDictionary[columnValuePair.Key] = updated.TrimStart().TrimEnd();
                    }
                }
            }

            foreach (var columnValuePair in request.ColumnValueSearchDictionary)
            {
                //if (columnValuePair.Key == SocialMediaMappingsColumns.CurrentCompany && request.IsCurrentCompanyOnly)
                //{
                //    whereClause.AppendFormat(SqlConstants.FILTER_WHERE_CLAUSE_BETWEEN_TEMPLATE,
                //        columnValuePair.Key.ToString(),
                //        columnValuePair.Value);
                //}

                //if (columnValuePair.Key == SocialMediaMappingsColumns.CurrentCompany && !request.IsCurrentCompanyOnly)
                //{
                //    whereClause.AppendFormat($"({SqlConstants.FILTER_WHERE_CLAUSE_BETWEEN_TEMPLATE.Replace("AND", "OR")}",
                //        columnValuePair.Key.ToString(),
                //        columnValuePair.Value);

                //    whereClause.AppendFormat(SqlConstants.FILTER_WHERE_CLAUSE_BETWEEN_TEMPLATE.Replace("AND", string.Empty),
                //        columnValuePair.Key.GetDescription(),
                //        columnValuePair.Value)
                //        .Append(") AND");
                //}

                // check for id based filters
                if (columnValuePair.Key == SocialMediaMappingsColumns.CountryId || columnValuePair.Key == SocialMediaMappingsColumns.CityId || columnValuePair.Key == SocialMediaMappingsColumns.IndustryId)
                {
                    if (!string.IsNullOrEmpty(columnValuePair.Value))
                    {
                        var items = columnValuePair.Value.Split(',').ToList();

                        if (items.Count == 1)
                        {
                            whereClause.AppendFormat(columnValuePair.Key.ToString() + " " + SqlConstants.FILTER_WHERE_CLAUSE_SINGLE_EQUALS_ID_TEMPLATE, columnValuePair.Value
                                .Replace("\"", string.Empty)
                                .Replace("'", string.Empty)
                                .TrimStart()
                                .TrimEnd());
                        }
                        else
                        {
                            whereClause.AppendFormat(columnValuePair.Key.ToString() + " " + SqlConstants.FILTER_WHERE_CLAUSE_MULTIPLE_EQUALS_ID_TEMPLATE, columnValuePair.Value
                                .Replace("\"", string.Empty)
                                .Replace("'",string.Empty)
                                .TrimStart()
                                .TrimEnd());
                        }
                    }
                }
                //else if (columnValuePair.Key != SocialMediaMappingsColumns.CurrentCompany)
                //{
                //    whereClause.AppendFormat(SqlConstants.FILTER_WHERE_CLAUSE_BETWEEN_TEMPLATE,
                //        columnValuePair.Key.ToString(),
                //        columnValuePair.Value.Replace("\"", string.Empty));
                //}
            }

            var ageRangeClause = new StringBuilder();

            if (request.AgeStart.HasValue)
            {
                if (whereClause.Length > default(int))
                {
                    ageRangeClause.Append($" (BirthYear <= {DateTime.UtcNow.Year - request.AgeStart} ");
                }

                if (whereClause.Length == default)
                {
                    ageRangeClause.Append($" AND (BirthYear <= {DateTime.UtcNow.Year - request.AgeStart} ");
                }
            }

            if (request.AgeStart.HasValue && request.AgeEnd.HasValue)
            {
                ageRangeClause.Append($" AND BirthYear >= {DateTime.UtcNow.Year - request.AgeEnd} ");
            }

            if (request.AgeStart.HasValue)
            {
                ageRangeClause.Append($" ) AND ");
            }

            whereClause.Append(ageRangeClause);

            var salaryRangeClause = new StringBuilder();

            if (request.SalaryStart.HasValue)
            {
                if (whereClause.Length > default(int))
                {
                    salaryRangeClause.Append($" ({SocialMediaMappingsColumns.InferredSalary} > {request.SalaryStart.Value} ");
                }

                if (whereClause.Length == default)
                {
                    salaryRangeClause.Append($" AND ({SocialMediaMappingsColumns.InferredSalary} > {request.SalaryStart.Value} ");
                }
            }

            if (request.SalaryStart.HasValue && request.SalaryEnd.HasValue)
            {
                salaryRangeClause.Append($" AND {SocialMediaMappingsColumns.InferredSalary} < {request.SalaryEnd.Value}");
            }

            if (request.SalaryStart.HasValue)
            {
                salaryRangeClause.Append(" ) AND ");
            }

            whereClause.Append(salaryRangeClause);

            //if (request.IsEmail)
            //{
            //    whereClause.AppendFormat(SqlConstants.FILTER_WHERE_CLAUSE_NOT_NULL_TEMPLATE,
            //        SocialMediaMappingsColumns.Email.ToString());
            //}

            //if (request.IsWorkEmail)
            //{
            //    whereClause.AppendFormat(SqlConstants.FILTER_WHERE_CLAUSE_NOT_NULL_TEMPLATE,
            //        SocialMediaMappingsColumns.WorkEmail.ToString());
            //}

            if (request.IsFacebook)
            {
                whereClause.AppendFormat(SqlConstants.FILTER_WHERE_CLAUSE_NOT_NULL_TEMPLATE,
                    SocialMediaMappingsColumns.FacebookUsername.ToString());
            }

            //if (request.IsPhone)
            //{
            //    //TODO: add phone number column to database and handle filter of it here
            //}

            if (request.IsTwitter)
            {
                whereClause.AppendFormat(SqlConstants.FILTER_WHERE_CLAUSE_NOT_NULL_TEMPLATE,
                    SocialMediaMappingsColumns.Twitter.ToString());
            }

            if (!(request.ProfilesToExclude is null) && request.ProfilesToExclude.Any())
            {
                whereClause.AppendFormat(SqlConstants.FB_PROFILES_TO_EXCLUDE_WHERE_CLAUSE_TEMPLATE,
                        string.Join("', '", request.ProfilesToExclude))
                    .Append(" AND ");
            }

            if (whereClause.Length > default(int))
            {
                whereClause.Insert(0, "WHERE ");
                whereClause.Remove(whereClause.ToString().TrimEnd().Length - 3, 3);
            }

            string result = string.Format(query.ToString(),
                   whereClause,
                   request.Skip,
                   request.Take);

            return result;
        }
        
        private string GeneratExplicitSearchQuery(FreeTextSearchRequestModel searchModel)
        {
            StringBuilder query = new StringBuilder();

            if (!string.IsNullOrWhiteSpace(searchModel.Fields) && !string.IsNullOrWhiteSpace(searchModel.Values))
            {   
                List<string> fieldList = searchModel.Fields.Split('|').ToList();
                List<string> values = searchModel.Values.Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries).ToList();

                if (fieldList.Count().Equals(values.Count()))
                {
                    StringBuilder stringBuilder = new StringBuilder();

                    for (int i = 0; i < fieldList.Count; i++)
                    {
                        stringBuilder.Append("CONTAINS(" + fieldList[i] + "," + "'" + values[i] + "') AND ");
                    }

                    stringBuilder.Remove(stringBuilder.Length - 4, 4);

                    query.Append(string.Format(SqlConstants.FREETEXT_EXPLICIT_SEARCH_QUERY_TEMPLATE, searchModel.TakeNumber, stringBuilder.ToString()));


                    if (string.IsNullOrWhiteSpace(searchModel.Fields))
                    {
                        var names = typeof(SocialMediaMapping)
                       .GetProperties()
                       .Select(property => property.Name);

                        query.Append(string.Format(SqlConstants.FREETEXT_SEARCH_QUERY_TEMPLATE, names, searchModel.Values));
                    }

                    if (searchModel.IgnoreIds is null || !searchModel.IgnoreIds.Any())
                    {
                        return query.ToString();
                    }
                }

                query.Append(string.Format(SqlConstants.IGNORE_IDS_STATEMENT_TEMPLATE, "'" + string.Join("','", searchModel.IgnoreIds) + "'"));
            }

            return query.ToString();
        }

        public async Task<int> UpdateEmailPhoneStatus(string requestBody)
        {
            int totalUpdates = default;
            var request = JsonConvert.DeserializeObject<UpdatePlatfomUpdateModel>(requestBody);
            
            if (request != null)
            {
                string[] workEmails = request.PlatfomUpdateModel.Where(i => i.PlatformType == PlatformType.WorkEmail).Select(i => i.Item).ToArray();
                List<SocialMediaMapping> foundItems = new List<SocialMediaMapping>();

                if (workEmails.Count() > 0)
                {
                    List<SocialMediaMapping> socialMediaMappings = await _socialMediaMappingRepository.GetByEmails(workEmails);
                    foreach (var socialMediaMapping in socialMediaMappings)
                    {
                        if (socialMediaMapping.WorkEmailStatus == null) {
                            socialMediaMapping.WorkEmailStatus = 1;
                        }
                        else
                            socialMediaMapping.WorkEmailStatus++;
                    }

                    totalUpdates = await _socialMediaMappingRepository.UpdateRange(socialMediaMappings);
                }

                string[] privateEmails = request.PlatfomUpdateModel.Where(i => i.PlatformType == PlatformType.PrivateEmail).Select(i => i.Item).ToArray();
                foundItems.Clear();

                if (privateEmails.Count() > 0)
                {
                    List<SocialMediaMapping> socialMediaMappings = await _socialMediaMappingRepository.GetByPrivateEmails(privateEmails);
                    foreach (var socialMediaMapping in socialMediaMappings)
                    {
                        if (socialMediaMapping.EmailStatus == null) {
                            socialMediaMapping.EmailStatus = 1;
                        }
                        else
                            socialMediaMapping.EmailStatus++;
                    }

                    totalUpdates += await _socialMediaMappingRepository.UpdateRange(socialMediaMappings);
                }

                string[] workPhones = request.PlatfomUpdateModel.Where(i => i.PlatformType == PlatformType.WorkPhone).Select(i => i.Item).ToArray();
                foundItems.Clear();

                if (workPhones.Count() > 0)
                {
                    List<SocialMediaMapping> socialMediaMappings = await _socialMediaMappingRepository.GetByMobile(workPhones);
                    foreach (var socialMediaMapping in socialMediaMappings)
                    {
                        if (socialMediaMapping.PhoneStatus == null) {
                            socialMediaMapping.PhoneStatus = 1;
                        }
                        else
                            socialMediaMapping.PhoneStatus++;
                    }

                    totalUpdates += await _socialMediaMappingRepository.UpdateRange(socialMediaMappings);
                }

                string[] privatePhones = request.PlatfomUpdateModel.Where(i => i.PlatformType == PlatformType.PrivatePhone).Select(i => i.Item).ToArray();
                foundItems.Clear();

                if (privatePhones.Count() > 0)
                {
                    List<SocialMediaMapping> socialMediaMappings = await _socialMediaMappingRepository.GetByPrivatePhone(privateEmails);
                    foreach (var socialMediaMapping in socialMediaMappings)
                    {
                        if(socialMediaMapping.MobileStatus == null) {
                            socialMediaMapping.MobileStatus = 1;
                        }
                        else
                            socialMediaMapping.PhoneStatus++;
                    }

                    totalUpdates += await _socialMediaMappingRepository.UpdateRange(socialMediaMappings);
                }
            }

            return totalUpdates;
        }
    }
}

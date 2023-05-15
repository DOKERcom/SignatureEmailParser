using SignatureEmailParser.Models;
using SignatureEmailParser.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace SignatureEmailParser.Services.Implementation
{
    public class EmailParseModelFillService : IEmailParseModelFillService
    {

        public EmailParseModel FillName(EmailParseModel emailParseModel, FullNameModel nameModel)
        {

            if (nameModel != null)
            {
                emailParseModel.Name = nameModel.Name;
                emailParseModel.Surname = nameModel.Surname;
            }

            return emailParseModel;
        }

        public EmailParseModel FillLocation(EmailParseModel emailParseModel, LocationModel locationModel)
        {

            if (locationModel != null)
            {
                if (locationModel.Country != null)
                {
                    emailParseModel.Country = locationModel.Country.CountryName;

                    emailParseModel.CountryId = locationModel.Country.Id;
                }

                if (locationModel.City != null)
                {
                    emailParseModel.City = locationModel.City.CityName;

                    emailParseModel.CityId = locationModel.City.Id;
                }

                if (locationModel.Region != null)
                {
                    emailParseModel.Region = locationModel.Region.RegionName;

                    emailParseModel.RegionId = locationModel.Region.Id;
                }
            }

            return emailParseModel;
        }

        public EmailParseModel FillPosition(EmailParseModel emailParseModel, PositionModel position, string positionWord)
        {

            if (position != null)
            {
                emailParseModel.Position = position.PositionName;

                emailParseModel.PositionId = position.Id;
            }
            else if (!string.IsNullOrEmpty(positionWord))
            {
                emailParseModel.Position = positionWord;
            }

            return emailParseModel;
        }

        public EmailParseModel FillEmails(EmailParseModel emailParseModel, ExtractWordModels extractWordModels)
        {
            if (extractWordModels != null)
            {
                if (extractWordModels.ExtractDatas.Count == 1)
                {
                    emailParseModel.WorkEmail = extractWordModels.ExtractDatas[0].Word;
                }
                else if (extractWordModels.ExtractDatas.Count > 1)
                {
                    emailParseModel.WorkEmail = extractWordModels.ExtractDatas[0].Word;
                    emailParseModel.PersonalEmail = extractWordModels.ExtractDatas[1].Word;
                }
            }

            return emailParseModel;
        }

        public EmailParseModel FillInstagram(EmailParseModel emailParseModel, ExtractWordModels extractWordModels)
        {

            if (extractWordModels != null && extractWordModels.ExtractDatas.Count > 0)
            {
                emailParseModel.Instagram = extractWordModels.ExtractDatas[0].Word;
            }

            return emailParseModel;
        }

        public EmailParseModel FillTwitter(EmailParseModel emailParseModel, ExtractWordModels extractWordModels)
        {

            if (extractWordModels != null && extractWordModels.ExtractDatas.Count > 0)
            {
                emailParseModel.Twitter = extractWordModels.ExtractDatas[0].Word;
            }

            return emailParseModel;
        }

        public EmailParseModel FillFacebook(EmailParseModel emailParseModel, ExtractWordModels extractWordModels)
        {

            if (extractWordModels != null && extractWordModels.ExtractDatas.Count > 0)
            {
                emailParseModel.Facebook = extractWordModels.ExtractDatas[0].Word;
            }

            return emailParseModel;
        }

        public EmailParseModel FillPostCode(EmailParseModel emailParseModel, ExtractWordModels extractWordModels)
        {

            if (extractWordModels != null && extractWordModels.ExtractDatas.Count > 0)
            {
                emailParseModel.Postcode = extractWordModels.ExtractDatas[0].Word;
            }

            return emailParseModel;
        }

        public EmailParseModel FillLinkedIn(EmailParseModel emailParseModel, ExtractWordModels extractWordModels)
        {
            if (extractWordModels != null && extractWordModels.ExtractDatas.Count > 0)
            {
                emailParseModel.LinkedIn = extractWordModels.ExtractDatas[0].Word;
            }

            return emailParseModel;
        }

        public EmailParseModel FillMobile(EmailParseModel emailParseModel, ExtractWordModels extractWordModels)
        {

            if (extractWordModels != null && extractWordModels.ExtractDatas.Count > 0)
            {
                emailParseModel.Mobile = extractWordModels.ExtractDatas[0].Word;
            }

            return emailParseModel;
        }

        public EmailParseModel FillWebSite(EmailParseModel emailParseModel, ExtractWordModels extractWordModels)
        {

            if (extractWordModels != null && extractWordModels.ExtractDatas.Count > 0)
            {
                emailParseModel.Website = extractWordModels.ExtractDatas[0].Word;
            }

            return emailParseModel;
        }

    }
}

using SignatureEmailParser.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SignatureEmailParser.Services.Interfaces
{
    public interface IEmailParseModelFillService
    {

        public EmailParseModel FillName(EmailParseModel emailParseModel, FullNameModel nameModel);

        public EmailParseModel FillLocation(EmailParseModel emailParseModel, LocationModel locationModel);

        public EmailParseModel FillPosition(EmailParseModel emailParseModel, PositionModel position, string positionWord);

        public EmailParseModel FillEmails(EmailParseModel emailParseModel, ExtractWordModels extractWordModels);

        public EmailParseModel FillInstagram(EmailParseModel emailParseModel, ExtractWordModels extractWordModels);

        public EmailParseModel FillTwitter(EmailParseModel emailParseModel, ExtractWordModels extractWordModels);

        public EmailParseModel FillFacebook(EmailParseModel emailParseModel, ExtractWordModels extractWordModels);

        public EmailParseModel FillPostCode(EmailParseModel emailParseModel, ExtractWordModels extractWordModels);

        public EmailParseModel FillLinkedIn(EmailParseModel emailParseModel, ExtractWordModels extractWordModels);

        public EmailParseModel FillMobile(EmailParseModel emailParseModel, ExtractWordModels extractWordModels);

        public EmailParseModel FillWebSite(EmailParseModel emailParseModel, ExtractWordModels extractWordModels);

    }
}

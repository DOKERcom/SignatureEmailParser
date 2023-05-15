using System;
using System.Collections.Generic;
using System.Text;

namespace SignatureEmailParser.Models
{
    public class ExtractDataModel
    {

        public ExtractWordModels Names { get; set; }

        public ExtractWordModels Surnames { get; set; }

        public ExtractWordModels WorkEmails { get; set; }

        public ExtractWordModels PersonalEmails { get; set; }

        public ExtractWordModels Mobiles { get; set; }

        public ExtractWordModels Telephones { get; set; }

        public ExtractWordModels LinkedIns { get; set; }

        public ExtractWordModels Facebooks { get; set; }

        public ExtractWordModels Twitters { get; set; }

        public ExtractWordModels Instagrams { get; set; }

        public ExtractWordModels WebSites { get; set; }

        public ExtractWordModels Addresses { get; set; }

        public ExtractWordModels Positions { get; set; }

        public ExtractWordModels Countries { get; set; }

        public ExtractWordModels Regions { get; set; }

        public ExtractWordModels Cities { get; set; }

        public ExtractWordModels PostCodes { get; set; }

        public string GlobalData { get; set; }

    }
}

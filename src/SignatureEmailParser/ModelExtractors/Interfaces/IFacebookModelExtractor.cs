using SignatureEmailParser.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SignatureEmailParser.ModelExtractors.Interfaces
{
    public interface IFacebookModelExtractor
    {

        public Task<ExtractWordModels> ExtractEmails(string data);

    }
}

using SignatureEmailParser.Extractors.Interfaces;
using SignatureEmailParser.ModelExtractors.Interfaces;
using SignatureEmailParser.Models;
using System.Threading.Tasks;

namespace SignatureEmailParser.ModelExtractors.Implementation
{
    public class FacebookModelExtractor : IFacebookModelExtractor
    {
        IFacebookExtractor _facebookExtractor;

        public FacebookModelExtractor(IFacebookExtractor facebookExtractor)
        {
            _facebookExtractor = facebookExtractor;
        }

        public async Task<ExtractWordModels> ExtractEmails(string data)
        {
            return await _facebookExtractor.ExtractFacebooks(data);
        }

    }
}

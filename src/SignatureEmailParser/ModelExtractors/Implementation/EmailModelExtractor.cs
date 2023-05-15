using SignatureEmailParser.Extractors.Interfaces;
using SignatureEmailParser.ModelExtractors.Interfaces;
using SignatureEmailParser.Models;
using System.Threading.Tasks;

namespace SignatureEmailParser.ModelExtractors.Implementation
{

    public class EmailModelExtractor : IEmailModelExtractor
    {
        IEmailExtractor _emailExtractor;

        public EmailModelExtractor(IEmailExtractor emailExtractor)
        {
            _emailExtractor = emailExtractor;
        }

        public async Task<ExtractWordModels> ExtractEmails(string data)
        {
            return await _emailExtractor.ExtractEmails(data);
        }

    }
}

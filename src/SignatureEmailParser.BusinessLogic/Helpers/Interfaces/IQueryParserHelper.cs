using Microsoft.AspNetCore.Http;

namespace SignatureEmailParser.BusinessLogic.Helpers.Interfaces
{
    public interface IQueryParserHelper
    {
        T ParseFromQueryCollection<T>(IQueryCollection queryCollection) where T : new();
    }
}

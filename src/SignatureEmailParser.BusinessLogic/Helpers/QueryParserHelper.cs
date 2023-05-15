using SignatureEmailParser.BusinessLogic.Helpers.Interfaces;
using Microsoft.AspNetCore.Http;
using System;

namespace SignatureEmailParser.BusinessLogic.Helpers
{
    public class QueryParserHelper : IQueryParserHelper
    {
        public T ParseFromQueryCollection<T>(IQueryCollection queryCollection) where T : new()
        {
            var obj = new T();

            var properties = typeof(T).GetProperties();

            foreach (var property in properties)
            {
                var valueAsString = queryCollection[property.Name].ToString();

                if (string.IsNullOrWhiteSpace(valueAsString))
                {
                    continue;
                }

                var value = Convert.ChangeType(valueAsString.ToString(), property.PropertyType);

                if (value is null)
                {
                    continue;
                }

                property.SetValue(obj, value, null);
            }
            return obj;
        }
    }
}

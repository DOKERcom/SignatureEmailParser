using SignatureEmailParser.Factories.Interfaces;
using SignatureEmailParser.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SignatureEmailParser.Factories.Implementation
{
    public class DictionaryToListModelFactory : IDictionaryToListModelFactory
    {
        public List<CountryModel> GetCountryModel(Dictionary<long, string> countries)
        {
            List<CountryModel> countryList = new List<CountryModel>();

            foreach (var country in countries)
            {
                countryList.Add(new CountryModel { Id = country.Key, CountryName = country.Value });
            }

            return countryList;
        }

        public List<PositionModel> GetPositionModel(Dictionary<long, string> positions)
        {
            List<PositionModel> positionList = new List<PositionModel>();

            foreach (var position in positions)
            {
                positionList.Add(new PositionModel { Id = position.Key, PositionName = position.Value });
            }

            return positionList;
        }
    }
}

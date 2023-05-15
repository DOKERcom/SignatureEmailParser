using SignatureEmailParser.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SignatureEmailParser.Factories.Interfaces
{
    public interface IDictionaryToListModelFactory
    {

        public List<CountryModel> GetCountryModel(Dictionary<long, string> countries);

        public List<PositionModel> GetPositionModel(Dictionary<long, string> positions);


    }
}

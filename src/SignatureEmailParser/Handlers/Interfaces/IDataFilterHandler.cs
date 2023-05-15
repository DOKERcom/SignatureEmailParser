using SignatureEmailParser.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SignatureEmailParser.Handlers.Interfaces
{
    public interface IDataFilterHandler
    {

        public ExtractWordModel GetLongestWord(ExtractWordModels models);

        public ExtractWordModels SortWordModelsByMiddleNumber(ExtractWordModels models, int number);

        public int GetMiddleNumber(List<ExtractWordModels> models);

        public ExtractWordModels ClearDuplicatesByWord(ExtractWordModels models);

    }
}

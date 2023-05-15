using SignatureEmailParser.Handlers.Interfaces;
using SignatureEmailParser.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SignatureEmailParser.Handlers.Implementation
{
    public class DataFilterHandler : IDataFilterHandler
    {
        public ExtractWordModel GetLongestWord(ExtractWordModels models)
        {
            ExtractWordModel longestModel = new ExtractWordModel();


            foreach (var data in models.ExtractDatas)
            {
                if (string.IsNullOrEmpty(longestModel.Word) || longestModel.WordLength < data.WordLength)
                {
                    longestModel = data;
                }
            }

            return longestModel;
        }

        public ExtractWordModels SortWordModelsByMiddleNumber(ExtractWordModels models, int number)
        {

            ExtractWordModels newModels = new ExtractWordModels();

            Dictionary<int, ExtractWordModel> keyValuePairs = new Dictionary<int, ExtractWordModel>();

            foreach (var data in models.ExtractDatas)
            {

                int modelMidNumber = Math.Abs((data.FirstCharPos + data.LastCharPos) / 2 - number);

                if (!keyValuePairs.ContainsKey(modelMidNumber))
                    keyValuePairs.Add(modelMidNumber, data);

            }

            keyValuePairs = keyValuePairs.OrderBy(obj => obj.Key).ToDictionary(obj => obj.Key, obj => obj.Value);

            foreach (var keyValue in keyValuePairs)
            {
                newModels.ExtractDatas.Add(keyValue.Value);
            }

            newModels.OutUpdateData = models.OutUpdateData;

            return newModels;
        }

        public int GetMiddleNumber(List<ExtractWordModels> models)
        {
            int number = 0;

            foreach (var model in models)
            {
                foreach (var data in model.ExtractDatas)
                {
                    if (number > 0)
                    {
                        if (data.FirstCharPos > 0 && data.LastCharPos > 0)
                        {
                            number = ((data.FirstCharPos + data.LastCharPos) / 2 + number) / 2;
                        }
                    }
                    else
                    {
                        number = (data.FirstCharPos + data.LastCharPos) / 2;
                    }
                }
            }

            return number;
        }

        public ExtractWordModels ClearDuplicatesByWord(ExtractWordModels models)
        {
            ExtractWordModels noDuplicatesExtracted = new ExtractWordModels();

            foreach (var model in models.ExtractDatas)
            {
                if (noDuplicatesExtracted.ExtractDatas.Where(m => m.Word == model.Word).Count() == 0)
                {
                    noDuplicatesExtracted.ExtractDatas.Add(model);
                }
            }

            return noDuplicatesExtracted;
        }
    }
}

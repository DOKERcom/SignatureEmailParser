using SignatureEmailParser.Extensions;
using SignatureEmailParser.Extractors.Implementation;
using SignatureEmailParser.Handlers.Interfaces;
using SignatureEmailParser.ModelExtractors.Implementation;
using SignatureEmailParser.ModelExtractors.Interfaces;
using SignatureEmailParser.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignatureEmailParser.Handlers.Implementation
{
    public class ExtractParallelHandler : IExtractParallelHandler
    {


        private ExtractDataHandler _extractDataHandler;

        public ExtractParallelHandler(ExtractDataHandler extractDataHandler)
        {
            if(_extractDataHandler == null)
                _extractDataHandler = extractDataHandler;
        }
        public ExtractWordModels GetParallelExtractSurnamesByNames(string globalData, ExtractWordModels firstNameModels)
        {

            List<ExtractWordModels> listExtractWordModels = new List<ExtractWordModels>();

            List<List<ExtractWordModel>> listfirstNameModels = firstNameModels.ExtractDatas.Partition(1);

            Parallel.ForEach(listfirstNameModels, firstNameModels =>
            {

                ExtractWordModels _extractWordModels = new ExtractWordModels();

                _extractWordModels.ExtractDatas = firstNameModels;

                listExtractWordModels.Add(_extractDataHandler.ExtractSurnamesByNames(globalData, _extractWordModels).Result);

            });

            ExtractWordModels extractWordModels = GetExtractWordModels(listExtractWordModels);

            return extractWordModels;
        }

        public ExtractWordModels GetParallelExtractNames(string globalData, List<FirstNameModel> firstNameModels)
        {

            List<string> firstNames = firstNameModels.Select(m => m.Name).ToList();

            List<ExtractWordModels> listExtractWordModels = new List<ExtractWordModels>();

            List<List<string>> listfirstNameModels = firstNames.Partition(500);

            Parallel.ForEach(listfirstNameModels, firstNameModels =>
            {

                listExtractWordModels.Add(_extractDataHandler.ExtractNames(globalData, firstNameModels).Result);

            });

            ExtractWordModels extractWordModels = GetExtractWordModels(listExtractWordModels);

            return extractWordModels;
        }

        public ExtractWordModels GetParallelExtractRegions(string globalData, List<RegionModel> regionModels)
        {

            List<ExtractWordModels> listExtractWordModels = new List<ExtractWordModels>();

            List<List<RegionModel>> listregionModels = regionModels.Partition(500);

            Parallel.ForEach(listregionModels, regionModels =>
            {

                listExtractWordModels.Add(_extractDataHandler.ExtractRegions(globalData, regionModels).Result);

            });

            ExtractWordModels extractWordModels = GetExtractWordModels(listExtractWordModels);

            return extractWordModels;
        }

        public ExtractWordModels GetParallelExtractCities(string globalData, List<CityModel> cityModels)
        {

            List<ExtractWordModels> listExtractWordModels = new List<ExtractWordModels>();

            List<List<CityModel>> listcityModels = cityModels.Partition(500);

            Parallel.ForEach(listcityModels, cityModels =>
            {

                listExtractWordModels.Add(_extractDataHandler.ExtractCities(globalData, cityModels).Result);

            });

            ExtractWordModels extractWordModels = GetExtractWordModels(listExtractWordModels);

            return extractWordModels;
        }

        public ExtractWordModels GetParallelExtractPositions(string globalData, List<PositionModel> positionModels)
        {

            List<ExtractWordModels> listExtractWordModels = new List<ExtractWordModels>();

            List<List<PositionModel>> listPositionModels = positionModels.Partition(500);

            Parallel.ForEach(listPositionModels, positionModels =>
            {

                listExtractWordModels.Add(_extractDataHandler.ExtractPositions(globalData, positionModels).Result);

            });

            ExtractWordModels extractWordModels = GetExtractWordModels(listExtractWordModels);

            return extractWordModels;
        }


        private ExtractWordModels GetExtractWordModels(List<ExtractWordModels> listExtractWordModels)
        {
            ExtractWordModels extractWordModels = new ExtractWordModels();

            foreach (var positionModel in listExtractWordModels)
            {
                extractWordModels.ExtractDatas.AddRange(positionModel.ExtractDatas);
            }

            return extractWordModels;
        }
    }
}

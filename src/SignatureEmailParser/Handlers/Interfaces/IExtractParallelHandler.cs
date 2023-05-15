using SignatureEmailParser.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SignatureEmailParser.Handlers.Interfaces
{
    public interface IExtractParallelHandler
    {

        public ExtractWordModels GetParallelExtractSurnamesByNames(string globalData, ExtractWordModels firstNameModels);

        public ExtractWordModels GetParallelExtractNames(string globalData, List<FirstNameModel> firstNameModels);

        public ExtractWordModels GetParallelExtractRegions(string globalData, List<RegionModel> regionModels);

        public ExtractWordModels GetParallelExtractCities(string globalData, List<CityModel> cityModels);

        public ExtractWordModels GetParallelExtractPositions(string globalData, List<PositionModel> positionModels);

    }
}

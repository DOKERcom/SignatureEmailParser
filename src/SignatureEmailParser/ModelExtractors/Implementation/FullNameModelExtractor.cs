using SignatureEmailParser.Extractors.Implementation;
using SignatureEmailParser.Extractors.Interfaces;
using SignatureEmailParser.ModelExtractors.Interfaces;
using SignatureEmailParser.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SignatureEmailParser.ModelExtractors.Implementation
{
    public class FullNameModelExtractor : IFullNameModelExtractor
    {
        private List<FirstNameModel> _names;

        private List<string> _surnames;

        private IFullNameExtractor _fullNameExtractor;

        public FullNameModelExtractor(List<FirstNameModel> names, List<string> surnames)
        {

            if (_names == null)
                _names = names;

            if (_surnames == null)
                _surnames = surnames;

            if (_fullNameExtractor == null)
                _fullNameExtractor = new FullNameExtractor(_names.Select(n => n.Name).ToList(), _surnames);

        }



        public async Task<ExtractWordModels> ExtractNames(string data, List<string> names = null)
        {
            ExtractWordModels firstNames = await _fullNameExtractor.ExtractNames(data, names);

            return firstNames;
        }


        public async Task<ExtractWordModels> ExtractSurnamesByNames(string data, ExtractWordModels names, List<string> surnames = null)
        {
            ExtractWordModels lastNames = await _fullNameExtractor.ExtractSurnamesByNames(data, names, surnames);

            return lastNames;
        }


        public async Task<ExtractWordModels> ExtractNearestSurnames(string data, ExtractWordModels names)
        {
            ExtractWordModels lastNames = await _fullNameExtractor.ExtractNearestLastNames(data, names);

            return lastNames;
        }

        public async Task<List<FullNameModel>> GetFullNameModels(string data, ExtractWordModels names, ExtractWordModels surnames)
        {
            List<FullNameModel> fullNameModels = new List<FullNameModel>();

            foreach (var name in names.ExtractDatas)
            {
                foreach (var surname in surnames.ExtractDatas)
                {
                    if (Regex.IsMatch(data, $"\n{name.Word}\\s+{surname.Word}\\s"))
                    {
                        var model = new FullNameModel { Name = name.Word, Surname = surname.Word, Gender = GetGenderByName(_names, name.Word) };

                        if (IsUniqueModel(fullNameModels, model))
                            fullNameModels.Add(model);
                    }
                }
            }

            return fullNameModels;
        }

        private bool IsUniqueModel(List<FullNameModel> models, FullNameModel model)
        {

            if (models.Where(m => m.Name == model.Name && m.Surname == model.Surname && m.Gender == model.Gender).Count() > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private string GetGenderByName(List<FirstNameModel> _names, string name)
        {
            foreach (var _name in _names)
            {
                if (_name.Name == name)
                {
                    return _name.Gender;
                }
            }
            return null;
        }

    }
}

using SignatureEmailParser.Extractors.Interfaces;
using SignatureEmailParser.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SignatureEmailParser.Extractors.Implementation
{
    public class FullNameExtractor : Extractor, IFullNameExtractor
    {
        static List<string> _names;

        static List<string> _surnames;

        public FullNameExtractor(
            List<string> names,
            List<string> surnames)
        {

            if (_names == null)
                _names = names;

            if (_surnames == null)
                _surnames = surnames;

        }

        public async Task<ExtractWordModels> ExtractNames(string data, List<string> names = null)
        {
            ExtractWordModels firstNames = await ExtractFirstNames(data, names);

            return firstNames;
        }


        public async Task<ExtractWordModels> ExtractSurnamesByNames(string data, ExtractWordModels names, List<string> surnames = null)
        {
            ExtractWordModels lastNames = await ExtractLastNamesByFirstNames(data, names, surnames);

            return lastNames;
        }


        public async Task<ExtractWordModels> ExtractNearestSurnames(string data, ExtractWordModels names)
        {
            ExtractWordModels lastNames = await ExtractNearestLastNames(data, names);

            return lastNames;
        }


        private async Task<ExtractWordModels> ExtractLastNamesByFirstNames(string data, ExtractWordModels names, List<string> surnames = null)
        {
            ExtractWordModels lastNames = new ExtractWordModels();

            if (!string.IsNullOrEmpty(data))
            {
                if (names.ExtractDatas != null && names.ExtractDatas.Count > 0)
                {

                    List<Regex> regexes = new List<Regex>();

                    if (surnames == null)
                        surnames = _surnames;

                    foreach (var name in names.ExtractDatas)
                    {
                        foreach (var surname in surnames)
                        {
                            regexes.Add(new Regex($"{name.Word}\\s+{surname}\\s"));
                        }
                    }

                    lastNames = await ExtractDataByWords(data, regexes);
                }

            }

            return lastNames;
        }


        public async Task<ExtractWordModels> ExtractNearestLastNames(string data, ExtractWordModels names)
        {
            ExtractWordModels lastNames = new ExtractWordModels();

            if (!string.IsNullOrEmpty(data))
            {
                List<Regex> regexes = new List<Regex>();

                foreach (var name in names.ExtractDatas)
                {
                    regexes.Add(new Regex(name.Word + "\\s([A-Z]\\w+)\\s"));
                }

                lastNames = ClearBadDatas(await ExtractDataByWords(data, regexes));
            }

            return lastNames;
        }


        private async Task<ExtractWordModels> ExtractFirstNames(string data, List<string> names = null)
        {
            ExtractWordModels firstNames = new ExtractWordModels();

            if (!string.IsNullOrEmpty(data))
            {
                List<Regex> regexes = new List<Regex>();

                if (names == null)
                    names = _names;

                foreach (var name in names)
                {
                    regexes.Add(new Regex("[\\s]" + name + "[\\s+]"));
                }

                firstNames = ClearBadDatas(await ExtractDataByWords(data, regexes));
            }

            return firstNames;
        }

        private ExtractWordModels ClearBadDatas(ExtractWordModels datas)
        {
            ExtractWordModels newDatas = new ExtractWordModels();

            foreach (var data in datas.ExtractDatas)
            {
                if (data.WordLength > 3 && newDatas.ExtractDatas.Where(e => e.FirstCharPos == data.FirstCharPos).FirstOrDefault() == null)
                {
                    newDatas.ExtractDatas.Add(data);
                }
            }

            newDatas.OutUpdateData = datas.OutUpdateData;

            return newDatas;
        }

    }
}

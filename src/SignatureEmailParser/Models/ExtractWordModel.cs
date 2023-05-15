using System;
using System.Collections.Generic;
using System.Text;

namespace SignatureEmailParser.Models
{
    public class ExtractWordModel
    {
        public string Word { get; set; }

        public int FirstCharPos { get; set; }

        public int LastCharPos { get; set; }

        public int WordLength { get; set; }

        public ExtractWordModel CreateExtractData(string word, int firstCharPos, int lastCharPos, int wordLength)
        {
            return new ExtractWordModel { Word = word, FirstCharPos = firstCharPos, LastCharPos = lastCharPos, WordLength = wordLength};
        }

        
    }
    public class ExtractWordModels
    {
        public List<ExtractWordModel> ExtractDatas = new List<ExtractWordModel>();

        public string OutUpdateData { get; set; } = null;
    }


}

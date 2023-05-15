using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace SignatureEmailParser.Services.Implementation
{
    public class EmailModifierService
    {

        public List<string> SplitEmailToBlocks(string data)
        {
            List<string> datas = new List<string>();

            string[] blocks = data.Split("From:", StringSplitOptions.None);

            foreach (var block in blocks)
            {
                if(block.Length > 100)
                    datas.Add(block);
            }

            return datas;
        }

    }
}

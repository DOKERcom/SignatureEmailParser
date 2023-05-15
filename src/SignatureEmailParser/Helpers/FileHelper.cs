using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SignatureEmailParser.Helpers
{
    public class FileHelper
    {
        public static string ReadAllFromFile(string path, string filename)
        {
            using (FileStream fstream = new FileStream(path+filename, FileMode.Open))
            {
                byte[] buffer = new byte[fstream.Length];

                fstream.Read(buffer, 0, buffer.Length);

                return Encoding.UTF8.GetString(buffer);
            }
        }
    }
}

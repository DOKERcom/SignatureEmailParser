using HtmlAgilityPack;
using Newtonsoft.Json;
using SignatureEmailParser.Helpers;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Web;

namespace OutParser
{
    public class FirstName
    {
        string Name { get; set; } 
    }

    internal class Program
    {
        private static string basepath = "JsonData/";

        static void Main(string[] args)
        {
            dynamic jsonFn = FileHelper.ReadAllFromFile(basepath, "FirstNames.json");

            dynamic jsonLn = FileHelper.ReadAllFromFile(basepath, "LastNames.json");

            var lastNames = JsonConvert.DeserializeObject(jsonLn);

            var firstNames = JsonConvert.DeserializeObject(jsonFn);
        }

        private void DownloadAndParse()
        {
            string url = "https://geneagraphie.com/surnames-all.php?tree=1";

            List<string> strings = new List<string>();

            WebClient client = new WebClient();

            var result = client.DownloadString(url);

            HtmlDocument doc = new HtmlDocument();

            doc.LoadHtml(result);

            var nodes = doc.DocumentNode.SelectNodes("//td[@class='sncol']/a");

            foreach (var node in nodes)
            {
                string value = HttpUtility.HtmlDecode(Regex.Match(node.OuterHtml, "<a.*?tree=1\">(.*?)<").Groups[1].Value);

                if (value.Length > 3)
                    strings.Add(value);
            }

            var json = Newtonsoft.Json.JsonConvert.SerializeObject(strings);
        }
    }
}

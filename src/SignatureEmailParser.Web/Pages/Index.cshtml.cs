using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SignatureEmailParser;
using SignatureEmailParser.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmailParser.Web.Pages
{
    public class IndexModel : PageModel
    {
        public void OnGet()
        {
            SignatureParser signatureParser = new SignatureParser();
            signatureParser.Parse(FileHelper.ReadAllFromFile("Emails/", "11.txt"));
        }
    }
}

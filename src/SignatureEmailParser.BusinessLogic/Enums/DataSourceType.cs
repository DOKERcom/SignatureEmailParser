using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace SignatureEmailParser.BusinessLogic.Enums
{
    public enum DataSourceType : int
    {
        [Description("LinkedIn App")]
        LinkedInApp = 0,

        [Description("SNOV.IO")]
        SnovIO = 1,

        [Description("Voila Norbert")]
        VoilaNorbert = 2,

        [Description("LinkedIn Data")]
        LinkedInDate = 3
    }
}

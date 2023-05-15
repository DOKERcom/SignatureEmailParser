using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace SignatureEmailParser.Models.Enums
{
    public enum EmailWorkOrPersonalOptionsType
    {
        [Description("None")]
        None = 0,

        [Description("Work")]
        Public = 1,

        [Description("Personal")]
        Private = 2,

        [Description("Work or personal")]
        PublicOrPrivate = 3,

        [Description("Personal email, if no work email is available")]
        PersonalIfNoWorkIsAvailable = 4,

        [Description("Work email, if no personal email is available")]
        WorkIfNoPersonalIsAvailable = 5
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace SignatureEmailParser.Models.Enums
{
    public enum PhoneOptionsType
    {
        [Description("None")]
        None = 0,

        [Description("Mobile (SMS)")]
        Mobile = 1,

        [Description("Phone")]
        Phone = 2
    }
}

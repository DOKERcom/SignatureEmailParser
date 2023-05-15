using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace SignatureEmailParser.EFCore.Enums
{
    public enum StatusType : int
    {
        [Description("NA")]
        NA = 0,

        [Description("Disabled")]
        Disabled = 1,

        [Description("OK")]
        OK = 2
    }
}

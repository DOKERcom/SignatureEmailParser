using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace SignatureEmailParser.EFCore.Enums
{
    public enum BounceResponseType : int
    {
        [Description("NA")]
        NA = 0,

        [Description("Hard Bounce")]
        HardBounce = 1
    }
}

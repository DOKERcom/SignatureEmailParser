using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace SignatureEmailParser.BusinessLogic.Enums
{
    public enum HeadCountType : int
    {
        [Description("None")]
        None = 0,

        [Description("1-10")]
        OneToTen = 1,

        [Description("11-50")]
        ElevenToFifty = 2,

        [Description("51-200")]
        FiftyOneToTwoHundred = 3,

        [Description("201-500")]
        TwoHundredAndOneToFiveHundred = 4,

        [Description("501-1000")]
        FiveHundredAndOneToOneThousand = 5,

        [Description("1001-5000")]
        OneThousandAndOneToFiveThousand = 6,

        [Description("5001-10000")]
        FiveThousandAndOneToTenThousand = 7,

        [Description("10001+")]
        TenThousandPlus = 8
    }
}

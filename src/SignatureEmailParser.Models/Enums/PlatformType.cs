using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace SignatureEmailParser.Models.Enums
{
    public enum PlatformType : int
    {
        [Description("LinkedIn")]
        LinkedIn = 0,
        [Description("Facebook")]
        Facebook = 1,
        [Description("Twitter")]
        Twitter = 2,
        [Description("Private Email")]
        PrivateEmail = 3,
        [Description("Work Email")]
        WorkEmail = 4,
        [Description("Private Phone")]
        PrivatePhone = 5,
        [Description("Work Phone")]
        WorkPhone = 6,
        [Description("None")]
        None = 7
    }
}

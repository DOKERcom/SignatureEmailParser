using System;

namespace SignatureEmailParser.Models.DailyRespondsModels
{
    public class DailyRespondsStatisticReportModel
    {
        public Guid Key { get; set; }
        public int Interested { get; set; }
        public int NotInterested { get; set; }
        public int TellMeMore { get; set; }
        public int Cost { get; set; }
        public int MoreInfo { get; set; }
        public int NotSure { get; set; }
        public int NotNow { get; set; }
        public int ConnectionCount { get; set; }
    }
}

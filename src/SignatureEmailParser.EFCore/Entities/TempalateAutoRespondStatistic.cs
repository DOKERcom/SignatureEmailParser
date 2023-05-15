using System;

namespace SignatureEmailParser.EFCore.Entities
{
    public class TempalateAutoRespondStatistic : BaseEntity
    {
        public Guid TemplateKey { get; set; }
        public int Interested { get; set; }
        public int NotInterested { get; set; }
        public int TellMeMore { get; set; }
        public int Cost { get; set; }
        public int MoreInfo { get; set; }
        public int NotSure { get; set; }
        public int NotAtTheMoment { get; set; }
        public int Total { get; set; }
    }
}

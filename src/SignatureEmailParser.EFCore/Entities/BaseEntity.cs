using System;

namespace SignatureEmailParser.EFCore.Entities
{
    public class BaseEntity
    {
        public long Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public BaseEntity()
        {
            CreatedAt = DateTime.UtcNow;
        }
    }
}

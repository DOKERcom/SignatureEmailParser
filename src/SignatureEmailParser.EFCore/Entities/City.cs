using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SignatureEmailParser.EFCore.Entities
{
    public class City : BaseEntity
    {
        [Column(TypeName = "nvarchar(200)")]
        public string Name { get; set; }

        public long? CountryId { get; set; }

        [ForeignKey("CountryId")]
        public Country Countries { get; set; }

        public long? RegionId { get; set; }

        [ForeignKey("RegionId")]
        public Regions Regions { get; set; }

    }
}

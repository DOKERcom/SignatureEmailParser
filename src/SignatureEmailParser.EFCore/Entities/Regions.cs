using System.ComponentModel.DataAnnotations.Schema;

namespace SignatureEmailParser.EFCore.Entities
{
    public class Regions : BaseEntity
    {
        [Column(TypeName = "varchar(150)")]
        public string Name { get; set; }

        public long? CountryId { get; set; }

        [ForeignKey("CountryId")]
        public Country Countries { get; set; }
    }
}

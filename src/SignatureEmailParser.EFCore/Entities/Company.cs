using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SignatureEmailParser.EFCore.Entities
{
    public class Company : BaseEntity
    {
        [Column(TypeName = "varchar(300)")]
        public string Name { get; set; }

        [Column(TypeName = "varchar(300)")]
        public string Domain { get; set; }

        [Column(TypeName = "varchar(300)")]
        public string Email { get; set; }

        [Column(TypeName = "varchar(100)")]
        public string Locality { get; set; }

        [Column(TypeName = "varchar(90)")]
        public string City { get; set; }

        // delete the above when the coountry columns has been updated
        public long? CityId { get; set; }

        [ForeignKey("CityId")]
        public City Cities { get; set; }

        [Column(TypeName = "varchar(300)")]
        public string Street { get; set; }
        public string Country { get; set; }

        // delete the above when the coountry columns has been updated
        public long? CountryId { get; set; }

        [ForeignKey("CountryId")]
        public Country Countries { get; set; }

        [Column(TypeName = "varchar(30)")]
        public string Postcode { get; set; }

        [Column(TypeName = "varchar(12)")]
        public string IPAddress { get; set; }

        [Column(TypeName = "varchar(15)")]
        public string HeadCount { get; set; }

        public int HeadCountId { get; set; }

        public decimal AnnualTurnover { get; set; }
        public DateTime WhenUpdated { get; set; }

        [Column(TypeName = "varchar(300)")]
        public string? FacebookUrl { get; set; }

        public DateTime? Founded { get; set; }

        public long? IndustryId { get; set; }

        [ForeignKey("IndustryId")]
        public Industry Industries { get; set; }

        [Column(TypeName = "varchar(250)")]
        public string? Twitter { get; set; }

        public long? LinkedInCompanyId { get; set; }

        public override bool Equals(object obj)
        {
            if (!(obj is Company company))
            {
                return false;
            }

            return company.Name == Name &&
                   company.Domain == Domain &&
                   company.Email == Email &&
                   company.Locality == Locality &&
                   company.City == City &&
                   company.Street == Street &&
                   company.Country == Country &&
                   company.Postcode == Postcode &&
                   company.IPAddress == IPAddress &&
                   company.HeadCount == HeadCount &&
                   company.AnnualTurnover == AnnualTurnover &&
                    company.FacebookUrl == FacebookUrl &&
                    company.Founded == Founded &&
                    company.IndustryId == IndustryId &&
                    company.Twitter == Twitter;
        }
    }
}

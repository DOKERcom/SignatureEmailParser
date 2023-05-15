using SignatureEmailParser.EFCore.Enums;
using System;
using SignatureEmailParser.Models.Enums;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace SignatureEmailParser.EFCore.Entities
{
    public class SocialMediaMapping : BaseEntity
    {
        [Column(TypeName = "varchar(20)")]
        public string Title { get; set; }

        [Column(TypeName = "varchar(70)")]
        public string Name { get; set; }

        [Column(TypeName = "varchar(100)")]
        public string Surname { get; set; }

        [Column(TypeName = "varchar(100)")]
        public string Domain { get; set; }

        [Column(TypeName = "varchar(250)")]
        public string Email { get; set; }

        [Column(TypeName = "nvarchar(600)")]
        public string LinkedIn { get; set; }

        [Column(TypeName = "varchar(250)")]
        public string Twitter { get; set; }
        public long? IndustryId { get; set; }

        [ForeignKey("IndustryId")]
        public Industry Industries { get; set; }

        [Column(TypeName = "varchar(300)")]
        public string PastCompany { get; set; }
        public string School { get; set; }
        public string ProfileLanguage { get; set; }
        public string ServiceCategories { get; set; }

        [Column(TypeName = "varchar(200)")]
        public string Locality { get; set; }

        public long? CityId { get; set; }

        [ForeignKey("CityId")]
        public City Cities { get; set; }

        [Column(TypeName = "varchar(300)")]
        public string Street { get; set; }

        // above will need to be deleted
        public long? CountryId { get; set; }

        [ForeignKey("CountryId")]
        public Country Countries { get; set; }

        public long? RegionId { get; set; }

        [ForeignKey("RegionId")]
        public Regions Regions { get; set; }

        [Column(TypeName = "varchar(60)")]
        public string Postcode { get; set; }

        [Column(TypeName = "varchar(15)")]
        public string IPAddress { get; set; }

        [Column(TypeName = "varchar(350)")]
        public string Position { get; set; }

        [Column(TypeName = "varchar(10)")]
        public string Gender { get; set; }

        [Column(TypeName = "varchar(12)")]
        public string BirthDate { get; set; }

        public string Experience { get; set; }

        [Column(TypeName = "varchar(150)")]
        public string WorkEmail { get; set; }

        [Column(TypeName = "varchar(100)")]
        public string FacebookId { get; set; }

        public string FacebookUsername { get; set; }

        [Column(TypeName = "varchar(20)")]
        public string InferredSalary { get; set; }

        public short? InferredYearsExperience { get; set; }

        [Column(TypeName = "varchar(20)")]
        public string LocationGeo { get; set; }

        [Column(TypeName = "varchar(4000)")]
        public string Language { get; set; }

        [Column(TypeName = "varchar(130)")]
        public string Phone { get; set; }

        [Column(TypeName = "varchar(20)")]
        public string Mobile { get; set; }
        public string Summary { get; set; }
        public string Skills { get; set; }
        public int LinkedInConnections { get; set; }

        public DataSourceType DataSourceType { get; set; }
        public DateTime WhenUpdated { get; set; }

        public long CompanyId { get; set; }
        public virtual Company Company { get; set; }

        public int BirthYear { get; set; }

        public StatusType StatusType { get; set; }

        public BounceResponseType BounceResponseType { get; set; }

        [Column(TypeName = "varchar(2000)")]
        public string ProfileImage { get; set; }

        [Column(TypeName = "varchar(100)")]
        public string ProfileImageMapping { get; set; }

        public long? CurrentCompanyId { get; set; }

        [ForeignKey("CurrentCompanyId")]
        public virtual Company CurrentCompanies { get; set; }

        public long? PastCompanyId { get; set; }

        [ForeignKey("PastCompanyId")]
        public virtual Company PastCompanies { get; set; }

        public long? PositionId { get; set; }

        [ForeignKey("PositionId")]
        public Position Positions { get; set; }

        public int? EmailStatus { get; set; }

        public int? WorkEmailStatus { get; set; }

        public StatusType? LinkedInStatus { get; set; }

        public StatusType? TwitterStatus { get; set; }

        public StatusType? FacebookStatus { get; set; }

        public int? MobileStatus { get; set; }

        public int? PhoneStatus { get; set; }

        public string LastPost { get; set; }

        public DateTime? LastPostDate { get; set; }

        public string SharedConnections { get; set; }
    }
}
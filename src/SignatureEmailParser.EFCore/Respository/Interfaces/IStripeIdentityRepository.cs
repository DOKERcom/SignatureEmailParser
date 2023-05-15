using SignatureEmailParser.EFCore.Entities;
using System.Threading.Tasks;

namespace SignatureEmailParser.EFCore.Respository.Interfaces
{
    public interface IStripeIdentityRepository : IBaseRepository<StripeIdentity>
    {
        public Task<StripeIdentity> GetByLicensingId(string licenseId);
        public Task<StripeIdentity> CreateOrUpdate(StripeIdentity stripeIdentity);
    }
}

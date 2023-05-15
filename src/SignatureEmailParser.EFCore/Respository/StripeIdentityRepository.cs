using SignatureEmailParser.EFCore.Entities;
using SignatureEmailParser.EFCore.Respository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Threading.Tasks;

namespace SignatureEmailParser.EFCore.Respository
{
    public class StripeIdentityRepository : BaseRepository<StripeIdentity>, IStripeIdentityRepository
    {
        public StripeIdentityRepository(ApplicationContext context) : base(context)
        {
        }

        public async Task<StripeIdentity> GetByLicensingId(string licenseId)
        {
            StripeIdentity stripeIdentity = await _context.StripeIdentities.FirstOrDefaultAsync(stripeIdentity => stripeIdentity.LicenseId == licenseId);

            return stripeIdentity;
        }

        public async Task<StripeIdentity> CreateOrUpdate(StripeIdentity stripeIdentity)
        {
            StripeIdentity entity = await _context.StripeIdentities.FirstOrDefaultAsync(entity => entity.Id == stripeIdentity.Id || entity.LicenseId == stripeIdentity.LicenseId);

            if (entity is null)
            {
                EntityEntry createEntityEntry = await _context.AddAsync(stripeIdentity);

                await _context.SaveChangesAsync();

                return createEntityEntry.Entity as StripeIdentity;
            }

            entity.Email = stripeIdentity.Email;
            entity.LicenseId = stripeIdentity.LicenseId;
            entity.StripeStatusType = stripeIdentity.StripeStatusType;

            EntityEntry updateEntityEntry = _context.Update(entity);

            await _context.SaveChangesAsync();

            return updateEntityEntry.Entity as StripeIdentity;
        }
    }
}

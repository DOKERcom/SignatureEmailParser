using System;
using SignatureEmailParser.EFCore.Entities;
using SignatureEmailParser.EFCore.Respository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using SignatureEmailParser.Models.Enums;
using SignatureEmailParser.Models.SocialMediaMappingModels;

namespace SignatureEmailParser.EFCore.Respository
{
    public class SocialMediaMappingRepository : BaseRepository<SocialMediaMapping>, ISocialMediaMappingRepository
    {
        public SocialMediaMappingRepository(ApplicationContext context) : base(context)
        {
        }

        public async Task CreateOrUpdate(SocialMediaMapping socialMediaMapping)
        {
            SocialMediaMapping entity = await _dbSet.FirstOrDefaultAsync(entity => entity.LinkedIn == socialMediaMapping.LinkedIn);

            if (entity is null)
            {
                await _dbSet.AddAsync(socialMediaMapping);
            }

            if (!(entity is null))
            {
                _context.Entry(socialMediaMapping).State = EntityState.Modified;
            }

            await _context.SaveChangesAsync();
        }

        public async Task<List<string>> GetInferredSalaryFiltersAsync()
        {
            List<string> inferredSalary = await _context.SocialMediaMappings
                .Where(profile => profile.InferredSalary != null)
                .Select(profile => profile.InferredSalary)
                .Distinct()
                .ToListAsync();

            return inferredSalary;
        }

        public async Task<List<SocialMediaMapping>> GetByEmails(string[] emails)
        {
            List<SocialMediaMapping> result = await _context
                    .SocialMediaMappings
                    .Include(x => x.Company)
                    .Where(x => emails.Contains(x.Email))
                    .ToListAsync();

            return result;
        }

        public async Task<SocialMediaMapping> GetByAnyIdentifiableIds(ProfileFromAppModel profileFromAppModel)
        {
            SocialMediaMapping result = null;

            try
            {
                result = await _context
                        .SocialMediaMappings
                        .Include(x => x.Company)
                        .Include(x => x.Industries)
                        .Include(x => x.Countries)
                        .Include(x => x.Cities)
                        .Where(x => x.LinkedIn == profileFromAppModel.ProfileUrl)
                        .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
            }

            if (result != null)
                return result;

            else if (!string.IsNullOrEmpty(profileFromAppModel.Twitter))
            {
                result = await _context
                    .SocialMediaMappings
                    .Include(x => x.Company)
                    .Include(x => x.Industries)
                    .Include(x => x.Countries)
                    .Include(x => x.Cities)
                    .Where(x => x.Twitter == profileFromAppModel.Twitter)
                    .FirstOrDefaultAsync();

                if (result != null)
                    return result;
            }
            else if (!string.IsNullOrEmpty(profileFromAppModel.Facebook))
            {
                result = await _context
                .SocialMediaMappings
                .Include(x => x.Company)
                .Include(x => x.Industries)
                .Include(x => x.Countries)
                .Include(x => x.Cities)
                .Where(x => x.FacebookId == profileFromAppModel.Facebook)
                .FirstOrDefaultAsync();

                if (result != null)
                    return result;
            }

            return result;
        }

        public async Task<List<SocialMediaMapping>> GetByLinkedInIds(string[] ids)
        {
            List<SocialMediaMapping> result = await _context
                    .SocialMediaMappings
                    .Include(x => x.Company)
                    .Include(x => x.Industries)
                    .Include(x => x.Countries)
                    .Include(x => x.Cities)
                    .Where(x => ids.Contains(x.LinkedIn))
                    .ToListAsync();

            return result;
        }


        public async Task<List<SocialMediaMapping>> GetByTwitterIds(string[] ids)
        {
            List<SocialMediaMapping> result = await _context
                    .SocialMediaMappings
                    .Include(x => x.Company)
                    .Include(x => x.Industries)
                    .Include(x => x.Countries)
                    .Include(x => x.Cities)
                    .Where(x => ids.Contains(x.Twitter))
                    .ToListAsync();

            return result;
        }

        public async Task<List<SocialMediaMapping>> GetByFacebookIds(string[] ids)
        {
            List<SocialMediaMapping> result = await _context
                    .SocialMediaMappings
                    .Include(x => x.Company)
                    .Include(x => x.Industries)
                    .Include(x => x.Countries)
                    .Include(x => x.Cities)
                    .Where(x => ids.Contains(x.FacebookId))
                    .ToListAsync();

            return result;
        }

        public async Task<List<SocialMediaMapping>> GetByLinkedInUrl(string[] url)
        {
            List<SocialMediaMapping> result = await _context
                    .SocialMediaMappings
                    .Include(x => x.Company)
                    .Include(x => x.Industries)
                    .Include(x => x.Countries)
                    .Include(x => x.Cities)
                    .Where(x => url.Contains(x.LinkedIn))
                    .ToListAsync();

            return result;
        }

        public async Task<List<SocialMediaMapping>> GetByMobile(string[] mobileNum)
        {
            List<SocialMediaMapping> result = await _context
                    .SocialMediaMappings
                    .Include(x => x.Company)
                    .Include(x => x.Industries)
                    .Include(x => x.Countries)
                    .Include(x => x.Cities)
                    .Where(x => mobileNum.Contains(x.Mobile))
                    .ToListAsync();

            return result;
        }

        public async Task<List<SocialMediaMapping>> ExecuteQuery(string query)
        {
            List<SocialMediaMapping> result = await _context
                .SocialMediaMappings
                .FromSqlRaw(query)
                .ToListAsync();

            return result;
        }

        public async Task<List<SocialMediaMapping>> GetByPrivateEmails(string[] emails)
        {
            List<SocialMediaMapping> result = await _context
                    .SocialMediaMappings
                    .Include(x => x.Company)
                    .Include(x => x.Industries)
                    .Include(x => x.Countries)
                    .Include(x => x.Cities)
                    .Where(x => emails.Contains(x.Email))
                    .ToListAsync();

            return result;
        }

        public async Task<List<SocialMediaMapping>> GetByPrivatePhone(string[] mobileNum)
        {
            List<SocialMediaMapping> result = await _context
                    .SocialMediaMappings
                    .Include(x => x.Company)
                    .Include(x => x.Industries)
                    .Include(x => x.Countries)
                    .Include(x => x.Cities)
                    .Where(x => mobileNum.Contains(x.Mobile))
                    .ToListAsync();

            return result;
        }
    }
}  

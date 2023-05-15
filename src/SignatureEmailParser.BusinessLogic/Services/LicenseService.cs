using SignatureEmailParser.BusinessLogic.Services.Interfaces;
using SignatureEmailParser.EFCore.Entities;
using SignatureEmailParser.EFCore.Respository.Interfaces;
using SignatureEmailParser.Models.ProfileModels;
using Newtonsoft.Json;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignatureEmailParser.BusinessLogic.Services
{
    public class LicenseService : ILicenseService
    {
        private readonly SubscriptionService _subscriptionService;
        private readonly IStripeIdentityRepository _stripeIdentityRepository;

        public LicenseService(SubscriptionService subscriptionService, IStripeIdentityRepository stripeIdentityRepository)
        {
            _subscriptionService = subscriptionService;
            _stripeIdentityRepository = stripeIdentityRepository;
        }

        public async Task UpdateProfilesLicenseAsync(string requestBody)
        {
            UpdateProfileListModel profileListModel = JsonConvert.DeserializeObject<UpdateProfileListModel>(requestBody);

            if (profileListModel is null || !profileListModel.ProfileLicenseModels.Any())
            {
                return;
            }

            StripeList<Subscription> subscriptions = await _subscriptionService.ListAsync();

            List<StripeIdentity> stripeIdentities = profileListModel.ProfileLicenseModels
                .Where
                (
                    profile => subscriptions.Data.Select(subscription => subscription.Id).Contains(profile.LicenseId)
                )
                .Select(profile => new StripeIdentity 
                { 
                    LicenseId = profile.LicenseId,
                    Email = profile.Email,
                    StripeStatusType = profile.StripeStatusType
                })
                .ToList();

            List<StripeIdentity> allIdentities = await _stripeIdentityRepository.GetAll();

            List<StripeIdentity> existingIdentities = allIdentities
                .Where
                (
                    stripeIdentity => stripeIdentities.Select(stripeIdentity => stripeIdentity.LicenseId).Contains(stripeIdentity.LicenseId)
                )
                .ToList();

            await _stripeIdentityRepository.UpdateRange(existingIdentities);

            List<StripeIdentity> nonExistingIdentities = stripeIdentities
                .Where
                (
                    identity => !allIdentities.Select(identity => identity.LicenseId).Contains(identity.LicenseId)
                )
                .ToList();

            await _stripeIdentityRepository.CreateRange(nonExistingIdentities);
        }
    }
}

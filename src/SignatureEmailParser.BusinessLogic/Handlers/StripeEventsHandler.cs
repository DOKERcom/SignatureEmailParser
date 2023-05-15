using SignatureEmailParser.BusinessLogic.Handlers.Interfaces;
using SignatureEmailParser.EFCore.Entities;
using SignatureEmailParser.EFCore.Enums;
using SignatureEmailParser.EFCore.Respository.Interfaces;
using Newtonsoft.Json;
using Stripe;
using System;
using System.Threading.Tasks;

namespace SignatureEmailParser.BusinessLogic.Handlers
{
    public class StripeEventsHandler : IStripeEventsHandler
    {
        private CustomerService _customerService;
        private IStripeIdentityRepository _stripeIdentityRepository;

        public StripeEventsHandler(CustomerService customerService, IStripeIdentityRepository stripeIdentityRepository)
        {
            _customerService = customerService;
            _stripeIdentityRepository = stripeIdentityRepository;
        }

        public async Task HandleAsync(string requestBody)
        {
            Event stripeEvent = JsonConvert.DeserializeObject<Event>(requestBody);

            switch (stripeEvent.Type)
            {
                case Events.CustomerSubscriptionCreated:
                    if (!(stripeEvent.Data.Object is Subscription createdSubscription))
                    {
                        return;
                    }

                    await OnSubscriptionCreatedAsync(createdSubscription);

                    break;

                case Events.CustomerSubscriptionDeleted:
                    if (!(stripeEvent.Data.Object is Subscription deletedSubscription))
                    {
                        return;
                    }

                    await OnSubscriptionDeletedAsync(deletedSubscription);

                    break;
            }
        }

        private async Task OnSubscriptionCreatedAsync(Subscription subscription)
        {
            try
            {
                Customer customer = await _customerService.GetAsync(subscription.CustomerId);

                StripeIdentity stripeIdentity = new StripeIdentity
                {
                    StripeStatusType = subscription.Status == SubscriptionStatuses.Active ? StripeStatusType.OK : StripeStatusType.Expired,
                    Email = customer.Email,
                    LicenseId = subscription.Id,
                    CreatedAt = DateTime.UtcNow
                };

                await _stripeIdentityRepository.CreateOrUpdate(stripeIdentity);
            }
            catch (Exception)
            {
                return;
            }
        }

        private async Task OnSubscriptionDeletedAsync(Subscription subscription)
        {
            StripeIdentity stripeIdentity = await _stripeIdentityRepository.GetByLicensingId(subscription.Id);

            if (stripeIdentity is null)
            {
                return;
            }

            stripeIdentity.StripeStatusType = subscription.Status == SubscriptionStatuses.Active ? StripeStatusType.OK : StripeStatusType.Expired;

            await _stripeIdentityRepository.Update(stripeIdentity);
        }
    }
}

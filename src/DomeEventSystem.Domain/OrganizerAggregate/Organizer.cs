using DomeEventSystem.Domain.Common;
using DomeEventSystem.Domain.SubscriptionAggregate;
using ErrorOr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomeEventSystem.Domain.OrganizerAggregate
{
   
    public class Organizer : AggregateRoot
    {
        public Guid UserId { get; }
        public Guid? SubscriptionId { get; private set; }

        public Organizer(
            Guid userId,
            Guid? subscriptionId = null,
            Guid? id = null)
            : base(id ?? Guid.NewGuid())
        {
            UserId = userId;
            SubscriptionId = subscriptionId;
        }

        public ErrorOr<Success> SetSubscription(Subscription subscription)
        {
            if (SubscriptionId.HasValue)
            {
                return Error.Conflict(description: "Organizer already has an active subscription");
            }

            SubscriptionId = subscription.Id;

            return Result.Success;
        }

        private Organizer() { }
    }
}

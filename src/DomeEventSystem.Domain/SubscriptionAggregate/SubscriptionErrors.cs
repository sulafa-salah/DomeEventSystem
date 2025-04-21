using ErrorOr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomeEventSystem.Domain.SubscriptionAggregate
{
    public static class SubscriptionErrors
    {
        public static readonly Error CannotHaveMoreVenuesThanSubscriptionAllows = Error.Validation(
            "Subscription.CannotHaveMoreVenuesThanSubscriptionAllows",
            "A subscription cannot have more Venues than the subscription allows");
    }
}

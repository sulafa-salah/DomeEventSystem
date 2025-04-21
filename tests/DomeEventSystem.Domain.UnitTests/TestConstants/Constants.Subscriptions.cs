using DomeEventSystem.Domain.SubscriptionAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomeEventSystem.Domain.UnitTests.TestConstants
{
    public static partial class Constants
    {
        public static class Subscriptions
        {
            public static readonly SubscriptionType DefaultSubscriptionType = SubscriptionType.Free;
            public static readonly Guid Id = Guid.NewGuid();
            public const int MaxDailyEventsFreeTier = 4;
            public const int MaxHallsFreeTier = 1;
            public const int MaxVenuesFreeTier = 1;
        }
    }
}
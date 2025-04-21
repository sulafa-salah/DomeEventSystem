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
        public static class Hall
        {
            public static readonly Guid Id = Guid.NewGuid();
            public const int MaxDailyEvents = Subscriptions.MaxDailyEventsFreeTier;
            public const string Name = "Hall 1";
        }
    }
}
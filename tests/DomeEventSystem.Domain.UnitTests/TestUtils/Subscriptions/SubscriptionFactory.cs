using DomeEventSystem.Domain.SubscriptionAggregate;
using DomeEventSystem.Domain.UnitTests.TestConstants;

namespace DomeEventSystem.Domain.UnitTests.TestUtils.Subscriptions
{
    public static class SubscriptionFactory
    {
        public static Subscription CreateSubscription(
      SubscriptionType? subscriptionType = null,
      Guid? organizerId = null,
      Guid? id = null)
    {
        return new Subscription(
            subscriptionType: subscriptionType ?? Constants.Subscriptions.DefaultSubscriptionType,
            organizerId ?? Constants.Organizer.Id,
            id ?? Constants.Subscriptions.Id);
    }
}
}

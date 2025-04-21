using DomeEventSystem.Domain.SubscriptionAggregate;
using DomeEventSystem.Domain.UnitTests.TestUtils.Subscriptions;
using DomeEventSystem.Domain.UnitTests.TestUtils.Venues;
using FluentAssertions;
using OneOf.Types;


namespace DomeEventSystem.Domain.UnitTests.SubscriptionAggregate
{
    public class SubscriptionTests
    {
        [Fact]
        public void AddVenue_WhenMoreThanSubscriptionAllows_ShouldFail()
        {
            // Arrange
            // Create a subscription — it has a rule like "max N venues allowed"
            var subscription = SubscriptionFactory.CreateSubscription();

            // Generate (maxVenues + 1) venues to simulate exceeding the limit
            // For example, if max is 3, this will create 4 venues
            var venues = Enumerable.Range(0, subscription.GetMaxVenues() + 1)
                .Select(_ => VenueFactory.CreateVenue(id: Guid.NewGuid()))
                .ToList();
             
            // Act
            // Try to add all the venues to the subscription
            // This returns a list of results, one per attempt
            var addVenueResults = venues.ConvertAll(subscription.AddVenue);

            // Assert
            // Take all the results except the last one — they should all be successful
            var allButLastAddVenueResults = addVenueResults.Take(..^1);
            allButLastAddVenueResults.Should().AllSatisfy(result => result.IsError.Should().BeFalse()); // All should succeed (within the allowed limit)

            // The last venue exceeds the limit, so it should fail

            var lastAddGymResult = addVenueResults.Last();
            lastAddGymResult.IsError.Should().BeTrue(); // This one should return an error

            // The specific error should match the expected business rule violation
            lastAddGymResult.FirstError.Should().Be(SubscriptionErrors.CannotHaveMoreVenuesThanSubscriptionAllows);
        }
    }
}

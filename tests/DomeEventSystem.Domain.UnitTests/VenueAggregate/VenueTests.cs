using DomeEventSystem.Domain.UnitTests.TestUtils.Halls;
using DomeEventSystem.Domain.UnitTests.TestUtils.Venues;
using DomeEventSystem.Domain.VenueAggregate;
using FluentAssertions;


namespace DomeEventSystem.Domain.UnitTests.VenueAggregate
{
    public class VenueTests
    {
        [Fact]
        public void AddHall_WhenMoreThanSubscriptionAllows_ShouldFail()
        {
            // Arrange
            // Create a venue that only allows 1 hall (based on the organizer's subscription)
            var venue = VenueFactory.CreateVenue(maxHalls: 1);
            // Create two hall instances to try adding to the venue
            var hall1 = HallFactory.CreateHall(id: Guid.NewGuid());
            var hall2 = HallFactory.CreateHall(id: Guid.NewGuid());

            // Act
            // Try to add the first hall — this should succeed
            var addHall1Result = venue.AddHall(hall1);
            // Try to add the second hall — this should fail because it exceeds the max allowed
            var addHall2Result = venue.AddHall(hall2);

            // Assert
            // The first hall should be added successfully, so IsError should be false
            addHall1Result.IsError.Should().BeFalse();
            // The second hall should NOT be added, so IsError should be true
            addHall2Result.IsError.Should().BeTrue();
            addHall2Result.FirstError.Should().Be(VenueErrors.CannotHaveMoreHallsThanSubscriptionAllows);
        }
    }

}

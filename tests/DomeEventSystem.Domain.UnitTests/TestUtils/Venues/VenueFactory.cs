using DomeEventSystem.Domain.VenueAggregate;
using DomeEventSystem.Domain.UnitTests.TestConstants;


namespace DomeEventSystem.Domain.UnitTests.TestUtils.Venues
{
    public static class VenueFactory
    {
        public static Venue CreateVenue(
            string name = Constants.Venue.Name,
            int maxHalls = Constants.Subscriptions.MaxHallsFreeTier,
            Guid? id = null)
        {
            return new Venue(
                name,
                maxHalls,
                subscriptionId: Constants.Subscriptions.Id,
                id: id ?? Constants.Venue.Id);
        }
    }
}

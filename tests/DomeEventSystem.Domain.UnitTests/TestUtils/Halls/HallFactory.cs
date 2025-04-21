using DomeEventSystem.Domain.HallAggregate;
using DomeEventSystem.Domain.UnitTests.TestConstants;

namespace DomeEventSystem.Domain.UnitTests.TestUtils.Halls
{
    public static class HallFactory
    {
        public static Hall CreateHall(
        

            string name = Constants.Hall.Name,
         int maxDailyEvents = Constants.Hall.MaxDailyEvents,
         Guid? venueId = null,
         Guid? id = null)
            {
                return new Hall(
                    name: name,
                    maxDailyEvents: maxDailyEvents,
                    venueId: venueId ?? Constants.Venue.Id,
                    id: id ?? Constants.Hall.Id);
            }
        }
    }

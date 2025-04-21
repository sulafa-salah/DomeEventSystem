using DomeEventSystem.Domain.AttendeeAggregate;
using DomeEventSystem.Domain.UnitTests.TestConstants;

namespace DomeEventSystem.Domain.UnitTests.TestUtils.Attendees
{
  
    public static class AttendeeFactory
    {
        public static Attendee CreateAttendee(Guid? id = null, Guid? userId = null)
        {
            return new Attendee(
                userId: userId ?? Constants.User.Id,
                id: id ?? Constants.Attendee.Id);
        }
    }
}

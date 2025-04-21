using DomeEventSystem.Domain.AttendeeAggregate;
using DomeEventSystem.Domain.EventAggregate;
using DomeEventSystem.Domain.UnitTests.TestUtils.Attendees;
using DomeEventSystem.Domain.UnitTests.TestUtils.Events;
using FluentAssertions;
using DomeEventSystem.Domain.UnitTests.TestConstants;
using DomeEventSystem.Domain.UnitTests.TestUtils.Services;


namespace DomeEventSystem.Domain.UnitTests.EventAggregate
{

    public class EventTests
    {
        [Fact]
        public void ReserveSpot_WhenNoMoreHall_ShouldFailReservation()
        {
            // Arrange
            // Create an event that only allows 1 attendee  (simulates limited capacity)
            var eventCreation = EventFactory.CreateEvent(maxAttendees: 1);
            // Create two unique attendees trying to reserve spots in the same event
            var attendee1 = AttendeeFactory.CreateAttendee(id: Guid.NewGuid(), userId: Guid.NewGuid());
            var attendee2 = AttendeeFactory.CreateAttendee(id: Guid.NewGuid(), userId: Guid.NewGuid());

            // Act
            // First reservation — should succeed since the event allows 1 attendee
            var reserveAttendee1Result = eventCreation.ReserveSpot(attendee1);
            // Second reservation — should fail since the capacity has already been reached
            var reserveAttendee2Result = eventCreation.ReserveSpot(attendee2);

            // Assert
            // First reservation should succeed → no error
            reserveAttendee1Result.IsError.Should().BeFalse();

            // Second reservation should fail → error expected
            reserveAttendee2Result.IsError.Should().BeTrue();
            // The error returned should match the expected business rule
            reserveAttendee2Result.FirstError.Should().Be(EventErrors.CannotHaveMoreReservationsThanAttendees);
        }

        [Fact]
        public void CancelReservation_WhenCancellationIsTooCloseToEvent_ShouldFailCancellation()
        {
            // Arrange
            var event1 = EventFactory.CreateEvent(
                date: Constants.Event.Date,
                time: Constants.Event.Time);

            var attendee = AttendeeFactory.CreateAttendee();

            var cancellationDateTime = Constants.Event.Date.ToDateTime(TimeOnly.MinValue);

            // Act
            var reserveSpotResult = event1.ReserveSpot(attendee);
            var cancelReservationResult = event1.CancelReservation(
                attendee,
                new TestDateTimeProvider(fixedDateTime: cancellationDateTime));

            // Assert
            reserveSpotResult.IsError.Should().BeFalse();

            cancelReservationResult.IsError.Should().BeTrue();
            cancelReservationResult.FirstError.Should().Be(EventErrors.CannotCancelReservationTooCloseToEvent);
        }


    }
}


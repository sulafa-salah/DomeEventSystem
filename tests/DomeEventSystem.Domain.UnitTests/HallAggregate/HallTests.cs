using DomeEventSystem.Domain.UnitTests.TestUtils.Events;
using DomeEventSystem.Domain.UnitTests.TestUtils.Halls;
using FluentAssertions;
using DomeEventSystem.Domain.HallAggregate;
using DomeEventSystem.Domain.UnitTests.TestUtils.Common;

namespace DomeEventSystem.Domain.UnitTests.HallAggregate
{
  
    public class HallTests
    {
        [Fact]
        //A hall can't have more events than its subscription allows per day
        public void ScheduleEvent_WhenMoreThanSubscriptionAllows_ShouldFail()
        {
            // Arrange
            // Create a hall that only allows 1 event per day (subscription rule)
            var hall = HallFactory.CreateHall(
                maxDailyEvents: 1);
            // Create two events on the same day (trying to exceed the daily limit)
            var firstDailyEvent = EventFactory.CreateEvent(
                date: Constants.Event.Date,
                id: Guid.NewGuid());

            var secondDailyEvent = EventFactory.CreateEvent(
                date: Constants.Event.Date,
                id: Guid.NewGuid());

            // Create a third event on a different day — this should be allowed

            var eventOnAnotherDay = EventFactory.CreateEvent(
                date: Constants.Event.Date.AddDays(1),
                id: Guid.NewGuid());

            // Act
            // Schedule the first event — should succeed
            var scheduleFirstEventResult = hall.ScheduleEvent(firstDailyEvent);
            // Schedule the second event on the same day — should fail (exceeds daily limit)
            var scheduleSecondEventResult = hall.ScheduleEvent(secondDailyEvent);
            // Schedule an event on another day — should succeed (limit applies per day)
            var scheduleEventOnAnotherDayResult = hall.ScheduleEvent(eventOnAnotherDay);

            // Assert
            scheduleFirstEventResult.IsError.Should().BeFalse(); // first one OK
            scheduleEventOnAnotherDayResult.IsError.Should().BeFalse(); // another day OK

            scheduleSecondEventResult.IsError.Should().BeTrue(); // same day exceeds limit
            scheduleSecondEventResult.FirstError.Should().Be(HallErrors.CannotHaveMoreEventThanSubscriptionAllows);
        }

        [Theory]
        [InlineData(1, 3, 1, 3)] // exact overlap
        [InlineData(1, 3, 2, 3)] // second event inside first event
        [InlineData(1, 3, 2, 4)] // second event ends after event, but overlaps
        [InlineData(1, 3, 0, 2)] // second event starts before second event, but overlaps
        //A hall cannot host overlapping events, even if limit isn't reached
        public void ScheduleEvent_WhenEventOverlapsWithAnotherEvent_ShouldFail(
            int startHourEvent1,
            int endHourEvent1,
            int startHourEvent2,
            int endHourEvent2)
        {
            // Arrange
            // Create a hall that allows 2 events per day — enough for testing overlap
            var hall = HallFactory.CreateHall(
                maxDailyEvents: 2);
            // Create event1 with given start and end times
            var event1 = EventFactory.CreateEvent(
                date: Constants.Event.Date,
                time: TimeRangeFactory.CreateFromHours(startHourEvent1, endHourEvent1),
                id: Guid.NewGuid());

            // Create event2 with a potentially overlapping time
            var event2 = EventFactory.CreateEvent(
                date: Constants.Event.Date,
                time: TimeRangeFactory.CreateFromHours(startHourEvent2, endHourEvent2),
                id: Guid.NewGuid());

            // Act
            // Schedule event1 — should succeed
            var scheduleEvent1Result = hall.ScheduleEvent(event1);
            // Try to schedule event2 — should fail if there's an overlap
            var scheduleEvent2Result = hall.ScheduleEvent(event2);

            // Assert
            scheduleEvent1Result.IsError.Should().BeFalse(); // event1 OK

            scheduleEvent2Result.IsError.Should().BeTrue(); // event2 overlaps, should fail
            scheduleEvent2Result.FirstError.Should().Be(HallErrors.CannotHaveTwoOrMoreOverlappingEvents);
        }
    }
}

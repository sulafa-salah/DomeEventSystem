using DomeEventSystem.Domain.Common.Entities;
using DomeEventSystem.Domain.Common;
using ErrorOr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;
using DomeEventSystem.Domain.EventAggregate;

namespace DomeEventSystem.Domain.HallAggregate
{
    public class Hall : AggregateRoot
    {
        private readonly Dictionary<DateOnly, List<Guid>> _eventIdsByDate = new();
        private readonly int _maxDailyEvents;
        private readonly Schedule _schedule = Schedule.Empty();

        public string Name { get; } = null!;

        public Guid VenueId { get; }

        public IReadOnlyList<Guid> EventsIds => _eventIdsByDate.Values
            .SelectMany(eventsIds => eventsIds)
            .ToList()
            .AsReadOnly();

        public Hall(
            string name,
            int maxDailyEvents,
            Guid venueId,
            Schedule? schedule = null,
            Guid? id = null) : base(id ?? Guid.NewGuid())
        {
            Name = name;
            _maxDailyEvents = maxDailyEvents;
            VenueId = venueId;
            _schedule = schedule ?? Schedule.Empty();
        }

        private Hall() { }
        public ErrorOr<Success> ScheduleEvent(Event event1)
        {
            if (EventsIds.Any(id => id == event1.Id))
            {
                return Error.Conflict(description: "Event already exists in hall");
            }

            if (!_eventIdsByDate.ContainsKey(event1.Date))
            {
                _eventIdsByDate[event1.Date] = new();
            }

            var dailyEvents = _eventIdsByDate[event1.Date];

            if (dailyEvents.Count >= _maxDailyEvents)
            {
                return HallErrors.CannotHaveMoreEventThanSubscriptionAllows;
            }

            var addEventResult = _schedule.BookTimeSlot(event1.Date, event1.Time);

            if (addEventResult.IsError && addEventResult.FirstError.Type == ErrorType.Conflict)
            {
                return HallErrors.CannotHaveTwoOrMoreOverlappingEvents;
            }

            dailyEvents.Add(event1.Id);

            return Result.Success;
        }

        public bool HasEvent(Guid eventId)
        {
            return EventsIds.Contains(eventId);
        }


    }

}

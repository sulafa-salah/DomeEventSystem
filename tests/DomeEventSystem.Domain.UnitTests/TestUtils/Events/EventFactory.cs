using DomeEventSystem.Domain.Common.ValueObjects;
using DomeEventSystem.Domain.EventAggregate;
using DomeEventSystem.Domain.UnitTests.TestConstants;

namespace DomeEventSystem.Domain.UnitTests.TestUtils.Events
{
        public static class EventFactory
        {
            public static Event CreateEvent(
                string name = Constants.Event.Name,
                string description = Constants.Event.Description,
                Guid? hallId = null,
                Guid? speakerId = null,
                int maxAttendees = Constants.Event.MaxAttendees,
                DateOnly? date = null,
                TimeRange? time = null,
                List<EventCategory>? categories = null,
                Guid? id = null)
            {
                return new Event(
                    name: name,
                    description: description,
                    maxAttendees: maxAttendees,
                    hallId: hallId ?? Constants.Hall.Id,
                    speakerId: speakerId ?? Constants.Speaker.Id,
                    date: date ?? Constants.Event.Date,
                    time: time ?? Constants.Event.Time,
                    categories: categories ?? Constants.Event.Categories,
                    id: id ?? Constants.Event.Id);
            }
        }
    }

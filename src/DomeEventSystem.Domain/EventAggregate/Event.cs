using DomeEventSystem.Domain.AttendeeAggregate;
using DomeEventSystem.Domain.Common;
using DomeEventSystem.Domain.Common.ValueObjects;
using ErrorOr;


namespace DomeEventSystem.Domain.EventAggregate
{
   
    public class Event : AggregateRoot
    {
        private readonly List<Reservation> _reservations = new();
        private readonly List<EventCategory> _categories = new();

        public int NumAttendees => _reservations.Count;

        public DateOnly Date { get; }

        public TimeRange Time { get; } = null!;

        public string Name { get; } = null!;

        public string Description { get; } = null!;

        public int MaxAttendees { get; }

        public Guid HallId { get; }

        public IReadOnlyList<EventCategory> Categories => _categories;

        public Guid SpeakerId { get; }

        public Event(
            string name,
            string description,
            int maxAttendees,
            Guid hallId,
            Guid speakerId,
            DateOnly date,
            TimeRange time,
            List<EventCategory> categories,
            Guid? id = null)
                : base(id ?? Guid.NewGuid())
        {
            Name = name;
            Description = description;
            MaxAttendees = maxAttendees;
            HallId = hallId;
            SpeakerId = speakerId;
            Date = date;
            Time = time;
            _categories = categories;
        }

       
        private Event() { }

        public ErrorOr<Success> ReserveSpot(Attendee attendee)
        {
            if (_reservations.Count >= MaxAttendees)
            {
                return EventErrors.CannotHaveMoreReservationsThanAttendees;
            }

            if (_reservations.Any(reservation => reservation.AttendeeId == attendee.Id))
            {
                return Error.Conflict(description: "Attendees cannot reserve twice to the same event");
            }

            var reservation = new Reservation(attendee.Id);
            _reservations.Add(reservation);

            return Result.Success;
        }
    }
}

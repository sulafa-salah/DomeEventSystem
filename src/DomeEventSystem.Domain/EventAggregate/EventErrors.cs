using ErrorOr;


namespace DomeEventSystem.Domain.EventAggregate
{
 
    public static class EventErrors
    {

        public static readonly Error CannotCancelPastEvent = Error.Validation(
     "Event.CannotCancelPastEvent",
     "An attendee cannot cancel a reservation for a event that has completed");

        public readonly static Error CannotHaveMoreReservationsThanAttendees = Error.Validation(
            code: "Event.CannotHaveMoreReservationsThanAttendees",
            description: "Cannot have more reservations than attendees");

        public readonly static Error CannotCancelReservationTooCloseToEvent = Error.Validation(
       code: "Event.CannotCancelReservationTooCloseToEvent",
       description: "Cannot cancel reservation too close to event start time");


    }
}

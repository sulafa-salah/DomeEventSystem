using ErrorOr;

namespace DomeEventSystem.Domain.HallAggregate
{
    public static class HallErrors
    {
        public static readonly Error CannotHaveMoreEventThanSubscriptionAllows = Error.Validation(
            "Hall.CannotHaveMoreEventThanSubscriptionAllows",
            "A hall cannot have more scheduled events than the subscription allows");

        public static readonly Error CannotHaveTwoOrMoreOverlappingEvents = Error.Validation(
            "Hall.CannotHaveTwoOrMoreOverlappingEvents",
            "A hall cannot have two or more overlapping events");
    }
}

using DomeEventSystem.Domain.Common;
using DomeEventSystem.Domain.VenueAggregate;
using ErrorOr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomeEventSystem.Domain.SubscriptionAggregate
{
    public class Subscription : AggregateRoot
    {
        private readonly List<Guid> _venueIds = new();
        private readonly int _maxVenues;
        private readonly Guid _organizerId;

        public SubscriptionType SubscriptionType { get; } = default!;

        public Subscription(
            SubscriptionType subscriptionType,
            Guid organizerId,
            Guid? id = null)
                : base(id ?? Guid.NewGuid())
        {
            SubscriptionType = subscriptionType;
            _maxVenues = GetMaxVenues();
            _organizerId = organizerId;
        }

        public int GetMaxVenues() => SubscriptionType.Name switch
        {
            nameof(SubscriptionType.Free) => 1,
            nameof(SubscriptionType.Starter) => 1,
            nameof(SubscriptionType.Pro) => 3,
            _ => throw new InvalidOperationException()
        };

        public int GetMaxHalls() => SubscriptionType.Name switch
        {
            nameof(SubscriptionType.Free) => 1,
            nameof(SubscriptionType.Starter) => 3,
            nameof(SubscriptionType.Pro) => int.MaxValue,
            _ => throw new InvalidOperationException()
        };

        public int GetMaxDailyEvents() => SubscriptionType.Name switch
        {
            nameof(SubscriptionType.Free) => 4,
            nameof(SubscriptionType.Starter) => int.MaxValue,
            nameof(SubscriptionType.Pro) => int.MaxValue,
            _ => throw new InvalidOperationException()
        };

        public ErrorOr<Success> AddVenue(Venue venue)
        {
            if (_venueIds.Contains(venue.Id))
            {
                return Error.Conflict(description: "Venue already exists");
            }

            if (_venueIds.Count >= _maxVenues)
            {
                return SubscriptionErrors.CannotHaveMoreVenuesThanSubscriptionAllows;
            }

            _venueIds.Add(venue.Id);

            return Result.Success;
        }

        public bool HasVenue(Guid venueId)
        {
            return _venueIds.Contains(venueId);
        }

        private Subscription() { }
    }
}
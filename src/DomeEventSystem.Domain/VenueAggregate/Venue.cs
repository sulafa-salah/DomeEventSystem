using DomeEventSystem.Domain.Common;
using DomeEventSystem.Domain.HallAggregate;
using DomeEventSystem.Domain.SpeakerAggregate;
using ErrorOr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomeEventSystem.Domain.VenueAggregate
{
    
    public class Venue : AggregateRoot
    {
        private readonly int _maxHalls;
        private readonly List<Guid> _hallIds = new();
        private readonly List<Guid> _speakerIds = new();

        public string Name { get; } = null!;

        public IReadOnlyList<Guid> HallIds => _hallIds;

        public Guid SubscriptionId { get; }

        public Venue(
            string name,
            int maxHalls,
            Guid subscriptionId,
            Guid? id = null)
                : base(id ?? Guid.NewGuid())
        {
            Name = name;
            _maxHalls = maxHalls;
            SubscriptionId = subscriptionId;
        }

        public ErrorOr<Success> AddHall(Hall hall)
        {
            if (_hallIds.Contains(hall.Id))
            {
                return Error.Conflict(description: "Hall already exists in venue");
            }

            if (_hallIds.Count >= _maxHalls)
            {
                return VenueErrors.CannotHaveMoreHallsThanSubscriptionAllows;
            }

            _hallIds.Add(hall.Id);

            return Result.Success;
        }
        public bool HasHall(Guid hallId)
        {
            return _hallIds.Contains(hallId);
        }

        public ErrorOr<Success> AddSpeaker(Speaker speaker)
        {
            if (_speakerIds.Contains(speaker.Id))
            {
                return Error.Conflict(description: "Speaker already assigned to venue");
            }

            _speakerIds.Add(speaker.Id);

            return Result.Success;
        }

        public bool HasSpeaker(Guid speakerId)
        {
            return _speakerIds.Contains(speakerId);
        }

        public ErrorOr<Success> RemoveHall(Hall hall)
        {
            if (!_hallIds.Contains(hall.Id))
            {
                return Error.NotFound(description: "Hall not found");
            }

            _hallIds.Remove(hall.Id);

            return Result.Success;
        }

        private Venue() { }
    }

}

using DomeEventSystem.Domain.Common;
using DomeEventSystem.Domain.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomeEventSystem.Domain.AttendeeAggregate
{

    public class Attendee : AggregateRoot
    {
        private readonly Schedule _schedule = Schedule.Empty();
        private readonly List<Guid> _eventIds = new();

        public Guid UserId { get; }

        public IReadOnlyList<Guid> EventIds => _eventIds;

        public Attendee(
            Guid userId,
            Schedule? schedule = null,
            Guid? id = null) : base(id ?? Guid.NewGuid())
        {
            UserId = userId;
            _schedule = schedule ?? Schedule.Empty();
        }

    }
}

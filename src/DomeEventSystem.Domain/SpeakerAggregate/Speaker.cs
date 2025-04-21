using DomeEventSystem.Domain.Common.Entities;
using DomeEventSystem.Domain.Common.ValueObjects;
using DomeEventSystem.Domain.Common;
using ErrorOr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace DomeEventSystem.Domain.SpeakerAggregate
{
   
    public class Speaker : AggregateRoot
    {
        private readonly List<Guid> _eventIds = new();
        private readonly Schedule _schedule = Schedule.Empty();

        public Guid UserId { get; }

        public Speaker(
            Guid userId,
            Schedule? schedule = null,
        Guid? id = null)
                : base(id ?? Guid.NewGuid())
        {
            UserId = userId;
            _schedule = schedule ?? Schedule.Empty();
        }

        private Speaker() { }
    }
}

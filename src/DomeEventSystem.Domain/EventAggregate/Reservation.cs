using DomeEventSystem.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomeEventSystem.Domain.EventAggregate
{
    public class Reservation : Entity
    {
        public Guid AttendeeId { get; }

        public Reservation(Guid attendeeId, Guid? id = null)
            : base(id ?? Guid.NewGuid())
        {
            AttendeeId = attendeeId;
        }

        private Reservation() { }
    }
}

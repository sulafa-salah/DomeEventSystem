using DomeEventSystem.Domain.Common.ValueObjects;
using ErrorOr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomeEventSystem.Domain.Common.Entities
{
    public class Schedule : Entity
    {
        private readonly Dictionary<DateOnly, List<TimeRange>> _calendar = new();

        public Schedule(
            Dictionary<DateOnly, List<TimeRange>>? calendar = null,
            Guid? id = null)
                : base(id ?? Guid.NewGuid())
        {
            _calendar = calendar ?? new();
        }

        public static Schedule Empty()
        {
            return new Schedule(id: Guid.NewGuid());
        }

 
        private Schedule() { }
    }
}
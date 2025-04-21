using DomeEventSystem.Domain.Common.ValueObjects;
using DomeEventSystem.Domain.EventAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomeEventSystem.Domain.UnitTests.TestConstants
{
    public static partial class Constants
    {
        public static class Event
        {
            public static readonly Guid Id = Guid.NewGuid();
            public static readonly DateOnly Date = DateOnly.FromDateTime(DateTime.UtcNow);
            public static readonly TimeRange Time = new(
                TimeOnly.MinValue.AddHours(8),
                TimeOnly.MinValue.AddHours(9));

            public static readonly List<EventCategory> Categories = new();

            public const int MaxAttendees = 10;
            public const string Name = "Developer Event";
            public const string Description = "The best Developer event";
        }
    }
}
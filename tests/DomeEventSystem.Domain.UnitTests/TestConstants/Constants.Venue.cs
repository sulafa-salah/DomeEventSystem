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
        public static class Venue
        {
            public static readonly Guid Id = Guid.NewGuid();
            public const string Name = "DomeVenue";
        }
    }
}
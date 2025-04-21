using DomeEventSystem.Domain.Common.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Throw;

namespace DomeEventSystem.Domain.UnitTests.TestUtils.Common
{

    public static class TimeRangeFactory
    {
        public static TimeRange CreateFromHours(int startHour, int endHour)
        {
            startHour.Throw()
                .IfGreaterThanOrEqualTo(endHour)
                .IfNegative()
                .IfGreaterThan(23);

            endHour.Throw()
                .IfLessThan(1)
                .IfGreaterThan(24);

            return new TimeRange(
                start: TimeOnly.MinValue.AddHours(startHour),
                end: TimeOnly.MinValue.AddHours(endHour));
        }
    }
}

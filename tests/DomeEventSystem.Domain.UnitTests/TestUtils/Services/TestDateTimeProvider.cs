using DomeEventSystem.Domain.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomeEventSystem.Domain.UnitTests.TestUtils.Services
{
    public class TestDateTimeProvider : IDateTimeProvider
    {
        private readonly DateTime? _fixedDateTime;

        public TestDateTimeProvider(DateTime? fixedDateTime = null)
        {
            _fixedDateTime = fixedDateTime;
        }

        public DateTime UtcNow => _fixedDateTime ?? DateTime.UtcNow;
    }
}

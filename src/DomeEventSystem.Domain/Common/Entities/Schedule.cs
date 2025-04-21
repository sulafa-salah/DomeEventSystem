﻿using DomeEventSystem.Domain.Common.ValueObjects;
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

        internal bool CanBookTimeSlot(DateOnly date, TimeRange time)
        {
            if (!_calendar.TryGetValue(date, out var timeSlots))
            {
                return true;
            }

            return !timeSlots.Any(timeSlot => timeSlot.OverlapsWith(time));
        }

        internal ErrorOr<Success> BookTimeSlot(DateOnly date, TimeRange time)
        {
            if (!_calendar.TryGetValue(date, out var timeSlots))
            {
                _calendar[date] = new() { time };
                return Result.Success;
            }

            if (timeSlots.Any(timeSlot => timeSlot.OverlapsWith(time)))
            {
                return Error.Conflict();
            }

            timeSlots.Add(time);
            return Result.Success;
        }

        internal ErrorOr<Success> RemoveBooking(DateOnly date, TimeRange time)
        {
            if (!_calendar.TryGetValue(date, out var timeSlots) || !timeSlots.Contains(time))
            {
                return Error.NotFound(description: "Booking not found");
            }

            if (!timeSlots.Remove(time))
            {
                return Error.Unexpected();
            }

            return Result.Success;
        }
        private Schedule() { }
    }
}
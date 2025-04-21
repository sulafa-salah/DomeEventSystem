using Ardalis.SmartEnum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomeEventSystem.Domain.EventAggregate
{
    public class EventCategory : SmartEnum<EventCategory>
    {
        public static readonly EventCategory Technology = new(nameof(Technology), 0);
        public static readonly EventCategory Enginner = new(nameof(Enginner), 1);
        public static readonly EventCategory MBA = new(nameof(MBA), 2);
     

        public EventCategory(string name, int value) : base(name, value)
        {
        }
    }
}

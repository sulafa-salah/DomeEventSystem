using Ardalis.SmartEnum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomeEventSystem.Domain.Profiles
{
    public sealed class ProfileType : SmartEnum<ProfileType>
    {
        public static readonly ProfileType Organizer = new(nameof(Organizer), 0);
        public static readonly ProfileType Speaker = new(nameof(Speaker), 1);
        public static readonly ProfileType Attendee = new(nameof(Attendee), 2);

        private ProfileType(string name, int id) : base(name, id) { }
    }
}
using ErrorOr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomeEventSystem.Domain.VenueAggregate
{
    public static class VenueErrors
    {
        public static readonly Error CannotHaveMoreHallsThanSubscriptionAllows = Error.Validation(
       "Hall.CannotHaveMoreHallsThanSubscriptionAllows",
       "A Venue cannot have more halls than the subscription allows");
    }
}

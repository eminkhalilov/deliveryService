using Farfetch.DeliveryService.Models.Points;
using Farfetch.DeliveryService.Models.Routes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Farfetch.DeliveryService.Services
{
    public interface IDeliveryServiceProvider
    {
        IEnumerable<CalculatedRoute> CalculateAllRoutesBySourceAndDestination(long sourcePointId,
            long destinationPointId,
            IEnumerable<Route> routes,
            IEnumerable<Point> points);
    }
}

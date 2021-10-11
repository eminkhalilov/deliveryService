using Farfetch.DeliveryService.Models.Routes;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Farfetch.DeliveryService.Services
{
    public interface IRouteProvider
    {
        Task CreateRouteAsync(Route route);
        Task UpdateRouteAsync(Route route);
        Task DeleteRouteAsync(long Id);
        Task<bool> IsRouteExistsAsync(long sourceRouteId, long destinationRouteId);
        Task<bool> IsRouteExistsAsync(long id);
        Task<IEnumerable<Route>> GetRoutesAsync();
        Task<IEnumerable<CalculatedRoute>> GetRoutesBySourceAndDestinationAsync(long sourcepointid, long destinationpointid);
        Task<CalculatedRoute> GetRouteBySmallestTimeAsync(long sourcepointid, long destinationpointid);
        Task<CalculatedRoute> GetRouteByLowestCostAsync(long sourcepointid, long destinationpointid);
    }
}

using Farfetch.DeliveryService.Models.Routes;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Farfetch.DeliveryService.Repositories
{
    public interface IRouteRepository
    {
        Task CreateRouteAsync(Route route);
        Task UpdateRouteAsync(Route route);
        Task DeleteRouteAsync(long Id);
        Task<bool> IsRouteExistsAsync(long sourceRouteId, long destinationRouteId);
        Task<bool> IsRouteExistsAsync(long id);
        Task<IEnumerable<Route>> GetRoutesAsync();
    }
}

using Farfetch.DeliveryService.Models.Points;
using Farfetch.DeliveryService.Models.Routes;
using Farfetch.DeliveryService.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Farfetch.DeliveryService.Services
{
    public class RouteProvider : IRouteProvider
    {
        private readonly IRouteRepository _routeRepository;
        private readonly IPointProvider _pointProvider;
        private readonly IDeliveryServiceProvider _deliveryServiceProvider;
        public RouteProvider(IRouteRepository routeRepository, 
            IPointProvider pointProvider, 
            IDeliveryServiceProvider deliveryServiceProvider)
        {
            _routeRepository = routeRepository;
            _pointProvider = pointProvider;
            _deliveryServiceProvider = deliveryServiceProvider;
        }

        public async Task CreateRouteAsync(Route route)
        {
            await _routeRepository.CreateRouteAsync(route);
        }

        public async Task UpdateRouteAsync(Route route)
        {
            await _routeRepository.UpdateRouteAsync(route);
        }

        public async Task DeleteRouteAsync(long Id)
        {
            await _routeRepository.DeleteRouteAsync(Id);
        }

        public async Task<bool> IsRouteExistsAsync(long sourceRouteId, long destinationRouteId)
        {
            var isExists = await _routeRepository.IsRouteExistsAsync(sourceRouteId, destinationRouteId);

            return isExists;
        }

        public async Task<bool> IsRouteExistsAsync(long id)
        {
            var isExists = await _routeRepository.IsRouteExistsAsync(id);

            return isExists;
        }

        public async Task<IEnumerable<Route>> GetRoutesAsync()
        {
            var result = await _routeRepository.GetRoutesAsync();

            return result;
        }

        public async Task<IEnumerable<CalculatedRoute>> GetRoutesBySourceAndDestinationAsync(long sourcepointid, long destinationpointid)
        {
            var routes = await GetRoutesAsync();
            var points = await _pointProvider.GetPointsAsync();

            var result = _deliveryServiceProvider.CalculateAllRoutesBySourceAndDestination(sourcepointid,
                destinationpointid, routes, points);

            return result;
        }

        public async Task<CalculatedRoute> GetRouteBySmallestTimeAsync(long sourcepointid, long destinationpointid)
        {
            var routes = await GetRoutesAsync();
            var points = await _pointProvider.GetPointsAsync();

            var result = _deliveryServiceProvider.CalculateAllRoutesBySourceAndDestination(sourcepointid,
                destinationpointid, routes, points);

            var routeBySmallestTime = result.ToList().OrderBy(l => l.TotalTime).FirstOrDefault();

            return routeBySmallestTime;
        }

        public async Task<CalculatedRoute> GetRouteByLowestCostAsync(long sourcepointid, long destinationpointid)
        {
            var routes = await GetRoutesAsync();
            var points = await _pointProvider.GetPointsAsync();

            var result = _deliveryServiceProvider.CalculateAllRoutesBySourceAndDestination(sourcepointid,
                destinationpointid, routes, points);

            var routeByLowestCost = result.ToList().OrderBy(l => l.TotalCost).FirstOrDefault();

            return routeByLowestCost;
        }
    }
}

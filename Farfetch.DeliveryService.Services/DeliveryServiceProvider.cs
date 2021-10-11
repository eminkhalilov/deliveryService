using Farfetch.DeliveryService.Models.Points;
using Farfetch.DeliveryService.Models.Routes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Farfetch.DeliveryService.Services
{
    public class DeliveryServiceProvider : IDeliveryServiceProvider
    {
        private const int _quantity = 10;
        private List<long>[] _adjList;
        private List<CalculatedRoute> _calculatedRoutes;
        private List<Point> _points;
        private List<Route> _routes;

        public DeliveryServiceProvider()
        {
            _calculatedRoutes = new List<CalculatedRoute>();
            _points = new List<Point>();
            _adjList = new List<long>[_quantity];

            for (int i = 0; i < _quantity; i++)
            {
                _adjList[i] = new List<long>();
            }
        }

        public IEnumerable<CalculatedRoute> CalculateAllRoutesBySourceAndDestination(long sourcePointId,
            long destinationPointId,
            IEnumerable<Route> routes,
            IEnumerable<Point> points)
        {
            routes.ToList().ForEach(r =>
            {
                CreatePairs(r.SourcePointId, r.DestinationPointId);
            });

            long source = sourcePointId;
            long destination = destinationPointId;

            var result  = CalculateRoutesBySourceAndDest(source, destination, points, routes);

            return result;
        }

        private void CreatePairs(long u, long v)
        {
            _adjList[u].Add(v);
        }

        private List<CalculatedRoute> CalculateRoutesBySourceAndDest(long s, long d, IEnumerable<Point> points, IEnumerable<Route> routes)
        {
            var isVisited = new bool[_quantity];
            var pointIdList = new List<long>();
            _points = points.ToList();
            _routes = routes.ToList();

            pointIdList.Add(s);

            CalculateRoute(s, d, isVisited, pointIdList);

            return _calculatedRoutes;
        }

        private void CalculateRoute(long u, long d, bool[] isVisited, List<long> pointIds)
        {
            isVisited[u] = true;

            if (u.Equals(d))
            {
                if (pointIds.Count > 2)
                    _calculatedRoutes.Add(new CalculatedRoute
                    {
                        RouteSchema = string.Join("=>", pointIds),
                        PointRoutes = ToPoints(pointIds, _points),
                        TotalCost = CalculateTotalTimeAndCost(pointIds, _routes).Item2,
                        TotalTime = CalculateTotalTimeAndCost(pointIds, _routes).Item1
                    });

                isVisited[u] = false;
                return;
            }

            foreach (int i in _adjList[u])
            {
                if (!isVisited[i])
                {
                    pointIds.Add(i);

                    CalculateRoute(i, d, isVisited, pointIds);

                    pointIds.Remove(i);
                }
            }
            isVisited[u] = false;
        }


        private Point[] ToPoints(List<long> pointIds, List<Point> points)
        {
            var generatedPoints = new List<Point>();

            foreach (var id in pointIds)
            {
                generatedPoints.Add(new Point
                {
                    Id = id,
                    Name = points.Where(p => p.Id == id).Select(p => p.Name).FirstOrDefault()
                });
            }

            return generatedPoints.ToArray();
        }

        private Tuple<decimal, decimal> CalculateTotalTimeAndCost(List<long> pointIds, List<Route> routes)
        {
            decimal totalTime = 0;
            decimal totalCost = 0;

            var arraySize = pointIds.Count;

            for (int i = 0; i < arraySize - 1; i++)
            {
                long s = pointIds[i];
                long d = pointIds[i + 1];

                totalTime += routes.FirstOrDefault(r => r.SourcePointId == s && r.DestinationPointId == d).Time;
                totalCost += routes.FirstOrDefault(r => r.SourcePointId == s && r.DestinationPointId == d).Cost;
            }

            return new Tuple<decimal, decimal>(totalTime, totalCost);
        }
    }
}

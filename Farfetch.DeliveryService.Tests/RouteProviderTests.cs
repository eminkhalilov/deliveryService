using Farfetch.DeliveryService.Models.Points;
using Farfetch.DeliveryService.Models.Routes;
using Farfetch.DeliveryService.Repositories;
using Farfetch.DeliveryService.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using NSubstitute;
using Xunit;

namespace Farfetch.DeliveryService.Tests
{
    public class RouteProviderTests
    {
        private readonly IRouteRepository _routeRepository;
        private readonly IPointProvider _pointProvider;
        public RouteProviderTests()
        {
            _routeRepository = Substitute.For<IRouteRepository>();
            _pointProvider = Substitute.For<IPointProvider>();
        }

        [Fact]
        public async Task Given_SourceAndDestinationPointId_When_Calculating_Routes_Then_ShowLowestCostRoute()
        {
            //arrange
            var points = new List<Point>{
                new Point
                {
                    Id = 1,
                    Name = "A"
                },
                  new Point
                {
                    Id = 2,
                    Name = "B"
                },
                    new Point
                {
                    Id = 3,
                    Name = "C"
                }
                    ,
                    new Point
                {
                    Id = 4,
                    Name = "D"
                }
                    ,
                    new Point
                {
                    Id = 5,
                    Name = "E"
                },
                    new Point
                {
                    Id = 6,
                    Name = "F"
                }
                    ,
                    new Point
                {
                    Id = 7,
                    Name = "G"
                }
                    ,
                    new Point
                {
                    Id = 8,
                    Name = "H"
                }
            };

            var routes = new List<Route>
            {
                new Route
                {
                    Id =1,
                    Cost = 20,
                    SourcePointId = 1,
                    DestinationPointId = 3,
                    Time = 1
                },
                new Route
                {
                    Id =1,
                    Cost = 12,
                    SourcePointId = 3,
                    DestinationPointId = 2,
                    Time = 1
                },
                new Route
                {
                    Id =1,
                    Cost =5,
                    SourcePointId = 1,
                    DestinationPointId = 5,
                    Time = 30
                },
                new Route
                {
                    Id =1,
                    Cost = 1,
                    SourcePointId = 4,
                    DestinationPointId = 5,
                    Time = 30
                },
                new Route
                {
                    Id =1,
                    Cost = 2,
                    SourcePointId = 4,
                    DestinationPointId = 6,
                    Time = 4
                },
                new Route
                {
                    Id =1,
                    Cost = 3,
                    SourcePointId = 3,
                    DestinationPointId = 7,
                    Time = 2
                },
                new Route
                {
                    Id =1,
                    Cost = 2,
                    SourcePointId = 1,
                    DestinationPointId = 4,
                    Time = 12
                },
                new Route
                {
                    Id =1,
                    Cost = 2,
                    SourcePointId = 6,
                    DestinationPointId = 2,
                    Time = 12
                }
            };

            var sourceId = 1;
            var destId = 2;
            var provider = new RouteProvider(_routeRepository, _pointProvider, new DeliveryServiceProvider());

            _routeRepository.GetRoutesAsync().ReturnsForAnyArgs(routes);
            _pointProvider.GetPointsAsync().ReturnsForAnyArgs(points);

            //act
            var result = await provider.GetRouteByLowestCostAsync(sourceId, destId);

            //assert

            Assert.Equal(6, result.TotalCost);
        }

        [Fact]
        public async Task Given_SourceAndDestinationPointId_When_Calculating_Routes_Then_ShowSmallestTimeRoute()
        {
            //arrange
            var points = new List<Point>{
                new Point
                {
                    Id = 1,
                    Name = "A"
                },
                  new Point
                {
                    Id = 2,
                    Name = "B"
                },
                    new Point
                {
                    Id = 3,
                    Name = "C"
                }
                    ,
                    new Point
                {
                    Id = 4,
                    Name = "D"
                }
                    ,
                    new Point
                {
                    Id = 5,
                    Name = "E"
                },
                    new Point
                {
                    Id = 6,
                    Name = "F"
                }
                    ,
                    new Point
                {
                    Id = 7,
                    Name = "G"
                }
                    ,
                    new Point
                {
                    Id = 8,
                    Name = "H"
                }
            };

            var routes = new List<Route>
            {
                new Route
                {
                    Id =1,
                    Cost = 20,
                    SourcePointId = 1,
                    DestinationPointId = 3,
                    Time = 1
                },
                new Route
                {
                    Id =1,
                    Cost = 12,
                    SourcePointId = 3,
                    DestinationPointId = 2,
                    Time = 1
                },
                new Route
                {
                    Id =1,
                    Cost =5,
                    SourcePointId = 1,
                    DestinationPointId = 5,
                    Time = 30
                },
                new Route
                {
                    Id =1,
                    Cost = 1,
                    SourcePointId = 4,
                    DestinationPointId = 5,
                    Time = 30
                },
                new Route
                {
                    Id =1,
                    Cost = 2,
                    SourcePointId = 4,
                    DestinationPointId = 6,
                    Time = 4
                },
                new Route
                {
                    Id =1,
                    Cost = 3,
                    SourcePointId = 3,
                    DestinationPointId = 7,
                    Time = 2
                },
                new Route
                {
                    Id =1,
                    Cost = 2,
                    SourcePointId = 1,
                    DestinationPointId = 4,
                    Time = 12
                },
                new Route
                {
                    Id =1,
                    Cost = 2,
                    SourcePointId = 6,
                    DestinationPointId = 2,
                    Time = 12
                }
            };

            var sourceId = 1;
            var destId = 2;

            var provider = new RouteProvider(_routeRepository, _pointProvider, new DeliveryServiceProvider());

            _routeRepository.GetRoutesAsync().ReturnsForAnyArgs(routes);
            _pointProvider.GetPointsAsync().ReturnsForAnyArgs(points);

            //act
            var result = await provider.GetRouteBySmallestTimeAsync(sourceId, destId);

            //assert

            Assert.Equal(2, result.TotalTime);
        }
    }
}

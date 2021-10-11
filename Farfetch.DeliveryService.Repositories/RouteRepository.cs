using Dapper;
using Farfetch.DeliveryService.Models;
using Farfetch.DeliveryService.Models.Routes;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Farfetch.DeliveryService.Repositories
{
    public class RouteRepository : IRouteRepository
    {
        private readonly IFarfetchConfiguration _farfetchConfiguration;
        public RouteRepository(IFarfetchConfiguration farfetchConfiguration)
        {
            _farfetchConfiguration = farfetchConfiguration;
        }

        public async Task CreateRouteAsync(Route route)
        {
            using (var db = new SqlConnection(_farfetchConfiguration.ConnectionString))
            {
                await db.OpenAsync();
                var queryString = @"Insert into Routes (SourcePointId, DestinationPointId,Time,Cost) values
					 (@SourcePointId, @DestinationPointId,@Time,@Cost)";

                await db.ExecuteAsync(queryString, new
                {
                    SourcePointId = route.SourcePointId,
                    DestinationPointId = route.DestinationPointId,
                    Time = route.Time,
                    Cost = route.Cost
                });
            }
        }

        public async Task DeleteRouteAsync(long Id)
        {
            using (var db = new SqlConnection(_farfetchConfiguration.ConnectionString))
            {
                await db.OpenAsync();
                var queryString = @"Update Routes set IsDelete = @Isdelete where Id = @Id";

                await db.ExecuteAsync(queryString, new { IsDelete = 1, Id = Id });
            }
        }

        public async Task<IEnumerable<Route>> GetRoutesAsync()
        {
            using (var db = new SqlConnection(_farfetchConfiguration.ConnectionString))
            {
                await db.OpenAsync();

                var queryString = @"select r.id, p.Name as SourcePointName, r.SourcePointId, 
	   po.Name as DestinationPointName, r.DestinationPointId, 
    r.Cost, r.Time from Routes r inner join Points p on r.SourcePointId = p.Id and p.IsDelete = @IsDelete and r.IsDelete = @IsDelete
inner join Points po  on r.DestinationPointId = po.Id and po.IsDelete = @IsDelete and r.IsDelete = @IsDelete";

                var result = await db.QueryAsync<NameRoute>(queryString, new { IsDelete = 0 });

                return result;
            }
        }

        public async Task<bool> IsRouteExistsAsync(long sourceRouteId, long destinationRouteId)
        {
            using (var db = new SqlConnection(_farfetchConfiguration.ConnectionString))
            {
                await db.OpenAsync();

                var queryString = @"SELECT count(*)
                                        FROM [Farfetch].[dbo].[Routes] where IsDelete = @IsDelete and SourcePointId = @SourcePointId
                                         and DestinationPointId = @DestinationPointId";
                var result = await db.QueryFirstAsync<int>(queryString, new { IsDelete = 0, SourcePointId  = sourceRouteId, DestinationPointId  = destinationRouteId});

                return result > 0;
            }
        }

        public async Task<bool> IsRouteExistsAsync(long id)
        {
            using (var db = new SqlConnection(_farfetchConfiguration.ConnectionString))
            {
                await db.OpenAsync();

                var queryString = @"SELECT count(*)
                                        FROM [Farfetch].[dbo].[Routes] where IsDelete = @IsDelete and Id = @Id";
                var result = await db.QueryFirstAsync<int>(queryString, new { IsDelete = 0, Id = id });

                return result > 0;
            }
        }

        public async Task UpdateRouteAsync(Route route)
        {
            using (var db = new SqlConnection(_farfetchConfiguration.ConnectionString))
            {
                await db.OpenAsync();
                var queryString = @"Update Routes set SourcePointId = @SourcePointId,
					DestinationPointId = @DestinationPointId,
					Time = @Time,
					Cost = @Cost where Id = @Id";

                await db.ExecuteAsync(queryString, new { SourcePointId = route.SourcePointId,
                    DestinationPointId = route.DestinationPointId,
                    Time = route.Time,
                    Cost = route.Cost,
                    Id = route.Id});
            }
        }
    }
}

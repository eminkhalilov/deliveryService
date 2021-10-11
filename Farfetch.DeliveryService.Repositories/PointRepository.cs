using Dapper;
using Farfetch.DeliveryService.Models;
using Farfetch.DeliveryService.Models.Points;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace Farfetch.DeliveryService.Repositories
{
    public class PointRepository : IPointRepository
    {
        private readonly IFarfetchConfiguration _farfetchConfiguration;
        public PointRepository(IFarfetchConfiguration farfetchConfiguration)
        {
            _farfetchConfiguration = farfetchConfiguration;
        }

        public async Task UpdatePointAsync(string name, long Id)
        {
            using (var db = new SqlConnection(_farfetchConfiguration.ConnectionString))
            {
                await db.OpenAsync();
                var queryString = @"Update Points set Name = @Name where Id = @Id";

                await db.ExecuteAsync(queryString, new { Name = name, Id = Id });
            }
        }

        public async Task CreatePointAsync(string name)
        {
            using (var db = new SqlConnection(_farfetchConfiguration.ConnectionString))
            {
                await db.OpenAsync();
                var queryString = @"INSERT INTO Points (Name) values (@Name);";

                await db.ExecuteAsync(queryString, new { Name = name});
            }
        }

        public async Task DeletePointAsync(long Id)
        {
            using (var db = new SqlConnection(_farfetchConfiguration.ConnectionString))
            {
                await db.OpenAsync();
                var queryString = @"Update Points set IsDelete = @Isdelete where Id = @Id";

                await db.ExecuteAsync(queryString, new { IsDelete = 1, Id =  Id});
            }
        }

        public async Task<IEnumerable<Point>> GetPointsAsync()
        {
            using (var db = new SqlConnection(_farfetchConfiguration.ConnectionString))
            {
                await db.OpenAsync();

                var queryString = @"SELECT [Id],[Name]
                                        FROM [Farfetch].[dbo].[Points] where IsDelete = @IsDelete";
                var result = await db.QueryAsync<Point>(queryString, new { IsDelete = 0 });

                return result;
            }
        }

        public async Task<bool> IsPointExistsAsync(string name)
        {
            using (var db = new SqlConnection(_farfetchConfiguration.ConnectionString))
            {
                await db.OpenAsync();

                var queryString = @"SELECT count(*)
                                        FROM [Farfetch].[dbo].[Points] where IsDelete = @IsDelete and Upper(Name) = @Name";
                var result = await db.QueryFirstAsync<int>(queryString, new { IsDelete = 0, Name = name.ToUpper() });

                return result > 0;
            }
        }

        public async Task<bool> IsPointExistsAsync(long id)
        {
            using (var db = new SqlConnection(_farfetchConfiguration.ConnectionString))
            {
                await db.OpenAsync();

                var queryString = @"SELECT count(*)
                                        FROM [Farfetch].[dbo].[Points] where IsDelete = @IsDelete and Id = @Id";
                var result = await db.QueryFirstAsync<int>(queryString, new { IsDelete = 0, Id = id });

                return result > 0;
            }
        }
    }
}

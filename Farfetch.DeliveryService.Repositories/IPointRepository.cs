using Farfetch.DeliveryService.Models.Points;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Farfetch.DeliveryService.Repositories
{
    public interface IPointRepository
    {
        Task CreatePointAsync(string name);
        Task UpdatePointAsync(string name, long Id);
        Task DeletePointAsync(long Id);
        Task<IEnumerable<Point>> GetPointsAsync();
        Task<bool> IsPointExistsAsync(string name);
        Task<bool> IsPointExistsAsync(long id);
    }
}

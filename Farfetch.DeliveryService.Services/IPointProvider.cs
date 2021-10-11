using Farfetch.DeliveryService.Models.Points;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Farfetch.DeliveryService.Services
{
    public interface IPointProvider
    {
        Task CreatePointAsync(string name);
        Task ChangePointAsync(string name, long Id);
        Task DeletePointAsync(long Id);
        Task<IEnumerable<Point>> GetPointsAsync();
        Task<bool> IsPointExistsAsync(string name);
        Task<bool> IsPointExistsAsync(long id);
    }
}

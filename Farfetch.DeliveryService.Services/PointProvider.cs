using Farfetch.DeliveryService.Models.Points;
using Farfetch.DeliveryService.Models.Routes;
using Farfetch.DeliveryService.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Farfetch.DeliveryService.Services
{
    public class PointProvider : IPointProvider
    {
        private readonly IPointRepository _pointRepository;
        public PointProvider(IPointRepository pointRepository)
        {
            _pointRepository = pointRepository;
        }
        public async Task ChangePointAsync(string name, long id)
        {
            await _pointRepository.UpdatePointAsync(name, id);
        }

        public async Task CreatePointAsync(string name)
        {
            await _pointRepository.CreatePointAsync(name);
        }

        public async Task DeletePointAsync(long Id)
        {
            await _pointRepository.DeletePointAsync(Id);
        }

        public async Task<IEnumerable<Point>> GetPointsAsync()
        {
            var result = await _pointRepository.GetPointsAsync();

            return result;
        }

        public async Task<bool> IsPointExistsAsync(string name)
        {
            var isExists = await _pointRepository.IsPointExistsAsync(name);

            return isExists;
        }

        public async Task<bool> IsPointExistsAsync(long id)
        {
            var isExists = await _pointRepository.IsPointExistsAsync(id);

            return isExists;
        }
    }
}

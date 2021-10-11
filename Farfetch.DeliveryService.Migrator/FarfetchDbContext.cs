using Microsoft.EntityFrameworkCore;

namespace Farfetch.DeliveryService.Migrator
{
    public class FarfetchDbContext : DbContext
    {
        public FarfetchDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {

        }
    }
}

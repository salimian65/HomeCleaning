using Microsoft.EntityFrameworkCore;

namespace HomeCleaning.Persistance
{
    public class HomeCleaningContext : DbContext
    {
        public HomeCleaningContext (DbContextOptions<HomeCleaningContext> options)
            : base(options)
        {
        }

        public DbSet<HomeCleaning.Domain.CleaningCategory> CleaningCategory { get; set; }
    }
}
